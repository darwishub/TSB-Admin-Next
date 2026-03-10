using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class ContentRepository : RepositoryBase<MetaDatum>, IContentRepository
    {
        public ContentRepository(InvesteurContext _context) : base(_context)
        {
        }

        public async Task<MetaDatum> GetContentByKeyAsync(string key_name, string content_type)
        {
            return await GetByCondition(content => content.MetaKey == key_name && content.Page == content_type).FirstOrDefaultAsync();
        }

    }
}
