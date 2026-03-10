using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using TheStartupBuddyV3.Models;
using TheStartupBuddyV3.Repository;

namespace TheStartupBuddyV3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CompanyController : Controller
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        private readonly IRepositoryWrapper _repository;
        public CompanyController(
        IRepositoryWrapper repository
        )
        {
            _repository = repository;
        }

        #region get all company data
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var getcompanies = await _repository.Company.GetAllCompanyAsync();

                string companiesResult = JsonConvert.SerializeObject(getcompanies, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Content(companiesResult);

            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion
    }
}
