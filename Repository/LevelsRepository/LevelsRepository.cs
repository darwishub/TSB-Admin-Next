using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class LevelsRepository : RepositoryBase<Level>, ILevelsRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public LevelsRepository(InvesteurContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Level>> GetLevels()
        {
            var levelLists = await (from _level in investeur_context.Levels.AsNoTracking()
                              select new Level
                              {
                                  IdLevel = _level.IdLevel,
                                  NameLevel = _level.NameLevel,
                                  IdGoalsCategory = _level.IdGoalsCategory,
                              }).ToListAsync();
            return levelLists;
        }

        public async Task<Level> GetLevelById(int? levelId)
        {
            return await GetByCondition(data => data.IdLevel == levelId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Level>> GetDataByStartupId(int? idGoalStep)
        {
            var query = await (from _goalStep in investeur_context.GoalsSteps.AsNoTracking()
                         join _goal in investeur_context.Goals.AsNoTracking()
                         on _goalStep.IdGoal equals _goal.IdGoal
                         join _level in investeur_context.Levels.AsNoTracking()
                         on _goal.IdLevel equals _level.IdLevel
                         where _goalStep.IdGoalStep == idGoalStep
                         select new Level
                         {
                             IdLevel = _level.IdLevel,
                             LogoLevel = _level.LogoLevel,
                             IdGoalsCategory = _level.IdGoalsCategory
                         }).ToListAsync();
                         
                         //into test
                         //group test by new Level
                         //{
                         //    IdLevel = test.IdLevel,
                         //    LogoLevel = test.LogoLevel,
                         //    IdGoalsCategory = test.IdGoalsCategory
                         //}).ToList();
            return query;
        }

        public async Task<IEnumerable<Level>> GetLevelsZero()
        {
            var levelLists = await (from _level in investeur_context.Levels.AsNoTracking()
                              where _level.IdLevel == 1 || _level.IdLevel == 5 || _level.IdLevel == 9 || _level.IdLevel == 13
                              select new Level
                              {
                                  IdLevel = _level.IdLevel,
                                  LogoLevel = _level.LogoLevel,
                              }).ToListAsync();
            return levelLists;
        }

        public async Task<IEnumerable<Level>> GetLevelsByCategory(int? idGoalStep)
        {
            var query = await (from _level in investeur_context.Levels.AsNoTracking()         
                         where _level.IdGoalsCategory != idGoalStep
                         select new Level
                         {
                             IdLevel = _level.IdLevel,
                             LogoLevel = _level.LogoLevel,
                             IdGoalsCategory = _level.IdGoalsCategory
                         }).ToListAsync();

            //into test
            //group test by new Level
            //{
            //    IdLevel = test.IdLevel,
            //    LogoLevel = test.LogoLevel,
            //    IdGoalsCategory = test.IdGoalsCategory
            //}).ToList();
            return query;
        }

        public async Task<Logos> GetLogoByLevel(int? id_goals_category, int? code_level)
        {
            var query = await (from _query in investeur_context.Levels.AsNoTracking()
                        where _query.IdGoalsCategory == id_goals_category && _query.CodeLevel == code_level
                        select new Logos
                        {
                            id_level = _query.IdLevel,
                            logo = _query.LogoLevel,
                            code_level = _query.CodeLevel,
                            id_category = _query.IdGoalsCategory
                        }).FirstOrDefaultAsync();
                        
            return query;
        }
    }
}
