// using Microsoft.AspNetCore.Mvc;
// using Newtonsoft.Json;
// using TheStartupBuddyV3.Models;
// using TheStartupBuddyV3.Repository;

// namespace TheStartupBuddyV3.Controllers
// {
    
//     [ApiController]
//     [Route("api/[controller]/[action]")]
//     public class GoalsTemplatesController : Controller
//     {
//         private readonly IRepositoryWrapper _repository;

//         [HttpGet]
//         public async Task<IActionResult> GetAllTemplates()
//         {
//             var templates = await _repository.GoalsTemplates.GetAllGoalsTemplates();
//             string jsonTemplates = JsonConvert.SerializeObject(templates, new JsonSerializerSettings
//             {
//                 Formatting = Formatting.Indented
//             });

//             return Content(jsonTemplates);
//         }
//     }
// }
