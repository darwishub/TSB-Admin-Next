using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public CompanyRepository(InvesteurContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Company>> GetAllCompanyAsync()
        {
            var query = await (from _company in investeur_context.Companies.AsNoTracking()
                               select new Company
                               {
                                   Companyid = _company.Companyid,
                                   Userid = _company.Userid,
                                   Companyname = _company.Companyname,
                                   Website = _company.Website,
                                   Description = _company.Description,
                                   Contact = _company.Contact,
                                   Email = _company.Email,
                                   Logo = _company.Logo,
                               }).ToListAsync();
            return query;
        }
    }
}
