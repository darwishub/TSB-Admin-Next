using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class GoalRepository : RepositoryBase<Goal>, IGoalRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public GoalRepository(InvesteurContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Goal>> GetAllGoalAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<Goal> GetGoalByIdAsync(int goalId)
        {
            return await GetByCondition(goal => goal.IdGoal.Equals(goalId)).FirstOrDefaultAsync();
        }

        public async Task<Goal> GetSingleGoalByPromoGoalStatus(bool status)
        {
            return await GetByCondition(goal => goal.StatusPromo == true).SingleOrDefaultAsync();
        }

        public async Task<Goal> GetGoalsByStatusProgram(int programId)
        {
            return await GetByCondition(goal => goal.StatusOnboarding == true && goal.ProgramId == programId).SingleOrDefaultAsync();
        }

        public void CreateGoal(Goal goal)
        {
            Create(goal);
        }
        public void UpdateGoal(Goal goal)
        {
            Update(goal);
        }
        public void DeleteGoal(Goal goal)
        {
            Delete(goal);
        }

        public async Task<Goal> GetGoalsById(int goalId)
        {
            var GoalsQuery = await (from goals in investeur_context.Goals.AsNoTracking()
                                    where goals.IdGoal == goalId
                                    select new Goal
                                    {

                                        IdGoal = goals.IdGoal,
                                        TitleGoal = goals.TitleGoal,
                                        IdLevel = goals.IdLevel,
                                        Logo = goals.Logo,
                                        TotalPoints = goals.TotalPoints,
                                        Description = goals.Description,
                                        SecondTitle = goals.SecondTitle,
                                        Label = goals.Label,
                                        StatusPromo = goals.StatusPromo,
                                        StatusOnboarding = goals.StatusOnboarding,
                                        StepsJsonUI = goals.StepsJsonUI,
                                        HelpText = goals.HelpText,
                                        IsPublished = goals.IsPublished,

                                    }).FirstOrDefaultAsync();
            return GoalsQuery;
        }

    }
}
