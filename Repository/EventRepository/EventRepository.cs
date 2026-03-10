using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(InvesteurContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await GetAll()
               .ToListAsync();
        }

        public async Task<Event> GetItemById(int? Id)
        {
            return await GetByCondition(data => data.Id == Id).FirstOrDefaultAsync();
        }

        public void CreateEvent(Event Event)
        {
            Create(Event);
        }
        public void UpdateEvent(Event Event)
        {
            Update(Event);
        }
        public void DeleteEvent(Event Event)
        {
            Delete(Event);
        }

    }
}
