using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IEventRepository : IRepositoryBase<Event>
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetItemById(int? Id);
        void CreateEvent(Event Event);
        void UpdateEvent(Event Event);
        void DeleteEvent(Event Event);
    }
}
