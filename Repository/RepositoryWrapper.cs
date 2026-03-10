using TheStartupBuddyV3.Models;
using TheStartupBuddyV3.Repository;

namespace TheStartupBuddyV3.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly InvesteurContext _context;
        private IUserRepository? _user;
        private IProgramRepository? _program;
        private IUserProfileRepository? _userProfile;
        private ISubscriptionStartupRepository? _subscriptionStartup;
        private ISubscriptionInvestorRepository? _subscriptionInvestor;
        private IInvestorRepository? _investor;
        private IStartupRepository? _startup;
        private IStartupProgramRepository? _startupProgram;
        private IProgramGroupRepository? _programGroup;
        private ICardsRepository? _cards;
        private IContentRepository? _content;
        private ICompanyRepository? _company;
        private IHistoryCompanyStepsRepository? _historyCompanySteps;
        private ILevelsRepository? _levels;
        private IGoalsRepository? _goals;
        private IGoalsCategoryRepository? _goalsCategory;
        private IStepsRepository? _steps;
        private ICompanyGoalsRepository? _companyGoals;
        private ICompanyGoalsStepRepository? _companyGoalsStep;
        private ICompanyGoalsCardFieldsRepository _companyGoalsCardsField;
        private IGoalsStepRepository? _goalsStep;
        private IGoalsCardFieldsRepository? _goalsCard;
        private IGoalRepository? _goal;
        private IGoalsTemplatesRepository? _goalsTemplates;
        private IAdminUsersRepository? _adminUsers;
        private IAspNetUsersRepository? _aspNetUsers;
        private INetworkingConnectRepository? _networkingConnect;
        private INotificationsRepository? _notifications;
        private IStartupMemberRepository? _startupMember;
        private IEventRepository? _event;
        private IBenefitRepository? _benefit;
        private IToolkitLearningRepository? _toolkitLearning;
        private IStartupVPCRepository _startupVPC;
        private ISessionRatingRepository? _sessionRating;
        private ICoinHistoryRepository? _coinsHistory;
        private IShopCoinsRepository _shopCoins;
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context);
                }
                return _user;
            }
        }

        public IProgramRepository Program
        {
            get
            {
                if (_program == null)
                {
                    _program = new ProgramRepository(_context);
                }
                return _program;
            }
        }

        public IUserProfileRepository UserProfile
        {
            get
            {
                if (_userProfile == null)
                {
                    _userProfile = new UserProfileRepository(_context);
                }
                return _userProfile;
            }
        }

        public ISubscriptionStartupRepository SubscriptionStartup
        {
            get
            {
                if (_subscriptionStartup == null)
                {
                    _subscriptionStartup = new SubscriptionStartupRepository(_context);
                }
                return _subscriptionStartup;
            }
        }
        public IStartupVPCRepository StartupVPC
        {
            get
            {
                if (_startupVPC == null)
                {
                    _startupVPC = new StartupVPCRepository(_context);
                }
                return _startupVPC;
            }
        }
        public ISubscriptionInvestorRepository SubscriptionInvestor
        {
            get
            {
                if (_subscriptionInvestor == null)
                {
                    _subscriptionInvestor = new SubscriptionInvestorRepository(_context);
                }
                return _subscriptionInvestor;
            }
        }

        public IInvestorRepository Investor
        {
            get
            {
                if (_investor == null)
                {
                    _investor = new InvestorRepository(_context);
                }
                return _investor;
            }
        }

        public IStartupRepository Startup
        {
            get
            {
                if (_startup == null)
                {
                    _startup = new StartupRepository(_context);
                }
                return _startup;
            }
        }

        public IStartupProgramRepository StartupProgram
        {
            get
            {
                if (_startupProgram == null)
                {
                    _startupProgram = new StartupProgramRepository(_context);
                }
                return _startupProgram;
            }
        }

        public IProgramGroupRepository ProgramGroup
        {
            get
            {
                if (_programGroup == null)
                {
                    _programGroup = new ProgramGroupRepository(_context);
                }
                return _programGroup;
            }
        }

        public ICardsRepository Cards
        {
            get
            {
                if (_cards == null)
                {
                    _cards = new CardsRepository(_context);
                }
                return _cards;
            }
        }

        public IContentRepository Content
        {
            get
            {
                if (_content == null)
                {
                    _content = new ContentRepository(_context);
                }
                return _content;
            }
        }

        public ICompanyRepository Company
        {
            get
            {
                if (_company == null)
                {
                    _company = new CompanyRepository(_context);
                }
                return _company;
            }
        }

        public IHistoryCompanyStepsRepository HistoryCompanySteps
        {
            get
            {
                if (_historyCompanySteps == null)
                {
                    _historyCompanySteps = new HistoryCompanyStepsRepository(_context);
                }
                return _historyCompanySteps;
            }
        }

        public ILevelsRepository Levels
        {
            get
            {
                if (_levels == null)
                {
                    _levels = new LevelsRepository(_context);
                }
                return _levels;
            }
        }

        public IGoalsRepository Goals
        {
            get
            {
                if (_goals == null)
                {
                    _goals = new GoalsRepository(_context);
                }
                return _goals;
            }
        }

        public IGoalsCategoryRepository GoalsCategory
        {
            get
            {
                if (_goalsCategory == null)
                {
                    _goalsCategory = new GoalsCategoryRepository(_context);
                }
                return _goalsCategory;
            }
        }
        

        public IGoalsCardFieldsRepository GoalsCard
        {
            get
            {
                if (_goalsCard == null)
                {
                    _goalsCard = new GoalsCardFieldsRepository(_context);
                }
                return _goalsCard;
            }
        }

        public IGoalRepository Goal
        {
            get
            {
                if (_goal == null)
                {
                    _goal = new GoalRepository(_context);
                }
                return _goal;
            }
        }

        public IStepsRepository Steps
        {
            get
            {
                if (_steps == null)
                {
                    _steps = new StepsRepository(_context);
                }
                return _steps;
            }
        }

        public ICompanyGoalsRepository CompanyGoals
        {
            get
            {
                if (_companyGoals == null)
                {
                    _companyGoals = new CompanyGoalsRepository(_context);
                }
                return _companyGoals;
            }
        }

        public ICompanyGoalsStepRepository CompanyGoalsStep
        {
            get
            {
                if (_companyGoalsStep == null)
                {
                    _companyGoalsStep = new CompanyGoalsStepRepository(_context);
                }
                return _companyGoalsStep;
            }
        }

        public IGoalsStepRepository GoalsStep
        {
            get
            {
                if (_goalsStep == null)
                {
                    _goalsStep = new GoalsStepRepository(_context);
                }
                return _goalsStep;
            }
        }

        public ICompanyGoalsCardFieldsRepository CompanyGoalsCardsField
        {
            get
            {
                if (_companyGoalsCardsField == null)
                {
                    _companyGoalsCardsField = new CompanyGoalsCardFieldsRepository(_context);
                }
                return _companyGoalsCardsField;
            }
        }

        public IGoalsTemplatesRepository GoalsTemplates
        {
            get
            {
                if (_goalsTemplates == null)
                {
                    _goalsTemplates = new GoalsTemplatesRepository(_context);
                }
                return _goalsTemplates;
            }
        }

        public IAdminUsersRepository AdminUsers
        {
            get
            {
                if (_adminUsers == null)
                {
                    _adminUsers = new AdminUsersRepository(_context);
                }
                return _adminUsers;
            }
        }

        public IAspNetUsersRepository AspNetUsers
        {
            get
            {
                if (_aspNetUsers == null)
                {
                    _aspNetUsers = new AspNetUsersRepository(_context);
                }
                return _aspNetUsers;
            }
        }

        public INetworkingConnectRepository NetworkingConnect
        {
            get
            {
                if (_networkingConnect == null)
                {
                    _networkingConnect = new NetworkingConnectRepository(_context);
                }
                return _networkingConnect;
            }
        }

        public INotificationsRepository Notifications
        {
            get
            {
                if (_notifications == null)
                {
                    _notifications = new NotificationsRepository(_context);
                }
                return _notifications;
            }
        }

        public IStartupMemberRepository StartupMember
        {
            get
            {
                if (_startupMember == null)
                {
                    _startupMember = new StartupMemberRepository(_context);
                }
                return _startupMember;
            }
        }

        public IEventRepository Event
        {
            get
            {
                if (_event == null)
                {
                    _event = new EventRepository(_context);
                }
                return _event;
            }
        }

        public IBenefitRepository Benefit
        {
            get
            {
                if (_benefit == null)
                {
                    _benefit = new BenefitRepository(_context);
                }
                return _benefit;
            }
        }

        public IToolkitLearningRepository ToolkitLearning
        {
            get
            {
                if (_toolkitLearning == null)
                {
                    _toolkitLearning = new ToolkitLearningRepository(_context);
                }
                return _toolkitLearning;
            }
        }

        public ISessionRatingRepository SessionRating
        {
            get
            {
                if (_sessionRating == null)
                {
                    _sessionRating = new SessionRatingRepository(_context);
                }
                return _sessionRating;
            }
        }
        public ICoinHistoryRepository CoinHistory
        {
            get
            {
                if (_coinsHistory == null)
                {
                    _coinsHistory = new CoinHistoryRepository(_context);
                }
                return _coinsHistory;
            }
        }
        public IShopCoinsRepository ShopCoins
        {
            get
            {
                if (_shopCoins == null)
                {
                    _shopCoins = new ShopCoinsRepository(_context);
                }
                return _shopCoins;
            }
        }
        public RepositoryWrapper(InvesteurContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
