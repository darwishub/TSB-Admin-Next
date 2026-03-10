using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class StartupProgramRepository : RepositoryBase<StartupProgram>, IStartupProgramRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public StartupProgramRepository(InvesteurContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<StartupProgram>> GetAllStartupsJoinProgramAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<StartupProgram> GetStartupJoinProgramByStartupIdAsync(int? startupid)
        {
            return await GetByCondition(startup => startup.StartupId == startupid).FirstOrDefaultAsync();
        }

        public void CreateStartupJoinProgram(StartupProgram startup_program)
        {
            Create(startup_program);
        }
        public void UpdateStartupJoinProgram(StartupProgram startup_program)
        {
            Update(startup_program);
        }
        public void DeleteStartupJoinProgram(StartupProgram startup_program)
        {
            Delete(startup_program);
        }
        public async Task<ProgramStatus> GetStartupProgram(int startupid)
        {
            var query = (from _query in investeur_context.StartupPrograms.AsNoTracking()
                        join _program in investeur_context.ProgramCodes.AsNoTracking()
                        on _query.ProgramId equals _program.Programid
                        where _query.StartupId == startupid
                        select new ProgramStatus
                        {
                            ProgramId = _query.ProgramId,
                            StatusGoal = _program.StatusGoal,
                            ProgramName = _program.Name,
                            ProgramGroupId = _query.ProgramGroupId,
                        }).FirstOrDefault();
            return query;
        }
    }
}
