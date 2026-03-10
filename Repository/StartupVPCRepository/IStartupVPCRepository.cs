using TheStartupBuddyV3.Models;
namespace TheStartupBuddyV3.Repository
{
    public interface IStartupVPCRepository : IRepositoryBase<StartupVpc>
    {
        Task<StartupVpc> GetVPCByStartupIdAsync(int startupid);
        void CreateVPC(StartupVpc VPC);
        void UpdateVPC(StartupVpc VPC);
        void DeleteVPC(StartupVpc VPC);
    }
}
