using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class GoalsTemplatesRepository : RepositoryBase<GoalsTemplates>, IGoalsTemplatesRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public GoalsTemplatesRepository(InvesteurContext context) : base(context)
        {
        }

        public async Task<IEnumerable<GoalsTemplates>> GetAllGoalsTemplates()
        {
            var query = await (from _query in investeur_context.GoalsTemplates.AsNoTracking()
                         select new GoalsTemplates
                         {
                             id_template = _query.id_template,
                             photo_template = _query.photo_template,
                             id_level = _query.id_level,
                             status = _query.status,
                         }).ToListAsync();
            return query;
        }
    }
}
