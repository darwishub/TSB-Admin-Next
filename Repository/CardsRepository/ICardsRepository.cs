using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public interface ICardsRepository : IRepositoryBase<Cards>
    {
        Task<IEnumerable<CardsStep>> GetCardsByStep(int? id_step);
        IEnumerable<GoalDetailsCards> GetCardsDetailByStep(int? id_step);
        void CreateCards(Cards Cards);
        void UpdateCards(Cards Cards);
        void DeleteCards(Cards Cards);
    }
}
