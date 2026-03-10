using Microsoft.EntityFrameworkCore;
using TheStartupBuddyV3.Models;

namespace TheStartupBuddyV3.Repository
{
    public class GoalsStepRepository : RepositoryBase<GoalsStep>, IGoalsStepRepository
    {
        private InvesteurContext investeur_context = new InvesteurContext();
        public GoalsStepRepository(InvesteurContext context) : base(context)
        {
        }

        public async Task<IEnumerable<GoalsStepModel>> GetTotalSteps(int ProgramId, bool statusGoal)
        {
            if(statusGoal == true)
            {
                var query = await (from _query in investeur_context.GoalsSteps.AsNoTracking()
                        join _goal in investeur_context.Goals.AsNoTracking()
                        on _query.IdGoal equals _goal.IdGoal into _goalGroup
                        from _goal in _goalGroup.DefaultIfEmpty()
                        where (_goal.ProgramId == ProgramId || _goal.ProgramId == 0) && _goal.IsPublished == true
                         select new GoalsStepModel
                         {
                             IdGoalStep = _query.IdGoalStep,
                             ProgramId = _goal.ProgramId,
                             ProgramGroupId = _goal.ProgramGroupId

                         }).ToListAsync();
                return query;
            }
            else
            {
                var query = await (from _query in investeur_context.GoalsSteps.AsNoTracking()
                        join _goal in investeur_context.Goals.AsNoTracking()
                        on _query.IdGoal equals _goal.IdGoal into _goalGroup
                        from _goal in _goalGroup.DefaultIfEmpty()
                        where _goal.ProgramId == ProgramId && _goal.IsPublished == true
                         select new GoalsStepModel
                         {
                             IdGoalStep = _query.IdGoalStep,
                             ProgramId = _goal.ProgramId,
                             ProgramGroupId = _goal.ProgramGroupId

                         }).ToListAsync();
                return query;
            }
        }

        public async Task<IEnumerable<GoalsStep>> GetAllStepsByGoalIdAsync(int? goalId)
        {
            var query = await (from gs in investeur_context.GoalsSteps.AsNoTracking()
                        where gs.IdGoal == goalId
                        select new GoalsStep {
                            IdGoal = gs.IdGoal,
                            IdStep = gs.IdStep
                        }).ToListAsync();

            return query;
        }

        public async Task<GoalsStep> GetGoalsStepByIdAsync(int stepId)
        {
            return await GetByCondition(step => step.IdStep.Equals(stepId)).FirstOrDefaultAsync();
        }

        public void CreateGoalStep(GoalsStep goal_step)
        {
            Create(goal_step);
        }
        public void UpdateGoalStep(GoalsStep goal_step)
        {
            Update(goal_step);
        }
        public void DeleteGoalStep(GoalsStep goal_step)
        {
            Delete(goal_step);
        }

        #region get all step
        public async Task<IEnumerable<GoalsStep>> GetAllStepsAsync(int? programid, bool? goalBasics)
        {
            if(programid == null || programid == 0)
            {
                var query = await (from _goal in investeur_context.Goals.AsNoTracking()
                                join _goalStep in investeur_context.GoalsSteps.AsNoTracking()
                                on _goal.IdGoal equals _goalStep.IdGoal
                                where _goal.ProgramId == 0 && _goal.IsPublished == true
                               select new GoalsStep
                               {
                                    IdGoalStep = _goalStep.IdGoalStep,
                                    IdGoal = _goal.IdGoal
                               }).ToListAsync();
                return query;    
            }  
            else
            {
                if(goalBasics == true)
                {
                    var query = await (from _goal in investeur_context.Goals.AsNoTracking()
                                join _goalStep in investeur_context.GoalsSteps.AsNoTracking()
                                on _goal.IdGoal equals _goalStep.IdGoal
                                where (_goal.ProgramId == programid || _goal.ProgramId == 0) && _goal.IsPublished == true
                               select new GoalsStep
                               {
                                    IdGoalStep = _goalStep.IdGoalStep,
                                    IdGoal = _goal.IdGoal
                               }).ToListAsync();
                    return query; 
                }   
                else
                {
                    var query = await (from _goal in investeur_context.Goals.AsNoTracking()
                                join _goalStep in investeur_context.GoalsSteps.AsNoTracking()
                                on _goal.IdGoal equals _goalStep.IdGoal
                                where _goal.ProgramId == programid && _goal.IsPublished == true
                               select new GoalsStep
                               {
                                    IdGoalStep = _goalStep.IdGoalStep,
                                    IdGoal = _goal.IdGoal
                               }).ToListAsync();
                    return query; 
                }
            } 
        }
        #endregion
    }
}
