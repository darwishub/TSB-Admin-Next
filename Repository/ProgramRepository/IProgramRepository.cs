using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IProgramRepository : IRepositoryBase<ProgramCode>
    {
        Task<IEnumerable<ProgramCode>> GetAllProgramsAsync();
        Task<ProgramCode> GetProgramByCodeAsync(string? code);
        Task<ProgramCode> GetProgramByProgramId(int? code);
        void CreateProgram(ProgramCode program);
        void UpdateProgram(ProgramCode program);
        void DeleteProgram(ProgramCode program);
    }
}
