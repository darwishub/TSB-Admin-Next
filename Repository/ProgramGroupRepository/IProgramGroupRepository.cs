using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IProgramGroupRepository : IRepositoryBase<ProgramGroup>
    {
        Task<List<ProgramGroup>> GetProgramGroupByProgramIdAsync(int programId);
        void CreateProgramGroup(ProgramGroup programGroup);
        void UpdateProgramGroup(ProgramGroup programGroup);
        void DeleteProgramGroup(ProgramGroup programGroup);
        Task<ProgramGroup> GetProgramGroupByGroupId(int? programGroupId);
    }
}
