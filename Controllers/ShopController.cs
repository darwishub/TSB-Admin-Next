using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;
using System.Text.RegularExpressions;
using TheStartupBuddyV3.Helpers;
using TheStartupBuddyV3.Models;
using TheStartupBuddyV3.Repository;
using TheStartupBuddyV3.Service;

namespace TheStartupBuddyV3.Controllers
{
    public class Shops
    {
        public int? id { get; set; }
        public string? tags { get; set; }
        public DateTime? date { get; set; }
        public string? name { get; set; }
        public int? item_type { get; set; }
        public byte? action_type { get; set; }
        public int? price { get; set; }
        public int? amount { get; set; }
        public string? logo { get; set; }
        public string? description { get; set; } //info
        public string? content { get; set; } //description
        public int? id_level { get; set; }
        public int? id_category { get; set; }
        public int? apply_to { get; set; }
        public int? event_type { get; set; }
        public string? event_address { get; set; }
        public string? event_url { get; set; }
        public string? category_logo { get; set; }
        public bool is_published { get; set; }
        public int? programId { get; set; }
        public DateTime? create_date { get; set; }
        public int programGroupId { get; set; }

    }

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ShopController : Controller
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        private readonly IRepositoryWrapper _repository;
        private readonly IServiceWrapper _service;
        private readonly IHelpers _helpers;

        public ShopController(
        IRepositoryWrapper repository,
        IServiceWrapper service,
        IHelpers helpers
        )
        {
            _repository = repository;
            _service = service;
            _helpers = helpers;
        }

        #region submit user on boarding data
        public async Task<IActionResult> SubmitShopEntry(IFormCollection form)
        {
            var Email = Request.Cookies["X-Admin-Email"] ?? String.Empty;

            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();

            try
            {

                if (form.TryGetValue("data", out var Data) && form.TryGetValue("programId", out var ProgramId))
                {

                    var data = JsonConvert.DeserializeObject<dynamic>(Data.ToString());
                    int programId = ProgramId != "" ? int.Parse(ProgramId) : 0;
                    var key = Guid.NewGuid().ToString().Substring(0, 5);
                    var img = Regex.Replace(data?.goal_img.Value, "[^a-zA-Z0-9]", String.Empty);

                    if (data?.content_type == 0)
                    {
                        var newEvent = new Event
                        {
                            EventName = data?.title ?? "",
                            // Logo = key + img ?? "",
                            EventDescription = data?.description ?? "",
                            EventInfo = data?.content ?? "",
                            ApplyTo = data?.apply_to,
                            EventDateStart = data?.date ?? DateTime.Now,
                            EventDateEnd = data?.date ?? DateTime.Now,
                            EventUrl = data?.event_url ?? "",
                            EventType = data?.event_type,
                            EventAddress = data?.event_address ?? "",
                            EventPlatform = 6,
                            AssignToProgram = programId,
                            EmailNotification = 0,
                            ActionType = data?.price_type ?? 0,
                            PriceEvent = data?.price ?? 0,
                            IsPublished = true,
                            tags = data?.tags.ToString() ?? "[]",
                            AssignToLevel = string.IsNullOrEmpty(data?.level_code_number.ToString()) ? 1 : data?.level_code_number ?? 1,
                            AssignToCategory = string.IsNullOrEmpty(data?.level_id.ToString()) ? 1 : data?.level_id ?? 1,
                            CreateDate = DateTime.Now,
                            ProgramGroupId = data?.program_group_id ?? 0
                        };

                        if (file != null)
                            {

                                Regex regex = new Regex("[^a-zA-Z0-9]");
                                var fileName = regex.Replace(file.FileName.ToLower(), "");

                                await _service.AWSUserImgService.UploadImageFile(file);
          
                                newEvent.Logo = fileName;
                            }

                        _repository.Event.CreateEvent(newEvent);
                        await _repository.SaveAsync();
                    }

                    if (data?.content_type == 1)
                    {
                        var newBenefit = new Benefit
                        {
                            BenefitName = data?.title ?? "",
                         
                            Worth = "",
                            PackType = 0,
                            BenefitDescription = data?.description ?? "",
                            BenefitInfo = data?.content ?? "",
                            ApplyTo = data?.apply_to,
                            ActionType = data?.price_type,
                            PriceBenefit = data?.price,
                            IsPublished = true,
                            AssignToProgram = programId,
                            tags = data?.tags.ToString() ?? "[]",
                            AssignToLevel = string.IsNullOrEmpty(data?.level_code_number.ToString()) ? 1 : data?.level_code_number ?? 1,
                            AssignToCategory = string.IsNullOrEmpty(data?.level_id.ToString()) ? 1 : data?.level_id ?? 1,
                            CreateDate = DateTime.Now,
                            ProgramGroupId = data?.program_group_id ?? 0
                        };

                        if (file != null)
                            {

                                Regex regex = new Regex("[^a-zA-Z0-9]");
                                var fileName = regex.Replace(file.FileName.ToLower(), "");

                                await _service.AWSUserImgService.UploadImageFile(file);
          
                                newBenefit.Logo = fileName;
                            }

                        _repository.Benefit.CreateBenefit(newBenefit);
                        await _repository.SaveAsync();
             
                    }

                    if (data?.content_type == 2)
                    {
                        var newShopCoins = new ShopCoins
                        {
                            AmountOfCoins = data?.coins_amount,
                            Price = data?.coins_price,
                            Title = data?.title,
                            Apply_to = data?.apply_to,
                            CreatedDate = DateTime.Now,
                            IsPublished = true
                        };

                        if (file != null)
                        {

                            Regex regex = new Regex("[^a-zA-Z0-9]");
                            var fileName = regex.Replace(file.FileName.ToLower(), "");

                            await _service.AWSUserImgService.UploadImageFile(file);
        
                            newShopCoins.Logo = fileName;
                        }

                        _repository.ShopCoins.Create(newShopCoins);
                        await _repository.SaveAsync();
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

        #region  get all list shops
        [HttpGet]
        public async Task<IActionResult> AllShopItems(string? Search, string? ItemType, int? ProgramId, int ShowEntries, int Pagination,
        int? ProgramGroupId, bool ALL)
        {
            ArrayList finalResult = new ArrayList();
            var _programData = await _repository.Program.GetProgramByProgramId(ProgramId);
            int startupProgram = 0;
            int statusGoal = 0;
            if (_programData != null)
            {
                startupProgram = _programData.Programid;
                if (_programData.StatusGoal == true)
                {
                    statusGoal = 1;
                }
                else
                {
                    statusGoal = 0;
                }
            }
            else
            {
                startupProgram = 0;
                statusGoal = 0;
            }

            var benefitsQuery = await (from benefit in investeur_context.Benefits.AsNoTracking()
                                       join _level in investeur_context.Levels.AsNoTracking()
                                       on benefit.AssignToCategory equals _level.IdLevel into benefitTable
                                       from bl in benefitTable.DefaultIfEmpty()
                                       select new Shops
                                       {
                                           id = benefit.Id,
                                           tags = benefit.tags ?? "",
                                           name = benefit.BenefitName ?? "",
                                           item_type = 1,
                                           action_type = benefit.ActionType ?? 0,
                                           price = benefit.PriceBenefit ?? 0,
                                           logo = benefit.Logo ?? "",
                                           description = benefit.BenefitDescription ?? "",
                                           id_level = bl.CodeLevel ?? 1,
                                           id_category = bl.IdGoalsCategory ?? 1,
                                           category_logo = bl.LogoLevel ?? "",
                                           is_published = benefit.IsPublished,
                                           programId = benefit.AssignToProgram,
                                           create_date = benefit.CreateDate,
                                           programGroupId = benefit.ProgramGroupId
                                       }
                                       ).ToListAsync();

            foreach (var benefit in benefitsQuery)
            {
                var DataBenefits = new Shops
                {

                    id = benefit.id,
                    tags = benefit.tags,
                    name = benefit.name,
                    item_type = 1,
                    action_type = benefit.action_type,
                    price = benefit.price,
                    logo = benefit.logo,
                    description = benefit.description,
                    id_level = benefit.id_level,
                    id_category = benefit.id_category,
                    category_logo = benefit.category_logo,
                    is_published = benefit.is_published,
                    programId = benefit.programId,
                    create_date = benefit.create_date,
                    programGroupId = benefit.programGroupId
                };
                finalResult.Add(DataBenefits);
            }

            var eventsQuery = await (from _event in investeur_context.Events.AsNoTracking()
                                     join _level in investeur_context.Levels.AsNoTracking()
                                     on _event.AssignToCategory equals _level.IdLevel into benefitTable
                                     from bl in benefitTable.DefaultIfEmpty()
                                     select new Shops
                                     {
                                         id = _event.Id,
                                         tags = _event.tags ?? "",
                                         name = _event.EventName ?? "",
                                         item_type = 0,
                                         action_type = _event.ActionType ?? 0,
                                         price = _event.PriceEvent ?? 0,
                                         logo = _event.Logo ?? "",
                                         description = _event.EventDescription ?? "",
                                         id_level = bl.CodeLevel ?? 1,
                                         id_category = bl.IdGoalsCategory ?? 1,
                                         category_logo = bl.LogoLevel ?? "",
                                         date = _event.EventDateStart,
                                         is_published = _event.IsPublished,
                                         programId = _event.AssignToProgram,
                                         create_date = _event.CreateDate,
                                         programGroupId = _event.ProgramGroupId
                                     }
                                       ).ToListAsync();

            foreach (var _event in eventsQuery)
            {
                var DataEvents = new Shops
                {

                    id = _event.id,
                    tags = _event.tags,
                    name = _event.name,
                    item_type = 0,
                    action_type = _event.action_type,
                    price = _event.price,
                    logo = _event.logo,
                    description = _event.description,
                    id_level = _event.id_level,
                    id_category = _event.id_category,
                    date = _event.date,
                    category_logo = _event.category_logo,
                    is_published = _event.is_published,
                    programId = _event.programId,
                    create_date = _event.create_date,
                    programGroupId = _event.programGroupId
                };
                finalResult.Add(DataEvents);
            }

            //get all shop coins
            var _shopCoinsData = await _repository.ShopCoins.getAllShopCoins();
            foreach (var _shopCoins in _shopCoinsData)
            {
                var DataCoins = new Shops
                {
                    id = _shopCoins.Id,
                    name = _shopCoins.Title,
                    item_type = 2,
                    price = _shopCoins.Price,
                    logo = _shopCoins.Logo,
                    is_published = true,
                    date = _shopCoins.CreatedDate,
                    create_date = _shopCoins.CreatedDate,
                };
                finalResult.Add(DataCoins);
            }

            if (finalResult == null)
            {
                return BadRequest("Sorry, we can't find any data");
            }

            List<Shops> listData = finalResult.Cast<Shops>().ToList();
            
            if (!string.IsNullOrEmpty(ItemType))
            {
                listData = listData.Where(i => i.item_type == int.Parse(ItemType)).ToList();
            }
            if((statusGoal == 0 && startupProgram != 0) || startupProgram != 0)
            {
                listData = listData.Where(program => program.programId == startupProgram).ToList();
            }

            if(ProgramGroupId != 0)
            {
                listData = listData.Where(program => program.programGroupId == ProgramGroupId).ToList();
            }

            if(ProgramId != 0 && ProgramGroupId == 0 && ALL == false)
            {
                listData = listData.Where(i => i.programGroupId == 0).ToList();
            }

            if (!string.IsNullOrEmpty(Search))
            {
                string SearchResult = Search.ToLower().Trim();
                listData = listData.Where(o => o.name != null && o.name.ToLower().Trim().Contains(SearchResult) ||
                o.description != null && o.description.ToLower().Trim().Contains(SearchResult) ||
                o.tags != null && o.tags.ToLower().Trim().Contains(SearchResult)).ToList();
            } 

            int TotalPage = listData.Count();

            var listsData = listData.OrderByDescending(i => i.create_date).Skip((Pagination - 1) * ShowEntries).Take(ShowEntries);

            string Results = JsonConvert.SerializeObject(new { listsData, TotalPage }, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
            return Content(Results);
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> DeleteItem(int content_type, int id)
        {
            try
            {
                //delete event
                if (content_type == 0)
                {
                    var events = await _repository.Event.GetItemById(id);
                    var key = investeur_context.Events.Where(i => i.Id == id)?.FirstOrDefault()?.Logo ?? "";
                    var eventData = new Event
                    {
                        Id = id
                    };
                    _repository.Event.DeleteEvent(eventData);
                    await _repository.SaveAsync();
                    await _service.AWSUserImgService.DeleteImageFile(key);
                }
                //delete benefit
                if (content_type == 1)
                {
                    var benefits = await _repository.Benefit.GetItemById(id);
                    var key = investeur_context.Benefits.Where(i => i.Id == id)?.FirstOrDefault()?.Logo ?? "";
                    var benefitData = new Benefit
                    {
                        Id = id
                    };
                    _repository.Benefit.DeleteBenefit(benefitData);
                    await _repository.SaveAsync();
                    await _service.AWSUserImgService.DeleteImageFile(key);
                }
                //delete shop coins
                if (content_type == 2)
                {
                    var shopCoins = await _repository.ShopCoins.GetItemById(id);
                    var key = investeur_context.ShopCoins.Where(i => i.Id == id)?.FirstOrDefault()?.Logo ?? "";
                    var shopCoinsData = new ShopCoins
                    {
                        Id = id
                    };
                    _repository.ShopCoins.Delete(shopCoinsData);
                    await _repository.SaveAsync();
                    await _service.AWSUserImgService.DeleteImageFile(key);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #region  get item detail by id
        [HttpGet]
        public async Task<IActionResult> ShopItemDetail(int content_type, int id)
        {
            try
            {
                //delete event
                if (content_type == 0)
                {
                    var _event = await _repository.Event.GetItemById(id);
                    var level = await _repository.Levels.GetLevelById(_event.AssignToLevel);

                    var DataEvents = new Shops
                    {

                        id = _event.Id,
                        tags = _event.tags ?? "[]",
                        name = _event.EventName ?? "",
                        item_type = 0,
                        action_type = _event.ActionType ?? 0,
                        price = _event.PriceEvent ?? 0,
                        logo = _event.Logo ?? "",
                        description = _event.EventDescription ?? "",
                        id_level = _event.AssignToLevel ?? 1,
                        id_category = _event.AssignToCategory ?? 1,
                        date = _event.EventDateStart,
                        content = _event.EventInfo ?? "",
                        apply_to = _event.ApplyTo ?? 5,
                        event_type = _event.EventType ?? 0,
                        event_address = _event.EventAddress ?? "",
                        event_url = _event.EventUrl ?? "",
                        is_published = _event.IsPublished,
                        create_date = _event.CreateDate,
                        programGroupId = _event.ProgramGroupId
                    };

                    string eventResult = JsonConvert.SerializeObject(DataEvents, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented
                    });

                    return Ok(eventResult);
                }
                //delete benefit
                if (content_type == 1)
                {
                    var benefit = await _repository.Benefit.GetItemById(id);
                    var level = await _repository.Levels.GetLevelById(benefit.AssignToLevel);

                    var DataBenefit = new Shops
                    {
                        id = benefit.Id,
                        tags = benefit.tags ?? "[]",
                        name = benefit.BenefitName ?? "",
                        item_type = 1,
                        action_type = benefit.ActionType ?? 0,
                        price = benefit.PriceBenefit ?? 0,
                        logo = benefit.Logo ?? "",
                        description = benefit.BenefitDescription ?? "",
                        id_level = benefit.AssignToLevel ?? 1,
                        id_category = benefit.AssignToCategory ?? 1,
                        date = null,
                        content = benefit.BenefitInfo ?? "",
                        apply_to = benefit.ApplyTo ?? 5,
                        event_type = 0,
                        event_address = "",
                        event_url = "",
                        is_published = benefit.IsPublished,
                        create_date = benefit.CreateDate
                    };

                    string benefitResult = JsonConvert.SerializeObject(DataBenefit, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented
                    });

                    return Ok(benefitResult);

                }

                if (content_type == 2)
                {
                    var shopCoins = await _repository.ShopCoins.GetItemById(id);

                    var DataCoins = new Shops
                    {
                        id = shopCoins.Id,
                        name = shopCoins.Title,
                        item_type = 2,
                        price = shopCoins.Price,
                        amount = shopCoins.AmountOfCoins,
                        logo = shopCoins.Logo,
                        is_published = shopCoins.IsPublished,
                        create_date = shopCoins.CreatedDate,
                        apply_to = shopCoins.Apply_to
                    };

                    string coinsResult = JsonConvert.SerializeObject(DataCoins, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented
                    });

                    return Ok(coinsResult);
                }

                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region update shop entry
        public async Task<IActionResult> SubmitEditShopEntry(IFormCollection form)
        {
            var Email = Request.Cookies["X-Admin-Email"] ?? String.Empty;

            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.FirstOrDefault();

            try
            {

                if (form.TryGetValue("data", out var Data) && form.TryGetValue("programId", out var ProgramId)
                && form.TryGetValue("item_id", out var ItemId)
                )
                {

                    var data = JsonConvert.DeserializeObject<dynamic>(Data.ToString());
                    int programId = ProgramId != "" ? int.Parse(ProgramId) : 0;

                    if (data?.content_type == 0)
                    {
                        int event_id = ItemId != "" ? int.Parse(ItemId) : 0;
                        var eventData = await _repository.Event.GetItemById(event_id);

                        if (eventData != null)
                        {

                            eventData.EventName = data?.title ?? "";
                            // eventData.Logo = data?.goal_img;
                            eventData.EventDescription = data?.description ?? "";
                            eventData.EventInfo = data?.content ?? "";
                            eventData.ApplyTo = data?.apply_to;
                            eventData.EventDateStart = data?.date;
                            eventData.EventDateEnd = data?.date;
                            eventData.EventUrl = data?.event_url ?? "";
                            eventData.EventType = data?.event_type;
                            eventData.EventAddress = data?.event_address ?? "";
                            eventData.EventPlatform = 6;
                            eventData.AssignToProgram = programId;
                            eventData.EmailNotification = 0;
                            eventData.ActionType = data?.price_type ?? 0;
                            eventData.PriceEvent = data?.price ?? 0;
                            eventData.IsPublished = true;
                            eventData.tags = data?.tags.ToString() ?? "[]";
                            eventData.AssignToLevel = string.IsNullOrEmpty(data?.level_code_number.ToString()) ? 1 : data?.level_code_number ?? 1;
                            eventData.AssignToCategory = string.IsNullOrEmpty(data?.level_id.ToString()) ? 1 : data?.level_id ?? 1;
                            eventData.CreateDate = DateTime.Now;
                            eventData.ProgramGroupId = data?.program_group_id ?? 0;
                            if (file != null)
                            {

                                Regex regex = new Regex("[^a-zA-Z0-9]");
                                var fileName = regex.Replace(file.FileName.ToLower(), "");

                                await _service.AWSUserImgService.UploadImageFile(file);
          
                                eventData.Logo = fileName;
                            }

                            _repository.Event.UpdateEvent(eventData);
                            await _repository.SaveAsync();

                        }

                    }

                    if (data?.content_type == 1)
                    {
                        int benefit_id = ItemId != "" ? int.Parse(ItemId) : 0;
                        var benefit = await _repository.Benefit.GetItemById(benefit_id);

                        if (benefit != null)
                        {

                            benefit.BenefitName = data?.title ?? "";
                            // benefit.Logo = data?.goal_img;
                            benefit.Worth = "";
                            benefit.PackType = 0;
                            benefit.BenefitDescription = data?.description ?? "";
                            benefit.BenefitInfo = data?.content ?? "";
                            benefit.ApplyTo = data?.apply_to;
                            benefit.ActionType = data?.price_type;
                            benefit.PriceBenefit = data?.price;
                            benefit.IsPublished = true;
                            benefit.AssignToProgram = programId;
                            benefit.tags = data?.tags.ToString() ?? "[]";
                            benefit.AssignToLevel = string.IsNullOrEmpty(data?.level_code_number.ToString()) ? 1 : data?.level_code_number ?? 1;
                            benefit.AssignToCategory = string.IsNullOrEmpty(data?.level_id.ToString()) ? 1 : data?.level_id ?? 1;
                            benefit.CreateDate = DateTime.Now;
                            benefit.ProgramGroupId = data?.program_group_id ?? 0;
                            if (file != null)
                            {

                                Regex regex = new Regex("[^a-zA-Z0-9]");
                                var fileName = regex.Replace(file.FileName.ToLower(), "");

                                await _service.AWSUserImgService.UploadImageFile(file);
  
                                benefit.Logo = fileName;
                            }

                            _repository.Benefit.UpdateBenefit(benefit);
                            await _repository.SaveAsync();

                        }

                    }

                    if (data?.content_type == 2)
                    {
                        int shop_coin_id = ItemId != "" ? int.Parse(ItemId) : 0;
                        var shopCoins = await _repository.ShopCoins.GetItemById(shop_coin_id);

                        if (shopCoins != null)
                        {

                            shopCoins.Title = data?.title;
                            shopCoins.Price = data?.coins_price;
                            shopCoins.AmountOfCoins = data?.coins_amount;
                            shopCoins.Apply_to =  data?.apply_to;
                            shopCoins.IsPublished = true;

                            if (file != null)
                            {

                                Regex regex = new Regex("[^a-zA-Z0-9]");
                                var fileName = regex.Replace(file.FileName.ToLower(), "");

                                await _service.AWSUserImgService.UploadImageFile(file);
  
                                shopCoins.Logo = fileName;
                            }

                            _repository.ShopCoins.Update(shopCoins);
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

        #region save shop entry
        public async Task<IActionResult> SaveShopEntry(IFormCollection form)
        {
            var Email = Request.Cookies["X-Admin-Email"] ?? String.Empty;

            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.FirstOrDefault();

            try
            {

                if (form.TryGetValue("data", out var Data) && form.TryGetValue("programId", out var ProgramId))
                {

                    var data = JsonConvert.DeserializeObject<dynamic>(Data.ToString());
                    int programId = ProgramId != "" ? int.Parse(ProgramId) : 0;
                    var key = Guid.NewGuid().ToString().Substring(0, 5);
                    var img = "";
                    if (data?.goal_img != null)
                    {
                        img = Regex.Replace(data?.goal_img.Value, "[^a-zA-Z0-9]", String.Empty);
                    }

                    if (data?.content_type == 0)
                    {
                        var newEvent = new Event
                        {
                            EventName = data?.title ?? "No Title",
                            EventDescription = data?.description ?? "",
                            EventInfo = data?.content ?? "",
                            ApplyTo = string.IsNullOrEmpty(data?.apply_to.ToString()) ? Convert.ToByte(5) : Convert.ToByte(data?.apply_to),
                            EventDateStart = data?.date ?? DateTime.Now,
                            EventDateEnd = data?.date ?? DateTime.Now,
                            EventUrl = data?.event_url ?? "",
                            EventType = string.IsNullOrEmpty(data?.event_type.ToString()) ? Convert.ToByte(1) : Convert.ToByte(data?.event_type),
                            EventAddress = data?.event_address ?? "",
                            EventPlatform = Convert.ToByte(6),
                            AssignToProgram = programId,
                            EmailNotification = Convert.ToByte(0),
                            ActionType = string.IsNullOrEmpty(data?.price_type.ToString()) ? Convert.ToByte(0) : Convert.ToByte(data?.price_type),
                            PriceEvent = data?.price ?? 0,
                            IsPublished = false,
                            tags = data?.tags?.ToString() ?? "[]",
                            AssignToLevel = string.IsNullOrEmpty(data?.level_code_number.ToString()) ? 1 : data?.level_code_number ?? 1,
                            AssignToCategory = string.IsNullOrEmpty(data?.level_id.ToString()) ? 1 : data?.level_id ?? 1,
                            CreateDate = DateTime.Now,
                            ProgramGroupId = data?.program_group_id ?? 0
                        };

                        if (file != null)
                            {

                                Regex regex = new Regex("[^a-zA-Z0-9]");
                                var fileName = regex.Replace(file.FileName.ToLower(), "");

                                await _service.AWSUserImgService.UploadImageFile(file);
  
                                newEvent.Logo = fileName;
                            } else {
                                newEvent.Logo = "";
                            }

                        _repository.Event.CreateEvent(newEvent);
                        await _repository.SaveAsync();

                        return Ok(new
                        {
                            id = newEvent.Id,
                            item_type = 0,
                            name = newEvent.EventName
                        });

                    }

                    if (data?.content_type == 1)
                    {
                        var newBenefit = new Benefit
                        {
                            BenefitName = data?.title ?? "No Title",
                            Worth = "",
                            PackType = Convert.ToByte(0),
                            BenefitDescription = data?.description ?? "",
                            BenefitInfo = data?.content ?? "",
                            ApplyTo = string.IsNullOrEmpty(data?.apply_to.ToString()) ? Convert.ToByte(5) : Convert.ToByte(data?.apply_to),
                            ActionType = string.IsNullOrEmpty(data?.price_type.ToString()) ? Convert.ToByte(0) : Convert.ToByte(data?.price_type),
                            PriceBenefit = data?.price ?? 0,
                            IsPublished = false,
                            AssignToProgram = programId,
                            tags = data?.tags?.ToString() ?? "[]",
                            AssignToLevel = string.IsNullOrEmpty(data?.level_code_number.ToString()) ? 1 : data?.level_code_number ?? 1,
                            AssignToCategory = string.IsNullOrEmpty(data?.level_id.ToString()) ? 1 : data?.level_id ?? 1,
                            CreateDate = DateTime.Now,
                            ProgramGroupId = data?.program_group_id ?? 0
                        };

                        if (file != null)
                            {

                                Regex regex = new Regex("[^a-zA-Z0-9]");
                                var fileName = regex.Replace(file.FileName.ToLower(), "");

                                await _service.AWSUserImgService.UploadImageFile(file);
  
                                newBenefit.Logo = fileName;
                            } else {
                                newBenefit.Logo = "";
                            }

                        _repository.Benefit.CreateBenefit(newBenefit);
                        await _repository.SaveAsync();

                        return Ok(new
                        {
                            id = newBenefit.Id,
                            item_type = 1,
                            name = newBenefit.BenefitName
                        });
                    }

                    if (data?.content_type == 2)
                    {
                        var newShopCoins = new ShopCoins
                        {
                            AmountOfCoins = data?.coins_amount ?? 0,
                            Price = data?.coins_price ?? 0,
                            Title = data?.title ?? "No Title",
                            Apply_to = string.IsNullOrEmpty(data?.apply_to.ToString()) ? Convert.ToByte(5) : Convert.ToByte(data?.apply_to),
                            CreatedDate = DateTime.Now,
                            IsPublished = false
                        };

                        if (file != null)
                        {

                            Regex regex = new Regex("[^a-zA-Z0-9]");
                            var fileName = regex.Replace(file.FileName.ToLower(), "");

                            await _service.AWSUserImgService.UploadImageFile(file);
        
                            newShopCoins.Logo = fileName;
                        } else {
                            newShopCoins.Logo = "";
                        }

                        _repository.ShopCoins.Create(newShopCoins);
                        await _repository.SaveAsync();

                        return Ok(new
                        {
                            id = newShopCoins.Id,
                            item_type = 2,
                            name = newShopCoins.Title
                        });
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

        #region save update shop entry
        public async Task<IActionResult> SaveEditShopEntry(IFormCollection form)
        {
            var Email = Request.Cookies["X-Admin-Email"] ?? String.Empty;

            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.FirstOrDefault();

            try
            {

                if (form.TryGetValue("data", out var Data) && form.TryGetValue("programId", out var ProgramId)
                && form.TryGetValue("item_id", out var ItemId)
                )
                {

                    var data = JsonConvert.DeserializeObject<dynamic>(Data.ToString());
                    int programId = ProgramId != "" ? int.Parse(ProgramId) : 0;

                    if (data?.content_type == 0)
                    {
                        int event_id = ItemId != "" ? int.Parse(ItemId) : 0;
                        var eventData = await _repository.Event.GetItemById(event_id);

                        if (eventData != null)
                        {

                            eventData.EventName = data?.title ?? "No Title";
                            // eventData.Logo = data?.goal_img != null ? key + img : "";
                            eventData.EventDescription = data?.description ?? "";
                            eventData.EventInfo = data?.content ?? "";
                            eventData.ApplyTo = string.IsNullOrEmpty(data?.apply_to.ToString()) ? Convert.ToByte(5) : Convert.ToByte(data?.apply_to);
                            eventData.EventDateStart = data?.date ?? DateTime.Now;
                            eventData.EventDateEnd = data?.date ?? DateTime.Now;
                            eventData.EventUrl = data?.event_url ?? "";
                            eventData.EventType = string.IsNullOrEmpty(data?.event_type.ToString()) ? Convert.ToByte(1) : Convert.ToByte(data?.event_type);
                            eventData.EventAddress = data?.event_address ?? "";
                            eventData.EventPlatform = Convert.ToByte(6);
                            eventData.AssignToProgram = programId;
                            eventData.EmailNotification = Convert.ToByte(0);
                            eventData.ActionType = string.IsNullOrEmpty(data?.price_type.ToString()) ? Convert.ToByte(0) : Convert.ToByte(data?.price_type);
                            eventData.PriceEvent = data?.price ?? 0;
                            eventData.IsPublished = false;
                            eventData.tags = data?.tags?.ToString() ?? "[]";
                            eventData.AssignToLevel = string.IsNullOrEmpty(data?.level_code_number.ToString()) ? 1 : data?.level_code_number == 0 ? 1 : data?.level_code_number;
                            eventData.AssignToCategory = string.IsNullOrEmpty(data?.level_id.ToString()) ? 1 : data?.level_id ?? 1;
                            eventData.CreateDate = DateTime.Now;
                            eventData.ProgramGroupId = data?.program_group_id ?? 0;

                            if (file != null)
                            {

                                Regex regex = new Regex("[^a-zA-Z0-9]");
                                var fileName = regex.Replace(file.FileName.ToLower(), "");

                                await _service.AWSUserImgService.UploadImageFile(file);
  
                                eventData.Logo = fileName;
                            }

                            _repository.Event.UpdateEvent(eventData);
                            await _repository.SaveAsync();

                        }

                    }

                    if (data?.content_type == 1)
                    {
                        int benefit_id = ItemId != "" ? int.Parse(ItemId) : 0;
                        var benefit = await _repository.Benefit.GetItemById(benefit_id);

                        if (benefit != null)
                        {

                            benefit.BenefitName = data?.title ?? "No Title";
                            // benefit.Logo = data?.goal_img != null ? key + img : "";
                            benefit.Worth = "";
                            benefit.PackType = Convert.ToByte(0);
                            benefit.BenefitDescription = data?.description ?? "";
                            benefit.BenefitInfo = data?.content ?? "";
                            benefit.ApplyTo = string.IsNullOrEmpty(data?.apply_to.ToString()) ? Convert.ToByte(5) : Convert.ToByte(data?.apply_to);
                            benefit.ActionType = string.IsNullOrEmpty(data?.price_type.ToString()) ? Convert.ToByte(0) : Convert.ToByte(data?.price_type);
                            benefit.PriceBenefit = data?.price ?? 0;
                            benefit.IsPublished = false;
                            benefit.AssignToProgram = programId;
                            benefit.tags = data?.tags?.ToString() ?? "[]";
                            benefit.AssignToLevel = string.IsNullOrEmpty(data?.level_code_number.ToString()) ? 1 : data?.level_code_number == 0 ? 1 : data?.level_code_number;
                            benefit.AssignToCategory = string.IsNullOrEmpty(data?.level_id.ToString()) ? 1 : data?.level_id ?? 1;
                            benefit.CreateDate = DateTime.Now;
                            benefit.ProgramGroupId = data?.program_group_id ?? 0;

                            if (file != null)
                            {

                                Regex regex = new Regex("[^a-zA-Z0-9]");
                                var fileName = regex.Replace(file.FileName.ToLower(), "");

                                await _service.AWSUserImgService.UploadImageFile(file);
  
                                benefit.Logo = fileName;
                            }

                            _repository.Benefit.UpdateBenefit(benefit);
                            await _repository.SaveAsync();
                        }
                    }

                    if (data?.content_type == 2)
                    {
                        int shop_coin_id = ItemId != "" ? int.Parse(ItemId) : 0;
                        var shopCoins = await _repository.ShopCoins.GetItemById(shop_coin_id);

                        if (shopCoins != null)
                        {

                            shopCoins.Title = data?.title;
                            shopCoins.Price = data?.coins_price;
                            shopCoins.AmountOfCoins = data?.coins_amount;
                            shopCoins.Apply_to =  data?.apply_to;

                            if (file != null)
                            {

                                Regex regex = new Regex("[^a-zA-Z0-9]");
                                var fileName = regex.Replace(file.FileName.ToLower(), "");

                                await _service.AWSUserImgService.UploadImageFile(file);
  
                                shopCoins.Logo = fileName;
                            }

                            _repository.ShopCoins.Update(shopCoins);
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

    }
}
