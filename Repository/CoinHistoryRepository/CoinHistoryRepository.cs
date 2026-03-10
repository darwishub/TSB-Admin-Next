using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class CoinHistoryRepository : RepositoryBase<CoinHistory>, ICoinHistoryRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public CoinHistoryRepository(InvesteurContext context) : base(context)
        {
            
        }
        public async Task<IEnumerable<CoinHistory>> GetAllHistoryByStartup(int? startupId)
        {
            if (startupId == null)
            {
                return new List<CoinHistory>();
            }

            var _coinsHistory = await investeur_context.CoinHistory
                .AsNoTracking()
                .Where(_coinHistory => _coinHistory.StartupId == startupId)
                .ToListAsync();

            return _coinsHistory;
        }    
    }
}
