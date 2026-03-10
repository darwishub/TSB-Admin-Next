using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IStartupMemberRepository : IRepositoryBase<StartupMember>
    {
        Task<StartupMember> GetMemberByEmail(string? email);
        Task<StartupMember> GetStartupIdByEmail(string? email);
        Task<StartupMember> GetMemberByStartupIdEmail(int? startupid, string? email);
        Task<IEnumerable<MembersOfStartup>> GetAllMembersOfStartup(int? startupid);
        Task<IEnumerable<UserMembers>> GetAllUserMembers(int? startupid);
        Task<IEnumerable<StartupMember>> GetAllCompanyByEmail(string? email);
    }
}