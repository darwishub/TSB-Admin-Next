using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class StartupMemberRepository : RepositoryBase<StartupMember>, IStartupMemberRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public StartupMemberRepository(InvesteurContext context) : base(context)
        {
        }
        public async Task<StartupMember> GetMemberByEmail(string? email)
        {
            return await GetByCondition(startup => startup.Email == email).FirstOrDefaultAsync();
        }
        public async Task<StartupMember> GetStartupIdByEmail(string? email)
        {
            return await GetByCondition(startup => startup.Email == email && startup.Status == 1).FirstOrDefaultAsync();
        }

        public async Task<StartupMember> GetMemberByStartupIdEmail(int? startupid, string? email)
        {
            var query = (from _member in investeur_context.StartupMembers.AsNoTracking()
                         where _member.Startupid == startupid && _member.Email == email
                         select _member).ToList().FirstOrDefault();
            return query;
        }
        public async Task<IEnumerable<MembersOfStartup>> GetAllMembersOfStartup(int? startupid)
        {
            var query = await (from _member in investeur_context.StartupMembers.AsNoTracking()
                   join _startup in investeur_context.Startups.AsNoTracking()
                   on _member.Startupid equals _startup.Startupid
                   join _user in investeur_context.UsersData.AsNoTracking()
                   on _member.Email equals _user.Email into member_data_table
                   from member_data in member_data_table.DefaultIfEmpty()
                   join userProfile in investeur_context.UserProfiles.AsNoTracking()
                   on member_data.UserId equals userProfile.Userid into member_info
                   from members in member_info.DefaultIfEmpty()
                   where _member.Startupid == startupid
                   select new MembersOfStartup
                   {
                       Startupid = _startup.Startupid,
                       UserId = members.Userid,
                       Email = _member.Email,
                       Status = _member.Status,
                       Name = members.Name,
                       Photo = member_data.Photo,
                       isOwner = false
                   }).ToListAsync();

            var owner = await (from Startup in investeur_context.Startups.AsNoTracking()
                               join User in investeur_context.UsersData.AsNoTracking()
                               on Startup.Email equals User.Email
                               join UserProfile in investeur_context.UserProfiles.AsNoTracking()
                               on User.UserId equals UserProfile.Userid
                               where Startup.Startupid == startupid
                               select new MembersOfStartup
                               {
                                   Startupid = Startup.Startupid,
                                   UserId = UserProfile.Userid,
                                   Email = Startup.Email,
                                   Status = 1,
                                   Name = UserProfile.Name,
                                   Photo = User.Photo,
                                    isOwner = true
                               }).ToListAsync();

            query.AddRange(owner);

            return query;
        }

        #region get member join with user table
        public async Task<IEnumerable<UserMembers>> GetAllUserMembers(int? startupid)
        {
            var query = await (from _member in investeur_context.StartupMembers.AsNoTracking()
                               join _user in investeur_context.UsersData.AsNoTracking()
                               on _member.Email equals _user.Email
                               where _member.Startupid == startupid && _member.Status == 1
                               select new UserMembers
                               {
                                   Startupid = _member.Startupid,
                                   Email = _member.Email,
                                   Status = _member.Status,
                                   Name = _member.Name,
                                   UserId = _user.UserId
                               }).ToListAsync();
            return query;
        }
        #endregion

        #region get all company by email
        public async Task<IEnumerable<StartupMember>> GetAllCompanyByEmail(string? email)
        {
            var query = await (from _member in investeur_context.StartupMembers.AsNoTracking()
                               where _member.Email == email
                               select new StartupMember
                               {
                                   Startupid = _member.Startupid,
                                   Email = _member.Email,
                                   Status = _member.Status,
                                   Name = _member.Name
                               }).ToListAsync();
            return query;
        }
        #endregion
    }
}