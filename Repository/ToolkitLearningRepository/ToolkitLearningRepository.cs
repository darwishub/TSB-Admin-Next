using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class ToolkitLearningRepository : RepositoryBase<ToolkitLearning>, IToolkitLearningRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public ToolkitLearningRepository(InvesteurContext _context) : base(_context)
        {
        }
        public async Task<IEnumerable<ToolkitLearningModel>> GetAllAcademy()
        {
            var query = await (from _academy in investeur_context.ToolkitLearnings.AsNoTracking()
                         join _level in investeur_context.Levels.AsNoTracking() 
                         on _academy.IdLevel equals _level.IdLevel
                         join _category in investeur_context.GoalsCategories.AsNoTracking()
                         on _level.IdGoalsCategory equals _category.IdGoalsCategory
                         select new ToolkitLearningModel
                         {
                            Id = _academy.Id,
                            Title = _academy.Title,
                            Description = _academy.Description,
                            Url = _academy.Url,
                            Tag = _academy.Tag,
                            Exclusive = _academy.Exclusive,
                            ProgramId = _academy.ProgramId,
                            ContentType = _academy.ContentType,
                            ContentArticle = _academy.ContentArticle,
                            IdLevel = _academy.IdLevel,
                            Logo = _academy.Logo,
                            IsPublished = _academy.IsPublished,
                            Rewards = _academy.Rewards,
                            DateCreated = _academy.DateCreated,
                            CategoryLogo = _category.LogoCategory,
                            ProgramGroupId = _academy.ProgramGroupId
                         }).ToListAsync();

            return query;
        }

        public async Task<ToolkitLearningModel> GetDetailsItem(int? Id)
        {
            var query = await (from _academy in investeur_context.ToolkitLearnings.AsNoTracking()
                         join _level in investeur_context.Levels.AsNoTracking()
                         on _academy.IdLevel equals _level.IdLevel
                         where _academy.Id == Id
                         select new ToolkitLearningModel
                         {
                            Id = _academy.Id,
                            Title = _academy.Title,
                            Description = _academy.Description,
                            Url = _academy.Url,
                            Tag = _academy.Tag,
                            Exclusive = _academy.Exclusive,
                            ProgramId = _academy.ProgramId,
                            ContentType = _academy.ContentType,
                            ContentArticle = _academy.ContentArticle,
                            IdLevel = _level.CodeLevel,
                            Logo = _academy.Logo,
                            IsPublished = _academy.IsPublished,
                            Rewards = _academy.Rewards,
                            DateCreated = _academy.DateCreated,
                            IdCategory = _level.IdLevel,
                            ProgramGroupId = _academy.ProgramGroupId
                         }).FirstOrDefaultAsync();

            return query;
        }

        public async Task<ToolkitLearning> GetItemById(int? Id)
        {
            return await GetByCondition(data => data.Id == Id).FirstOrDefaultAsync();
        }
    }
}
