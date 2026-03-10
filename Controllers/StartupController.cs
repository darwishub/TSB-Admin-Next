using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using TheStartupBuddyV3.Models;
using TheStartupBuddyV3.Repository;
using TheStartupBuddyV3.Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace TheStartupBuddyV3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StartupController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IRepositoryWrapper _repository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IEmailSender _emailSender;
        private readonly IServiceWrapper _service;
        private readonly IConfiguration _configuration;
        private InvesteurContext investeur_context = new InvesteurContext();
        public StartupController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IRepositoryWrapper repository,
        IEmailSender emailSender,
        IServiceWrapper service,
        IHttpContextAccessor accessor,
        IConfiguration configuration
        )
        {

            _userManager = userManager;
            _repository = repository;
            _signInManager = signInManager;
            _accessor = accessor;
            _emailSender = emailSender;
            _service = service;
            _configuration = configuration;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetUserProfile()
        {

            string? userId = Request.Cookies["X-Id"] ?? String.Empty;

            var userProfile = await _repository.UserProfile.GetUserProfileByUserIdAsync(userId);

            string usersResult = JsonConvert.SerializeObject(userProfile, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });

            return Content(usersResult);

        }

        #region get all company data
        [HttpGet]
        public async Task<IActionResult> GetAllStartups(string? search, bool sortByRank, bool sortByCoins, int ShowEntries,
        int Pagination, int ProgramId, int ProgramGroupId, bool ALL)
        {
            try
            {
                var getcompanies = await _repository.Startup.GetAllStartupsAsync(ProgramId);

                #region search by name company
                if (!string.IsNullOrEmpty(search))
                {
                    string SearchResult = search.ToLower().Trim();
                    getcompanies = getcompanies.Where(o => o.companyname != null && o.companyname.ToLower().Trim().Contains(SearchResult) ||
                    o.companyEmail != null && o.companyEmail.ToLower().Trim().Contains(SearchResult) ||
                    o.members.Any(m => m.Email.ToLower().Trim().Contains(SearchResult)));
                }
                #endregion

                if(ProgramGroupId != 0)
                {
                    getcompanies = getcompanies.Where(o => o.ProgramGroupId == ProgramGroupId);
                }

                if(ProgramId != 0 && ProgramGroupId == 0 && ALL == false)
                {
                    getcompanies = getcompanies.Where(o => o.ProgramGroupId == 0);
                }

                int TotalPage = getcompanies.Count();

                var sortedResults = getcompanies.OrderByDescending(x => x.Score).ThenByDescending(x => x.tsbCoin)
                    .Skip((Pagination - 1) * ShowEntries).Take(ShowEntries).ToList();

                var startupIds = sortedResults.Select(c => c.startupid).ToList();

                // Batch 1: program data for all startups on this page
                var startupProgramsRaw = await investeur_context.StartupPrograms
                    .AsNoTracking()
                    .Where(sp => startupIds.Contains(sp.StartupId))
                    .Join(investeur_context.ProgramCodes.AsNoTracking(),
                        sp => sp.ProgramId,
                        pc => pc.Programid,
                        (sp, pc) => new { sp.StartupId, sp.ProgramId, sp.ProgramGroupId, ProgramName = pc.Name, pc.StatusGoal })
                    .ToListAsync();
                var programByStartup = startupProgramsRaw.ToDictionary(x => x.StartupId);

                // Batch 2: program groups per unique programId (collapses N calls to 1-2)
                var uniqueProgramIds = startupProgramsRaw.Select(p => p.ProgramId).Distinct().ToList();
                var programGroupsByProgramId = new Dictionary<int, List<ProgramGroup>>();
                foreach (var pid in uniqueProgramIds)
                {
                    var groups = await _repository.ProgramGroup.GetProgramGroupByProgramIdAsync(pid);
                    programGroupsByProgramId[pid] = groups.ToList();
                }

                // Batch 3: startup members for all startupIds
                var allMembers = await investeur_context.StartupMembers
                    .AsNoTracking()
                    .Where(sm => startupIds.Contains(sm.Startupid))
                    .Select(sm => new { sm.Startupid, sm.Email })
                    .ToListAsync();
                var membersByStartup = allMembers.GroupBy(m => m.Startupid)
                    .ToDictionary(g => g.Key, g => g.ToList());

                // Batch 4: photos for all member and owner emails in one query
                var memberEmails = allMembers.Select(m => m.Email).Distinct();
                var ownerEmails = sortedResults.Select(c => c.companyEmail).Where(e => e != null).Distinct();
                var allEmails = memberEmails.Union(ownerEmails).ToList();
                var photoByEmail = (await investeur_context.UsersData
                    .AsNoTracking()
                    .Where(u => allEmails.Contains(u.Email))
                    .Select(u => new { u.Email, u.Photo })
                    .ToListAsync())
                    .ToDictionary(u => u.Email, u => u.Photo);

                // Batch 5: completed steps per startup
                var completedStepsByStartup = (await investeur_context.CompanyGoalsStep
                    .AsNoTracking()
                    .Where(cgs => cgs.StartupId != null && startupIds.Contains(cgs.StartupId.Value) && cgs.IsStepComplete == true)
                    .Select(cgs => new { StartupId = cgs.StartupId!.Value, cgs.IdGoalStep })
                    .Distinct()
                    .ToListAsync())
                    .GroupBy(x => x.StartupId)
                    .ToDictionary(g => g.Key, g => g.Count());

                // Batch 6: total steps per unique (programId, statusGoal, programGroupId)
                var totalStepsCache = new Dictionary<(int, bool, int), int>();
                foreach (var pd in startupProgramsRaw)
                {
                    bool pdStatusGoal = pd.StatusGoal ?? false;
                    var key = (pd.ProgramId, pdStatusGoal, pd.ProgramGroupId);
                    if (!totalStepsCache.ContainsKey(key))
                    {
                        var steps = await _repository.GoalsStep.GetTotalSteps(pd.ProgramId, pdStatusGoal);
                        steps = pd.ProgramGroupId != 0
                            ? steps.Where(x => x.ProgramGroupId == pd.ProgramGroupId || x.ProgramGroupId == 0)
                            : steps.Where(x => x.ProgramGroupId == 0);
                        totalStepsCache[key] = steps?.Count() ?? 0;
                    }
                }
                if (!totalStepsCache.ContainsKey((0, false, 0)))
                {
                    var steps = await _repository.GoalsStep.GetTotalSteps(0, false);
                    totalStepsCache[(0, false, 0)] = steps?.Where(x => x.ProgramGroupId == 0).Count() ?? 0;
                }

                // Batch 7: all goals with level info (replaces 12N DB queries for level calculation)
                var allGoalsLevelData = await (from g in investeur_context.Goals.AsNoTracking()
                                               join l in investeur_context.Levels.AsNoTracking() on g.IdLevel equals l.IdLevel
                                               select new { g.IdGoal, l.IdGoalsCategory, l.CodeLevel })
                                              .ToListAsync();

                // Batch 8: goals taken by all startups for level calculation
                var allGoalsTaken = await (from cg in investeur_context.CompanyGoals.AsNoTracking()
                                           join g in investeur_context.Goals.AsNoTracking() on cg.IdGoal equals g.IdGoal
                                           join l in investeur_context.Levels.AsNoTracking() on g.IdLevel equals l.IdLevel
                                           where startupIds.Contains(cg.StartupId ?? 0)
                                           select new { cg.StartupId, l.IdGoalsCategory, l.CodeLevel })
                                          .ToListAsync();
                var goalsTakenByStartup = allGoalsTaken
                    .GroupBy(x => x.StartupId ?? 0)
                    .ToDictionary(g => g.Key, g => g.ToList());

                // Batch 9: level logos (replaces 4N logo queries)
                var logoLookup = (await investeur_context.Levels
                    .AsNoTracking()
                    .Select(l => new { l.IdGoalsCategory, l.CodeLevel, l.LogoLevel })
                    .ToListAsync())
                    .GroupBy(l => (Cat: l.IdGoalsCategory.GetValueOrDefault(), Lvl: (int)l.CodeLevel.GetValueOrDefault()))
                    .ToDictionary(grp => grp.Key, grp => grp.First().LogoLevel ?? "");

                // Build results in memory — no more DB calls in this loop
                int rank = ((Pagination - 1) * ShowEntries) + 1;
                var finalResults = new List<StartupCompanyWithRanking>();

                foreach (var company in sortedResults)
                {
                    programByStartup.TryGetValue(company.startupid, out var pd);
                    int programId = pd?.ProgramId ?? 0;
                    int programGroupId = pd?.ProgramGroupId ?? 0;
                    bool statusGoal = pd?.StatusGoal ?? false;

                    var groups = programGroupsByProgramId.TryGetValue(programId, out var pg)
                        ? new List<ProgramGroup>(pg)
                        : new List<ProgramGroup>();
                    groups.Add(new ProgramGroup { GroupName = "No Group", ProgramGroupId = 0 });

                    var members = membersByStartup.TryGetValue(company.startupid, out var ml) ? ml : new();
                    ArrayList poto = new ArrayList();
                    foreach (var m in members)
                        poto.Add(photoByEmail.TryGetValue(m.Email, out var photo) ? photo : "");
                    if (company.companyEmail != null && photoByEmail.TryGetValue(company.companyEmail, out var ownerPhoto))
                        poto.Add(ownerPhoto);

                    int step = completedStepsByStartup.TryGetValue(company.startupid, out var sc) ? sc : 0;
                    int totalSteps = totalStepsCache.TryGetValue((programId, statusGoal, programGroupId), out var ts) ? ts : 0;

                    ArrayList levels = new ArrayList();
                    var takenGoals = goalsTakenByStartup.TryGetValue(company.startupid, out var tg) ? tg : new();
                    int[] codeLevels = new int[] { 2, 3, 4 };
                    for (int cat = 1; cat <= 4; cat++)
                    {
                        int currentLevel = 1;
                        foreach (int cl in codeLevels)
                        {
                            int totalForLevel = allGoalsLevelData.Count(g => g.IdGoalsCategory == cat && g.CodeLevel == cl);
                            int takenForLevel = takenGoals.Count(g => g.IdGoalsCategory == cat && g.CodeLevel == cl);
                            if (totalForLevel > 0 && takenForLevel > 0 && totalForLevel == takenForLevel)
                                currentLevel = cl;
                        }
                        levels.Add(logoLookup.TryGetValue((Cat: cat, Lvl: currentLevel), out var logo) ? logo : "");
                    }

                    finalResults.Add(new StartupCompanyWithRanking
                    {
                        startupid = company.startupid,
                        companyname = company.companyname,
                        companyEmail = company.companyEmail,
                        startupLogo = company.startupLogo,
                        memberphoto = poto,
                        tsbCoin = company.tsbCoin,
                        completeStep = step,
                        totalSteps = totalSteps,
                        description = company.companyDescription,
                        totalLeaderBoard = 0,
                        level = levels,
                        program = pd?.ProgramName ?? "",
                        Programid = programId,
                        programGroups = groups,
                        ProgramGroupId = programGroupId,
                        Score = company.Score ?? 0,
                        ranking = rank++
                    });
                }

                IEnumerable<StartupCompanyWithRanking> companies = finalResults;

                if (sortByCoins)
                {
                    companies = finalResults
                        .OrderByDescending(i => i.tsbCoin)
                        .ThenByDescending(i => i.Score)
                        .ThenByDescending(i => i.dateCreated);
                }

                string companiesResult = JsonConvert.SerializeObject(new { companies, TotalPage }, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Content(companiesResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region get details company
        [HttpGet]
        public async Task<IActionResult> GetCompanyDetails(int? startupid, int? programId)
        {
            int BMC_id = int.Parse(_configuration["BMC_id:goal_id"]);
            try
            {
                int levelTeam = await _repository.Startup.GetCurrentLevelEachCategory((int)startupid, 1);
                int levelCompany = await _repository.Startup.GetCurrentLevelEachCategory((int)startupid, 2);
                int levelService = await _repository.Startup.GetCurrentLevelEachCategory((int)startupid, 3);
                int levelGrowth = await _repository.Startup.GetCurrentLevelEachCategory((int)startupid, 4);
                
                var GoalsQuery = await (from company_goals in investeur_context.CompanyGoals.AsNoTracking()
                                        join goals in investeur_context.Goals.AsNoTracking()
                                        on company_goals.IdGoal equals goals.IdGoal
                                        join goals_level in investeur_context.Levels.AsNoTracking()
                                        on goals.IdLevel equals goals_level.IdLevel
                                        where company_goals.StartupId == startupid && company_goals.IdGoal != BMC_id
                                        select new
                                        {

                                            id = goals.IdGoal,
                                            title = goals.TitleGoal,
                                            id_level = goals.IdLevel,
                                            id_goal_category = goals_level.IdGoalsCategory,
                                            code_level = goals_level.CodeLevel,
                                            logo = goals.Logo,
                                            total_points = goals.TotalPoints,
                                            description = goals.Description,
                                            second_title = goals.SecondTitle,
                                            label = goals.Label,
                                            goal_category_logo = goals_level.LogoLevel,
                                            goal_help_text = goals.HelpText

                                        }).ToListAsync();

                var TotalCoins = await _repository.Startup.GetDataByStartupId(startupid);

                List<GoalDetails> CompanyGoalQuery = new List<GoalDetails>();
                List<MembersOfStartup> MemberLists = new List<MembersOfStartup>();

                for (var i = 0; i < GoalsQuery.Count; i++)
                {

                    var IdGoal = GoalsQuery[i]?.id;

                    var GoalsStepsQuery = await (from company_goals_steps in investeur_context.CompanyGoalsStep.AsNoTracking()
                                                 join goals_steps in investeur_context.GoalsSteps.AsNoTracking()
                                                 on company_goals_steps.IdGoalStep equals goals_steps.IdGoalStep
                                                 where company_goals_steps.StartupId == startupid && goals_steps.IdGoal == IdGoal
                                                 select new
                                                 {
                                                     id_goal_step = goals_steps.IdGoalStep,
                                                     id_step = goals_steps.IdStep,
                                                     id_goal = goals_steps.IdGoal,
                                                     step_title = goals_steps.Title,
                                                     step_description = goals_steps.Description,

                                                 }).ToListAsync();

                    var ResultQuery = new GoalDetails();
                    List<GoalDetailsStep> StepsList = new List<GoalDetailsStep>();

                    ResultQuery.id = GoalsQuery[i]?.id;
                    ResultQuery.logo = GoalsQuery[i]?.logo;
                    ResultQuery.label = GoalsQuery[i]?.label;
                    ResultQuery.id_level = GoalsQuery[i]?.id_level ?? 0;
                    ResultQuery.title = GoalsQuery[i]?.title;
                    ResultQuery.description = GoalsQuery[i]?.description;
                    ResultQuery.second_title = GoalsQuery[i]?.second_title;
                    ResultQuery.total_points = GoalsQuery[i]?.total_points ?? 0;
                    ResultQuery.goal_category_logo = GoalsQuery[i]?.goal_category_logo;
                    ResultQuery.goal_help_text = GoalsQuery[i]?.goal_help_text;
                    ResultQuery.id_goal_category = GoalsQuery[i]?.id_goal_category;
                    ResultQuery.code_level = GoalsQuery[i]?.code_level;

                    foreach (var step in GoalsStepsQuery)
                    {

                        GoalDetailsStep StepQuery = new GoalDetailsStep();

                        var GoalsCardFieldsQuery = await (from company_goals_card_fields in investeur_context.CompanyGoalsCardFields.AsNoTracking()
                                                          join goals_card_fields in investeur_context.GoalsCardFields.AsNoTracking()
                                                          on company_goals_card_fields.IdCard equals goals_card_fields.IdCard
                                                          where company_goals_card_fields.StartupId == startupid && goals_card_fields.IdStep == step.id_step
                                                          select new
                                                          {
                                                              id_card = company_goals_card_fields.IdCard,
                                                              value = company_goals_card_fields.Value,
                                                              field_code = goals_card_fields.FieldCode,
                                                              title = goals_card_fields.Title,
                                                              label = goals_card_fields.Label,
                                                              required = goals_card_fields.IsRequired,
                                                              score = company_goals_card_fields.Score,
                                                              origin_score = goals_card_fields.Score,
                                                              visible = goals_card_fields.Visible

                                                          }).Where(i => i.visible == true).ToListAsync();

                        StepQuery.step = step.id_step ?? 0;
                        StepQuery.id_goal_step = step.id_goal_step;
                        StepQuery.step_title = step.step_title;
                        StepQuery.step_description = step.step_description;

                        List<GoalDetailsCards> cardsList = new List<GoalDetailsCards>();

                        foreach (var card in GoalsCardFieldsQuery)
                        {

                            GoalDetailsCards CardQuery = new GoalDetailsCards();
                            GoalDetailsSchema CardSchemaQuery = new GoalDetailsSchema();
                            GoalDetailsProps CardPropsQuery = new GoalDetailsProps();

                            CardQuery.id = card.id_card;
                            CardQuery.title = card.title;
                            CardPropsQuery.value = card.value;
                            CardPropsQuery.label = card.label;
                            CardPropsQuery.required = card.required;
                            CardSchemaQuery.props = CardPropsQuery;
                            CardSchemaQuery.field_code = card.field_code;
                            CardQuery.schema = CardSchemaQuery;
                            CardQuery.score = card.score;
                            CardQuery.origin_score = card.origin_score;

                            cardsList.Add(CardQuery);
                        }

                        StepQuery.cards = cardsList;

                        StepsList.Add(StepQuery);
                    }

                    ResultQuery.steps = StepsList.OrderBy(x => x.id_goal_step).ToList();
                    ResultQuery.score = StepsList.SelectMany(s => s.cards).Sum(c => c.score);

                    CompanyGoalQuery.Add(ResultQuery);
                }

                List<Logos> Logos = new List<Logos>();

                var _logoTeam = await _repository.Levels.GetLogoByLevel(1, levelTeam);
                var _logoCompany = await _repository.Levels.GetLogoByLevel(2, levelCompany);
                var _logoService = await _repository.Levels.GetLogoByLevel(3, levelService);
                var _logoGrowth = await _repository.Levels.GetLogoByLevel(4, levelGrowth);

                Logos.Add(_logoTeam);
                Logos.Add(_logoCompany);
                Logos.Add(_logoService);
                Logos.Add(_logoGrowth);

                var getMembers = await _repository.StartupMember.GetAllMembersOfStartup(startupid);
                var startupData = await _repository.Startup.GetDataByStartupId(startupid);
                var startupProgram = await _repository.StartupProgram.GetStartupProgram(startupid ?? 0);
                int? Ranking = 0;
                if(programId != 0)
                {
                    Ranking = await _repository.Startup.GetStartupRanking(startupid ?? 0, startupProgram?.ProgramId ?? 0);
                }
                else
                {
                    Ranking = await _repository.Startup.GetStartupRanking(startupid ?? 0, 0);
                }
                
                var getCompanies = await _repository.Startup.GetCompanies(programId);
                string goalResults = JsonConvert.SerializeObject(
                    new
                    {
                        startup = startupData,
                        companyData = CompanyGoalQuery,
                        logos = Logos,
                        coins = TotalCoins.tsbCoin,
                        score = startupData.startupScore,
                        ranking = Ranking,
                        members = getMembers.OrderBy(x => x.Startupid).ToList(),
                        totalCompany = getCompanies.Count()
                    }, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented
                    });

                return Ok(goalResults);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region get details of personal startup
        [HttpGet]
        public async Task<IActionResult> GetStartupInfo(int? startupid)
        {
            try
            {
                int levelTeam = await _repository.Startup.GetCurrentLevelEachCategory((int)startupid, 1);
                int levelCompany = await _repository.Startup.GetCurrentLevelEachCategory((int)startupid, 2);
                int levelService = await _repository.Startup.GetCurrentLevelEachCategory((int)startupid, 3);
                int levelGrowth = await _repository.Startup.GetCurrentLevelEachCategory((int)startupid, 4);

                List<Logos> Logos = new List<Logos>();

                var _logoTeam = await _repository.Levels.GetLogoByLevel(1, levelTeam);
                var _logoCompany = await _repository.Levels.GetLogoByLevel(2, levelCompany);
                var _logoService = await _repository.Levels.GetLogoByLevel(3, levelService);
                var _logoGrowth = await _repository.Levels.GetLogoByLevel(4, levelGrowth);

                Logos.Add(_logoTeam);
                Logos.Add(_logoCompany);
                Logos.Add(_logoService);
                Logos.Add(_logoGrowth);

                var PersonalInfo = await _repository.Startup.GetStartupInfo(startupid);

                string startupResults = JsonConvert.SerializeObject(
                    new
                    {
                        companyData = PersonalInfo,
                        logos = Logos,
                    }, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented
                    });

                return Ok(startupResults);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> GetBMCbyStartupId(int startupid)
        {

            try
            {

                var KeyPartners = await _repository.CompanyGoalsCardsField.GetCompanyGoalCardFieldsByIdAsync(int.Parse(_configuration["BMC_id:KeyPartners"]), startupid);
                var KeyActivities = await _repository.CompanyGoalsCardsField.GetCompanyGoalCardFieldsByIdAsync(int.Parse(_configuration["BMC_id:KeyActivities"]), startupid);
                var KeyResources = await _repository.CompanyGoalsCardsField.GetCompanyGoalCardFieldsByIdAsync(int.Parse(_configuration["BMC_id:KeyResources"]), startupid);
                var ValueProposition = await _repository.CompanyGoalsCardsField.GetCompanyGoalCardFieldsByIdAsync(int.Parse(_configuration["BMC_id:ValueProposition"]), startupid);
                var CustomerRelationships = await _repository.CompanyGoalsCardsField.GetCompanyGoalCardFieldsByIdAsync(int.Parse(_configuration["BMC_id:CustomerRelationships"]), startupid);
                var Channels = await _repository.CompanyGoalsCardsField.GetCompanyGoalCardFieldsByIdAsync(int.Parse(_configuration["BMC_id:Channels"]), startupid);
                var CustomerSegments = await _repository.CompanyGoalsCardsField.GetCompanyGoalCardFieldsByIdAsync(int.Parse(_configuration["BMC_id:CustomerSegments"]), startupid);
                var CostStructure = await _repository.CompanyGoalsCardsField.GetCompanyGoalCardFieldsByIdAsync(int.Parse(_configuration["BMC_id:CostStructure"]), startupid);
                var RevenueStreams = await _repository.CompanyGoalsCardsField.GetCompanyGoalCardFieldsByIdAsync(int.Parse(_configuration["BMC_id:RevenueStreams"]), startupid);

                string BMCResults = JsonConvert.SerializeObject(
                new
                {
                    key_partners_column = KeyPartners?.Value ?? "",
                    key_activities_column = KeyActivities?.Value ?? "",
                    key_resources_column = KeyResources?.Value ?? "",
                    value_propostion_column = ValueProposition?.Value ?? "",
                    customer_relationships_column = CustomerRelationships?.Value ?? "",
                    channels_column = Channels?.Value ?? "",
                    customer_segments_column = CustomerSegments?.Value ?? "",
                    cost_structure_column = CostStructure?.Value ?? "",
                    revenue_streams_column = RevenueStreams?.Value ?? ""
                }, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Ok(BMCResults);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetVPCStartupId(int startupid)
        {
            try
            {
                var VPC = await _repository.StartupVPC.GetVPCByStartupIdAsync(startupid);

                if (VPC != null)
                {
                    string VPCResults = JsonConvert.SerializeObject(
                    new { VPC.Data, hasVPC = true }, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented
                    });
                    return Ok(VPCResults);
                }
                else
                {

                    var CustomerJobs = await _repository.CompanyGoalsCardsField.GetCompanyGoalCardFieldsByIdAsync(int.Parse(_configuration["VPC_id:CustomerJobs"]), startupid);
                    var CustomerNeeds = await _repository.CompanyGoalsCardsField.GetCompanyGoalCardFieldsByIdAsync(int.Parse(_configuration["VPC_id:CustomerNeeds"]), startupid);
                    var CustomerWants = await _repository.CompanyGoalsCardsField.GetCompanyGoalCardFieldsByIdAsync(int.Parse(_configuration["VPC_id:CustomerWants"]), startupid);

                    var data = new
                    {
                        CustomerJobs = CustomerJobs.Value,
                        CustomerNeeds = CustomerNeeds.Value,
                        CustomerWants = CustomerWants.Value
                    };

                    string VPCResults = JsonConvert.SerializeObject(
                    new { data, hasVPC = false }, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented
                    });
                    return Ok(VPCResults);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #region assign startup to spesific program
        [HttpPost]
        public async Task<IActionResult> SelectStartupForProgram(int startupId, int programId, int programGroupId)
        {
            try
            {
                var _startupProgramData = await _repository.StartupProgram.GetStartupJoinProgramByStartupIdAsync(startupId);
                if (_startupProgramData != null && programId != 0)
                {
                   
                    if(programId == _startupProgramData.ProgramId)
                    {
                         _repository.StartupProgram.DeleteStartupJoinProgram(_startupProgramData);
                        await _repository.SaveAsync();

                        var newStartupProgramData = new StartupProgram
                        {
                            ProgramId = programId,
                            StartupId = startupId,
                            JoinDate = DateTime.Now,
                            ProgramGroupId = programGroupId
                        };

                        _repository.StartupProgram.Create(newStartupProgramData);
                        await _repository.SaveAsync();
                    }
                    else
                    {
                         _repository.StartupProgram.DeleteStartupJoinProgram(_startupProgramData);
                        await _repository.SaveAsync();

                        var programGroup = await _repository.ProgramGroup.GetProgramGroupByProgramIdAsync(programGroupId);

                        var newStartupProgramData = new StartupProgram
                        {
                            ProgramId = programId,
                            StartupId = startupId,
                            JoinDate = DateTime.Now,
                            ProgramGroupId = 0
                        };

                        _repository.StartupProgram.Create(newStartupProgramData);
                        await _repository.SaveAsync();
                    }
                }
                else
                {
                    if(programId != 0)
                    {
                        var newStartupProgramData = new StartupProgram
                        {
                            ProgramId = programId,
                            StartupId = startupId,
                            JoinDate = DateTime.Now,
                            ProgramGroupId = programGroupId
                        };
                        _repository.StartupProgram.Create(newStartupProgramData);
                        await _repository.SaveAsync();
                    }
                }         
                
                // #region add data to notifications
                // var userOwner = await _repository.Startup.GetDataByStartupId(startupId);
                // var userProfile = await _repository.UserProfile.GetUserProfileByUserIdAsync(userOwner.Userid);
                // var dataProgram = await _repository.Program.GetProgramByProgramId(programId);
                // var programGroup = await _repository.ProgramGroup.GetProgramGroupByGroupId(programGroupId);
                // var newNotification = new Notifications
                // {
                //     StartupId = startupId,
                //     UserId = userOwner.Userid ?? "",
                //     IsAction = false,
                //     Message = "Congratulations! You have been selected into the " + dataProgram.Name
                //     + " program, as a member of " + programGroup.GroupName + " group",
                //     Link = "",
                //     ReadStatus = false,
                //     JoinedAs = userProfile.Joinedas,
                //     Startdate = DateTime.UtcNow,
                //     Enddate = DateTime.UtcNow,
                //     HasButton = false,
                //     ButtonLabel = "",
                //     SuccessMessage = "",
                //     Redirect = "",
                //     NotifyToTeam = true,
                //     Clicked = false
                // };

                // _repository.Notifications.CreateNotification(newNotification);
                // await _repository.SaveAsync();
                // #endregion

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region delete startup program
        [HttpPost]
        public async Task<IActionResult> DeleteFromProgram(int startupId)
        {
            try
            {
                var _startupProgramData = await _repository.StartupProgram.GetStartupJoinProgramByStartupIdAsync(startupId);
                if (_startupProgramData != null)
                {
                    _repository.StartupProgram.DeleteStartupJoinProgram(_startupProgramData);
                    await _repository.SaveAsync();

                    #region add data to notifications removed
                    var userOwner = await _repository.Startup.GetDataByStartupId(startupId);
                    var userProfile = await _repository.UserProfile.GetUserProfileByUserIdAsync(userOwner.Userid);
                    var dataProgram = await _repository.Program.GetProgramByProgramId(_startupProgramData.ProgramId);
                    var newNotification = new Notifications
                    {
                        StartupId = startupId,
                        UserId = userOwner.Userid ?? "",
                        IsAction = false,
                        Message = "Unfortunately!, you are no longer part of " + dataProgram.Name + " program",
                        Link = "",
                        ReadStatus = false,
                        JoinedAs = userProfile.Joinedas,
                        Startdate = DateTime.UtcNow,
                        Enddate = DateTime.UtcNow,
                        HasButton = false,
                        ButtonLabel = "",
                        SuccessMessage = "",
                        Redirect = "",
                        NotifyToTeam = true,
                        Clicked = false

                    };

                    _repository.Notifications.CreateNotification(newNotification);
                    await _repository.SaveAsync();
                    #endregion

                    return Ok("success");
                }
                else
                {
                    return BadRequest("no data");
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region get graphic for startup profile
        [HttpGet]
        public async Task<IActionResult> graphProfile(int? programid, int? startupid, string? time)
        {
            try
            {
                int totalStepsCount = 0;
                if (programid != 0)
                {
                    var goalBasics = investeur_context.ProgramCodes.Where(x => x.Programid == programid).FirstOrDefault();
                    var totalSteps = await _repository.GoalsStep.GetAllStepsAsync(programid, goalBasics.StatusGoal);
                    totalStepsCount = totalSteps.Count();
                }
                else
                {
                    var totalSteps = await _repository.GoalsStep.GetAllStepsAsync(programid, false);
                    totalStepsCount = totalSteps.Count();
                }
                var stepsComplete = await _repository.CompanyGoals.GetCompanySteps(programid, startupid);
                var AllConnections = await _repository.NetworkingConnect.GetConnections(startupid);
                var AllCoins = await _repository.CompanyGoals.GetCoins(startupid);

                DateTime startDate, endDate;
                int stepLastTimes = 0;

                if (time == "weekly")
                {
                    endDate = DateTime.Today;
                    startDate = endDate.AddDays(-6);
                    var stepLastTime = stepsComplete.Where(a => a.DateCreated.Date >= startDate.AddDays(-7).Date && a.DateCreated.Date <= endDate.AddDays(-7).Date);
                    stepLastTimes = stepLastTime.Count();
                }
                else if (time == "monthly")
                {
                    startDate = DateTime.Now.AddMonths(-1);
                    endDate = DateTime.Now;
                    var stepLastTime = stepsComplete.Where(a => a.DateCreated.AddMonths(1).Date >= startDate.Date && a.DateCreated.AddMonths(1).Date <= endDate.Date);
                    stepLastTimes = stepLastTime.Count();
                }
                else if (time == "last3months")
                {
                    startDate = DateTime.Now.AddMonths(-3);
                    endDate = DateTime.Now;
                    var stepLastTime = stepsComplete.Where(a => a.DateCreated.AddMonths(3).Date >= startDate.Date && a.DateCreated.AddMonths(3).Date <= endDate.Date);
                    stepLastTimes = stepLastTime.Count();
                }
                else if (time == "yearly")
                {
                    startDate = DateTime.Now.AddMonths(-11);
                    endDate = DateTime.Now;
                    var stepLastTime = stepsComplete.Where(a => a.DateCreated.AddYears(1).Date >= startDate.Date && a.DateCreated.AddYears(1).Date <= endDate.Date);
                    stepLastTimes = stepLastTime.Count();
                }
                else
                {
                    return StatusCode(400, "Invalid period. Valid values are 'weekly', 'monthly', 'yearly'.");
                }

                List<int> dateArray = new List<int>();
                List<int> listConnectionPerPeriod = new List<int>();
                List<int> coinsPerPeriod = new List<int>();
                List<string> dayArray = new List<string>();
                List<int> yearsLabel = new List<int>();

                double _stepsComplete = stepsComplete.Count();
                double percent = _stepsComplete * 100 / totalStepsCount;

                var res = new GraphProfile();
                if (time == "yearly")
                {

                    var stepThisTime = stepsComplete.Where(o => o.DateCreated.Year == DateTime.Now.Year);
                    for (var date = startDate; date < endDate; date = date.AddMonths(1))
                    {
                        var stepsCount = stepsComplete.Count(g => g.DateCreated.Month == date.Month);
                        dateArray.Add(stepsCount);

                        var networkCount = AllConnections.Count(n => n.RequestDate.Month == date.Month);
                        listConnectionPerPeriod.Add(networkCount);

                        var total_coins = AllCoins.Where(c => c.date_created.Month == date.Month).Sum(c => (int)c.coins);
                        coinsPerPeriod.Add(total_coins);
                    }
                    //add months to years label
                    var currentMonth = DateTime.Now.Month - 1; // -1 because array is 0-indexed
                    var monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Take(12).ToArray();
                    var reversedMonthNames = monthNames.Reverse().ToArray();
                    var monthLabels = reversedMonthNames.Skip(12 - currentMonth - 1).Take(12).Concat(reversedMonthNames.Take(12 - currentMonth - 1)).ToArray();
                    res.stepLastTime = stepLastTimes;
                    res.stepThisTime = stepThisTime.Count();
                    res.labels = monthLabels.Reverse().ToArray();
                    res.stepPerPeriod = dateArray.ToArray();
                    res.percentage = percent;
                    res.listConnectionPerPeriod = listConnectionPerPeriod.ToArray();
                    res.coinsPerPeriod = coinsPerPeriod.ToArray();
                    res.totalStep = totalStepsCount;
                    res.stepTaken = stepsComplete.Count();
                }
                else
                {
                    var stepThisTime = stepsComplete.Where(o => o.DateCreated.Date >= startDate.Date && o.DateCreated.Date <= endDate.Date);
                    for (var date = startDate; date <= endDate; date = date.AddDays(1))
                    {
                        var stepsCount = stepsComplete.Count(s => s.DateCreated.Date == date.Date);
                        dateArray.Add(stepsCount);

                        var networkCount = AllConnections.Count(n => n.RequestDate.Date == date.Date);
                        listConnectionPerPeriod.Add(networkCount);

                        var total_coins = AllCoins.Where(c => c.date_created.Date == date.Date).Sum(c => (int)c.coins);
                        coinsPerPeriod.Add(total_coins);

                        if (time == "last3months" || time == "monthly")
                        {
                            dayArray.Add(date.ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            dayArray.Add(date.ToString("dddd"));
                        }
                    }
                    res.stepLastTime = stepLastTimes;
                    res.stepThisTime = stepThisTime.Count();
                    res.labels = dayArray.ToArray();
                    res.stepPerPeriod = dateArray.ToArray();
                    res.percentage = percent;
                    res.listConnectionPerPeriod = listConnectionPerPeriod.ToArray();
                    res.coinsPerPeriod = coinsPerPeriod.ToArray();
                    res.totalStep = totalStepsCount;
                    res.stepTaken = stepsComplete.Count();
                }

                string companiesResult = JsonConvert.SerializeObject(res, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Content(companiesResult);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region startup note
        [HttpGet]
        public async Task<IActionResult> GetStartupNote(int startupid)
        {
            try
            {
                var startup = await investeur_context.Startups
                    .AsNoTracking()
                    .Where(s => s.Startupid == startupid)
                    .Select(s => new { s.startupNote })
                    .FirstOrDefaultAsync();

                if (startup == null) return NotFound("Startup not found");

                return Ok(JsonConvert.SerializeObject(new { note = startup.startupNote ?? "" }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveStartupNote(int startupid, [FromBody] SaveNoteRequest request)
        {
            try
            {
                var startup = await investeur_context.Startups.FirstOrDefaultAsync(s => s.Startupid == startupid);

                if (startup == null) return NotFound("Startup not found");

                startup.startupNote = request.Note;
                await investeur_context.SaveChangesAsync();

                return Ok("success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region get all company data
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(string? search, int ShowEntries,
        int Pagination, int ProgramId, int ProgramGroupId, bool ALL)
        {
            try
            {
                var getAllUsers = await _repository.Startup.GetAllUsers(ProgramId);

                var sortedResults = getAllUsers.OrderByDescending(x => x.startupid)
                .Skip((Pagination - 1) * ShowEntries).Take(ShowEntries).ToList();

                int TotalPage = getAllUsers.Count();
                string companiesResult = JsonConvert.SerializeObject(new { sortedResults, TotalPage }, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Content(companiesResult);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion
    }
}
