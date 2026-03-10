using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using TheStartupBuddyV3.Models;
using TheStartupBuddyV3.Repository;
using TheStartupBuddyV3.Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace TheStartupBuddyV3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SessionRatingController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IRepositoryWrapper _repository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IEmailSender _emailSender;
        private readonly IServiceWrapper _service;
        private readonly IConfiguration _configuration;
        private InvesteurContext investeur_context = new InvesteurContext();
        public SessionRatingController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IRepositoryWrapper repository,
        IEmailSender emailSender,
        IServiceWrapper service,
        IHttpContextAccessor accessor,
        IConfiguration configuration
        )
        {

            _userManager = userManager;
            _repository = repository;
            _signInManager = signInManager;
            _accessor = accessor;
            _emailSender = emailSender;
            _service = service;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            try
            {
                var reviews = await _repository.SessionRating.GetAllRatings();

                string userResult = JsonConvert.SerializeObject(new {
                    reviews
                }, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Ok(userResult);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
