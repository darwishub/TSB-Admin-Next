using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections;
using System.Text.RegularExpressions;
using TheStartupBuddyV3.Helpers;
using TheStartupBuddyV3.Models;
using TheStartupBuddyV3.Repository;
using TheStartupBuddyV3.Service;
using NuGet.Common;

namespace TheStartupBuddyV3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AcademyController : Controller
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        private readonly IRepositoryWrapper _repository;
        private readonly IServiceWrapper _service;
        private readonly IHelpers _helpers;

        public AcademyController(
        IRepositoryWrapper repository,
        IServiceWrapper service,
        IHelpers helpers
        )
        {
            _repository = repository;
            _service = service;
            _helpers = helpers;
        }

        #region submit academy data entry
        public async Task<IActionResult> SubmitAcademyEntry(IFormCollection form)
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
                    var img = Regex.Replace(data?.goal_img.Value, "[^a-zA-Z0-9]", String.Empty);

                    var newAcademy = new ToolkitLearning
                    {
                        Title = data?.title ?? "",
                        Description = data?.description ?? "",
                        Url = data?.url ?? "",
                        Tag = data?.tags.ToString() ?? "[]",
                        Exclusive = data?.exclusive ?? 0,
                        ProgramId = programId,
                        ContentType = data?.content_type ?? "",
                        IsPublished = true,
                        ContentArticle = data?.content ?? "",
                        Rewards = data?.rewards ?? 0,
                        IdLevel = string.IsNullOrEmpty(data?.level_code_number.ToString()) ? 1 : data?.level_code_number ?? 1,
                        IdCategory = string.IsNullOrEmpty(data?.level_id.ToString()) ? 1 : data?.level_id ?? 1,
                        DateCreated = DateTime.Now,
                        ProgramGroupId = data?.program_group_id ?? 0
                    };

                    if (data?.content_type == 0)
                    {
                        newAcademy.Type = "video";
                    }
                    else
                    {
                        newAcademy.Type = "article";
                    }

                    if (file != null)
                    {
                        Regex regex = new Regex("[^a-zA-Z0-9]");
                        var fileName = regex.Replace(file.FileName.ToLower(), "");

                        await _service.AWSUserImgService.UploadImageFile(file);

                        newAcademy.Logo = fileName;
                    }

                    _repository.ToolkitLearning.Create(newAcademy);
                    await _repository.SaveAsync();

                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message, confirm = true });
            }

        }
        #endregion

        #region  get all list academy
        [HttpGet]
        public async Task<IActionResult> AllAcademyItems(string? ItemType, int ProgramId, string? Search, int ShowEntries, int Pagination,
        int programGroupId, bool ALL)
        {
            string? userId = Request.Cookies["X-Id"] ?? String.Empty;
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
                programGroupId = 0;
                statusGoal = 0;
            }

            var _academy = await _repository.ToolkitLearning.GetAllAcademy();

            if ((statusGoal == 0 && startupProgram != 0) || startupProgram != 0)
            {
                _academy = _academy.Where(program => program.ProgramId == startupProgram);
            }

            if (programGroupId != 0)
            {
                _academy = _academy.Where(program => program.ProgramGroupId == programGroupId);
            }
            if (ProgramId != 0 && programGroupId == 0 && ALL == false)
            {
                _academy = _academy.Where(o => o.ProgramGroupId == 0);
            }

            if (!string.IsNullOrEmpty(ItemType))
            {
                _academy = _academy.Where(i => i.ContentType == int.Parse(ItemType)).ToList();
            }
            #region search by title and description
            if (!string.IsNullOrEmpty(Search))
            {
                string SearchResult = Search.ToLower().Trim();
                _academy = _academy.Where(o => o.Title != null && o.Title.ToLower().Trim().Contains(SearchResult) ||
                o.Description != null && o.Description.ToLower().Trim().Contains(SearchResult));
            }
            #endregion

            int TotalPage = _academy.Count();
            var listsData = _academy.OrderByDescending(i => i.Id).Skip((Pagination - 1) * ShowEntries).Take(ShowEntries);

            string Results = JsonConvert.SerializeObject(new { listsData, TotalPage }, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
            return Content(Results);
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                var _academy = await _repository.ToolkitLearning.GetItemById(id);
                var key = investeur_context.ToolkitLearnings.Where(i => i.Id == id)?.FirstOrDefault()?.Logo ?? "";
                _repository.ToolkitLearning.Delete(_academy);
                await _repository.SaveAsync();
                await _service.AWSUserImgService.DeleteImageFile(key);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #region  get item detail by id
        [HttpGet]
        public async Task<IActionResult> AcademyItemDetail(int id)
        {
            try
            {
                var _academy = await _repository.ToolkitLearning.GetDetailsItem(id);

                string academyResult = JsonConvert.SerializeObject(_academy, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                });

                return Ok(academyResult);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region update shop entry
        public async Task<IActionResult> SubmitEditAcademyEntry(IFormCollection form)
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

                    int Id = ItemId != "" ? int.Parse(ItemId) : 0;
                    var _academyData = await _repository.ToolkitLearning.GetItemById(Id);

                    if (_academyData != null)
                    {
                        _academyData.Title = data?.title ?? "";
                        _academyData.Description = data?.description ?? "";
                        _academyData.Url = data?.Url ?? "";
                        _academyData.Tag = data?.tags.ToString() ?? "[]";
                        _academyData.Exclusive = data?.exclusive ?? 0;
                        _academyData.ProgramId = programId;
                        _academyData.ContentType = data?.content_type ?? "";
                        _academyData.IsPublished = true;
                        _academyData.ContentArticle = data?.content ?? "";
                        _academyData.Rewards = data?.rewards ?? 0;
                        _academyData.IdLevel = string.IsNullOrEmpty(data?.level_code_number.ToString()) ? 1 : data?.level_code_number ?? 1;
                        _academyData.DateCreated = data?.date;
                        _academyData.ProgramGroupId = data?.program_group_id ?? 0;
                        if (file != null)
                        {
                            Regex regex = new Regex("[^a-zA-Z0-9]");
                            var fileName = regex.Replace(file.FileName.ToLower(), "");

                            await _service.AWSUserImgService.UploadImageFile(file);
                            _academyData.Logo = fileName;
                        }
                        _repository.ToolkitLearning.Update(_academyData);
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

    }
}
