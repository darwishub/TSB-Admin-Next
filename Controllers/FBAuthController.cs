using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using TheStartupBuddyV3.Helpers;
using TheStartupBuddyV3.Models;
using TheStartupBuddyV3.Repository;

namespace TheStartupBuddyV3.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FBAuthController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IRepositoryWrapper _repository;
        private readonly IConfiguration _configuration;
        private readonly IHelpers _helpers;

        public FBAuthController(IHttpContextAccessor accessor, IRepositoryWrapper repository,
            IConfiguration configuration, IHelpers helpers)
        {
            _accessor = accessor;
            _repository = repository;
            _configuration = configuration;
            _helpers = helpers;
        }

        //string googleClientId
        //{
        //    get
        //    {
        //        //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
        //        //{
        //        //    return "770991724885-ef2qnimvbpj9u4i246blv0lo41ngt4o4.apps.googleusercontent.com";

        //        //}
        //        //else
        //        //{

        //        //}

        //        return "770991724885-ttkh7j3mk47onf4bq0r3am04e6vtt48r.apps.googleusercontent.com";

        //    }
        //}

        //string googleClientSecret
        //{
        //    get
        //    {
        //        //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
        //        //{
        //        //    return "t238MIxQxnJ3hMAX6qvI6qlD";
        //        //}
        //        //else
        //        //{


        //        //}

        //        return "8HRoAqfdf-Za-zU8NTsXEuND";

        //    }
        //}

        public async Task<IActionResult> FBCallback(string provider_id, string name_user, string role, string? token, short membership_type, string? coupon, short membership_duration, string email, string? programcode, bool skip_trial, string payment_model, bool card_valid)
        {

            var isUserExist = await _repository.User.GetUserByEmailAsync(email);
            var userRole = role.Contains("0") ? "1000" : "0001";
            bool isStartup = role.Contains("0") ? true : false;
            bool isInvestor = role.Contains("1") ? true : false;

            if (isUserExist != null)
            {
                return BadRequest("Sorry, this email has been registered using " + isUserExist.LoginProvider + "! please go to sign in page instead.");
            }

            try
            {
                if (isUserExist == null)
                {

                    var newUser = new User
                    {
                        UserId = System.Guid.NewGuid().ToString(),
                        Email = email,
                        LoginProvider = "Facebook",
                        ProviderKey = provider_id,
                        DisplayName = name_user,
                        Source = _helpers.GetBaseUrl(),
                        JoinDate = DateTime.Now,
                        RefreshToken = Guid.NewGuid().ToString()
                    };

                    string ip = string.Empty;

                    //first try to get IP address from the forwarded header
                    if (_accessor.HttpContext.Request.Headers != null)
                    {
                        //the X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a client
                        //connecting to a web server through an HTTP proxy or load balancer

                        var forwardedHeader = _accessor.HttpContext.Request.Headers["X-Forwarded-For"];
                        if (!StringValues.IsNullOrEmpty(forwardedHeader))
                            ip = forwardedHeader.FirstOrDefault();
                    }

                    //if this header not exists try get connection remote IP address
                    if (string.IsNullOrEmpty(ip) && _accessor.HttpContext.Connection.RemoteIpAddress != null)
                        ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

                    string[] ips = ip.Split(new char[] { ',' });
                    IpInfo ipInfo = IP2Country.GetUserCountryByIp(ips[0]);
                    string country = "";
                    string city = "";

                    if (ipInfo != null)

                    {
                        country = ipInfo.Country;
                        city = ipInfo.City;

                        if (city == null)
                        {
                            city = "";
                        }

                        if (country == null)
                        {
                            country = "";
                        }

                    }

                    newUser.Country = country;
                    newUser.City = city;

                    _repository.User.CreateUser(newUser);
                    await _repository.SaveAsync();

                    var isUserProfileExist = await _repository.UserProfile.GetUserProfileByUserIdAsync(newUser.UserId);

                    if (isUserProfileExist == null)
                    {
                        var newUserProfile = new UserProfile
                        {
                            Userid = newUser.UserId,
                            Joinedas = userRole
                        };

                        _repository.UserProfile.CreateUserProfile(newUserProfile);
                        await _repository.SaveAsync();
                    }

                    if (isStartup)
                    {
                        var newStartup = new Startup
                        {
                            Userid = newUser.UserId,
                            Email = email,
                        };

                        _repository.Startup.CreateStartup(newStartup);
                        await _repository.SaveAsync();

                        if (membership_type > 1 && payment_model == "0" && card_valid)
                        {

                            int countSubscriptionExist = _repository.SubscriptionStartup.CountSubscriptionByUserIdAsync(newUser.UserId);
                            if (countSubscriptionExist == 0)
                            {
                                _helpers.StripeCreateSubscription(role, newUser.UserId, token, membership_type, coupon, membership_duration, email, countSubscriptionExist, skip_trial);
                            }

                        }

                        if (programcode != "" && programcode != null && payment_model == "1")
                        {
                            var programData = await _repository.Program.GetProgramByCodeAsync(programcode);

                            if (programData != null)
                            {
                                var newStartupJoinProgram = new StartupProgram
                                {
                                    ProgramId = programData.Programid,
                                    StartupId = newStartup.Startupid,
                                    JoinDate = DateTime.Now
                                };
                                _repository.StartupProgram.CreateStartupJoinProgram(newStartupJoinProgram);
                                await _repository.SaveAsync();
                            }

                        }

                    }
                    else if (isInvestor)
                    {
                        var newInvestor = new Investor
                        {
                            UserId = newUser.UserId
                        };

                        _repository.Investor.CreateInvestor(newInvestor);
                        await _repository.SaveAsync();

                        if (membership_type > 1 && payment_model == "0" && card_valid)
                        {

                            int countSubscriptionExist = _repository.SubscriptionInvestor.CountSubscriptionByUserIdAsync(newUser.UserId);
                            if (countSubscriptionExist == 0)
                            {
                                _helpers.StripeCreateSubscription(role, newUser.UserId, token, membership_type, coupon, membership_duration, email, countSubscriptionExist, skip_trial);
                            }

                        }
                    }

                    var getToken = _helpers.GenerateToken(newUser.Email, newUser.UserId);

                    Response.Cookies.Append("X-Access-Token", getToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(1) });
                    Response.Cookies.Append("X-Admin-Email", email, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(1) });
                    Response.Cookies.Append("X-Refresh-Token", newUser.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(7) });
                    Response.Cookies.Append("X-Id", newUser.UserId, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(1) });

                    return Ok(new
                    {
                        email = newUser.Email,
                        id = newUser.UserId
                    });

                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return BadRequest("invalid");
        }

        public async Task<IActionResult> FBSignIn(string email)
        {

            var user = await _repository.User.GetUserByEmailAsync(email);

            try
            {
                if (user == null)
                {
                    return BadRequest("Sorry, we can't find an account with this email address. Try another or sign up");
                }

                if (user != null && user.LoginProvider != "Facebook")
                {
                    return BadRequest("" + email + " has been registered using " + user.LoginProvider + "");
                }

                var getToken = _helpers.GenerateToken(user.Email, user.UserId);
                var userData = await _repository.User.GetUserByEmailAsync(email);
                userData.RefreshToken = Guid.NewGuid().ToString();
                _repository.User.UpdateUser(userData);
                await _repository.SaveAsync();

                Response.Cookies.Append("X-Access-Token", getToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(1) });
                Response.Cookies.Append("X-Admin-Email", user.Email, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(1) });
                Response.Cookies.Append("X-Refresh-Token", userData.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(7) });
                Response.Cookies.Append("X-Id", userData.UserId, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(1) });

                return Ok(new
                {
                    email = userData.Email,
                    id = userData.UserId
                });

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}
