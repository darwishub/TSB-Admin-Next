using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;
namespace TheStartupBuddyV3.Repository
{
    public class StartupVPCRepository : RepositoryBase<StartupVpc>, IStartupVPCRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public StartupVPCRepository(InvesteurContext context) : base(context)
        {
        }

        public async Task<StartupVpc> GetVPCByStartupIdAsync(int startupid)
        {
            return await GetByCondition(startup => startup.Startupid == startupid).FirstOrDefaultAsync();
        }
        public void CreateVPC(StartupVpc VPC)
        {
            Create(VPC);
        }
        public void UpdateVPC(StartupVpc VPC)
        {
            Update(VPC);
        }
        public void DeleteVPC(StartupVpc VPC)
        {
            Delete(VPC);
        }
    }
}
