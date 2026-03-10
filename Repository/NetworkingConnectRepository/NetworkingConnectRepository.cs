using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class NetworkingConnectRepository : RepositoryBase<NetworkingConnect>, INetworkingConnectRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public NetworkingConnectRepository(InvesteurContext context) : base(context)
        {
        }

        public async Task<NetworkingConnect> GetDataByUserId(int? senderId, int? startupId)
        {
            return await GetByCondition(data => data.SenderId == senderId && data.ReceiverId == startupId).FirstOrDefaultAsync();
        }
        public async Task<NetworkingConnect> GetDataConnect(int? senderId, int? startupId)
        {
            return await GetByCondition(data => data.SenderId == startupId && data.ReceiverId == senderId).FirstOrDefaultAsync();
        }

        #region get all  by program
        public async Task<IEnumerable<NetworkingConnect>> GetAllConnectionsByProgram(int? programid)
        {
            if(programid == null || programid == 0)
            {
                var query = await (from _network in investeur_context.NetworkingConnect.AsNoTracking()
                            where _network.ConnectionStatus == true
                         select new NetworkingConnect
                         {
                             Id = _network.Id,
                             SenderId = _network.SenderId,
                             RequestDate = _network.RequestDate,
                             ConnectionStatus = _network.ConnectionStatus
                         }).ToListAsync();
                return query; 
            } 
            else
            {
                var query = await (from _network in investeur_context.NetworkingConnect.AsNoTracking()
                            join _program in investeur_context.StartupPrograms.AsNoTracking()
                            on _network.SenderId equals _program.StartupId
                            where _network.ConnectionStatus == true && _program.ProgramId == programid
                         select new NetworkingConnect
                         {
                             Id = _network.Id,
                             SenderId = _network.SenderId,
                             RequestDate = _network.RequestDate,
                             ConnectionStatus = _network.ConnectionStatus
                         }).ToListAsync();
                return query; 
            }
            
        }
        #endregion

        #region get all connection by startup
        public async Task<IEnumerable<NetworkingConnect>> GetConnections(int? startupid)
        {
            var query = await (from _network in investeur_context.NetworkingConnect.AsNoTracking()
                where _network.SenderId == startupid && _network.ConnectionStatus == true
                         select new NetworkingConnect
                         {
                             Id = _network.Id,
                             SenderId = _network.SenderId,
                             RequestDate = _network.RequestDate,
                             ConnectionStatus = _network.ConnectionStatus
                         }).ToListAsync();
            return query; 
        }
        #endregion
    }
}
