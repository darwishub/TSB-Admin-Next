using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface INetworkingConnectRepository : IRepositoryBase<NetworkingConnect>
    {
        Task<IEnumerable<NetworkingConnect>> GetAllConnectionsByProgram(int? programid);
        Task<NetworkingConnect> GetDataByUserId(int? investorId, int? startupId);
        Task<NetworkingConnect> GetDataConnect(int? investorId, int? startupId);
        Task<IEnumerable<NetworkingConnect>> GetConnections(int? startupid);
    }
}
