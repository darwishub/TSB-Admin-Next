using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TheStartupBuddyV3.Models;
using TheStartupBuddyV3.Repository;

namespace TheStartupBuddyV3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CoinHistoryController : Controller
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        private readonly IRepositoryWrapper _repository;
        public CoinHistoryController(
            IRepositoryWrapper repository
        )
        {
            _repository = repository;
        }

        #region coin history
        [HttpGet]
        public async Task<IActionResult> GetCoinHistoryForStartup(int startupId, string? type, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var coinHistory = await _repository.CoinHistory.GetAllHistoryByStartup(startupId);

                if (!string.IsNullOrEmpty(type))
                {
                    string SearchResult = type.ToLower().Trim();
                    coinHistory = coinHistory.Where(o => o.TransactionType.ToLower().Trim() == SearchResult);
                }

                if (!coinHistory.Any())
                {
                    return Ok($"No coin history found for startup with ID {startupId}");
                }
                
                coinHistory = coinHistory.OrderByDescending(o => o.TransactionDate);
                
                int totalCount = coinHistory.Count();
                int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                var pagedCoinHistory = coinHistory.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                bool hasMore = pageNumber < totalPages;

                return Ok(new { data = pagedCoinHistory, hasMore });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region coin history graph
        [HttpGet]
        public async Task<IActionResult> GetGraph(int startupid, string? time, string? type)
        {
            try
            {
                var getCoinHistory = await _repository.CoinHistory.GetAllHistoryByStartup(startupid);
                DateTime startDate, endDate;

                switch (time)
                {
                    case "weekly":
                        endDate = DateTime.Today;
                        startDate = endDate.AddDays(-6);
                        break;
                    case "monthly":
                        startDate = DateTime.Now.AddMonths(-1);
                        endDate = DateTime.Now;
                        break;
                    case "last3months":
                        startDate = DateTime.Now.AddMonths(-3);
                        endDate = DateTime.Now;
                        break;
                    case "yearly":
                        startDate = DateTime.Now.AddYears(-1);
                        endDate = DateTime.Now;
                        break;
                    default:
                        return StatusCode(400, "Invalid period. Valid values are 'weekly', 'monthly', 'last3months', 'yearly'.");
                }

                if (!string.IsNullOrEmpty(type))
                {
                    getCoinHistory = getCoinHistory.Where(a => a.TransactionType.Contains(type)).ToList();
                }

                List<string> listLabel = new List<string>();
                List<int> income = new List<int>();
                List<int> outcome = new List<int>();

                if (time == "weekly" || time == "monthly" || time == "last3months")
                {
                    for (var date = startDate; date <= endDate; date = date.AddDays(1))
                    {
                        var transactionsForDay = getCoinHistory.Where(a => a.TransactionDate.Date == date.Date);
                        var incomeForDay = transactionsForDay.Where(a => a.TransactionType.Contains("income")).Sum(a => a.Amount);
                        var outcomeForDay = transactionsForDay.Where(a => a.TransactionType.Contains("outcome")).Sum(a => a.Amount);

                        income.Add(incomeForDay);
                        outcome.Add(outcomeForDay);
                        listLabel.Add(time == "weekly" ? date.ToString("dddd") : date.ToString("yyyy-MM-dd"));
                    }
                }
                else if (time == "yearly")
                {
                    var currentYear = DateTime.Now.Year;
                    var currentMonthIndex = DateTime.Now.Month - 1;
                    var monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.Where(m => !string.IsNullOrEmpty(m)).ToArray();

                    // Adjust the order of months so the current month is at the end and the next month starts the list
                    var monthsAfterCurrent = monthNames.Skip(currentMonthIndex + 1).Take(12 - (currentMonthIndex + 1)).ToArray();
                    var monthsBeforeCurrent = monthNames.Take(currentMonthIndex + 1).ToArray();
                    var months = monthsAfterCurrent.Concat(monthsBeforeCurrent).ToArray();

                    income = Enumerable.Repeat(0, 12).ToList();
                    outcome = Enumerable.Repeat(0, 12).ToList();
                    listLabel = months.ToList();

                    var transactionsByMonth = getCoinHistory
                        .Where(a => a.TransactionDate.Year == currentYear || a.TransactionDate.Year == currentYear - 1)
                        .GroupBy(a => a.TransactionDate.Month)
                        .Select(g => new
                        {
                            Month = g.Key,
                            Income = g.Where(a => a.TransactionType.Contains("income")).Sum(a => a.Amount),
                            Outcome = g.Where(a => a.TransactionType.Contains("outcome")).Sum(a => a.Amount)
                        }).ToList();

                    foreach (var transaction in transactionsByMonth)
                    {
                        int monthIndex = Array.IndexOf(monthNames, monthNames[transaction.Month - 1]);
                        // Calculate the new index based on the adjusted month order
                        int index = monthIndex <= currentMonthIndex ? monthIndex + (12 - (currentMonthIndex + 1)) : monthIndex - (currentMonthIndex + 1);
                        income[index] = transaction.Income;
                        outcome[index] = transaction.Outcome;
                    }
                }

                var result = new
                {
                    labels = listLabel.ToArray(),
                    income = income.ToArray(),
                    outcome = outcome.ToArray()
                };

                string Result = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Content(Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

    }
}
