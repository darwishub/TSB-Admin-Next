using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface IToolkitLearningRepository : IRepositoryBase<ToolkitLearning>
    {
        Task<IEnumerable<ToolkitLearningModel>> GetAllAcademy();
        Task<ToolkitLearning> GetItemById(int? Id);
        Task<ToolkitLearningModel> GetDetailsItem(int? Id);
    }
}
