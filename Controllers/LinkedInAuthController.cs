using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using TheStartupBuddyV3.Helpers;
using TheStartupBuddyV3.Models;
using TheStartupBuddyV3.Repository;

namespace TheStartupBuddyV3.Controllers
{
    public partial class State
    {
        [JsonProperty("role")]
        public string role { get; set; } = null!;
        [JsonProperty("token")]
        public string? token { get; set; }
        [JsonProperty("membership_type")]
        public short membership_type { get; set; }
        [JsonProperty("promo_code")]
        public string? promo_code { get; set; }
        [JsonProperty("program_code")]
        public string? program_code { get; set; }
        [JsonProperty("membership_duration")]
        public bool membership_duration { get; set; }
        [JsonProperty("skip_trial")]
        public bool skip_trial { get; set; }
        [JsonProperty("payment_model")]
        public string payment_model { get; set; } = null!;
        [JsonProperty("is_program_code_valid")]
        public bool is_program_code_valid { get; set; }
        [JsonProperty("is_card_valid")]
        public bool is_card_valid { get; set; }
        [JsonProperty("redirect")]
        public string? redirect { get; set; }
    }

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LinkedInAuthController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IRepositoryWrapper _repository;
        private readonly IConfiguration _configuration;
        private readonly IHelpers _helpers;

        public LinkedInAuthController(IHttpContextAccessor accessor, IRepositoryWrapper repository,
            IConfiguration configuration, IHelpers helpers)
        {
            _accessor = accessor;
            _repository = repository;
            _configuration = configuration;
            _helpers = helpers;
        }

        public string BaseUrl()
        {
            var request = _accessor.HttpContext.Request;
            // Now that you have the request you can select what you need from it.
            return string.Empty;
        }

        string INClientId
        {
            get
            {
                //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
                //{
                //    return "770991724885-ef2qnimvbpj9u4i246blv0lo41ngt4o4.apps.googleusercontent.com";

                //}
                //else
                //{

                //}

                return "8672v0fheyq6dm";

            }
        }

        string INClientSecret
        {
            get
            {
                //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
                //{
                //    return "t238MIxQxnJ3hMAX6qvI6qlD";
                //}
                //else
                //{


                //}

                return "8zMUMrhgIiIldJDQ";

            }
        }

        //public async Task<IActionResult> INCallback(string state, string code)
        //{
        //    var HelpersInstance = new Helpers(_repository, _configuration, _accessor);

        //    var stateObj = JsonConvert.DeserializeObject<State>(state);

        //    var client = new RestClient("https://www.linkedin.com/oauth/v2/accessToken");
        //    var request = new RestRequest();

        //    request.Method = Method.Get;
        //    request.AddParameter("grant_type", "authorization_code");
        //    request.AddParameter("code", code);
        //    request.AddParameter("redirect_uri", stateObj.redirect);
        //    request.AddParameter("client_id", _configuration["LinkedIn:ClientId"]);
        //    request.AddParameter("client_secret", _configuration["LinkedIn:ClientSecret"]);

        //    var response = client.Execute(request);
        //    var content = response.Content;

        //    var result = JObject.Parse(content);
        //    string accesstoken = result["access_token"].ToString();

        //    var userRole = stateObj.role.Contains("0") ? "1000" : "0001";
        //    bool isStartup = stateObj.role.Contains("0") ? true : false;
        //    bool isInvestor = stateObj.role.Contains("1") ? true : false;

        //    client = new RestClient("https://api.linkedin.com/v2/me?oauth2_access_token=" + result["access_token"]);
        //    request = new RestRequest();
        //    request.Method = Method.Get;

        //    response = client.Execute(request);
        //    content = response.Content;
        //    result = JObject.Parse(content);

        //    string id = result["id"].ToString();
        //    string name = result["localizedFirstName"] + " " + result["localizedLastName"];
        //    client = new RestClient("https://api.linkedin.com/v2/emailAddress?q=members&projection=(elements*(handle~))&oauth2_access_token=" + accesstoken);
        //    request = new RestRequest();
        //    request.Method = Method.Get;

        //    response = client.Execute(request);
        //    content = response.Content;

        //    result = JObject.Parse(content);

        //    string email = result["elements"][0]["handle~"]["emailAddress"].ToString();

        //    var user = await _repository.User.GetUserByEmailAsync(email);

        //    if (user != null)
        //    {
        //        BadRequest("Sorry, this email already exists!");
        //    }

        //    try
        //    {
        //        if (user == null)
        //        {

        //            var newUser = new User
        //            {
        //                UserId = System.Guid.NewGuid().ToString(),
        //                Email = email,
        //                LoginProvider = "Linkedin",
        //                ProviderKey = id,
        //                DisplayName = name,
        //                Source = HelpersInstance.GetBaseUrl(),
        //                JoinDate = DateTime.Now,
        //                RefreshToken = Guid.NewGuid().ToString()
        //            };

        //            string ip = string.Empty;

        //            //first try to get IP address from the forwarded header
        //            if (_accessor.HttpContext.Request.Headers != null)
        //            {
        //                //the X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a client
        //                //connecting to a web server through an HTTP proxy or load balancer

        //                var forwardedHeader = _accessor.HttpContext.Request.Headers["X-Forwarded-For"];
        //                if (!StringValues.IsNullOrEmpty(forwardedHeader))
        //                    ip = forwardedHeader.FirstOrDefault();
        //            }

        //            //if this header not exists try get connection remote IP address
        //            if (string.IsNullOrEmpty(ip) && _accessor.HttpContext.Connection.RemoteIpAddress != null)
        //                ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

        //            string[] ips = ip.Split(new char[] { ',' });
        //            IpInfo ipInfo = IP2Country.GetUserCountryByIp(ips[0]);
        //            string country = "";
        //            string city = "";

        //            if (ipInfo != null)

        //            {
        //                country = ipInfo.Country;
        //                city = ipInfo.City;

        //                if (city == null)
        //                {
        //                    city = "";
        //                }

        //                if (country == null)
        //                {
        //                    country = "";
        //                }

        //            }

        //            newUser.Country = country;
        //            newUser.City = city;

        //            _repository.User.CreateUser(newUser);
        //            await _repository.SaveAsync();

        //            var isUserProfileExist = await _repository.UserProfile.GetUserProfileByUserIdAsync(newUser.UserId);

        //            if (isUserProfileExist == null)
        //            {
        //                var newUserProfile = new UserProfile
        //                {
        //                    Userid = newUser.UserId,
        //                    Joinedas = userRole
        //                };

        //                _repository.UserProfile.CreateUserProfile(newUserProfile);
        //                await _repository.SaveAsync();
        //            }

        //            if (isStartup)
        //            {
        //                var newStartup = new Startup
        //                {
        //                    Userid = newUser.UserId,
        //                    Email = email,
        //                };

        //                _repository.Startup.CreateStartup(newStartup);
        //                await _repository.SaveAsync();

        //                if (stateObj.membership_type > 1 && stateObj.payment_model == "0" && stateObj.is_card_valid)
        //                {

        //                    int countSubscriptionExist = _repository.SubscriptionStartup.CountSubscriptionByUserIdAsync(newUser.UserId);
        //                    if (countSubscriptionExist == 0)
        //                    {
        //                        short durationCheck = stateObj.membership_duration ? (short)1 : (short)0;
        //                        HelpersInstance.StripeCreateSubscription(stateObj.role, newUser.UserId, stateObj.token, stateObj.membership_type, stateObj.promo_code, durationCheck, email, countSubscriptionExist, stateObj.skip_trial);
        //                    }

        //                }

        //                if (stateObj.promo_code != "" && stateObj.promo_code != null && stateObj.payment_model == "1" && stateObj.is_program_code_valid)
        //                {
        //                    var programData = await _repository.Program.GetProgramByCodeAsync(stateObj.promo_code);
        //                    var newStartupJoinProgram = new StartupProgram
        //                    {
        //                        ProgramId = programData.Programid,
        //                        StartupId = newStartup.Startupid,
        //                        JoinDate = DateTime.Now
        //                    };
        //                    _repository.StartupProgram.CreateStartupJoinProgram(newStartupJoinProgram);
        //                    await _repository.SaveAsync();
        //                }

        //            }
        //            else if (isInvestor)
        //            {
        //                var newInvestor = new Investor
        //                {
        //                    UserId = newUser.UserId
        //                };

        //                _repository.Investor.CreateInvestor(newInvestor);
        //                await _repository.SaveAsync();

        //                if (membership_type > 1 && payment_model == "0" && card_valid)
        //                {

        //                    int countSubscriptionExist = _repository.SubscriptionInvestor.CountSubscriptionByUserIdAsync(newUser.UserId);
        //                    if (countSubscriptionExist == 0)
        //                    {
        //                        HelpersInstance.StripeCreateSubscription(role, newUser.UserId, token, membership_type, coupon, membership_duration, email, countSubscriptionExist, skip_trial);
        //                    }

        //                }
        //            }

        //            var getToken = HelpersInstance.GenerateToken(newUser.Email, newUser.UserId);

        //            Response.Cookies.Append("X-Access-Token", getToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(1) });
        //            Response.Cookies.Append("X-Email", email, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(1) });
        //            Response.Cookies.Append("X-Refresh-Token", newUser.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(7) });

        //            return Ok(getToken);

        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    return Ok(user);
        //}

        public async Task<IActionResult> LinkedInSignUp(string provider_id, string name_user, string role, string? token, short membership_type, string? coupon, short membership_duration, string email, string? programcode, bool skip_trial, string payment_model, bool card_valid, bool program_valid)
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
                        LoginProvider = "LinkIn",
                        ProviderKey = provider_id,
                        DisplayName = name_user,
                        Source = _helpers.GetBaseUrl(),
                        JoinDate = DateTime.Now,
                        RefreshToken = Guid.NewGuid().ToString()
                    };

                    string? ip = string.Empty;

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

                    string[]? ips = ip?.Split(new char[] { ',' });
                    IpInfo ipInfo = IP2Country.GetUserCountryByIp(ips?[0]);
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

        public async Task<IActionResult> LinkedInSignIn(string email)
        {

            var user = await _repository.User.GetUserByEmailAsync(email);

            try
            {
                if (user == null)
                {
                    return BadRequest("Sorry, we can't find an account with this email address. Try another or sign up");
                }

                if (user != null && user.LoginProvider != "LinkIn")
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
