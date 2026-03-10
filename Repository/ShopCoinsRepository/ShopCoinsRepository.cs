using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class ShopCoinsRepository : RepositoryBase<ShopCoins>, IShopCoinsRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public ShopCoinsRepository(InvesteurContext _context) : base(_context)
        {
        }
        public async Task<IEnumerable<ShopCoins>> getAllShopCoins()
        {
            var query = await (from _shop in investeur_context.ShopCoins.AsNoTracking()
                               select _shop).ToListAsync();
            return query;
        }

        public async Task<ShopCoins> GetItemById(int id)
        {
            var query = await (from _shop in investeur_context.ShopCoins.AsNoTracking()
                               where _shop.Id == id
                               select _shop).FirstOrDefaultAsync();
            return query;
        }

    }
}
