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
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IRepositoryWrapper _repository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IEmailSender _emailSender;
        private readonly IServiceWrapper _service;
        private readonly IConfiguration _configuration;
        private InvesteurContext investeur_context = new InvesteurContext();
        public UserController(
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
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _repository.User.GetUserByIdAsync(id);
                var userProfile = await _repository.UserProfile.GetUserProfileByUserIdAsync(id);
                var StartupId = await _repository.Startup.GetStartupIdByEmail(user?.Email);
                var StartupIdMember = await _repository.StartupMember.GetStartupIdByEmail(user?.Email);
                var checkStartupId = StartupIdMember != null ? StartupIdMember.Startupid : StartupId.Startupid;
                var StartupProgram = await _repository.StartupProgram.GetStartupProgram(checkStartupId);
                var reviews = await _repository.SessionRating.GetReviewByStartupId(StartupId.Startupid);

                string userResult = JsonConvert.SerializeObject(new {
                    user,
                    userProfile,
                    reviews,
                    is_member = StartupIdMember != null ? true : false,
                    StartupId = checkStartupId,
                    Program = StartupProgram != null ? StartupProgram.ProgramName : "No Program"
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

        [HttpGet]
        public async Task<IActionResult> GetRecommendationStartups(int startupId)
        {
            try
            {

                var query = await _repository.User.GetStartupsRecommendation(startupId);
                var listProfile = new List<User>();
                var startupIds = new List<int>();

                if (query != null)
                {
                    foreach (var item in query)
                    {
                        var profile = await _repository.User.GetUserByIdAsync(item.Userid);
                        var StartupId = await _repository.Startup.GetStartupIdByEmail(profile?.Email);
                        var StartupIdMember = await _repository.StartupMember.GetStartupIdByEmail(profile?.Email);
                        var checkStartupId = StartupIdMember != null ? StartupIdMember.Startupid : StartupId.Startupid;
                        listProfile.Add(profile);
                        startupIds.Add(checkStartupId);
                    }
                }

                string Results = JsonConvert.SerializeObject(new {
                    query,
                    listProfile,
                    startupIds
                }, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });
                return Content(Results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
