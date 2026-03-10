using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IStartupRepository : IRepositoryBase<Startup>
    {
        Task<IEnumerable<StartupCompany>> GetAllStartupsAsync(int? programid);
        Task<Startup> GetStartupByUserIdAsync(string? userid);
        Task<Startup> GetStartupIdByEmail(string? email);
        void CreateStartup(Startup startup);
        void UpdateStartup(Startup startup);
        void DeleteStartup(Startup startup);
        Task<IEnumerable<Startup>> GetTotalLeaderBoard();
        Task<IEnumerable<Startup>> GetStartups(int? programid);
        Task<IEnumerable<Startup>> GetCompanies(int? programid);
        Task<int> GetCurrentLevelEachCategory(int startupid, int id_goal_category);
        Task<Startup> GetDataByStartupId(int? startupid);
        Task<StartupInfo> GetStartupInfo(int? startupid);
        Task<int> GetStartupRanking(int startupId, int? StartupProgramId);
        Task<IEnumerable<AllUsers>> GetAllUsers(int? programid);
    }
}
