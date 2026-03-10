using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Web;
using TheStartupBuddyV3.Helpers;
using TheStartupBuddyV3.Models;
using TheStartupBuddyV3.Repository;
using TheStartupBuddyV3.Service;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TheStartupBuddyV3.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IRepositoryWrapper _repository;
        private readonly IHttpContextAccessor _accessor;
        private readonly IEmailSender _emailSender;
        private readonly IServiceWrapper _service;
        private readonly IConfiguration _configuration;
        private readonly IHelpers _helpers;

        public AuthController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IRepositoryWrapper repository,
        IEmailSender emailSender,
        IServiceWrapper service,
        IHttpContextAccessor accessor,
        IConfiguration configuration,
        IHelpers helpers
        )
        {

            _userManager = userManager;
            _repository = repository;
            _signInManager = signInManager;
            _accessor = accessor;
            _emailSender = emailSender;
            _service = service;
            _configuration = configuration;
            _helpers = helpers;
        }

        public async Task<IActionResult> CheckSignInMethod(string? email)
        {

            var user = await _repository.User.GetUserByEmailAsync(email);

            if (user == null)
            {
                return BadRequest("Sorry, we can't find an account with this email address, try another or sign up.");
            }

            string usersResult = JsonConvert.SerializeObject(user, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });

            return Ok(usersResult);
        }

        public async Task<IActionResult> SignIn(string email, string password)
        {

            var admin = await _repository.AdminUsers.GetAdminByEmail(email);

            if (admin != null)
            {
                try
                {
                    if (password == admin.Password)
                    {
                        Response.Cookies.Append("X-Admin-Email", admin.Email, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(1) });
                        return Ok(new
                        {
                            email = admin.Email,
                            onBoardingStatus = admin.UserOnBoardingStatus,
                            photo = admin.Photo,
                            coins = admin.Coins,
                            programId = admin.ProgramId,
                            role = admin.Role,
                            name = admin.FirstName + " " + admin.LastName
                        });
                    }
                    else
                    {
                        return BadRequest(new { message = "The password you entered is incorrect. Please try again.", confirm = true });
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(new { message = e.Message, confirm = true });
                }
            }
            else
            {
                return NotFound();
            }

        }

        //public async Task<IActionResult> SignIn(string email, string password)
        //{

        //    var user = await _userManager.FindByEmailAsync(email);

        //    try
        //    {
        //        if (user != null)
        //        {
        //            bool isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

        //            if (!isEmailConfirmed)
        //            {
        //                return BadRequest(new { message = "The given email address has not been verified.", confirm = false });
        //            }
        //        }

        //        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

        //        if (result.Succeeded)
        //        {

        //            var getToken = _helpers.GenerateToken(user.Email, user.Id);
        //            var userData = await _repository.User.GetUserByEmailAsync(email);
        //            userData.RefreshToken = Guid.NewGuid().ToString();
        //            _repository.User.UpdateUser(userData);
        //            await _repository.SaveAsync();

        //            Response.Cookies.Append("X-Access-Token", getToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(1) });
        //            Response.Cookies.Append("X-Email", user.Email, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(1) });
        //            Response.Cookies.Append("X-Refresh-Token", userData.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(7) });
        //            Response.Cookies.Append("X-Id", userData.UserId, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.Now.AddDays(1) });

        //            return Ok(new
        //            {
        //                email = userData.Email,
        //                id = userData.UserId
        //            });
        //        }

        //        return BadRequest(new { message = "The password you entered is incorrect. Please try again.", confirm = true });
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new { message = e.Message, confirm = true });
        //    }

        //}

        public async Task<IActionResult> CheckProgramCode(string? code)
        {

            var program = await _repository.Program.GetProgramByCodeAsync(code);

            try
            {
                if (program == null)
                {
                    return BadRequest("Hmm, that's an invalid code. Please try again");
                }

                string programResult = JsonConvert.SerializeObject(program, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Ok(programResult);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<IActionResult> ConfirmEmail(string email, string token, string uId)
        {

            if (uId == null || token == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(uId);

            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {

            }

            return Content("expired");

        }

        public async Task<IActionResult> ResendConfirmEmail(string email, string address)
        {
            var user = await _userManager.FindByEmailAsync(email);

            try
            {
                if (user != null)
                {
                    bool isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    if (!isEmailConfirmed)
                    {
                        var uriBuilder = new UriBuilder(address);
                        uriBuilder.Path = "/confirm";
                        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                        query["userId"] = user.Id;
                        query["code"] = code;
                        uriBuilder.Query = query.ToString();
                        string url = uriBuilder.ToString();

                        var getEmailTemplate = await _repository.Content.GetContentByKeyAsync("Welcome_AWS", "Email");
                        string template = getEmailTemplate.Data.ToString().Replace("{#link#}", "href=" + url + "");

                        var message = new Message
                        {
                            To = email,
                            Subject = "Welcome to The Startup Buddy",
                            Content = template
                        };

                        await _service.EmailService.SendEmailAsync(message);
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("The given email address has been verified.");
                    }
                }
                else
                {
                    return BadRequest("Sorry, we can't find an account with this email address, try another or sign up.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        public async Task<IActionResult> SignUp(string role, string? token, short membership_type, string? coupon, short membership_duration, string email, string password, string? programcode, bool skip_trial, string payment_model, bool card_valid, bool program_valid, string address)
        {

            var isUserExist = await _repository.User.GetUserByEmailAsync(email);
            var userRole = role.Contains("0") ? "1000" : "0001";
            bool isStartup = role.Contains("0") ? true : false;
            bool isInvestor = role.Contains("1") ? true : false;

            if (isUserExist != null)
            {
                return BadRequest("Sorry, this email already exists!");
            }

            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            try
            {
                if (result.Succeeded)
                {
                    if (isUserExist == null)
                    {
                        var newUser = new User
                        {
                            UserId = System.Guid.NewGuid().ToString(),
                            Email = email,
                            LoginProvider = "Identity",
                            ProviderKey = "",
                            DisplayName = "",
                            Source = "WEB",
                            JoinDate = DateTime.Now
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
                        IpInfo? ipInfo = IP2Country.GetUserCountryByIp(ips?[0]);
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
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var uriBuilder = new UriBuilder(address);
                    uriBuilder.Path = "/confirm";
                    var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                    query["userId"] = user.Id;
                    query["code"] = code;
                    uriBuilder.Query = query.ToString();
                    string url = uriBuilder.ToString();

                    var getEmailTemplate = await _repository.Content.GetContentByKeyAsync("Welcome_AWS", "Email");
                    string template = getEmailTemplate.Data.ToString().Replace("{#link#}", "href=" + url + "");

                    var message = new Message
                    {
                        To = email,
                        Subject = "Welcome to The Startup Buddy",
                        Content = template
                    };

                    await _service.EmailService.SendEmailAsync(message);

                    return Ok("ok");
                }

                var errorMsg = "";
                foreach (var error in result.Errors)
                {
                    //if my function is not found error it will print it as it's
                    errorMsg = error.Description;
                }

                return BadRequest(errorMsg);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        public async Task<IActionResult> EmailVerification(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);

            try
            {
                if (user != null)
                {
                    var isConfirm = await _userManager.IsEmailConfirmedAsync(user);

                    if (!isConfirm)
                    {
                        var result = await _userManager.ConfirmEmailAsync(user, code);
                        if (result.Succeeded)
                        {
                            return Ok("success");
                        }

                        return BadRequest("failed");
                    }

                    return BadRequest("The given email address has been verified.");

                }

                return BadRequest("false");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public async Task<IActionResult> ResetPassword(string userId, string code, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("Sorry, we can't find an account with this email address, try another or sign up.");
            }

            var result = await _userManager.ResetPasswordAsync(user, code, password);
            if (result.Succeeded)
            {
                return Ok("success");
            }

            return BadRequest(result.Errors);
        }

        public async Task<IActionResult> Refresh()
        {
            if (!(Request.Cookies.TryGetValue("X-Admin-Email", out var Email) && Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken)))
                return BadRequest();

            var user = await _repository.User.GetUserByEmailAndTokenAsync(Email, refreshToken);

            if (user.RefreshToken == null)
                return BadRequest();

            var getToken = _helpers.GenerateToken(user.Email, user.UserId);

            user.RefreshToken = Guid.NewGuid().ToString();
            _repository.User.UpdateUser(user);
            await _repository.SaveAsync();

            Response.Cookies.Append("X-Access-Token", getToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("X-Admin-Email", user.Email, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
            Response.Cookies.Append("X-Refresh-Token", user.RefreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });

            return Ok();
        }

        public async Task<IActionResult> LogOut()
        {

            var user = await _repository.User.GetUserByEmailAndTokenAsync(Request.Cookies["X-Admin-Email"], Request.Cookies["X-Refresh-Token"]);

            if (user != null)
            {

                user.RefreshToken = null;
                _repository.User.UpdateUser(user);
                await _repository.SaveAsync();
            }

            Response.Cookies.Delete("X-Access-Token");
            Response.Cookies.Delete("X-Admin-Email");
            Response.Cookies.Delete("X-Refresh-Token");
            Response.Cookies.Delete("X-Id");
            Response.Cookies.Delete("X-StartupId");

            return Ok();
        }

        public async Task<IActionResult> ForgotPassword(string email, string address)
        {

            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var userData = await _repository.User.GetUserByEmailAsync(email);

                if (user == null)
                {
                    if (userData != null)
                    {
                        return BadRequest("Unable to reset password on email registered via social identity providers.");
                    }
                    else
                    {
                        return BadRequest("Sorry, we can't find an account with this email address, try another or sign up.");
                    }

                }

                if (user != null && !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return BadRequest("The given email address has not been verified, please check your email to find our welcome email and click on the verification link.");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var uriBuilder = new UriBuilder(address);
                uriBuilder.Path = "/reset";
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["userId"] = user.Id;
                query["code"] = code;
                uriBuilder.Query = query.ToString();
                string url = uriBuilder.ToString();

                var getEmailTemplate = await _repository.Content.GetContentByKeyAsync("Reset_password", "Email");
                string template = getEmailTemplate.Data.ToString().Replace("{#link#}", "href=" + url + "");

                var message = new Message
                {
                    To = email,
                    Subject = "Reset password link",
                    Content = template
                };

                await _service.EmailService.SendEmailAsync(message);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        public async Task<IActionResult> TokenValidity(string userId, string code)
        {

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest("Sorry, we can't find an account with this email address, try another or sign up.");
            }

            var validityResult = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", code);

            if (validityResult)
            {
                return Ok(validityResult);
            }

            return BadRequest(validityResult);
        }
        public async Task<IActionResult> FetchProgram()
        {
            try
            {

                var ProgramList = await _repository.Program.GetAllProgramsAsync();

                string goalResults = JsonConvert.SerializeObject(ProgramList, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Ok(goalResults);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region get users data like coins, name etc
        public async Task<IActionResult> GetUsersData()
        {
            try
            {
                var Email = Request.Cookies["X-Admin-Email"] ?? String.Empty;
                var admin = await _repository.AdminUsers.GetAdminByEmail(Email);
                var results = new
                {
                    email = admin.Email,
                    onBoardingStatus = admin.UserOnBoardingStatus,
                    photo = admin.Photo,
                    coins = admin.Coins,
                    programId = admin.ProgramId,
                    role = admin.Role,
                    name = admin.FirstName + " " + admin.LastName
                };
               
                string dataResults = JsonConvert.SerializeObject(results, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Ok(dataResults);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}