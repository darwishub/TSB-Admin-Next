using TheStartupBuddyV3.Repository;

namespace TheStartupBuddyV3.Repository
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IProgramRepository Program { get; }
        IUserProfileRepository UserProfile { get; }
        ISubscriptionStartupRepository SubscriptionStartup { get; }
        ISubscriptionInvestorRepository SubscriptionInvestor { get; }
        IInvestorRepository Investor { get; }
        IStartupRepository Startup { get; }
        IStartupProgramRepository StartupProgram { get; }
        IProgramGroupRepository ProgramGroup { get; }
        ICardsRepository Cards { get; }
        IContentRepository Content { get; }
        ICompanyRepository Company { get; }
        IHistoryCompanyStepsRepository HistoryCompanySteps { get; }
        ILevelsRepository Levels { get; }
        IGoalsRepository Goals { get; }
        IGoalsCategoryRepository GoalsCategory { get; }
        IGoalsStepRepository GoalsStep { get; }
        ICompanyGoalsCardFieldsRepository CompanyGoalsCardsField { get; }
        IGoalRepository Goal { get; }
        IStartupVPCRepository StartupVPC { get; }
        IGoalsCardFieldsRepository GoalsCard { get; }
        IStepsRepository Steps { get; }
        ICompanyGoalsRepository CompanyGoals { get; }
        ICompanyGoalsStepRepository CompanyGoalsStep { get; }
        IGoalsTemplatesRepository GoalsTemplates { get; }
        IAdminUsersRepository AdminUsers { get; }
        IAspNetUsersRepository AspNetUsers { get; }
        INetworkingConnectRepository NetworkingConnect { get; }
        INotificationsRepository Notifications { get; }
        IStartupMemberRepository StartupMember { get; }
        IEventRepository Event { get; }
        IBenefitRepository Benefit { get; }
        IToolkitLearningRepository ToolkitLearning { get; }
        ISessionRatingRepository SessionRating { get; }
        ICoinHistoryRepository CoinHistory { get; }
        IShopCoinsRepository ShopCoins { get; }
        Task SaveAsync();

    }
}
