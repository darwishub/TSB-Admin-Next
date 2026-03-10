using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IShopCoinsRepository : IRepositoryBase<ShopCoins>
    {
        Task<IEnumerable<ShopCoins>> getAllShopCoins();
        Task<ShopCoins> GetItemById(int id);
    }
}
