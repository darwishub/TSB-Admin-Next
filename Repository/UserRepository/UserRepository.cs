using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public UserRepository(InvesteurContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await GetAll()
               .ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string? email)
        {
            return await GetByCondition(user => user.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByIdAsync(string? userId)
        {
            return await GetByCondition(user => user.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmailAndTokenAsync(string? email, string? refreshToken)
        {
            return await GetByCondition(user => user.Email == email && user.RefreshToken == refreshToken && user.RefreshToken != null).FirstOrDefaultAsync();
        }

        // private IQueryable<NetworkProfile> GetNetworkProfilesQuery(IQueryable<NetworkingConnect> networks, int? programid)
        // {
        //     return from _startup in investeur_context.Startups.AsNoTracking()
        //            join _userProfile in investeur_context.UserProfiles.AsNoTracking()
        //            on _startup.Userid equals _userProfile.Userid into jointable
        //            from z in jointable.DefaultIfEmpty()
        //            join _networking in networks
        //            on _startup.Startupid equals _networking.SenderId into stable
        //            from sst in stable.DefaultIfEmpty()
        //            join _program in investeur_context.StartupPrograms.AsNoTracking()
        //            on _startup.Startupid equals _program.StartupId into tableProgram
        //            from _tbprogram in tableProgram.DefaultIfEmpty()
        //            where _tbprogram.ProgramId == programid && _startup.Email != "" && sst.ConnectionStatus == true
        //            select new NetworkProfile
        //            {
        //                StartupId = _startup.Startupid,
        //                Name = _startup.Name,
        //                PhotoProfile = _startup.startupLogo,
        //                Description = z.Description,
        //                JoinedAs = z.Joinedas,
        //                ConnectionStatus = sst.ConnectionStatus,
        //                PriceConnect = _startup.priceConnect,
        //                EmailAccount = _startup.Email,
        //                LinkedinAccount = z.Linkedin,
        //                RequestDate = sst.RequestDate,
        //                RequestBy = sst.RequestBy,
        //                AcceptDate = sst.AccepDate
        //            };
        // }

        public async Task<List<UserProfile>> GetStartupsRecommendation(int startupId)
        {
            var networks = await (from _network in investeur_context.NetworkingConnect
                                  where _network.SenderId == startupId && _network.ConnectionStatus == true ||
                                  _network.ReceiverId == startupId && _network.ConnectionStatus == true
                                  select _network).ToListAsync();

            List<int> startups = new List<int>();

            foreach (var network in networks)
            {
                if (network.SenderId == startupId)
                {
                    startups.Add(network.ReceiverId);
                }
                if (network.ReceiverId == startupId)
                {
                    startups.Add(network.SenderId);
                }
            }

            List<UserProfile> profiles = new List<UserProfile>();

            if (startups.Count > 0)
            {
                foreach (var startup in startups)
                {
                    var profile = await (from _startup in investeur_context.Startups.AsNoTracking()
                                         join _userProfile in investeur_context.UserProfiles.AsNoTracking()
                                         on _startup.Userid equals _userProfile.Userid
                                         where _startup.Startupid == startup
                                         select new UserProfile
                                         {
                                             Profileid = _userProfile.Profileid,
                                             Name = _userProfile.Name,
                                             Description = _userProfile.Description,
                                             Photo = _userProfile.Photo,
                                             Communication = _userProfile.Communication,
                                             Remarks = _userProfile.Remarks,
                                             Userid = _userProfile.Userid,
                                             Status = _userProfile.Status,
                                             Title = _userProfile.Title,
                                             Linkedin = _userProfile.Linkedin,
                                             Joinedas = _userProfile.Joinedas,
                                             Mentoringstatus = _userProfile.Mentoringstatus,
                                             Investorstatus = _userProfile.Investorstatus,
                                             Ispaidmentor = _userProfile.Ispaidmentor,
                                             Expertise = _userProfile.Expertise,
                                             Mentorprice = _userProfile.Mentorprice,
                                             Createdat = _userProfile.Createdat,
                                             BenefitTicket = _userProfile.BenefitTicket,
                                             LastInvoice = _userProfile.LastInvoice
                                         }).FirstOrDefaultAsync();

                    if (profile != null)
                    {
                        profiles.Add(profile);
                    }
                }
            }

            return profiles;
        }

        public void CreateUser(User user)
        {
            Create(user);
        }
        public void UpdateUser(User user)
        {
            Update(user);
        }
        public void DeleteUser(User user)
        {
            Delete(user);
        }

    }
}
