using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;
using System.Globalization;
using TheStartupBuddyV3.Helpers;
using TheStartupBuddyV3.Models;
using TheStartupBuddyV3.Repository;
using TheStartupBuddyV3.Service;

namespace TheStartupBuddyV3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        private readonly IRepositoryWrapper _repository;
        private readonly IServiceWrapper _service;
        private readonly IHelpers _helpers;
        public DashboardController(
        IRepositoryWrapper repository,
        IServiceWrapper service,
        IHelpers helpers
        )
        {
            _repository = repository;
            _service = service;
            _helpers = helpers;
        }

        #region get all company data
        [HttpGet]
        public async Task<IActionResult> GetGraph(int ProgramId, string? time)
        {
            try
            {
                int totalStepsCount = 0;
                if (ProgramId != 0)
                {
                    var goalBasics = investeur_context.ProgramCodes.Where(x => x.Programid == ProgramId).FirstOrDefault();
                    var totalSteps = await _repository.GoalsStep.GetAllStepsAsync(ProgramId, goalBasics.StatusGoal);
                    totalStepsCount = totalSteps.Count();
                }
                else
                {
                    var totalSteps = await _repository.GoalsStep.GetAllStepsAsync(ProgramId, false);
                    totalStepsCount = totalSteps.Count();
                }
                var getCompanies = await _repository.Startup.GetCompanies(ProgramId);
                var getstartups = await _repository.Startup.GetStartups(ProgramId);
                var steps = await _repository.CompanyGoals.GetAllCompanySteps(ProgramId);
                var AllConnections = await _repository.NetworkingConnect.GetAllConnectionsByProgram(ProgramId);
                var AllCoins = await _repository.CompanyGoals.GetAllCoinsByProgram(ProgramId);

                DateTime startDate, endDate;
                int stepLastTimes = 0;

                if (time == "weekly")
                {
                    endDate = DateTime.Today;
                    startDate = endDate.AddDays(-6);
                    var stepLastTime = steps.Where(a => a.DateCreated.Date >= startDate.AddDays(-7).Date && a.DateCreated.Date <= endDate.AddDays(-7).Date);
                    stepLastTimes = stepLastTime.Count();
                }
                else if (time == "monthly")
                {
                    startDate = DateTime.Now.AddMonths(-1);
                    endDate = DateTime.Now;
                    var stepLastTime = steps.Where(a => a.DateCreated.AddMonths(1).Date >= startDate.Date && a.DateCreated.AddMonths(1).Date <= endDate.Date);
                    stepLastTimes = stepLastTime.Count();
                }
                else if (time == "last3months")
                {
                    startDate = DateTime.Now.AddMonths(-3);
                    endDate = DateTime.Now;
                    var stepLastTime = steps.Where(a => a.DateCreated.AddMonths(3).Date >= startDate.Date && a.DateCreated.AddMonths(3).Date <= endDate.Date);
                    stepLastTimes = stepLastTime.Count();
                }
                else if (time == "yearly")
                {
                    startDate = DateTime.Now.AddMonths(-11);
                    endDate = DateTime.Now;
                    var stepLastTime = steps.Where(a => a.DateCreated.AddYears(1).Date >= startDate.Date && a.DateCreated.AddYears(1).Date <= endDate.Date);
                    stepLastTimes = stepLastTime.Count();
                }
                else
                {
                    return StatusCode(400, "Invalid period. Valid values are 'weekly', 'monthly', 'yearly'.");
                }

                var startupWeek = (from _startup in getstartups
                                join _user in investeur_context.UsersData on _startup.Userid equals _user.UserId
                                where _user.JoinDate.Date >= startDate.Date && _user.JoinDate.Date <= endDate.Date
                                select _startup.Startupid).ToList();

                int companyThisWeek = startupWeek.Count();

                List<int> dateArray = new List<int>();
                List<int> listConnectionPerPeriod = new List<int>();
                List<int> coinsPerPeriod = new List<int>();
                List<string> dayArray = new List<string>();
                List<int> yearsLabel = new List<int>();

                double _stepsComplete = steps.Count();
                double _totalSteps = getstartups.Count() * totalStepsCount;
                double percent = _stepsComplete * 100 / _totalSteps;
                
                var res = new Graph();
                if(time == "yearly")
                {
                    var stepThisTime = steps.Where(o => o.DateCreated.Year == DateTime.Now.Year);
                    for (var date = startDate; date < endDate; date = date.AddMonths(1))
                    {
                        var stepsCount = steps.Count(g => g.DateCreated.Month == date.Month);
                        dateArray.Add(stepsCount);

                        var networkCount = AllConnections.Count(n => n.RequestDate.Month == date.Month);
                        listConnectionPerPeriod.Add(networkCount);

                        var total_coins = AllCoins.Where(c => c.date_created.Month == date.Month).Sum(c => (int)c.coins);
                        coinsPerPeriod.Add(total_coins);
                    }
                    var currentMonth = DateTime.Now.Month - 1;
                    var monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Take(12).ToArray();
                    var reversedMonthNames = monthNames.Reverse().ToArray();
                    var monthLabels = reversedMonthNames.Skip(12 - currentMonth - 1).Take(12).Concat(reversedMonthNames.Take(12 - currentMonth - 1)).ToArray();
                    res.totalCompanies = getCompanies.Count();
                    res.stepLastTime = stepLastTimes;
                    res.stepThisTime = stepThisTime.Count();
                    res.labels = monthLabels.Reverse().ToArray();
                    res.stepPerPeriod = dateArray.ToArray();
                    res.percentage = percent;
                    res.listConnectionPerPeriod = listConnectionPerPeriod.ToArray();
                    res.coinsPerPeriod = coinsPerPeriod.ToArray();
                }
                else
                {
                    var stepThisTime = steps.Where(o => o.DateCreated.Date >= startDate.Date && o.DateCreated.Date <= endDate.Date);
                    for (var date = startDate; date <= endDate; date = date.AddDays(1))
                    {
                        var stepsCount = steps.Count(s => s.DateCreated.Date == date.Date);
                        dateArray.Add(stepsCount);

                        var networkCount = AllConnections.Count(n => n.RequestDate.Date == date.Date);
                        listConnectionPerPeriod.Add(networkCount);

                        var total_coins = AllCoins.Where(c => c.date_created.Date == date.Date).Sum(c => (int)c.coins);
                        coinsPerPeriod.Add(total_coins);

                        if(time == "last3months" || time == "monthly")
                        {
                            dayArray.Add(date.ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            dayArray.Add(date.ToString("dddd"));
                        }
                    }
                    res.totalCompanies = getCompanies.Count();
                    res.stepLastTime = stepLastTimes;
                    res.stepThisTime = stepThisTime.Count();
                    res.labels = dayArray.ToArray();
                    res.stepPerPeriod = dateArray.ToArray();
                    res.percentage = percent;
                    res.listConnectionPerPeriod = listConnectionPerPeriod.ToArray();
                    res.coinsPerPeriod = coinsPerPeriod.ToArray();
                }

                string companiesResult = JsonConvert.SerializeObject(res, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Content(companiesResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region get all company data
        [HttpGet]
        public async Task<IActionResult> GetTopGraph(int ProgramId, string? time = "7")
        {
            var Email = Request.Cookies["X-Admin-Email"] ?? String.Empty;
            try
            {
                int totalStepsCount = 0;
                if (ProgramId != 0)
                {
                    var goalBasics = investeur_context.ProgramCodes.Where(x => x.Programid == ProgramId).FirstOrDefault();
                    var totalSteps = await _repository.GoalsStep.GetAllStepsAsync(ProgramId, goalBasics.StatusGoal);
                    totalStepsCount = totalSteps.Count();
                }
                else
                {
                    var totalSteps = await _repository.GoalsStep.GetAllStepsAsync(ProgramId, false);
                    totalStepsCount = totalSteps.Count();
                }
                var getstartups = await _repository.Startup.GetStartups(ProgramId);
                var stepsComplete = await _repository.CompanyGoals.GetAllCompanySteps(ProgramId);
                var AllConnections = await _repository.NetworkingConnect.GetAllConnectionsByProgram(ProgramId);
                var AllCoins = await _repository.CompanyGoals.GetAllCoinsByProgram(ProgramId);
                ArrayList dateArray = new ArrayList();
                ArrayList dayArray = new ArrayList();
                ArrayList listConnectionPerday = new ArrayList();
                ArrayList coinsPerday = new ArrayList();
                if (!string.IsNullOrEmpty(time))
                {
                    int timeFilter = int.Parse(time);

                    if (time == "7")
                    {
                        for (var i = 0; i < timeFilter; i++)
                        {
                            var date_ = DateTime.Today.AddDays(-i);
                            var stepsCount = stepsComplete.Where(i => i.DateCreated.Date == date_);
                            dateArray.Add(stepsCount.Count());
                            //connections
                            var networkCount = AllConnections.Where(i => i.RequestDate.Date == date_);
                            listConnectionPerday.Add(networkCount.Count());
                            //coins
                            var AllCoinsData = AllCoins.Where(i => i.date_created.Date == date_);
                            int total_coins = 0;
                            foreach (var item in AllCoinsData)
                            {
                                total_coins = total_coins + (int)item.coins;
                            }
                            coinsPerday.Add(total_coins);
                            if (stepsCount != null)
                            {
                                dayArray.Add(date_.ToString("dddd"));
                            }
                            else
                            {
                                var dateDay = DateTime.Today.AddDays(-i);
                                dayArray.Add(dateDay.ToString("dddd"));
                            }
                        }
                    }
                }
                double _stepsComplete = stepsComplete.Count();
                double _totalSteps = getstartups.Count() * totalStepsCount;
                double percent = _stepsComplete * 100 / _totalSteps;

                var stepsResult = stepsComplete.ToList();
                var res = new TopGraph();
                dayArray.Reverse();
                res.labels = dayArray.ToArray();
                dateArray.Reverse();
                res.stepPerDay = dateArray.ToArray();
                res.percentage = percent;

                //connection
                listConnectionPerday.Reverse();
                res.listConnectionPerDay = listConnectionPerday.ToArray();

                //coins per day
                coinsPerday.Reverse();
                res.coinsPerDay = coinsPerday.ToArray();

                string companiesResult = JsonConvert.SerializeObject(res, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Content(companiesResult);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region get all user onboarding
        [HttpGet]
        public async Task<IActionResult> UserOnBoarding()
        {
            var Email = Request.Cookies["X-Admin-Email"] ?? String.Empty;
            try
            {
                ArrayList cards = new ArrayList();
                ArrayList steps = new ArrayList();

                var Clicked = (from _user in investeur_context.AdminUsers1.AsNoTracking()
                               where _user.Email == Email
                               select new
                               {
                                   value = _user.OnBoardingClicked,
                               }).ToList().FirstOrDefault();

                var ClickedNull = (from _user in investeur_context.AdminUsers1.AsNoTracking()
                                   where _user.Email == Email
                                   select new
                                   {
                                       value = "",
                                   }).ToList().FirstOrDefault();

                cards.Clear();
                cards.Add(ClickedNull);

                var result0 = new onBoardPersonalInfo();
                if (Clicked?.value != null && Clicked?.value != false)
                {
                    result0.is_step_complete = true;
                }
                else
                {
                    result0.is_step_complete = false;
                }
                result0.cards = cards.ToArray();
                steps.Add(result0);

                var FirstName = (from _user in investeur_context.AdminUsers1.AsNoTracking()
                                 where _user.Email == Email
                                 select new
                                 {
                                     value = _user.FirstName,
                                 }).ToList().FirstOrDefault();

                var LastName = (from _user in investeur_context.AdminUsers1.AsNoTracking()
                                where _user.Email == Email
                                select new
                                {
                                    value = _user.LastName,
                                }).ToList().FirstOrDefault();

                var Linkedin = (from _user in investeur_context.AdminUsers1.AsNoTracking()
                                where _user.Email == Email
                                select new
                                {
                                    value = _user.Linkedin,
                                }).ToList().FirstOrDefault();


                cards.Clear();
                cards.Add(FirstName);
                cards.Add(LastName);
                cards.Add(Linkedin);

                var result = new onBoardPersonalInfo();
                if (FirstName?.value != null && LastName?.value != null && Linkedin?.value != null &&
                FirstName.value != "" && LastName.value != "" && Linkedin.value != "")
                {
                    result.is_step_complete = true;
                }
                else
                {
                    result.is_step_complete = false;
                }
                result.cards = cards.ToArray();
                steps.Add(result);

                var Description = (from _user in investeur_context.AdminUsers1.AsNoTracking()
                                   where _user.Email == Email
                                   select new
                                   {
                                       value = _user.Description,
                                   }).ToList().FirstOrDefault();

                var photo = (from _user in investeur_context.AdminUsers1.AsNoTracking()
                         where _user.Email == Email
                         select new
                         {
                            value = _user.Photo,
                         }).ToList().FirstOrDefault();
                cards.Clear();
                cards.Add(Description);
                cards.Add(photo);

                var result2 = new onBoardPersonalInfo();
                if (Description?.value != "" && Description?.value != null)
                {
                    result2.is_step_complete = true;
                }
                else
                {
                    result2.is_step_complete = false;
                }
                result2.cards = cards.ToArray();

                steps.Add(result2);

                string companiesResult = JsonConvert.SerializeObject(new { steps }, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Content(companiesResult);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        #region submit user on boarding data
        public async Task<IActionResult> SubmitOnBoarding(IFormCollection form)
        {
            var Email = Request.Cookies["X-Admin-Email"] ?? String.Empty;

            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files;

            try
            {

                if (form.TryGetValue("data", out var Data))
                {

                    var data = JsonConvert.DeserializeObject<dynamic>(Data.ToString());
                    int idGoalStep = data?.id_goal_step;

                    if (idGoalStep == 0)
                    {
                        var _userData = await _repository.AdminUsers.GetAdminByEmail(Email);
                        _userData.OnBoardingClicked = true;
                        _repository.AdminUsers.Update(_userData);
                        await _repository.SaveAsync();
                    }

                    if (idGoalStep == 1)
                    {
                        var _userData = await _repository.AdminUsers.GetAdminByEmail(Email);
                        if (data?.cards?.Count > 0)
                        {

                            _userData.FirstName = data?.cards[0].value;
                            _userData.LastName = data?.cards[1].value;
                            _userData.Linkedin = data?.cards[2].value;
                            _repository.AdminUsers.Update(_userData);
                            await _repository.SaveAsync();
                        }
                    }

                    if (idGoalStep == 2)
                    {
                        var _userData = await _repository.AdminUsers.GetAdminByEmail(Email);
                        if (data?.cards?.Count > 0)
                        {

                        //    if (file?.Count > 0)
                        //     {
                        //         for (var x = 0; x < file.Count; x++)
                        //         {
                        //             var key = Guid.NewGuid().ToString().Substring(0, 5);
                        //             await _service.AWSUserImgService.UploadImageFile(file[x], key);
                        //             var keyFilteredName = key + file[x].FileName.ToLower();
                        //             var fileNameFiltered = _helpers.RegexFilter(keyFilteredName);
                                    
                        //             var fileStatus = await _service.AWSUserImgService.CheckFileExists(fileNameFiltered);
                        //             if (!fileStatus)
                        //             {
                        //                 return BadRequest(new { message = $"File {file[x].FileName} does not exist in the bucket.", success = false });
                        //             }
                        //             _userData.Photo = fileNameFiltered;
                        //         }
                        //         _repository.AdminUsers.Update(_userData);
                        //         await _repository.SaveAsync();
                        //     }

                            string input = data?.cards[1]?.value[0].ToString();
                            if (input != null)
                            {
                                var fileNameFiltered = _helpers.RegexFilter(input);
                                _userData.Photo = fileNameFiltered.ToLower();
                            }
                            _userData.Description = data?.cards[0].value;
                            _repository.AdminUsers.Update(_userData);
                            await _repository.SaveAsync();
                        }
                    }
                }

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message, confirm = true });

            }

        }
        #endregion

        #region collect rewards on boarding
        public async Task<IActionResult> SubmitCollect()
        {
            var Email = Request.Cookies["X-Admin-Email"] ?? String.Empty;
            try
            {
                var _userData = await _repository.AdminUsers.GetAdminByEmail(Email);
                if (_userData != null)
                {
                    if (_userData.UserOnBoardingStatus == false)
                    {
                        _userData.Coins = _userData.Coins + 100;
                        _userData.UserOnBoardingStatus = true;
                        _repository.AdminUsers.Update(_userData);
                        await _repository.SaveAsync();
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region change password
        public async Task<IActionResult> ChangePassword()
        {
            var Email = Request.Cookies["X-Admin-Email"] ?? String.Empty;
            try
            {
                var password = "faizah123";
                var _userData = await _repository.AdminUsers.GetAdminByEmail(Email);
                if (_userData != null)
                {
                    _userData.Password = password;
                    _repository.AdminUsers.Update(_userData);
                    await _repository.SaveAsync();
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }
}
