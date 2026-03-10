using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IContentRepository : IRepositoryBase<MetaDatum>
    {
        Task<MetaDatum> GetContentByKeyAsync(string key_name, string content_type);
    }
}
