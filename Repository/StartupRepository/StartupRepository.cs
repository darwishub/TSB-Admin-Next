using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using System.Collections;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class StartupRepository : RepositoryBase<Startup>, IStartupRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public StartupRepository(InvesteurContext _context) : base(_context)
        {
        }
        public async Task<IEnumerable<StartupCompany>> GetAllStartupsAsync(int? programid)
        {
            if(programid == null || programid == 0)
            {
                var companies = await (
                from startup in investeur_context.Startups.AsNoTracking()
                join userData in investeur_context.UsersData.AsNoTracking()
                    on startup.Userid equals userData.UserId into startupData
                from s in startupData.DefaultIfEmpty()
                join userProfile in investeur_context.UserProfiles.AsNoTracking()
                    on s.UserId equals userProfile.Userid into profileData
                from p in profileData.DefaultIfEmpty()
                where !investeur_context.StartupMembers.Any(sm => sm.Email == startup.Email) 
                    && (p.Joinedas == "1000" || p.Joinedas == "1100")
                select new
                {
                    startupid = startup.Startupid,
                    companyname = startup.Name,
                    companyEmail = startup.Email,
                    startupLogo = startup.startupLogo,
                    companyDescription = startup.Description,
                    startupScore = startup.startupScore ?? 0,
                    tsbCoin = startup.tsbCoin,
                    programGroupId = 0,
                    members = investeur_context.StartupMembers.Where(sm => sm.Startupid == startup.Startupid).Select(sm => new MembersOfStartup
                    {
                        Startupid = sm.Startupid,
                        Email = sm.Email,
                    }).ToList()
                }).ToListAsync();

                var companyGoals = await (from _companyGoal in investeur_context.CompanyGoalsStep.AsNoTracking()
                                            join _goalStep in investeur_context.GoalsSteps.AsNoTracking()
                                            on _companyGoal.IdGoalStep equals _goalStep.IdGoalStep
                                            where _companyGoal.IsStepComplete == true
                                            select new CompanyGoalStep
                                            {
                                                id_goal_step = _companyGoal.IdGoalStep,
                                                id_goal = _goalStep.IdGoal,
                                                is_step_complete = _companyGoal.IsStepComplete,
                                                dateCreated = _companyGoal.DateCreated,
                                                startupId = _companyGoal.StartupId
                                            })
                                            .GroupBy(x => new { x.id_goal_step, x.startupId })
                                            .Select(g => g.First())
                                            .ToListAsync();

                var resultsList = companies.Select(company => new StartupCompany
                {
                    startupid = company.startupid,
                    companyname = company.companyname,
                    companyEmail = company.companyEmail,
                    startupLogo = company.startupLogo,
                    companyDescription = company.companyDescription,
                    dateCreated = companyGoals.FirstOrDefault(cg => cg.startupId == company.startupid)?.dateCreated,
                    Score = company.startupScore,
                    tsbCoin = company.tsbCoin,
                    ProgramGroupId = company.programGroupId,
                    members = company.members
                }).ToList();

                return resultsList;
            }      
            else
            {
               var startupMembersEmails = await investeur_context.StartupMembers.Select(sm => sm.Email).ToListAsync();

                var companies = await (from _company in investeur_context.Startups.AsNoTracking()
                                join _program in investeur_context.StartupPrograms.AsNoTracking()
                                on _company.Startupid equals _program.StartupId
                                join user in investeur_context.UsersData.AsNoTracking()
                                on _company.Userid equals user.UserId into startupData
                                from s in startupData.DefaultIfEmpty()
                                join userProfile in investeur_context.UserProfiles.AsNoTracking()
                                on s.UserId equals userProfile.Userid into profileData
                                from p in profileData.DefaultIfEmpty()
                                where (p.Joinedas == "1000" || p.Joinedas == "1100")
                                && !startupMembersEmails.Contains(_company.Email) && _program.ProgramId == programid
                                select new
                                {
                                    startupid = _company.Startupid,
                                    companyname = _company.Name,
                                    companyEmail = _company.Email,
                                    startupLogo = _company.startupLogo,
                                    companyDescription = _company.Description,
                                    startupScore = _company.startupScore ?? 0,
                                    tsbCoin = _company.tsbCoin,
                                    programGroupId = _program.ProgramGroupId
                                }).ToListAsync();

                var companyGoals = await (from _companyGoal in investeur_context.CompanyGoalsStep.AsNoTracking()
                                            join _goalStep in investeur_context.GoalsSteps.AsNoTracking()
                                            on _companyGoal.IdGoalStep equals _goalStep.IdGoalStep
                                            where _companyGoal.IsStepComplete == true
                                            select new CompanyGoalStep
                                            {
                                                id_goal_step = _companyGoal.IdGoalStep,
                                                id_goal = _goalStep.IdGoal,
                                                is_step_complete = _companyGoal.IsStepComplete,
                                                dateCreated = _companyGoal.DateCreated,
                                                startupId = _companyGoal.StartupId
                                            })
                                            .GroupBy(x => new { x.id_goal_step, x.startupId })
                                            .Select(g => g.First())
                                            .ToListAsync();

                var resultsList = companies.Select(company => new StartupCompany
                {
                    startupid = company.startupid,
                    companyname = company.companyname,
                    companyEmail = company.companyEmail,
                    startupLogo = company.startupLogo,
                    companyDescription = company.companyDescription,
                    dateCreated = companyGoals.FirstOrDefault(cg => cg.startupId == company.startupid)?.dateCreated,
                    Score = company.startupScore,
                    tsbCoin = company.tsbCoin,
                    ProgramGroupId = company.programGroupId
                }).ToList();

                return resultsList;
            }      
        }
        public async Task<Startup> GetStartupByUserIdAsync(string? userid)
        {
            return await GetByCondition(startup => startup.Userid == userid).FirstOrDefaultAsync();
        }

        public void CreateStartup(Startup startup)
        {
            Create(startup);
        }
        public void UpdateStartup(Startup startup)
        {
            Update(startup);
        }
        public void DeleteStartup(Startup startup)
        {
            Delete(startup);
        }

        public async Task<IEnumerable<Startup>> GetTotalLeaderBoard()
        {
            var query = await (from _query in investeur_context.Startups.AsNoTracking()
                               select new Startup
                               {
                                   Startupid = _query.Startupid,
                               }).ToListAsync();
            return query;
        }
        public async Task<IEnumerable<Startup>> GetStartups(int? programid)
        {
            if(programid == null || programid == 0){

                var query = await (from _startup in investeur_context.Startups.AsNoTracking()
                               select new Startup
                               {
                                   Startupid = _startup.Startupid,
                                   Userid = _startup.Userid,
                               }).ToListAsync();
                return query;
            }else{

                var query = await (from _startup in investeur_context.Startups.AsNoTracking()
                            join _program in investeur_context.StartupPrograms.AsNoTracking()
                            on _startup.Startupid equals _program.StartupId
                            where _program.ProgramId == programid
                            select new Startup
                            {
                                Startupid = _startup.Startupid,
                                Userid = _startup.Userid,
                            }).ToListAsync();
                return query;
            }          
        }

        #region 
        public async Task<IEnumerable<Startup>> GetCompanies(int? programid)
        {
            if(programid == null || programid == 0){
                var startups = await (
                from startup in investeur_context.Startups.AsNoTracking()
                join userData in investeur_context.UsersData.AsNoTracking()
                    on startup.Userid equals userData.UserId into startupData
                from s in startupData.DefaultIfEmpty()
                join userProfile in investeur_context.UserProfiles.AsNoTracking()
                    on s.UserId equals userProfile.Userid into profileData
                from p in profileData.DefaultIfEmpty()
                where !investeur_context.StartupMembers.Any(sm => sm.Email == startup.Email) 
                    && (p.Joinedas == "1000" || p.Joinedas == "1100")
                select new Startup
                {
                    Startupid = startup.Startupid,
                    Userid = startup.Userid,
                    Email = startup.Email
                }
            ).ToListAsync();

            return startups;
            }else{
                var startups = await (
                from startup in investeur_context.Startups.AsNoTracking()
                join _program in investeur_context.StartupPrograms.AsNoTracking()
                    on startup.Startupid equals _program.StartupId into StartupProgramData
                from _program in StartupProgramData.DefaultIfEmpty()
                join userData in investeur_context.UsersData.AsNoTracking()
                    on startup.Userid equals userData.UserId into startupData
                from s in startupData.DefaultIfEmpty()
                join userProfile in investeur_context.UserProfiles.AsNoTracking()
                    on s.UserId equals userProfile.Userid into profileData
                from p in profileData.DefaultIfEmpty()
                where !investeur_context.StartupMembers.Any(sm => sm.Email == startup.Email) 
                    && (p.Joinedas == "1000" || p.Joinedas == "1100") && _program.ProgramId == programid
                select new Startup
                {
                    Startupid = startup.Startupid,
                    Userid = startup.Userid,
                    Email = startup.Email
                }
            ).ToListAsync();

            return startups;
            }          
        }
        #endregion
        public async Task<int> GetCurrentLevelEachCategory(int startupid, int id_goal_category)
        {

            int[] code_level = new int[] { 2, 3, 4 };
            int currentLevel = 1;

            for (var i = 0; i < code_level.Count(); i++)
            {

                var query = await (from goals in investeur_context.Goals.AsNoTracking()
                                   join level in investeur_context.Levels.AsNoTracking()
                                   on goals.IdLevel equals level.IdLevel
                                   where level.IdGoalsCategory == id_goal_category && level.CodeLevel == code_level[i]
                                   select new CurrentStartupLevelPerCategory
                                   {
                                       id_level = goals.IdLevel,
                                       id_goal_category = level.IdGoalsCategory,
                                       code_level = level.CodeLevel
                                   }
                         ).ToListAsync();

                var goalsTaken = await (from company_goals in investeur_context.CompanyGoals.AsNoTracking()
                                        join goals in investeur_context.Goals.AsNoTracking()
                                        on company_goals.IdGoal equals goals.IdGoal
                                        join level in investeur_context.Levels.AsNoTracking()
                                        on goals.IdLevel equals level.IdLevel
                                        where company_goals.StartupId == startupid && level.IdGoalsCategory == id_goal_category && level.CodeLevel == code_level[i]
                                        select new
                                        {
                                            id_level = goals.IdLevel,
                                            id_goal_category = level.IdGoalsCategory,
                                            code_level = level.CodeLevel
                                        }
                                    ).ToListAsync();


                if (query.Count > 0 && goalsTaken.Count > 0 && query.Count == goalsTaken.Count && code_level[i] == 2)
                {
                    currentLevel = 2;
                }

                if (query.Count > 0 && goalsTaken.Count > 0 && query.Count == goalsTaken.Count && code_level[i] == 3)
                {
                    currentLevel = 3;
                }

                if (query.Count > 0 && goalsTaken.Count > 0 && query.Count == goalsTaken.Count && code_level[i] == 4)
                {
                    currentLevel = 4;
                }

            }

            return currentLevel;

        }
        public async Task<Startup> GetDataByStartupId(int? startupid)
        {
            return await GetByCondition(data => data.Startupid == startupid).FirstOrDefaultAsync();
        }
        public async Task<Startup> GetStartupIdByEmail(string? email)
        {
            return await GetByCondition(startup => startup.Email == email).FirstOrDefaultAsync();
        }
        public async Task<StartupInfo> GetStartupInfo(int? startupid)
        {
            var members = await (from _member in investeur_context.StartupMembers.AsNoTracking()
                                join _user in investeur_context.UsersData.AsNoTracking()
                                on _member.Email equals _user.Email
                                join _userProfile in investeur_context.UserProfiles.AsNoTracking()
                                on _user.UserId equals _userProfile.Userid
                                where _member.Startupid == startupid && _member.Status == 1
                                select new MembersOfStartup
                                {
                                    Startupid = _member.Startupid,
                                    Email = _member.Email,
                                    Name = _userProfile.Name,
                                    Status = _member.Status,
                                    Type = _member.Type,
                                    UserId = _user.UserId,
                                    Photo = _user.Photo,
                                }).ToListAsync();
            var query = await (from _startup in investeur_context.Startups.AsNoTracking()
                            join _user in investeur_context.UsersData.AsNoTracking()
                            on _startup.Userid equals _user.UserId
                            join _userProfile in investeur_context.UserProfiles.AsNoTracking()
                            on _user.UserId equals _userProfile.Userid
                            where _startup.Startupid == startupid
                            select new StartupInfo
                            {
                                startupid = _startup.Startupid,
                                companyname = _startup.Userid,
                                companyEmail = _startup.Email,
                                companyDescription = _startup.Description,
                                tsbCoin = _startup.tsbCoin,
                                website = _startup.Website,
                                priceConnect = _startup.priceConnect,
                                startupLogo = _startup.startupLogo,
                                joinDate = _user.JoinDate,
                                country = _user.Country,
                                ownerPhoto = _user.Photo,
                                onBoardingStatus = _user.UserOnBoardingStatus,
                                ownerName = _userProfile.Name,
                                linkedin = _userProfile.Linkedin,
                                members = members
                            }).FirstOrDefaultAsync();
                            
            return query; 
        }

        #region get ranking for startup by startupid
        public async Task<int> GetStartupRanking(int startupId, int? StartupProgramId)
        {
            var query = from startup in investeur_context.Startups
                        join user in investeur_context.UsersData
                        on startup.Userid equals user.UserId
                        join _userProfile in investeur_context.UserProfiles
                        on user.UserId equals _userProfile.Userid
                        join _startupMember in investeur_context.StartupMembers
                        on startup.Email equals _startupMember.Email into sm
                        from _startupMember in sm.DefaultIfEmpty()
                        where (_userProfile.Joinedas == "1000" || _userProfile.Joinedas == "1100")
                        && _startupMember == null
                        orderby startup.startupScore descending, startup.tsbCoin descending
                        select new StartupCompany
                        {
                            startupid = startup.Startupid,
                            companyname = startup.Name,
                            companyEmail = startup.Email,
                            startupLogo = startup.startupLogo,
                            Score = startup.startupScore,
                            tsbCoin = startup.tsbCoin
                        };

            if (StartupProgramId != null && StartupProgramId != 0)
            {
                query = query.Join(investeur_context.StartupPrograms.AsNoTracking(),
                                _company => _company.startupid,
                                _program => _program.StartupId,
                                (_company, _program) => new { _company, _program })
                            .Where(x => x._program.ProgramId == StartupProgramId)
                            .Select(x => x._company);
            }

            var startups = await query.ToListAsync();

            // Find the ranking of the startup with the given startup
            var ranking = startups.FindIndex(s => s.startupid == startupId) + 1;

            return ranking;
        }
        #endregion

        #region get all users (startup, investor and mentor)
        public async Task<IEnumerable<AllUsers>> GetAllUsers(int? programid)
        {
            if(programid == null || programid == 0){
                var startups = await (
                from startup in investeur_context.Startups.AsNoTracking()
                join userData in investeur_context.UsersData.AsNoTracking()
                    on startup.Userid equals userData.UserId into startupData
                from s in startupData.DefaultIfEmpty()
                join userProfile in investeur_context.UserProfiles.AsNoTracking()
                    on s.UserId equals userProfile.Userid into profileData
                from p in profileData.DefaultIfEmpty()
                select new AllUsers
                {
                    startupid = startup.Startupid,
                    userid = startup.Userid,
                    email = startup.Email,
                    JoinedAs = p.Joinedas
                }
            ).ToListAsync();

            return startups;
            }else{
                var startups = await (
                from startup in investeur_context.Startups.AsNoTracking()
                join _program in investeur_context.StartupPrograms.AsNoTracking()
                    on startup.Startupid equals _program.StartupId into StartupProgramData
                from _program in StartupProgramData.DefaultIfEmpty()
                join userData in investeur_context.UsersData.AsNoTracking()
                    on startup.Userid equals userData.UserId into startupData
                from s in startupData.DefaultIfEmpty()
                join userProfile in investeur_context.UserProfiles.AsNoTracking()
                    on s.UserId equals userProfile.Userid into profileData
                from p in profileData.DefaultIfEmpty()
                where _program.ProgramId == programid
                select new AllUsers
                {
                    startupid = startup.Startupid,
                    userid = startup.Userid,
                    email = startup.Email,
                    JoinedAs = p.Joinedas
                }
            ).ToListAsync();

            return startups;
            }          
        }
        #endregion
    }
}
