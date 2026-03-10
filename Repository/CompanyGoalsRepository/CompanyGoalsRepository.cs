using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class CompanyGoalsRepository : RepositoryBase<CompanyGoals>, ICompanyGoalsRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public CompanyGoalsRepository(InvesteurContext context) : base(context)
        {
        }

        public Task<IEnumerable<Company>> GetAllCompanyAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CompanyGoalsStep>> GetDataByStartupId(int? startupid)
        {
            var query = await (from _company in investeur_context.CompanyGoalsStep.AsNoTracking()
                         join _goalStep in investeur_context.GoalsSteps.AsNoTracking()
                         on _company.IdGoalStep equals _goalStep.IdGoalStep
                         where _company.StartupId == startupid
                         select new CompanyGoalsStep
                         {
                             IdGoalStep = _company.IdGoalStep,
                         }).ToListAsync();
            return query;
        }

        public async Task<IEnumerable<CompanyGoalsStep>> GetAllCompanySteps(int? programid)
        {
            if(programid == null || programid == 0)
            {
                var query = await (from _company in investeur_context.CompanyGoalsStep.AsNoTracking()
                        where _company.IsStepComplete == true
                        select new CompanyGoalsStep
                        {
                            IdCompanyGoalsStep = _company.IdCompanyGoalsStep,
                            IdGoalStep = _company.IdGoalStep,
                            StartupId = _company.StartupId,
                            IdUser = _company.IdUser,
                            IsStepComplete = _company.IsStepComplete,
                            IsGoalComplete = _company.IsGoalComplete,
                            TSBCoin = _company.TSBCoin,
                            DateCreated = _company.DateCreated,
                        }).ToListAsync();
                return query;
            } 
            else
            {
                var query = await (from _company in investeur_context.CompanyGoalsStep.AsNoTracking()
                            join _program in investeur_context.StartupPrograms.AsNoTracking()
                            on _company.StartupId equals _program.StartupId
                            where _program.ProgramId == programid && _company.IsStepComplete == true
                            group _company by _company.IdCompanyGoalsStep into g
                        select new CompanyGoalsStep
                        {
                        IdCompanyGoalsStep = g.Key,
                        IdGoalStep = g.First().IdGoalStep,
                        StartupId = g.First().StartupId,
                        IdUser = g.First().IdUser,
                        IsStepComplete = g.First().IsStepComplete,
                        IsGoalComplete = g.First().IsGoalComplete,
                        TSBCoin = g.First().TSBCoin,
                        DateCreated = g.First().DateCreated,
                        }).ToListAsync();
                return query;
            }
            
        }

        #region get all company coins for each day by all startups
        public async Task<IEnumerable<CompanyGoalsWithCoins>> GetAllCoinsByProgram(int? programid)
        {
            if(programid == null || programid == 0)
            {
                var query = await (from _company in investeur_context.CompanyGoals.AsNoTracking()
                        join _goal in investeur_context.Goals.AsNoTracking()
                        on _company.IdGoal equals _goal.IdGoal
                        where _company.IsRewardCollected == true
                         select new CompanyGoalsWithCoins
                         {
                             id_company_goals = _company.IdCompanyGoals,
                             id_goal = _company.IdGoal,
                             startupid = _company.StartupId,
                             is_reward_collected = _company.IsRewardCollected,
                             date_created = _company.DateCreated,
                             coins = _goal.TotalPoints,
                         }).ToListAsync();
                return query; 
            } 
            else 
            {
                var query = await (from _company in investeur_context.CompanyGoals.AsNoTracking()
                        join _goal in investeur_context.Goals.AsNoTracking()
                        on _company.IdGoal equals _goal.IdGoal
                        join _program in investeur_context.StartupPrograms.AsNoTracking()
                        on _company.StartupId equals _program.StartupId
                        where _company.IsRewardCollected == true && _program.ProgramId == programid
                         select new CompanyGoalsWithCoins
                         {
                             id_company_goals = _company.IdCompanyGoals,
                             id_goal = _company.IdGoal,
                             startupid = _company.StartupId,
                             is_reward_collected = _company.IsRewardCollected,
                             date_created = _company.DateCreated,
                             coins = _goal.TotalPoints,
                         }).ToListAsync();
                return query; 
            }
            
        }
        #endregion

        #region get all company coins for each day by startup
        public async Task<IEnumerable<CompanyGoalsWithCoins>> GetCoins(int? startupid)
        {
            var query = await (from _company in investeur_context.CompanyGoals.AsNoTracking()
                join _goal in investeur_context.Goals.AsNoTracking()
                on _company.IdGoal equals _goal.IdGoal
                where _company.StartupId == startupid && _company.IsRewardCollected == true
                         select new CompanyGoalsWithCoins
                         {
                             id_company_goals = _company.IdCompanyGoals,
                             id_goal = _company.IdGoal,
                             startupid = _company.StartupId,
                             is_reward_collected = _company.IsRewardCollected,
                             date_created = _company.DateCreated,
                             coins = _goal.TotalPoints,
                         }).ToListAsync();
            return query; 
        }
        #endregion

        #region get company steps by program and startup
        public async Task<IEnumerable<CompanyGoalsStep>> GetCompanySteps(int? programid, int? startupid)
        {
            if(programid == null || programid == 0)
            {
                var query = await (from _company in investeur_context.CompanyGoalsStep.AsNoTracking()
                                    where _company.StartupId == startupid && _company.IsStepComplete == true
                         select new CompanyGoalsStep
                         {
                             IdCompanyGoalsStep = _company.IdCompanyGoalsStep,
                             IdGoalStep = _company.IdGoalStep,
                             StartupId = _company.StartupId,
                             IdUser = _company.IdUser,
                             IsStepComplete = _company.IsStepComplete,
                             IsGoalComplete = _company.IsGoalComplete,
                             TSBCoin = _company.TSBCoin,
                             DateCreated = _company.DateCreated,
                         }).ToListAsync();
                return query;
            } 
            else
            {
                var query = await (from _company in investeur_context.CompanyGoalsStep.AsNoTracking()
                   join _program in investeur_context.StartupPrograms.AsNoTracking()
                   on _company.StartupId equals startupid
                   where _program.ProgramId == programid && _company.StartupId == startupid
                   && _company.IsStepComplete == true
                   group _company by _company.IdCompanyGoalsStep into g
                   select new CompanyGoalsStep 
                   {
                       IdCompanyGoalsStep = g.Key,
                       IdGoalStep = g.First().IdGoalStep,
                       StartupId = g.First().StartupId,
                       IdUser = g.First().IdUser,
                       IsStepComplete = g.First().IsStepComplete,
                       IsGoalComplete = g.First().IsGoalComplete,
                       TSBCoin = g.First().TSBCoin,
                       DateCreated = g.First().DateCreated,
                   }).ToListAsync();
                return query;
            }
            
        }
        #endregion
    }
}
