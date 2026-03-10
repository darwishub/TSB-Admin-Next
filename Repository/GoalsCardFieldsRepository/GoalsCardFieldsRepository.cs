using TheStartupBuddyV3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;

namespace TheStartupBuddyV3.Repository
{
    public class GoalsCardFieldsRepository : RepositoryBase<GoalsCardFields>, IGoalsCardFieldsRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public GoalsCardFieldsRepository(InvesteurContext _context) : base(_context)
        {
        }

        public async Task<GoalsCardFields> GetGoalCardByIdAsync(int cardId)
        {
            return await GetByCondition(card => card.IdCard.Equals(cardId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GoalsCardFields>> GetGoalCardByIdStep(int idStep)
        {
            var query = await (from _goalCards in investeur_context.GoalsCardFields.AsNoTracking()
                        where _goalCards.IdStep == idStep
                        select _goalCards).ToListAsync();

            return query;
        }

        #region get all data of goal card field
        public async Task<IEnumerable<GoalsCardFields>> GetAllData(int? currentGoalId)
        {
            var query = await (from _card in investeur_context.GoalsCardFields.AsNoTracking()
                                join _goalStep in investeur_context.GoalsSteps.AsNoTracking()
                                on _card.IdStep equals _goalStep.IdStep
                                join _goal in investeur_context.Goals.AsNoTracking()
                                on _goalStep.IdGoal equals _goal.IdGoal
                                where _card.FieldCode > 0 && _goal.IsPublished == true
                               select new GoalsCardFields
                               {
                                    IdCard = _card.IdCard,
                                    IdStep = _card.IdStep,
                                    help_text = _card.help_text,
                                    Title = _card.Title,
                                    Description = _card.Description,
                                    FieldCode = _card.FieldCode,
                                    FieldCodeType = _card.FieldCodeType,
                                    HasOption = _card.HasOption,
                                    Option = _card.Option,
                                    DisplayOrder = _card.DisplayOrder,
                                    SuccessMessage = _card.SuccessMessage,
                                    Placeholder = _card.Placeholder,
                                    Type = _card.Type,
                                    TypeRule = _card.TypeRule,
                                    Label = _card.Label,
                                    Value = _card.Value,
                                    IsRequired = _card.IsRequired,
                                    IsDisabled = _card.IsDisabled,
                                    MaxData = _card.MaxData,
                                    AllowMultiple = _card.AllowMultiple,
                                    FilesType = _card.FilesType,
                                    TextButton = _card.TextButton,
                                    QuestionID = _card.QuestionID
                               }).ToListAsync();

            List<GoalsCardFields> list = new List<GoalsCardFields>();
            foreach (var item in from item in query
                                 where item.IdCard == item.QuestionID
                                 select item)
                    {
                        var Goal = await (from _goal in investeur_context.Goals.AsNoTracking()
                                    join _goalStep in investeur_context.GoalsSteps.AsNoTracking()
                                    on _goal.IdGoal equals _goalStep.IdGoal
                                    join _goalCardField in investeur_context.GoalsCardFields.AsNoTracking()
                                    on _goalStep.IdStep equals _goalCardField.IdStep
                                    where _goal.IdGoal == currentGoalId && _goalCardField.QuestionID == item.QuestionID
                                    select _goalCardField).ToListAsync();

                        if(Goal.Count() == 0 || Goal == null)
                        {
                            list.Add(item);
                        }
                    }

            return list;
        }
        #endregion

        public void CreateGoalCard(GoalsCardFields goal_card)
        {
            Create(goal_card);
        }
        public void UpdateGoalCard(GoalsCardFields goal_card)
        {
            Update(goal_card);
        }
        public void DeleteGoalCard(GoalsCardFields goal_card)
        {
            Delete(goal_card);
        }

        #region get all data to check last record
        public async Task<IEnumerable<GoalsCardFields>> GetAllDataRecord()
        {
            var query = await (from _card in investeur_context.GoalsCardFields.AsNoTracking()
                               select new GoalsCardFields
                               {
                                    IdCard = _card.IdCard,
                                    IdStep = _card.IdStep,
                                    help_text = _card.help_text,
                                    Title = _card.Title,
                                    Description = _card.Description,
                                    FieldCode = _card.FieldCode,
                                    FieldCodeType = _card.FieldCodeType,
                                    HasOption = _card.HasOption,
                                    Option = _card.Option,
                                    DisplayOrder = _card.DisplayOrder,
                                    SuccessMessage = _card.SuccessMessage,
                                    Placeholder = _card.Placeholder,
                                    Type = _card.Type,
                                    TypeRule = _card.TypeRule,
                                    Label = _card.Label,
                                    Value = _card.Value,
                                    IsRequired = _card.IsRequired,
                                    IsDisabled = _card.IsDisabled,
                                    MaxData = _card.MaxData,
                                    AllowMultiple = _card.AllowMultiple,
                                    FilesType = _card.FilesType,
                                    TextButton = _card.TextButton,
                                    QuestionID = _card.QuestionID
                               }).ToListAsync();

            return query;
        }
        #endregion

    }
}
