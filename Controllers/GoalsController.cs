using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using TheStartupBuddyV3.Models;
using TheStartupBuddyV3.Repository;
using TheStartupBuddyV3.Service;

namespace TheStartupBuddyV3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class GoalsController : Controller
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        private readonly IRepositoryWrapper _repository;
        private readonly IServiceWrapper _service;

        public GoalsController(
        IRepositoryWrapper repository,
        IServiceWrapper service
        )
        {
            _repository = repository;
            _service = service;
        }

        #region get all data goals for goal page with filter
        [HttpGet]
        public async Task<IActionResult> AllGoals(string? categoryId, string? codeLevel, int ShowEntries, int Pagination, int ProgramId, string? isPublished,
        string? Search, int programGroupId, bool ALL)
        {
            string? userId = Request.Cookies["X-Id"] ?? String.Empty;

            var goals = await _repository.Goals.GetAllGoals();

            var _programData = await _repository.Program.GetProgramByProgramId(ProgramId);
            int startupProgram = 0;
            int statusGoal = 0;
            if (_programData != null)
            {
                startupProgram = _programData.Programid;
                if (_programData.StatusGoal == true)
                {
                    statusGoal = 1;
                }
                else
                {
                    statusGoal = 0;
                }
            }
            else
            {
                startupProgram = 0;
                programGroupId = 0;
                statusGoal = 0;
            }

            if((statusGoal == 0 && startupProgram != 0) || startupProgram != 0)
            {
                goals = goals.Where(program => program.programid == startupProgram);
            }

            if(programGroupId != 0)
            {
                goals = goals.Where(program => program.program_group_id == programGroupId);
            }
            if(ProgramId != 0 && programGroupId == 0 && ALL == false)
            {
                goals = goals.Where(o => o.program_group_id == 0);
            }

            if (!string.IsNullOrEmpty(categoryId))
            {
                int IdCategory = int.Parse(categoryId);

                goals = goals.Where(o => o.id_category == IdCategory);
            }

            if (!string.IsNullOrEmpty(codeLevel))
            {
                int IdLevel = Byte.Parse(codeLevel);

                goals = goals.Where(o => o.code_level == IdLevel);
            }

            if (!string.IsNullOrEmpty(codeLevel) && !string.IsNullOrEmpty(categoryId))
            {
                int IdLevel = Byte.Parse(codeLevel);
                int IdCategory = int.Parse(categoryId);

                goals = goals.Where(o => o.id_category == IdCategory && o.code_level == IdLevel);
            }

            if (!string.IsNullOrEmpty(isPublished))
            {
                if (isPublished == "1")
                {
                    goals = goals.Where(o => o.is_published == true);
                }
                else if (isPublished == "0")
                {
                    goals = goals.Where(o => o.is_published == false);
                }
            }

            if (!string.IsNullOrEmpty(Search))
            {
                string SearchResult = Search.ToLower().Trim();
                goals = goals.Where(o => o.title != null && o.title.ToLower().Trim().Contains(SearchResult) ||
                o.description != null && o.description.ToLower().Trim().Contains(SearchResult));
            }  

            int TotalPage = goals.Count();
            var resultData = goals.OrderByDescending(i => i.id).Skip((Pagination - 1) * ShowEntries).Take(ShowEntries);
            
            if (goals == null)
            {
                return BadRequest("Sorry, we can't find any data");
            }

            string goalResults = JsonConvert.SerializeObject(new { resultData, TotalPage }, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });

            return Ok(goalResults);
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> DeleteGoal(int goalId)
        {
            try
            {
                var goals = await _repository.GoalsStep.GetAllStepsByGoalIdAsync(goalId);
                var key = investeur_context.Goals.Where(i => i.IdGoal == goalId)?.FirstOrDefault()?.Logo ?? "";
                int stepLength = goals.Count();
                var goal = new Goal
                {
                    IdGoal = goalId
                };

                _repository.Goal.DeleteGoal(goal);
                //await _repository.SaveAsync();
                await _service.AWSUserImgService.DeleteImageFile(key);

                foreach (var goalData in goals)
                {
                    var GoalStepToDelete = await _repository.GoalsStep.GetGoalsStepByIdAsync((int)goalData.IdStep);
                    var StepToDelete = await _repository.Steps.GetStepByIdAsync((int)goalData.IdStep);
                    var GoalCardToDelete = await _repository.GoalsCard.GetGoalCardByIdStep((int)goalData.IdStep);

                    _repository.GoalsStep.DeleteGoalStep(GoalStepToDelete);
                    _repository.Steps.DeleteStep(StepToDelete);
                    foreach (var item in GoalCardToDelete)
                    {
                        _repository.GoalsCard.DeleteGoalCard(item);
                    }
                    await _repository.SaveAsync();
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCard(int cardId)
        {
            try
            {
                var goalCard = new GoalsCardFields
                {
                    IdCard = cardId
                };

                _repository.GoalsCard.DeleteGoalCard(goalCard);
                await _repository.SaveAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCard(int cardId, int fieldCode)
        {
            try
            {

                var card = await _repository.GoalsCard.GetGoalCardByIdAsync(cardId);

                card.help_text = "";
                card.Description = "";
                card.Title = "";
                card.FieldCode = Convert.ToByte(fieldCode);
                card.FieldCodeType = Convert.ToByte(1);
                card.Option = "[]";
                card.HasOption = false;
                card.DisplayOrder = Convert.ToByte(0);
                card.SuccessMessage = "";
                card.Placeholder = "";
                card.Type = "";
                card.TypeRule = "";
                card.TextButton = "";
                card.Label = "";
                card.Value = "";
                card.IsRequired = false;
                card.IsDisabled = false;
                card.MaxData = Convert.ToByte(1);
                card.AllowMultiple = false;
                card.FilesType = "[]";


                _repository.GoalsCard.UpdateGoalCard(card);
                await _repository.SaveAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetAsPromoGoal()
        {
            try
            {
                var getActivedPromoGoal = await _repository.Goal.GetSingleGoalByPromoGoalStatus(true);
                if (getActivedPromoGoal != null)
                {
                    getActivedPromoGoal.StatusPromo = false;

                    _repository.Goal.UpdateGoal(getActivedPromoGoal);
                    await _repository.SaveAsync();
                }


                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> setAsOnboardingGoal(int goalId)
        {
            try
            {
                var Email = Request.Cookies["X-Admin-Email"] ?? String.Empty;
                var _userData = await _repository.AdminUsers.GetAdminByEmail(Email);
                var getActivedOnboardingGoal = await _repository.Goals.GetGoalsByStatusProgram((int)_userData.ProgramId);
                var chooseGoal = await _repository.Goal.GetGoalByIdAsync(goalId);
                if (getActivedOnboardingGoal != null)
                {
                    if (getActivedOnboardingGoal.IdGoal != chooseGoal.IdGoal)
                    {
                        getActivedOnboardingGoal.StatusOnboarding = false;
                        _repository.Goals.Update(getActivedOnboardingGoal);
                        await _repository.SaveAsync();
                        chooseGoal.StatusOnboarding = true;
                        _repository.Goal.Update(chooseGoal);
                        await _repository.SaveAsync();
                    }
                    else
                    {
                        return Ok("already user on boarding");
                    }
                }
                else
                {
                    chooseGoal.StatusOnboarding = true;
                    _repository.Goal.Update(chooseGoal);
                    await _repository.SaveAsync();
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #region publish goal
        [HttpPost]
        public async Task<IActionResult> PublishGoal(IFormCollection form)
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();

            try
            {
                if (form.TryGetValue("data", out var Data) && form.TryGetValue("data_ui", out var CardJSON) && form.TryGetValue("programId", out var ProgramId))
                {
                    var data = JsonConvert.DeserializeObject<dynamic>(Data.ToString());
                    int stepLength = data?.steps.Count ?? 0;
                    int programId = ProgramId != "" ? int.Parse(ProgramId) : 0;
                    var key = Guid.NewGuid().ToString().Substring(0, 5);
                    var img = Regex.Replace(data?.goal_img.Value, "[^a-zA-Z0-9]", String.Empty);
                    var newGoal = new Goal
                    {
                        TitleGoal = data?.goal_title ?? "",
                        IdLevel = data?.level_id ?? 1,
                        Logo = key + img ?? "",
                        TotalPoints = data?.goal_reward ?? 0,
                        Description = data?.goal_description ?? "",
                        SecondTitle = data?.goal_subtitle ?? "",
                        Label = data?.goal_label.ToString() ?? "[]",
                        HelpText = data?.goal_help_text ?? "",
                        DateCreated = DateTime.Now,
                        LockedStatus = false,
                        StatusComplete = false,
                        StatusPromo = Convert.ToBoolean(data?.promo_goal),
                        StatusOnboarding = Convert.ToBoolean(data?.onboarding_goal),
                        IsRequired = false,
                        StepsJsonUI = CardJSON.ToString(),
                        ProgramId = programId,
                        IsPublished = true
                    };

                    _repository.Goal.CreateGoal(newGoal);
                    await _repository.SaveAsync();
                    await _service.AWSUserImgService.UploadImageFile(file, key);

                    for (var i = 0; i < stepLength; i++)
                    {
                        int stepCardsLength = data?.steps[i].cards.Count ?? 0;

                        var newStep = new Step
                        {
                            TitleStep = data?.steps[i].step_title ?? "",
                            CreatedDateStep = newGoal.DateCreated,
                            DescriptionStep = data?.steps[i].step_description ?? ""
                        };

                        _repository.Steps.CreateStep(newStep);
                        await _repository.SaveAsync();

                        var newGoalsSteps = new GoalsStep
                        {
                            IdGoal = newGoal.IdGoal,
                            IdStep = newStep.IdStep,
                            GoalStepDateCreated = newGoal.DateCreated,
                            OrderStep = 0,
                            Title = newStep.TitleStep,
                            Description = newStep.DescriptionStep
                        };

                        _repository.GoalsStep.CreateGoalStep(newGoalsSteps);
                        await _repository.SaveAsync();

                        for (var card = 0; card < stepCardsLength; card++)
                        {

                            var newCards = new GoalsCardFields
                            {
                                IdStep = newGoalsSteps.IdStep ?? 0,
                                help_text = data?.steps[i].cards[card]?.help_text ?? "",
                                Title = "",
                                Description = data?.steps[i].cards[card]?.description ?? "",
                                FieldCode = Convert.ToByte(data?.steps[i].cards[card]?.schema?.field_code) ?? Convert.ToByte(0),
                                FieldCodeType = Convert.ToByte(data?.steps[i].cards[card]?.schema?.field_code_type) ?? Convert.ToByte(1),
                                Option = data?.steps[i].cards[card]?.schema?.option?.ToString() ?? "[]",
                                HasOption = false,
                                DisplayOrder = Convert.ToByte(0),
                                SuccessMessage = "",
                                Placeholder = data?.steps[i].cards[card]?.schema?.props?.placeholder ?? "",
                                TextButton = data?.steps[i].cards[card]?.schema?.props?.text_button ?? "",
                                Type = data?.steps[i].cards[card]?.schema?.props?.type ?? "",
                                TypeRule = data?.steps[i].cards[card]?.schema?.props?.type_rule ?? "",
                                Label = data?.steps[i].cards[card]?.schema?.props?.label ?? "",
                                Value = "",
                                IsRequired = data?.steps[i].cards[card]?.schema?.props?.required ?? false,
                                IsDisabled = false,
                                MaxData = Convert.ToByte(data?.steps[i].cards[card]?.schema?.props?.max_data) ?? Convert.ToByte(0),
                                AllowMultiple = data?.steps[i].cards[card]?.schema?.props?.allow_multiple ?? false,
                                FilesType = data?.steps[i].cards[card]?.schema?.props?.files_type?.ToString() ?? "[]"
                            };

                            _repository.GoalsCard.CreateGoalCard(newCards);
                            await _repository.SaveAsync();

                        }

                    }

                }

                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message, confirm = true });
            }
        }

        #endregion

        #region publish edited goal
        [HttpPost]
        public async Task<IActionResult> PublishEditGoal(IFormCollection form)
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.FirstOrDefault();

            try
            {

                if (form.TryGetValue("data", out var Data) && form.TryGetValue("data_ui", out var CardJSON) && form.TryGetValue("programId", out var ProgramId))
                {
                    var data = JsonConvert.DeserializeObject<dynamic>(Data.ToString());
                    int stepLength = data?.steps.Count ?? 0;
                    int programId = ProgramId != "" ? int.Parse(ProgramId) : 0;
                    int goal_id = data?.goal_id;

                    if (data != null)
                    {
                        var goal = await _repository.Goal.GetGoalByIdAsync(goal_id);

                        goal.TitleGoal = data?.goal_title ?? "";
                        goal.IdLevel = data?.level_id ?? 1;
                        goal.TotalPoints = data?.goal_reward ?? 0;
                        goal.Description = data?.goal_description ?? "";
                        goal.SecondTitle = data?.goal_subtitle ?? "";
                        goal.Label = data?.goal_label.ToString() ?? "[]";
                        goal.HelpText = data?.goal_help_text ?? "";
                        goal.StatusPromo = Convert.ToBoolean(data?.promo_goal);
                        goal.StepsJsonUI = CardJSON.ToString() ?? "[]";
                        goal.ProgramId = programId;
                        goal.IsPublished = true;

                        if (file != null)
                        {
                            var key = Guid.NewGuid().ToString().Substring(0, 5);
                            await _service.AWSUserImgService.DeleteImageFile(goal.Logo ?? "");
                            await _service.AWSUserImgService.UploadImageFile(file, key);
                            var img = Regex.Replace(data?.goal_img.Value, "[^a-zA-Z0-9]", String.Empty);

                            goal.Logo = $"{key}{img ?? ""}";
                        }

                        _repository.Goal.UpdateGoal(goal);
                        await _repository.SaveAsync();

                        for (var i = 0; i < stepLength; i++)
                        {

                            int stepCardsLength = data?.steps[i].cards.Count ?? 0;
                            int stepId = data?.steps[i]?.step ?? 0;

                            if (stepId > 0)
                            {
                                var step = await _repository.Steps.GetStepByIdAsync(stepId);
                                var goalStep = await _repository.GoalsStep.GetGoalsStepByIdAsync(stepId);

                                step.TitleStep = data?.steps[i]?.step_title ?? "";
                                step.DescriptionStep = data?.steps[i]?.step_description ?? "";

                                _repository.Steps.UpdateStep(step);
                                await _repository.SaveAsync();

                                goalStep.Title = data?.steps[i]?.step_title ?? "";
                                goalStep.Description = data?.steps[i]?.step_description ?? "";

                                _repository.GoalsStep.UpdateGoalStep(goalStep);
                                await _repository.SaveAsync();

                                for (var card = 0; card < stepCardsLength; card++)
                                {

                                    int cardId = data?.steps[i]?.cards[card]?.id ?? 0;

                                    if (cardId > 0)
                                    {
                                        var goalCard = await _repository.GoalsCard.GetGoalCardByIdAsync(cardId);

                                        goalCard.help_text = data?.steps[i].cards[card]?.help_text ?? "";
                                        goalCard.Description = data?.steps[i].cards[card]?.description ?? "";
                                        goalCard.Title = data?.steps[i].cards[card]?.title ?? "";
                                        goalCard.FieldCode = Convert.ToByte(data?.steps[i].cards[card]?.schema?.field_code) ?? Convert.ToByte(0);
                                        goalCard.FieldCodeType = Convert.ToByte(data?.steps[i].cards[card]?.schema?.field_code_type) ?? Convert.ToByte(1);
                                        goalCard.Option = data?.steps[i].cards[card]?.schema?.option?.ToString() ?? "[]";
                                        goalCard.HasOption = false;
                                        goalCard.DisplayOrder = Convert.ToByte(0);
                                        goalCard.SuccessMessage = "";
                                        goalCard.Placeholder = data?.steps[i].cards[card]?.schema?.props?.placeholder ?? "";
                                        goalCard.Type = data?.steps[i].cards[card]?.schema?.props?.type ?? "";
                                        goalCard.TypeRule = data?.steps[i].cards[card]?.schema?.props?.type_rule ?? "";
                                        goalCard.TextButton = data?.steps[i].cards[card]?.schema?.props?.text_button ?? "";
                                        goalCard.Label = data?.steps[i].cards[card]?.schema?.props?.label ?? "";
                                        goalCard.Value = "";
                                        goalCard.IsRequired = data?.steps[i].cards[card]?.schema?.props?.required ?? false;
                                        goalCard.IsDisabled = false;
                                        goalCard.MaxData = Convert.ToByte(data?.steps[i].cards[card]?.schema?.props?.max_data) ?? Convert.ToByte(0);
                                        goalCard.AllowMultiple = data?.steps[i].cards[card]?.schema?.props?.allow_multiple ?? false;
                                        goalCard.FilesType = data?.steps[i].cards[card]?.schema?.props?.files_type?.ToString() ?? "[]";

                                        _repository.GoalsCard.UpdateGoalCard(goalCard);
                                        await _repository.SaveAsync();

                                    }
                                    else
                                    {

                                        var newCards = new GoalsCardFields
                                        {
                                            IdStep = stepId,
                                            help_text = data?.steps[i].cards[card]?.help_text ?? "",
                                            Title = data?.steps[i].cards[card]?.title ?? "",
                                            Description = data?.steps[i].cards[card]?.description ?? "",
                                            FieldCode = Convert.ToByte(data?.steps[i].cards[card]?.schema?.field_code) ?? Convert.ToByte(0),
                                            FieldCodeType = Convert.ToByte(data?.steps[i].cards[card]?.schema?.field_code_type) ?? Convert.ToByte(1),
                                            Option = data?.steps[i].cards[card]?.schema?.option?.ToString() ?? "[]",
                                            HasOption = false,
                                            DisplayOrder = Convert.ToByte(0),
                                            SuccessMessage = "",
                                            Placeholder = data?.steps[i].cards[card]?.schema?.props?.placeholder ?? "",
                                            Type = data?.steps[i].cards[card]?.schema?.props?.type ?? "",
                                            TypeRule = data?.steps[i].cards[card]?.schema?.props?.type_rule ?? "",
                                            TextButton = data?.steps[i].cards[card]?.schema?.props?.text_button ?? "",
                                            Label = data?.steps[i].cards[card]?.schema?.props?.label ?? "",
                                            Value = "",
                                            IsRequired = data?.steps[i].cards[card]?.schema?.props?.required ?? false,
                                            IsDisabled = false,
                                            MaxData = Convert.ToByte(data?.steps[i].cards[card]?.schema?.props?.max_data) ?? Convert.ToByte(0),
                                            AllowMultiple = data?.steps[i].cards[card]?.schema?.props?.allow_multiple ?? false,
                                            FilesType = data?.steps[i].cards[card]?.schema?.props?.files_type?.ToString() ?? "[]"
                                        };

                                        _repository.GoalsCard.CreateGoalCard(newCards);
                                        await _repository.SaveAsync();

                                    }

                                }

                            }
                            else
                            {

                                var newStep = new Step
                                {
                                    TitleStep = data?.steps[i].step_title ?? "",
                                    CreatedDateStep = DateTime.Now,
                                    DescriptionStep = data?.steps[i].step_description ?? ""
                                };

                                _repository.Steps.CreateStep(newStep);
                                await _repository.SaveAsync();

                                var newGoalsSteps = new GoalsStep
                                {
                                    IdGoal = goal_id,
                                    IdStep = newStep.IdStep,
                                    GoalStepDateCreated = DateTime.Now,
                                    OrderStep = 0,
                                    Title = newStep.TitleStep,
                                    Description = newStep.DescriptionStep
                                };

                                _repository.GoalsStep.CreateGoalStep(newGoalsSteps);
                                await _repository.SaveAsync();

                                for (var card = 0; card < stepCardsLength; card++)
                                {

                                    var newCards = new GoalsCardFields
                                    {
                                        IdStep = newGoalsSteps.IdStep ?? 0,
                                        help_text = data?.steps[i].cards[card]?.help_text ?? "",
                                        Title = data?.steps[i].cards[card]?.title ?? "",
                                        Description = data?.steps[i].cards[card]?.description ?? "",
                                        FieldCode = Convert.ToByte(data?.steps[i].cards[card]?.schema?.field_code) ?? Convert.ToByte(0),
                                        FieldCodeType = Convert.ToByte(data?.steps[i].cards[card]?.schema?.field_code_type) ?? Convert.ToByte(1),
                                        Option = data?.steps[i].cards[card]?.option?.ToString() ?? "[]",
                                        HasOption = false,
                                        DisplayOrder = Convert.ToByte(0),
                                        SuccessMessage = "",
                                        Placeholder = data?.steps[i].cards[card]?.schema?.props?.placeholder ?? "",
                                        Type = data?.steps[i].cards[card]?.schema?.props?.type ?? "",
                                        TypeRule = data?.steps[i].cards[card]?.schema?.props?.type_rule ?? "",
                                        TextButton = data?.steps[i].cards[card]?.schema?.props?.text_button ?? "",
                                        Label = data?.steps[i].cards[card]?.schema?.props?.label ?? "",
                                        Value = "",
                                        IsRequired = data?.steps[i].cards[card]?.schema?.props?.required ?? false,
                                        IsDisabled = false,
                                        MaxData = Convert.ToByte(data?.steps[i].cards[card]?.schema?.props?.max_data) ?? Convert.ToByte(0),
                                        AllowMultiple = data?.steps[i].cards[card]?.schema?.props?.allow_multiple ?? false,
                                        FilesType = data?.steps[i].cards[card]?.schema?.props?.file_types?.ToString() ?? "[]"
                                    };

                                    _repository.GoalsCard.CreateGoalCard(newCards);
                                    await _repository.SaveAsync();

                                }

                            }
                        }

                    }


                }

                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message, confirm = true });
            }
        }

        #endregion

        #region publish edited goal
        [HttpPost]
        public async Task<IActionResult> UnpublishEditGoal(IFormCollection form)
        {

            try
            {

                if (form.TryGetValue("data", out var Data))
                {
                    var data = JsonConvert.DeserializeObject<dynamic>(Data.ToString());
                    int goal_id = data?.goal_id;

                    if (data != null)
                    {
                        var goal = await _repository.Goal.GetGoalByIdAsync(goal_id);

                        goal.IsPublished = false;

                        _repository.Goal.UpdateGoal(goal);
                        await _repository.SaveAsync();
                    }

                }

                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message, confirm = true });
            }
        }

        #endregion

        #region get details goal
        [HttpGet]
        public async Task<IActionResult> GetDetailsGoalById(int? idGoal)
        {
            try
            {

                var GoalsStepsQuery = await (from goals_steps in investeur_context.GoalsSteps.AsNoTracking()
                                             where goals_steps.IdGoal == idGoal
                                             select new
                                             {

                                                 id_step = goals_steps.IdStep,
                                                 id_goal = goals_steps.IdGoal,
                                                 step_title = goals_steps.Title,
                                                 step_description = goals_steps.Description,
                                                 visible = goals_steps.Visible

                                             }).Where(i => i.visible == true).ToListAsync();


                var GoalsQuery = await (from goals in investeur_context.Goals.AsNoTracking()
                                        join goals_level in investeur_context.Levels.AsNoTracking()
                                        on goals.IdLevel equals goals_level.IdLevel
                                        where goals.IdGoal == idGoal
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
                                            promo_goal = goals.StatusPromo,
                                            onboarding_goal = goals.StatusOnboarding,
                                            cards_ui = goals.StepsJsonUI,
                                            goal_help_text = goals.HelpText,
                                            is_published = goals.IsPublished,
                                            status_score = goals.StatusScore,
                                            program_group_id = goals.ProgramGroupId

                                        }).FirstOrDefaultAsync();

                var ResultQuery = new GoalDetails();
                List<GoalDetailsStep> StepsList = new List<GoalDetailsStep>();

                ResultQuery.id = GoalsQuery?.id;
                ResultQuery.promo_goal = GoalsQuery?.promo_goal;
                ResultQuery.onboarding_goal = GoalsQuery?.onboarding_goal;
                ResultQuery.logo = GoalsQuery?.logo;
                ResultQuery.label = GoalsQuery?.label;
                ResultQuery.id_level = GoalsQuery?.id_level ?? 0;
                ResultQuery.title = GoalsQuery?.title;
                ResultQuery.description = GoalsQuery?.description;
                ResultQuery.second_title = GoalsQuery?.second_title;
                ResultQuery.total_points = GoalsQuery?.total_points ?? 0;
                ResultQuery.goal_category_logo = GoalsQuery?.goal_category_logo;
                ResultQuery.goal_help_text = GoalsQuery?.goal_help_text;
                ResultQuery.id_goal_category = GoalsQuery?.id_goal_category;
                ResultQuery.code_level = GoalsQuery?.code_level;
                ResultQuery.is_published = GoalsQuery?.is_published;
                ResultQuery.cards_ui_json = GoalsQuery?.cards_ui;
                ResultQuery.status_score = GoalsQuery?.status_score;
                ResultQuery.program_group_id = GoalsQuery?.program_group_id ?? 0;

                foreach (var step in GoalsStepsQuery)
                {


                    GoalDetailsStep StepQuery = new GoalDetailsStep();
                    var cards = _repository.Cards.GetCardsDetailByStep(step.id_step);

                    StepQuery.step = step.id_step ?? 0;
                    StepQuery.step_title = step.step_title;
                    StepQuery.step_description = step.step_description;
                    StepQuery.is_step_complete = true;

                    StepQuery.cards = cards.ToList();

                    StepsList.Add(StepQuery);

                }

                ResultQuery.steps = StepsList;

                string goalResults = JsonConvert.SerializeObject(ResultQuery, new JsonSerializerSettings
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

        #region save goal as draft
        [HttpPost]
        public async Task<IActionResult> SaveGoal(IFormCollection form)
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.FirstOrDefault();

            try
            {
                if (form.TryGetValue("data", out var Data) && form.TryGetValue("data_ui", out var CardJSON) && form.TryGetValue("programId", out var ProgramId))
                {
                    var data = JsonConvert.DeserializeObject<dynamic>(Data.ToString());
                    int stepLength = data?.steps.Count ?? 0;
                    int programId = ProgramId != "" ? int.Parse(ProgramId) : 0;
                    var key = Guid.NewGuid().ToString().Substring(0, 5);
                    var img = "";
                    if (data?.goal_img != null)
                    {
                        img = Regex.Replace(data?.goal_img.Value, "[^a-zA-Z0-9]", String.Empty);
                    }

                    var newGoal = new Goal
                    {
                        TitleGoal = data?.goal_title ?? "",
                        IdLevel = data?.level_id,
                        Logo = file != null ? key + img : "",
                        TotalPoints = data?.goal_reward,
                        Description = data?.goal_description ?? "",
                        SecondTitle = data?.goal_subtitle ?? "",
                        Label = data?.goal_label.ToString() ?? "[]",
                        HelpText = data?.goal_help_text ?? "",
                        DateCreated = DateTime.Now,
                        LockedStatus = false,
                        StatusComplete = false,
                        StatusPromo = Convert.ToBoolean(data?.promo_goal ?? 0),
                        IsRequired = false,
                        StepsJsonUI = CardJSON.ToString(),
                        ProgramId = programId,
                        ProgramGroupId = 0,
                        IsPublished = false,
                        StatusScore = data?.status_score ?? true
                    };

                    _repository.Goal.CreateGoal(newGoal);
                    await _repository.SaveAsync();

                    if (file != null)
                    {
                        await _service.AWSUserImgService.UploadImageFile(file, key);
                    }

                    for (var i = 0; i < stepLength; i++)
                    {
                        int stepCardsLength = data?.steps[i].cards.Count ?? 0;

                        var newStep = new Step
                        {
                            TitleStep = data?.steps[i].step_title ?? "",
                            CreatedDateStep = newGoal.DateCreated,
                            DescriptionStep = data?.steps[i].step_description ?? ""
                        };

                        _repository.Steps.CreateStep(newStep);
                        await _repository.SaveAsync();

                        var newGoalsSteps = new GoalsStep
                        {
                            IdGoal = newGoal.IdGoal,
                            IdStep = newStep.IdStep,
                            GoalStepDateCreated = newGoal.DateCreated,
                            OrderStep = 0,
                            Title = newStep.TitleStep,
                            Description = newStep.DescriptionStep
                        };

                        _repository.GoalsStep.CreateGoalStep(newGoalsSteps);
                        await _repository.SaveAsync();

                        var goalCards = await _repository.GoalsCard.GetAllDataRecord();
                        var lastRecord = goalCards.OrderByDescending(x => x.IdCard).FirstOrDefault();

                        for (var card = 0; card < stepCardsLength; card++)
                        {

                            var newCards = new GoalsCardFields
                            {
                                IdStep = newGoalsSteps.IdStep ?? 0,
                                help_text = data?.steps[i].cards[card]?.help_text ?? "",
                                Title = "",
                                Description = data?.steps[i].cards[card]?.description ?? "",
                                FieldCode = Convert.ToByte(data?.steps[i].cards[card]?.schema?.field_code) ?? Convert.ToByte(0),
                                FieldCodeType = Convert.ToByte(data?.steps[i].cards[card]?.schema?.field_code_type) ?? Convert.ToByte(1),
                                Option = data?.steps[i].cards[card]?.schema?.option?.ToString() ?? "[]",
                                HasOption = false,
                                DisplayOrder = Convert.ToByte(0),
                                SuccessMessage = "",
                                Placeholder = data?.steps[i].cards[card]?.schema?.props?.placeholder ?? "",
                                TextButton = data?.steps[i].cards[card]?.schema?.props?.text_button ?? "",
                                Type = data?.steps[i].cards[card]?.schema?.props?.type ?? "",
                                TypeRule = data?.steps[i].cards[card]?.schema?.props?.type_rule ?? "",
                                Label = data?.steps[i].cards[card]?.schema?.props?.label ?? "",
                                Value = "",
                                IsRequired = data?.steps[i].cards[card]?.schema?.props?.required ?? false,
                                IsDisabled = false,
                                MaxData = Convert.ToByte(data?.steps[i].cards[card]?.schema?.props?.max_data) ?? Convert.ToByte(0),
                                AllowMultiple = data?.steps[i].cards[card]?.schema?.props?.allow_multiple ?? false,
                                FilesType = data?.steps[i].cards[card]?.schema?.props?.files_type?.ToString() ?? "[]",
                                QuestionID = lastRecord.IdCard + 1
                            };

                            _repository.GoalsCard.CreateGoalCard(newCards);
                            await _repository.SaveAsync();

                        }

                    }

                    return Ok(newGoal.IdGoal);

                }

                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message, confirm = true });
            }
        }

        #endregion

        #region publish edited goal
        [HttpPost]
        public async Task<IActionResult> UpdateSavedGoal(IFormCollection form)
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.FirstOrDefault();


                if (form.TryGetValue("data", out var Data) && form.TryGetValue("programId", out var ProgramId)
                && form.TryGetValue("goal_id", out var GoalId))
                {
                    var data = JsonConvert.DeserializeObject<dynamic>(Data.ToString());
                    int stepLength = data?.steps.Count ?? 0;
                    int programId = ProgramId != "" ? int.Parse(ProgramId) : 0;
                    int goal_id = GoalId != "" ? int.Parse(GoalId) : 0;
                    int levelId = data?.level_id.ToString() != "" ? data?.level_id : 1;

                    if (data != null)
                    {
                        var goal = await _repository.Goal.GetGoalByIdAsync(goal_id);

                        goal.TitleGoal = data?.goal_title ?? "";
                        goal.IdLevel = levelId;
                        goal.TotalPoints = data?.goal_reward;
                        goal.Description = data?.goal_description ?? "";
                        goal.SecondTitle = data?.goal_subtitle ?? "";
                        goal.Label = data?.goal_label.ToString() ?? "[]";
                        goal.HelpText = data?.goal_help_text ?? "";
                        goal.StatusPromo = Convert.ToBoolean(data?.promo_goal);
                        goal.StepsJsonUI = "[]";
                        goal.ProgramId = programId;
                        goal.ProgramGroupId = data?.program_group_id ?? 0;
                        goal.IsPublished = data?.is_published;
                        goal.StatusScore = data?.status_score ?? true;

                        if (file != null)
                        {
                             Regex regex = new Regex("[^a-zA-Z0-9]");
                             var fileName = regex.Replace(file.FileName.ToLower(), "");

                            // var key = Guid.NewGuid().ToString().Substring(0, 5);
                            // await _service.AWSUserImgService.DeleteImageFile(goal.Logo ?? "");
                            await _service.AWSUserImgService.UploadImageFile(file);
                            // var img = Regex.Replace(data?.goal_img.Value, "[^a-zA-Z0-9]", String.Empty);

                            goal.Logo = fileName;
                        }

                        _repository.Goal.UpdateGoal(goal);
                        await _repository.SaveAsync();

                        for (var i = 0; i < stepLength; i++)
                        {

                            int stepCardsLength = data?.steps[i]?.cards?.Count ?? 0;
                            int stepId = data?.steps[i]?.step ?? 0;
                            var goalCards = await _repository.GoalsCard.GetAllDataRecord();
                            var lastRecord = goalCards.OrderByDescending(x => x.IdCard).FirstOrDefault();

                            if (stepId > 0)
                            {
                                var step = await _repository.Steps.GetStepByIdAsync(stepId);
                                var goalStep = await _repository.GoalsStep.GetGoalsStepByIdAsync(stepId);

                                step.TitleStep = data?.steps[i]?.step_title ?? "";
                                step.DescriptionStep = data?.steps[i]?.step_description ?? "";

                                _repository.Steps.UpdateStep(step);
                                await _repository.SaveAsync();

                                goalStep.Title = data?.steps[i]?.step_title ?? "";
                                goalStep.Description = data?.steps[i]?.step_description ?? "";

                                _repository.GoalsStep.UpdateGoalStep(goalStep);
                                await _repository.SaveAsync();

                                for (var card = 0; card < stepCardsLength; card++)
                                {

                                    var cardId = data?.steps[i]?.cards[card]?.id ?? 0;
                                    int cardIdNotNull = (int)cardId;

                                    if (cardId > 0)
                                    {
                                        var goalCard = await _repository.GoalsCard.GetGoalCardByIdAsync(cardIdNotNull);

                                        goalCard.help_text = data?.steps[i]?.cards[card]?.help_text ?? "";
                                        goalCard.Description = data?.steps[i]?.cards[card]?.description ?? "";
                                        goalCard.Title = data?.steps[i]?.cards[card]?.title ?? "";
                                        goalCard.FieldCode = Convert.ToByte(data?.steps[i]?.cards[card]?.schema?.field_code) ?? Convert.ToByte(0);
                                        goalCard.FieldCodeType = Convert.ToByte(data?.steps[i]?.cards[card]?.schema?.field_code_type) ?? Convert.ToByte(1);
                                        goalCard.Option = data?.steps[i]?.cards[card]?.schema?.option?.ToString() ?? "[]";
                                        goalCard.HasOption = false;
                                        goalCard.DisplayOrder = Convert.ToByte(0);
                                        goalCard.SuccessMessage = "";
                                        goalCard.Placeholder = data?.steps[i]?.cards[card]?.schema?.props?.placeholder ?? "";
                                        goalCard.Type = data?.steps[i]?.cards[card]?.schema?.props?.type ?? "";
                                        goalCard.TypeRule = data?.steps[i]?.cards[card]?.schema?.props?.type_rule ?? "";
                                        goalCard.TextButton = data?.steps[i]?.cards[card]?.schema?.props?.text_button ?? "";
                                        goalCard.Label = data?.steps[i]?.cards[card]?.schema?.props?.label ?? "";
                                        goalCard.Value = "";
                                        goalCard.IsRequired = data?.steps[i]?.cards[card]?.schema?.props?.required ?? false;
                                        goalCard.IsDisabled = false;
                                        goalCard.MaxData = Convert.ToByte(data?.steps[i]?.cards[card]?.schema?.props?.max_data) ?? Convert.ToByte(1);
                                        goalCard.AllowMultiple = data?.steps[i]?.cards[card]?.schema?.props?.allow_multiple ?? false;
                                        goalCard.FilesType = data?.steps[i]?.cards[card]?.schema?.props?.files_type?.ToString() ?? "[]";
                                        goalCard.Score = 0;

                                        if(goal.StatusScore == true)
                                        {
                                            if(data?.level_code_number == 2)
                                            {
                                                goalCard.Score = 5;
                                            } else if(data?.level_code_number == 3)
                                            {
                                                goalCard.Score = 10;
                                            } else
                                            {
                                                goalCard.Score = data?.steps[i].cards[card]?.schema?.props?.score ?? 0;
                                            }
                                        }

                                        _repository.GoalsCard.UpdateGoalCard(goalCard);
                                        await _repository.SaveAsync();

                                    }
                                    else
                                    {

                                        var newCards = new GoalsCardFields
                                        {
                                            IdStep = stepId,
                                            help_text = data?.steps[i].cards[card]?.help_text ?? "",
                                            Title = data?.steps[i].cards[card]?.title ?? "",
                                            Description = data?.steps[i].cards[card]?.description ?? "",
                                            FieldCode = Convert.ToByte(data?.steps[i].cards[card]?.schema?.field_code) ?? Convert.ToByte(0),
                                            FieldCodeType = Convert.ToByte(data?.steps[i].cards[card]?.schema?.field_code_type) ?? Convert.ToByte(1),
                                            Option = data?.steps[i].cards[card]?.schema?.option?.ToString() ?? "[]",
                                            HasOption = false,
                                            DisplayOrder = Convert.ToByte(0),
                                            SuccessMessage = "",
                                            Placeholder = data?.steps[i].cards[card]?.schema?.props?.placeholder ?? "",
                                            Type = data?.steps[i].cards[card]?.schema?.props?.type ?? "",
                                            TypeRule = data?.steps[i].cards[card]?.schema?.props?.type_rule ?? "",
                                            TextButton = data?.steps[i].cards[card]?.schema?.props?.text_button ?? "",
                                            Label = data?.steps[i].cards[card]?.schema?.props?.label ?? "",
                                            Value = "",
                                            IsRequired = data?.steps[i].cards[card]?.schema?.props?.required ?? false,
                                            IsDisabled = false,
                                            MaxData = Convert.ToByte(data?.steps[i].cards[card]?.schema?.props?.max_data) ?? Convert.ToByte(0),
                                            AllowMultiple = data?.steps[i].cards[card]?.schema?.props?.allow_multiple ?? false,
                                            FilesType = data?.steps[i].cards[card]?.schema?.props?.files_type?.ToString() ?? "[]",
                                            QuestionID = lastRecord.IdCard + 1,
                                            Score = 0
                                        };

                                        if(goal.StatusScore == true)
                                        {
                                            if(data?.level_code_number == 2)
                                            {
                                                newCards.Score = 5;
                                            } else if(data?.level_code_number == 3)
                                            {
                                                newCards.Score = 10;
                                            } else
                                            {
                                                newCards.Score = data?.steps[i].cards[card]?.schema?.props?.score ?? 0;
                                            }
                                        }

                                        _repository.GoalsCard.CreateGoalCard(newCards);
                                        await _repository.SaveAsync();

                                    }

                                }

                            }
                            else
                            {

                                var newStep = new Step
                                {
                                    TitleStep = data?.steps[i].step_title ?? "",
                                    CreatedDateStep = DateTime.Now,
                                    DescriptionStep = data?.steps[i].step_description ?? ""
                                };

                                _repository.Steps.CreateStep(newStep);
                                await _repository.SaveAsync();

                                var newGoalsSteps = new GoalsStep
                                {
                                    IdGoal = goal_id,
                                    IdStep = newStep.IdStep,
                                    GoalStepDateCreated = DateTime.Now,
                                    OrderStep = 0,
                                    Title = newStep.TitleStep,
                                    Description = newStep.DescriptionStep
                                };

                                _repository.GoalsStep.CreateGoalStep(newGoalsSteps);
                                await _repository.SaveAsync();

                                for (var card = 0; card < stepCardsLength; card++)
                                {

                                    var newCards = new GoalsCardFields
                                    {
                                        IdStep = newGoalsSteps.IdStep ?? 0,
                                        help_text = data?.steps[i].cards[card]?.help_text ?? "",
                                        Title = data?.steps[i].cards[card]?.title ?? "",
                                        Description = data?.steps[i].cards[card]?.description ?? "",
                                        FieldCode = Convert.ToByte(data?.steps[i].cards[card]?.schema?.field_code) ?? Convert.ToByte(0),
                                        FieldCodeType = Convert.ToByte(data?.steps[i].cards[card]?.schema?.field_code_type) ?? Convert.ToByte(1),
                                        Option = data?.steps[i].cards[card]?.option?.ToString() ?? "[]",
                                        HasOption = false,
                                        DisplayOrder = Convert.ToByte(0),
                                        SuccessMessage = "",
                                        Placeholder = data?.steps[i].cards[card]?.schema?.props?.placeholder ?? "",
                                        Type = data?.steps[i].cards[card]?.schema?.props?.type ?? "",
                                        TypeRule = data?.steps[i].cards[card]?.schema?.props?.type_rule ?? "",
                                        TextButton = data?.steps[i].cards[card]?.schema?.props?.text_button ?? "",
                                        Label = data?.steps[i].cards[card]?.schema?.props?.label ?? "",
                                        Value = "",
                                        IsRequired = data?.steps[i].cards[card]?.schema?.props?.required ?? false,
                                        IsDisabled = false,
                                        MaxData = Convert.ToByte(data?.steps[i].cards[card]?.schema?.props?.max_data) ?? Convert.ToByte(0),
                                        AllowMultiple = data?.steps[i].cards[card]?.schema?.props?.allow_multiple ?? false,
                                        FilesType = data?.steps[i].cards[card]?.schema?.props?.file_types?.ToString() ?? "[]",
                                        QuestionID = lastRecord.IdCard + 1,
                                        Score = 0
                                    };

                                    if(goal.StatusScore == true)
                                    {
                                        if(data?.level_code_number == 2)
                                        {
                                            newCards.Score = 5;
                                        } else if(data?.level_code_number == 3)
                                        {
                                            newCards.Score = 10;
                                        } else
                                        {
                                            newCards.Score = data?.steps[i].cards[card]?.schema?.props?.score ?? 0;
                                        }
                                    }

                                    _repository.GoalsCard.CreateGoalCard(newCards);
                                    await _repository.SaveAsync();

                                }

                            }
                        }

                    }


                }

                return Ok();

        }

        #endregion

        public async Task<IActionResult> AddStep(int goalId){
            try
            {
                var goal = await _repository.Goal.GetGoalByIdAsync(goalId);
                var step = new Step
                {
                    TitleStep = "",
                    CreatedDateStep = DateTime.Now,
                    DescriptionStep = ""
                };

                _repository.Steps.CreateStep(step);
                await _repository.SaveAsync();

                var goalStep = new GoalsStep
                {
                    IdGoal = goalId,
                    IdStep = step.IdStep,
                    GoalStepDateCreated = DateTime.Now,
                    OrderStep = 0,
                    Title = "",
                    Description = "",
                    Visible = true
                };

                _repository.GoalsStep.CreateGoalStep(goalStep);
                await _repository.SaveAsync();

                return Ok(goalStep.IdStep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }   

        [HttpPost]
        public async Task<IActionResult> deleteStep(int stepId){
            try
            {
                var step = await _repository.Steps.GetStepByIdAsync(stepId);
                var goalStep = await _repository.GoalsStep.GetGoalsStepByIdAsync(stepId);
                var cards = await _repository.GoalsCard.GetGoalCardByIdStep(stepId);

                _repository.Steps.DeleteStep(step);
                await _repository.SaveAsync();

                _repository.GoalsStep.DeleteGoalStep(goalStep);
                await _repository.SaveAsync();

                foreach (var card in cards)
                {
                    _repository.GoalsCard.DeleteGoalCard(card);
                    await _repository.SaveAsync();
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddField (int stepId, byte field_code){
            try
            {
                var step = await _repository.Steps.GetStepByIdAsync(stepId);
                var goalStep = await _repository.GoalsStep.GetGoalsStepByIdAsync(stepId);
                var goalCards = await _repository.GoalsCard.GetAllDataRecord();
                var lastRecord = goalCards.OrderByDescending(x => x.IdCard).FirstOrDefault();
                var card = new GoalsCardFields
                {
                    IdStep = stepId,
                    help_text = "",
                    Title = "",
                    Description = "",
                    FieldCode = field_code,
                    FieldCodeType = 0,
                    Option = "[]",
                    HasOption = false,
                    DisplayOrder = 0,
                    SuccessMessage = "",
                    Placeholder = "",
                    Type = "",
                    TypeRule = "",
                    TextButton = "",
                    Label = "",
                    Value = "",
                    IsRequired = false,
                    IsDisabled = false,
                    MaxData = 1,
                    AllowMultiple = false,
                    FilesType = "[]",
                    QuestionID = lastRecord.IdCard + 1,
                    Visible = true
                };

                _repository.GoalsCard.CreateGoalCard(card);
                await _repository.SaveAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #region get list questions
        public async Task<IActionResult> GetListQuestions (string? search, int ShowEntries, int Pagination, int currentGoalId)
        {
            try
            {
                var _card = await _repository.GoalsCard.GetAllData(currentGoalId);
                #region search by title, description
                if (!string.IsNullOrEmpty(search))
                {
                    string SearchResult = search.ToLower().Trim();
                    _card = _card.Where(o => o.Title != null && o.Title.ToLower().Trim().Contains(SearchResult) ||
                    o.Description != null && o.Description.ToLower().Trim().Contains(SearchResult) ||
                    o.Label != null && o.Label.ToLower().Trim().Contains(SearchResult)
                    );
                }          
                #endregion

                int TotalPage = _card.Count();

                var resultData = _card.OrderByDescending(i => i.IdCard).Skip((Pagination - 1) * ShowEntries).Take(ShowEntries);
                string cardResults = JsonConvert.SerializeObject(new { resultData, TotalPage }, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Ok(cardResults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region copy selected question
        [HttpPost]
        public async Task<IActionResult> CopyQuestion (int cardId, int stepId)
        {
            try
            {
                var _card = await _repository.GoalsCard.GetGoalCardByIdAsync(cardId);
                var newCard = new GoalsCardFields
                {
                    IdStep = stepId,
                    help_text = _card.help_text,
                    Title = _card.Title,
                    Description = _card.Description,
                    FieldCode = _card.FieldCode,
                    FieldCodeType = _card.FieldCodeType,
                    HasOption = _card.HasOption,
                    Option = _card.Option,
                    DisplayOrder = _card.DisplayOrder,
                    SuccessMessage = _card.SuccessMessage,
                    Placeholder = _card.Placeholder,
                    Type = _card.Type,
                    TypeRule = _card.TypeRule,
                    Label = _card.Label,
                    Value = _card.Value,
                    IsRequired = _card.IsRequired,
                    IsDisabled = _card.IsDisabled,
                    MaxData = _card.MaxData,
                    AllowMultiple = _card.AllowMultiple,
                    FilesType = _card.FilesType,
                    TextButton = _card.TextButton,
                    Visible = true
                };

                if(_card.IdCard == _card.QuestionID)
                {
                    newCard.QuestionID = _card.IdCard;
                }
                else
                {
                    newCard.QuestionID = _card.QuestionID;
                }

                _repository.GoalsCard.Create(newCard);
                await _repository.SaveAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region score correction
        [HttpPost]
        public async Task<IActionResult> ScoreCorrection([FromBody] ScoreCorrectionInput input)
        {
            try
            {  
                var cardData = await _repository.CompanyGoalsCardsField.GetCompanyGoalCardFieldsByIdAsync(input.CardId, input.StartupId);
                var startupData = await _repository.Startup.GetDataByStartupId(input.StartupId);
                if (startupData == null)
                {
                    return BadRequest("No startup data found.");
                }
                var diffScore = input.Score - cardData.Score;

                startupData.startupScore += diffScore;
                _repository.Startup.Update(startupData);

                
                if (cardData == null)
                {
                    return BadRequest("No card data found.");
                }

                cardData.IsCorrected = true;
                cardData.Score = input.Score;
                _repository.CompanyGoalsCardsField.Update(cardData);

                await _repository.SaveAsync();

                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region upload file to aws
        [HttpPost]
        public async Task<ActionResult> CheckFileAWS(IFormCollection formCollection)
        {
            try
            {
                var files = formCollection.Files;
                if (files != null && files.Count > 0)
                {
                    Regex regex = new Regex("[^a-zA-Z0-9]");
                    foreach (var file in files)
                    {
                        var fileName = regex.Replace(file.FileName.ToLower(), "");
                        await _service.AWSUserImgService.UploadImageFile(file);
                        var fileStatus = await _service.AWSUserImgService.CheckFileExists(fileName);
                        if (!fileStatus)
                        {
                            return BadRequest(new { message = $"File {fileName} does not exist in the bucket.", success  = false });
                        }
                    }
                    return Ok(new { message = "All files exist in the bucket.", success  = true });
                }
                else
                {
                    return BadRequest(new { message = "No files were uploaded.", success  = false });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = e.Message,
                    success = false
                });
            }
        }
        #endregion

         #region delete file from aws
        [HttpPost]
        public async Task<ActionResult> DeleteFileAWS(string FileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(FileName))
                {
                    Regex regex = new Regex("[^a-zA-Z0-9]");
                    var fileName = regex.Replace(FileName.ToLower(), "");
                    await _service.AWSUserImgService.DeleteImageFile(fileName);
                    var fileStatus = await _service.AWSUserImgService.CheckFileExists(fileName);
                    if (fileStatus)
                    {
                        return BadRequest(new { message = $"File {fileName} still exists in the bucket.", success = false });
                    }
                    return Ok(new { message = "All files have been deleted from the bucket.", success = true });
                }
                else
                {
                    return BadRequest(new { message = "No files were uploaded.", success = false });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = e.Message,
                    success = false
                });
            }
        }
        #endregion

        // get programGroup by programId
        public async Task<IActionResult> GetProgramGroupByProgramId(int programId)
        {
            try
            {
                var programGroup = await _repository.ProgramGroup.GetProgramGroupByProgramIdAsync(programId);
                return Ok(programGroup);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
