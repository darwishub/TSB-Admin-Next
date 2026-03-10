using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IStartupProgramRepository : IRepositoryBase<StartupProgram>
    {
        Task<IEnumerable<StartupProgram>> GetAllStartupsJoinProgramAsync();
        Task<StartupProgram> GetStartupJoinProgramByStartupIdAsync(int? startupid);
        void CreateStartupJoinProgram(StartupProgram startup_program);
        void UpdateStartupJoinProgram(StartupProgram startup_program);
        void DeleteStartupJoinProgram(StartupProgram startup_program);
        Task<ProgramStatus> GetStartupProgram(int startupid);
    }
}
