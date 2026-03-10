using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class ProgramGroupRepository : RepositoryBase<ProgramGroup>, IProgramGroupRepository
    {
        public ProgramGroupRepository(InvesteurContext _context) : base(_context)
        {
        }

        public async Task<List<ProgramGroup>> GetProgramGroupByProgramIdAsync(int programId)
        {
            return await GetAll().Where(i => i.Programid == programId).ToListAsync();
        }

        public void CreateProgramGroup(ProgramGroup programGroup)
        {
            Create(programGroup);
        }
        public void UpdateProgramGroup(ProgramGroup programGroup)
        {
            Update(programGroup);
        }
        public void DeleteProgramGroup(ProgramGroup programGroup)
        {
            Delete(programGroup);
        }
        public async Task<ProgramGroup> GetProgramGroupByGroupId(int? programGroupId)
        {
            return await GetByCondition(startup => startup.ProgramGroupId == programGroupId).FirstOrDefaultAsync();
        }

    }
}
