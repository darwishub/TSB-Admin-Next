using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class ProgramRepository : RepositoryBase<ProgramCode>, IProgramRepository
    {
        public ProgramRepository(InvesteurContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<ProgramCode>> GetAllProgramsAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<ProgramCode> GetProgramByCodeAsync(string? code)
        {
            return await GetByCondition(data => data.Code == code && data.Enddate > DateTime.Now).FirstOrDefaultAsync();
        }
        public async Task<ProgramCode> GetProgramByProgramId(int? programId)
        {
            return await GetByCondition(data => data.Programid == programId).FirstOrDefaultAsync();
        }

        public void CreateProgram(ProgramCode program)
        {
            Create(program);
        }
        public void UpdateProgram(ProgramCode program)
        {
            Update(program);
        }
        public void DeleteProgram(ProgramCode program)
        {
            Delete(program);
        }

    }
}
