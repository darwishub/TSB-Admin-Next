using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class GoalsCategoryRepository : RepositoryBase<GoalsCategory>, IGoalsCategoryRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public GoalsCategoryRepository(InvesteurContext context) : base(context)
        {
        }

        public async Task<IEnumerable<GoalsCategory>> GetGoalsCategory()
        {
            var goalsCategory = await (from _category in investeur_context.GoalsCategories.AsNoTracking()
                                 select new GoalsCategory
                                 {
                                     IdGoalsCategory = _category.IdGoalsCategory,
                                     NameGoalsCategory = _category.NameGoalsCategory,
                                     LogoCategory = _category.LogoCategory,
                                 }).ToListAsync();
            return goalsCategory;
        }
    }
}
