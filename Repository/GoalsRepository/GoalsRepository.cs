using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TheStartupBuddyV3.Repository
{
    public class GoalsRepository : RepositoryBase<Goal>, IGoalsRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public GoalsRepository(InvesteurContext context) : base(context)
        {
        }

        #region get all goals
        public async Task<IEnumerable<GetAllGoalsWithSteps>> GetAllGoals()
        {
            List<GetAllGoalsWithSteps> result2 = new List<GetAllGoalsWithSteps>();
            
            var query = await (from _goal in investeur_context.Goals.AsNoTracking()
                         join _level in investeur_context.Levels.AsNoTracking()
                         on _goal.IdLevel equals _level.IdLevel
                         join _category in investeur_context.GoalsCategories.AsNoTracking()
                         on _level.IdGoalsCategory equals _category.IdGoalsCategory
                         select new GetAllGoalsWithSteps
                         {
                             id = _goal.IdGoal,
                             logo = _goal.Logo,
                             label = _goal.Label,
                             level = _goal.IdLevel,
                             title = _goal.TitleGoal,
                             required = _goal.IsRequired,
                             is_locked = _goal.LockedStatus,
                             description = _goal.Description,
                             second_title = _goal.SecondTitle,
                             total_points = _goal.TotalPoints,
                             is_goal_complete = _goal.StatusComplete,
                             goal_category_logo = _level.LogoLevel,
                             id_category = _level.IdGoalsCategory,
                             code_level = _level.CodeLevel,
                             programid = _goal.ProgramId,
                             is_published = _goal.IsPublished,
                             program_group_id = _goal.ProgramGroupId
                         }).ToListAsync();
            foreach(var item in query)
            {
                var steps = await (from _goalStep in investeur_context.GoalsSteps.AsNoTracking()
                             join _steps in investeur_context.Steps.AsNoTracking()
                             on _goalStep.IdStep equals _steps.IdStep
                             where _goalStep.IdGoal == item.id
                             select new Steps
                             {
                                 step = _goalStep.IdStep,
                                 step_title = _steps.TitleStep,
                                 is_step_complete = false,
                             }).ToListAsync();

                var results = new GetAllGoalsWithSteps();

                results.id = item.id;
                results.logo = item.logo;
                results.label = item.label;
                results.level = item.level;
                results.steps = steps;
                results.title = item.title;
                results.required = item.required;
                results.is_locked = item.is_locked;
                results.description = item.description;
                results.second_title = item.second_title;
                results.total_points = item.total_points;
                results.is_goal_complete = item.is_goal_complete;
                results.goal_category_logo = item.goal_category_logo;
                results.id_category = item.id_category;
                results.code_level = item.code_level;
                results.programid = item.programid;
                results.is_published = item.is_published;
                results.program_group_id = item.program_group_id;
                result2.Add(results);
            }
            
            return result2;
        }
        #endregion

        #region get complete goals
        public async Task<IEnumerable<CompanyGoalStep>> GetCompleteGoals(string? startupid)
        {
            var query = await (from _companyGoal in investeur_context.CompanyGoalsStep.AsNoTracking()
                         join _goalStep in investeur_context.GoalsSteps.AsNoTracking()
                         on _companyGoal.IdGoalStep equals _goalStep.IdGoalStep
                         where _companyGoal.StartupId.ToString() == startupid
                         select new CompanyGoalStep
                         {
                             id_goal_step = _companyGoal.IdGoalStep,
                             id_goal = _goalStep.IdGoal,
                             is_step_complete = _companyGoal.IsStepComplete
                         }).ToListAsync();

            return query;
        }
        #endregion

        #region get goals by id
        public async Task<IEnumerable<GetAllGoals>> GetGoalsById(int? idGoal)
        {
            var query = await (from _goal in investeur_context.Goals.AsNoTracking()
                         join _level in investeur_context.Levels.AsNoTracking()
                         on _goal.IdLevel equals _level.IdLevel
                         join _category in investeur_context.GoalsCategories.AsNoTracking()
                         on _level.IdGoalsCategory equals _category.IdGoalsCategory
                         where _goal.IdGoal == idGoal
                         select new GetAllGoals
                         {
                             id = _goal.IdGoal,
                             logo = _goal.Logo,
                             label = _goal.Label,
                             level = _goal.IdLevel,
                             title = _goal.TitleGoal,
                             required = _goal.IsRequired,
                             is_locked = _goal.LockedStatus,
                             description = _goal.Description,
                             second_title = _goal.SecondTitle,
                             total_points = _goal.TotalPoints,
                             is_goal_complete = _goal.StatusComplete,
                             goal_category_logo = _category.LogoCategory,
                             id_category = _level.IdGoalsCategory,
                         }).ToListAsync();

            return query;
        }
        #endregion

        public async Task<Goal> GetGoalsByStatusProgram(int programId)
        {
            return await GetByCondition(goal => goal.StatusOnboarding == true && goal.ProgramId == programId).SingleOrDefaultAsync();
        }
    }
}
