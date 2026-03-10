using System.Collections;
using TheStartupBuddyV3.Models;
using Newtonsoft.Json;

namespace TheStartupBuddyV3.Repository
{
    public class CardsRepository : RepositoryBase<Cards>, ICardsRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public CardsRepository(InvesteurContext context) : base(context)
        {
        }

        public IEnumerable<GoalDetailsCards> GetCardsDetailByStep(int? id_step)
        {
            var cards = (from goals_steps_card in investeur_context.GoalsCardFields
                         where goals_steps_card.IdStep == id_step
                         select new
                         {
                             id_card = goals_steps_card.IdCard,
                             help_text = goals_steps_card.help_text,
                             card_title = goals_steps_card.Title,
                             card_description = goals_steps_card.Description,
                             field_code = goals_steps_card.FieldCode,
                             field_code_type = goals_steps_card.FieldCodeType,
                             placeholder = goals_steps_card.Placeholder,
                             text_button = goals_steps_card.TextButton,
                             type = goals_steps_card.Type,
                             type_rule = goals_steps_card.TypeRule,
                             label = goals_steps_card.Label,
                             value = goals_steps_card.Value,
                             required = goals_steps_card.IsRequired,
                             disabled = goals_steps_card.IsDisabled,
                             max_data = goals_steps_card.MaxData,
                             allow_multiple = goals_steps_card.AllowMultiple,
                             files_type = goals_steps_card.FilesType,
                             has_option = goals_steps_card.HasOption,
                             option = goals_steps_card.Option,
                             display_order = goals_steps_card.DisplayOrder,
                             success_message = goals_steps_card.SuccessMessage,
                             score = goals_steps_card.Score,
                             visible = goals_steps_card.Visible
                         }).Where(i => i.visible == true).ToList();

            List<GoalDetailsCards> CardsList = new List<GoalDetailsCards>();

            foreach (var card in cards)
            {
                GoalDetailsCards CardsQuery = new GoalDetailsCards();
                GoalDetailsSchema Schema = new GoalDetailsSchema();
                GoalDetailsProps Props = new GoalDetailsProps();

                CardsQuery.id = card.id_card;
                CardsQuery.help_text = card.help_text;
                CardsQuery.title = card.card_title;
                CardsQuery.description = card.card_description;

                Props.placeholder = card.placeholder;
                Props.type = card.type;
                Props.type_rule = card.type_rule;
                Props.label = card.label;
                Props.value = card.value;
                Props.required = card.required;
                Props.disabled = card.disabled;
                Props.max_data = card.max_data;
                Props.allow_multiple = card.allow_multiple;
                Props.text_button = card.text_button;
                Props.files_type = JsonConvert.DeserializeObject<string[]>(card.files_type);
                Props.score = card.score;

                Schema.field_code = card.field_code;
                Schema.field_code_type = card.field_code_type;
                Schema.has_option = card.has_option;
                Schema.option = JsonConvert.DeserializeObject<string[]>(card.option);
                Schema.display_order = card.display_order;
                Schema.success_message = card.success_message;
                Schema.props = Props;

                CardsQuery.schema = Schema;

                CardsList.Add(CardsQuery);

            }

            return CardsList;
        }

        public async Task<IEnumerable<CardsStep>> GetCardsByStep(int? id_step)
        {
            ArrayList optionsResults = new ArrayList();
            var cards = (from _cards in investeur_context.Cards
                         where _cards.id_step == id_step
                         select new
                         {
                             id_card = _cards.id_card,
                             id_step = _cards.id_step,
                             help_text = _cards.help_text,
                             title = _cards.title,
                             description = _cards.description,
                             field_code = _cards.field_code,
                             field_code_type = _cards.field_code_type,
                             has_option = _cards.has_option,
                             placeholder = _cards.placeholder,
                             type = _cards.type,
                             type_rule = _cards.type_rule,
                             label = _cards.label,
                             value = _cards.value,
                             required = _cards.required,
                             disabled = _cards.disabled,
                             max_data = _cards.max_data,
                             allow_multiple = _cards.allow_multiple,
                             options = _cards.options,

                         }).ToList();
            if (cards.Count() > 1)
            {
                List<CardsStep> result2 = new List<CardsStep>();
                foreach (var card in cards)
                {
                    var props = (from _prop in cards
                                 where _prop.id_card == card.id_card
                                 select new Props
                                 {
                                     placeholder = _prop.placeholder,
                                     type = _prop.type,
                                     type_rule = _prop.type_rule,
                                     label = _prop.label,
                                     value = _prop.value,
                                     required = _prop.required,
                                     disabled = _prop.disabled,
                                     max_data = _prop.max_data,
                                     allow_multiple = _prop.allow_multiple,
                                 }).ToList().FirstOrDefault();

                    var schemas = (from _schema in cards
                                   where _schema.id_card == card.id_card
                                   select new Schema
                                   {
                                       field_code = _schema.field_code,
                                       field_code_type = _schema.field_code_type,
                                       props = props,
                                       has_option = _schema.has_option,
                                       options = _schema.options,
                                   }).ToList().FirstOrDefault();

                    var result = new CardsStep();
                    foreach (var _card in cards)
                    {
                        result.id = _card.id_card;
                        result.id_step = _card.id_step;
                        result.help_text = _card.help_text;
                        result.title = _card.title;
                        result.description = _card.description;
                        result.schema = schemas;
                    }

                    result2.Add(result);

                }
                return result2;
            }
            else
            {
                List<CardsStep> result2 = new List<CardsStep>();
                var props = (from _prop in cards
                             select new Props
                             {
                                 placeholder = _prop.placeholder,
                                 type = _prop.type,
                                 type_rule = _prop.type_rule,
                                 label = _prop.label,
                                 value = _prop.value,
                                 required = _prop.required,
                                 disabled = _prop.disabled,
                                 max_data = _prop.max_data,
                                 allow_multiple = _prop.allow_multiple,
                             }).ToList().FirstOrDefault();

                var schemas = (from _schema in cards
                               select new Schema
                               {
                                   field_code = _schema.field_code,
                                   field_code_type = _schema.field_code_type,
                                   props = props,
                                   has_option = _schema.has_option,
                                   options = _schema.options,
                               }).ToList().FirstOrDefault();

                var result = new CardsStep();
                foreach (var _card in cards)
                {
                    result.id = _card.id_card;
                    result.id_step = _card.id_step;
                    result.help_text = _card.help_text;
                    result.title = _card.title;
                    result.description = _card.description;
                    result.schema = schemas;
                }
                result2.Add(result);
                return result2;
            }

        }

        public void CreateCards(Cards Cards)
        {
            Create(Cards);
        }
        public void UpdateCards(Cards Cards)
        {
            Update(Cards);
        }
        public void DeleteCards(Cards Cards)
        {
            Delete(Cards);
        }
    }
}
