using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TheStartupBuddyV3.Models
{

    public partial class InvesteurContext : IdentityDbContext<IdentityUser>
    {
        public InvesteurContext()
        {
        }

        public InvesteurContext(DbContextOptions<InvesteurContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<AdminUser> AdminUsers { get; set; } = null!;
        public virtual DbSet<AdminUser1> AdminUsers1 { get; set; } = null!;
        //public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        //public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<Benefit> Benefits { get; set; } = null!;
        public virtual DbSet<Cards> Cards { get; set; } = null!;
        public virtual DbSet<ChosenBenefit> ChosenBenefits { get; set; } = null!;
        public virtual DbSet<ClicksDataLog> ClicksDataLogs { get; set; } = null!;
        public virtual DbSet<CommunityStartup> CommunityStartups { get; set; } = null!;
        public virtual DbSet<CommunityUser> CommunityUsers { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<CoinHistory> CoinHistory { get; set; } = null!;
        public virtual DbSet<CountClickedProfileStartup> CountClickedProfileStartups { get; set; } = null!;
        public virtual DbSet<Emailtempalte> Emailtempaltes { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Emailtempalte2> Emailtempalte2s { get; set; } = null!;
        public virtual DbSet<Enterprise> Enterprises { get; set; } = null!;
        public virtual DbSet<FavouritedDataLog> FavouritedDataLogs { get; set; } = null!;
        public virtual DbSet<FundingFieldFile> FundingFieldFiles { get; set; } = null!;
        public virtual DbSet<FundingFieldValue> FundingFieldValues { get; set; } = null!;
        public virtual DbSet<FundingMissionField> FundingMissionFields { get; set; } = null!;
        public virtual DbSet<FundingMissionTitle> FundingMissionTitles { get; set; } = null!;
        public virtual DbSet<Goal> Goals { get; set; } = null!;
        public virtual DbSet<GoalsCategory> GoalsCategories { get; set; } = null!;
        public virtual DbSet<GoalsCardFields> GoalsCardFields { get; set; } = null!;
        public virtual DbSet<GoalsStep> GoalsSteps { get; set; } = null!;
        public virtual DbSet<GoalsTemplates> GoalsTemplates { get; set; } = null!;
        public virtual DbSet<HangOutLink> HangOutLinks { get; set; } = null!;
        public virtual DbSet<HistoryCompanySteps> HistoryCompanySteps { get; set; } = null!;
        public virtual DbSet<InvestmentInvestorBuyShare> InvestmentInvestorBuyShares { get; set; } = null!;
        public virtual DbSet<InvestmentInvestorOfferShare> InvestmentInvestorOfferShares { get; set; } = null!;
        public virtual DbSet<InvestmentInvestorSellShare> InvestmentInvestorSellShares { get; set; } = null!;
        public virtual DbSet<InvestmentSellShare> InvestmentSellShares { get; set; } = null!;
        public virtual DbSet<Investor> Investors { get; set; } = null!;
        public virtual DbSet<InvestorMember> InvestorMembers { get; set; } = null!;
        public virtual DbSet<InvestorMission> InvestorMissions { get; set; } = null!;
        public virtual DbSet<Level> Levels { get; set; } = null!;
        public virtual DbSet<Membership> Memberships { get; set; } = null!;
        public virtual DbSet<Mentor> Mentors { get; set; } = null!;
        public virtual DbSet<MentorMission> MentorMissions { get; set; } = null!;
        public virtual DbSet<MentorSession> MentorSessions { get; set; } = null!;
        public virtual DbSet<MetaDatum> MetaData { get; set; } = null!;
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; } = null!;
        public virtual DbSet<MissionField> MissionFields { get; set; } = null!;
        public virtual DbSet<NetworkingProfile> NetworkingProfiles { get; set; } = null!;
        public virtual DbSet<NetworkingConnect> NetworkingConnect { get; set; } = null!;
        public virtual DbSet<Notifications> Notifications { get; set; } = null!;
        //public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<PortalUser> PortalUsers { get; set; } = null!;
        public virtual DbSet<Profile> Profiles { get; set; } = null!;
        public virtual DbSet<ProgramCode> ProgramCodes { get; set; } = null!;
        public virtual DbSet<ProgramGroup> ProgramGroups { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionCategoryScoreValue> QuestionCategoryScoreValues { get; set; } = null!;
        //public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ServiceField> ServiceFields { get; set; } = null!;
        public virtual DbSet<ServiceFile> ServiceFiles { get; set; } = null!;
        public virtual DbSet<ServicePurchase> ServicePurchases { get; set; } = null!;
        public virtual DbSet<SessionRating> SessionRating { get; set; } = null!;
        public virtual DbSet<ShareLink> ShareLinks { get; set; } = null!;
        public virtual DbSet<ShopCoins> ShopCoins { get; set; } = null!;
        public virtual DbSet<Startup> Startups { get; set; } = null!;
        public virtual DbSet<StartupAchievement> StartupAchievements { get; set; } = null!;
        public virtual DbSet<StartupBmc> StartupBmcs { get; set; } = null!;
        public virtual DbSet<StartupDemoDay> StartupDemoDays { get; set; } = null!;
        public virtual DbSet<StartupGoal> StartupGoals { get; set; } = null!;
        public virtual DbSet<StartupInvestor> StartupInvestors { get; set; } = null!;
        public virtual DbSet<StartupMember> StartupMembers { get; set; } = null!;
        public virtual DbSet<StartupMission> StartupMissions { get; set; } = null!;
        public virtual DbSet<StartupProgram> StartupPrograms { get; set; } = null!;
        public virtual DbSet<StartupProgress> StartupProgresses { get; set; } = null!;
        public virtual DbSet<StartupPv> StartupPvs { get; set; } = null!;
        public virtual DbSet<StartupVpc> StartupVpcs { get; set; } = null!;
        public virtual DbSet<Step> Steps { get; set; } = null!;
        public virtual DbSet<SubscriptionStartup> Subscriptions { get; set; } = null!;
        public virtual DbSet<SubscriptionInvestor> SubscriptionInvestors { get; set; } = null!;
        public virtual DbSet<ToolkitLearning> ToolkitLearnings { get; set; } = null!;
        public virtual DbSet<User> UsersData { get; set; } = null!;
        //public virtual DbSet<Users> Users { get; set; } = null!;
        public virtual DbSet<UserFieldFile> UserFieldFiles { get; set; } = null!;
        public virtual DbSet<UserFieldValue> UserFieldValues { get; set; } = null!;
        public virtual DbSet<UserMission> UserMissions { get; set; } = null!;
        public virtual DbSet<UserNotification> UserNotifications { get; set; } = null!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public virtual DbSet<Userlog> Userlogs { get; set; } = null!;
        public virtual DbSet<CompanyGoals> CompanyGoals { get; set; } = null!;
        public virtual DbSet<CompanyGoalsStep> CompanyGoalsStep { get; set; } = null!;
        public virtual DbSet<CompanyGoalsCardFields> CompanyGoalsCardFields { get; set; } = null!;
        public virtual DbSet<VpcCustomerJob> VpcCustomerJobs { get; set; } = null!;
        public virtual DbSet<VpcCustomerNeed> VpcCustomerNeeds { get; set; } = null!;
        public virtual DbSet<VpcCustomerType> VpcCustomerTypes { get; set; } = null!;
        public virtual DbSet<VpcFeature> VpcFeatures { get; set; } = null!;
        public virtual DbSet<VpcProduct> VpcProducts { get; set; } = null!;
        public virtual DbSet<WeeklyMentoringHour> WeeklyMentoringHours { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();       
            
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(config.GetConnectionString("TSBDatabase"));
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AdminUser>(entity =>
            {
                entity.ToTable("AdminUser");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .HasComment("''");

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("password")
                    .HasComment("''");

                entity.Property(e => e.Platformaccess)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("platformaccess")
                    .HasComment("''");

                entity.Property(e => e.Role)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("role")
                    .HasComment("''");
            });

            modelBuilder.Entity<AdminUser1>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_dbo.Users");

                entity.ToTable("AdminUsers");

                entity.Property(e => e.UserId).HasColumnType("UserId");
                entity.Property(e => e.Username).HasColumnType("Username");
                entity.Property(e => e.FirstName).HasColumnType("FirstName");
                entity.Property(e => e.LastName).HasColumnType("LastName");
                entity.Property(e => e.Email).HasColumnType("Email");
                //entity.Property(e => e.Username).HasColumnType("Username");

                entity.Property(e => e.PlatformAccess)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnType("Role");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").HasConstraintName("FK_dbo.UserRoles_dbo.Roles_RoleId"),
                        r => r.HasOne<AdminUser1>().WithMany().HasForeignKey("UserId").HasConstraintName("FK_dbo.UserRoles_dbo.Users_UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId").HasName("PK_dbo.UserRoles");

                            j.ToTable("UserRoles");
                        });

                entity.Property(e => e.EmailNotification).HasColumnType("EmailNotification");

                entity.Property(e => e.ProgramId).HasColumnType("ProgramId");

                entity.Property(e => e.Linkedin).HasColumnType("Linkedin");

                entity.Property(e => e.Description).HasColumnType("Description");

                entity.Property(e => e.Photo).HasColumnType("Photo");

                entity.Property(e => e.OnBoardingClicked).HasColumnType("OnBoardingClicked");

                entity.Property(e => e.UserOnBoardingStatus).HasColumnType("UserOnBoardingStatus");

                entity.Property(e => e.Coins).HasColumnType("Coins");
            });

            //modelBuilder.Entity<AspNetRole>(entity =>
            //{
            //    entity.Property(e => e.Id).HasMaxLength(128);

            //    entity.Property(e => e.Name).HasMaxLength(256);
            //});

            //modelBuilder.Entity<AspNetUser>(entity =>
            //{
            //    entity.Property(e => e.Id).HasMaxLength(128);

            //    entity.Property(e => e.ConcurrencyStamp)
            //        .HasMaxLength(256)
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.Email).HasMaxLength(256);

            //    entity.Property(e => e.FullName)
            //        .HasMaxLength(256)
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.LockoutEnd).HasColumnType("datetime");

            //    entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

            //    entity.Property(e => e.NormalizedEmail)
            //        .HasMaxLength(256)
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.NormalizedUserName)
            //        .HasMaxLength(256)
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.UserName).HasMaxLength(256);

            //    entity.HasMany(d => d.Roles)
            //        .WithMany(p => p.Users)
            //        .UsingEntity<Dictionary<string, object>>(
            //            "AspNetUserRole",
            //            l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId").HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId"),
            //            r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId").HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId"),
            //            j =>
            //            {
            //                j.HasKey("UserId", "RoleId").HasName("PK_dbo.AspNetUserRoles");

            //                j.ToTable("AspNetUserRoles");

            //                j.IndexerProperty<string>("UserId").HasMaxLength(128);

            //                j.IndexerProperty<string>("RoleId").HasMaxLength(128);
            //            });
            //});

            //modelBuilder.Entity<AspNetUserClaim>(entity =>
            //{
            //    entity.Property(e => e.UserId).HasMaxLength(128);

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserClaims)
            //        .HasForeignKey(d => d.UserId)
            //        .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            //});

            //modelBuilder.Entity<AspNetUserLogin>(entity =>
            //{
            //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
            //        .HasName("PK_dbo.AspNetUserLogins");

            //    entity.Property(e => e.LoginProvider).HasMaxLength(128);

            //    entity.Property(e => e.ProviderKey).HasMaxLength(128);

            //    entity.Property(e => e.UserId).HasMaxLength(128);

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserLogins)
            //        .HasForeignKey(d => d.UserId)
            //        .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            //});

            modelBuilder.Entity<Benefit>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ApplyTo)
                    .HasColumnName("apply_to")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BenefitDescription)
                    .HasColumnName("benefit_description")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BenefitInfo)
                    .HasColumnName("benefit_info")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.BenefitName)
                    .HasMaxLength(250)
                    .HasColumnName("benefit_name")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Logo)
                    .HasColumnName("logo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PackType).HasColumnName("pack_type");

                entity.Property(e => e.Worth)
                    .HasMaxLength(150)
                    .HasColumnName("worth")
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.ActionType).HasColumnName("action_type");
                entity.Property(e => e.PriceBenefit).HasColumnName("price_benefit");
                entity.Property(e => e.IsPublished).HasColumnName("is_published");
                entity.Property(e => e.AssignToProgram).HasColumnName("assign_to_program");
                entity.Property(e => e.tags).HasColumnName("tags");
                entity.Property(e => e.AssignToLevel).HasColumnName("assign_to_level");
                entity.Property(e => e.AssignToCategory).HasColumnName("assign_to_category");
                entity.Property(e => e.CreateDate).HasColumnName("create_date");
                entity.Property(e => e.ProgramGroupId).HasColumnName("program_group_id").HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Cards>(entity =>
            {
                entity.HasKey(e => e.id_card);

                entity.ToTable("Cards");

                entity.Property(e => e.id_card).HasColumnName("id_card");

                entity.Property(e => e.id_step).HasColumnName("id_step");

                entity.Property(e => e.help_text).HasColumnName("help_text");

                entity.Property(e => e.title).HasColumnName("title");

                entity.Property(e => e.description).HasColumnName("description");

                entity.Property(e => e.field_code).HasColumnName("field_code");

                entity.Property(e => e.field_code_type).HasColumnName("field_code_type");

                entity.Property(e => e.placeholder)
                    .HasMaxLength(50)
                    .HasColumnName("placeholder");

                entity.Property(e => e.type)
                   .HasMaxLength(50)
                   .HasColumnName("type");

                entity.Property(e => e.type_rule)
                   .HasMaxLength(50)
                   .HasColumnName("type_rule");

                entity.Property(e => e.label)
                   .HasMaxLength(50)
                   .HasColumnName("label");

                entity.Property(e => e.value)
                   .HasMaxLength(50)
                   .HasColumnName("value");

                entity.Property(e => e.required).HasColumnName("required");

                entity.Property(e => e.disabled).HasColumnName("disabled");

                entity.Property(e => e.max_data).HasColumnName("max_data");

                entity.Property(e => e.allow_multiple).HasColumnName("allow_multiple");

                entity.Property(e => e.display_order).HasColumnName("display_order");

                entity.Property(e => e.success_message)
                   .HasMaxLength(50)
                   .HasColumnName("success_message");

                entity.Property(e => e.has_option).HasColumnName("has_option");

                entity.Property(e => e.options).HasColumnName("options");
            });

            modelBuilder.Entity<ChosenBenefit>(entity =>
            {
                entity.HasKey(e => e.ChosenId);

                entity.ToTable("Chosen_Benefit");

                entity.Property(e => e.ChosenId).HasColumnName("chosen_id");

                entity.Property(e => e.BenefitChosenId).HasColumnName("benefit_chosen_id");

                entity.Property(e => e.BenefitDate)
                    .HasColumnType("datetime")
                    .HasColumnName("benefit_date");

                entity.Property(e => e.BenefitOwnerStartupid).HasColumnName("benefit_owner_startupid");

                entity.Property(e => e.BenefitOwnerUserid)
                    .HasMaxLength(500)
                    .HasColumnName("benefit_owner_userid")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ClicksDataLog>(entity =>
            {
                entity.HasKey(e => e.ClickId)
                    .HasName("PK__ClicksDa__92D409639FFA8329");

                entity.ToTable("ClicksDataLog");

                entity.Property(e => e.ClickId).HasColumnName("click_id");

                entity.Property(e => e.ClickDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("click_date_time");

                entity.Property(e => e.LatestCount).HasColumnName("latest_count");

                entity.Property(e => e.Startupid).HasColumnName("startupid");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid");

                entity.HasOne(d => d.Startup)
                    .WithMany(p => p.ClicksDataLogs)
                    .HasForeignKey(d => d.Startupid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClicksDat__start__5F141958");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ClicksDataLogs)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_ClicksDataLog_User");
            });

            modelBuilder.Entity<CommunityStartup>(entity =>
            {
                entity.HasKey(e => new { e.Communityid, e.Startupid });

                entity.ToTable("Community_Startup");

                entity.Property(e => e.Communityid).HasColumnName("communityid");

                entity.Property(e => e.Startupid).HasColumnName("startupid");

                entity.Property(e => e.Joineddate)
                    .HasColumnType("datetime")
                    .HasColumnName("joineddate")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<CommunityUser>(entity =>
            {
                entity.HasKey(e => new { e.Communityid, e.Userid });

                entity.ToTable("Community_User");

                entity.Property(e => e.Communityid).HasColumnName("communityid");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Companyid).HasColumnName("companyid");

                entity.Property(e => e.Companyname)
                    .HasMaxLength(150)
                    .HasColumnName("companyname")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Contact)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contact")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Logo).HasColumnName("logo");

                entity.Property(e => e.Userid)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("userid");

                entity.Property(e => e.Website)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("website")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<CoinHistory>(entity =>
            {
                entity.ToTable("Coin_History");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.StartupId).HasColumnName("StartupId");

                entity.Property(e => e.Amount)
                    .HasColumnName("Amount")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(10)
                    .HasColumnName("TransactionType")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TransactionDate");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("Description")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<CountClickedProfileStartup>(entity =>
            {
                entity.ToTable("CountClickedProfileStartup");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClickedProfile).HasColumnName("clickedProfile");
            });

            modelBuilder.Entity<Emailtempalte>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("emailtempalte");

                entity.Property(e => e.Column0)
                    .HasColumnType("text")
                    .HasColumnName("Column 0");
            });

            modelBuilder.Entity<Emailtempalte2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("emailtempalte2");

                entity.Property(e => e.Column0)
                    .HasColumnType("xml")
                    .HasColumnName("Column 0");
            });

            modelBuilder.Entity<Enterprise>(entity =>
            {
                entity.ToTable("Enterprise");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Enteprise)
                    .HasMaxLength(250)
                    .HasColumnName("enteprise")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Expirydate)
                    .HasColumnType("datetime")
                    .HasColumnName("expirydate");

                entity.Property(e => e.Source)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("source")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<FavouritedDataLog>(entity =>
            {
                entity.HasKey(e => e.FavId);

                entity.ToTable("FavouritedDataLog");

                entity.Property(e => e.FavId).HasColumnName("fav_id");

                entity.Property(e => e.DateTimeFav)
                    .HasColumnType("datetime")
                    .HasColumnName("date_time_fav");

                entity.Property(e => e.Startupid).HasColumnName("startupid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid");

                entity.HasOne(d => d.Startup)
                    .WithMany(p => p.FavouritedDataLogs)
                    .HasForeignKey(d => d.Startupid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavouritedDataLog_Startup");
            });

            modelBuilder.Entity<FundingFieldFile>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Funding_Field_File");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.FieldId).HasColumnName("field_id");

                entity.Property(e => e.FileName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("file_name");

                entity.Property(e => e.Lastupdated)
                    .HasColumnType("datetime")
                    .HasColumnName("lastupdated")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Type)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.Userid)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("userid");
            });

            modelBuilder.Entity<FundingFieldValue>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Funding_Field_Value");

                entity.Property(e => e.FieldId).HasColumnName("field_id");

                entity.Property(e => e.Lastupdated)
                    .HasColumnType("datetime")
                    .HasColumnName("lastupdated")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Userid)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("userid");

                entity.Property(e => e.Value)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<FundingMissionField>(entity =>
            {
                entity.HasKey(e => e.FieldId);

                entity.ToTable("Funding_Mission_Field");

                entity.Property(e => e.FieldId).HasColumnName("field_id");

                entity.Property(e => e.ControlType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("control_type");

                entity.Property(e => e.DisplayOrder).HasColumnName("display_order");

                entity.Property(e => e.IsDisable).HasColumnName("is_disable");

                entity.Property(e => e.IsRequired).HasColumnName("is_required");

                entity.Property(e => e.MissionId).HasColumnName("mission_id");

                entity.Property(e => e.Options)
                    .HasColumnType("text")
                    .HasColumnName("options");

                entity.Property(e => e.TitleName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("title_name");
            });

            modelBuilder.Entity<FundingMissionTitle>(entity =>
            {
                entity.HasKey(e => e.MissionId);

                entity.ToTable("Funding_Mission_Title");

                entity.Property(e => e.MissionId).HasColumnName("mission_id");

                entity.Property(e => e.DefaultOrder).HasColumnName("default_order");

                entity.Property(e => e.IsDisable).HasColumnName("is_disable");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.HasKey(e => e.IdGoal);

                entity.Property(e => e.IdGoal).HasColumnName("id_goal");

                entity.Property(e => e.IdLevel).HasColumnName("id_level");

                entity.Property(e => e.IsRequired).HasColumnName("is_required");

                entity.Property(e => e.LockedStatus).HasColumnName("locked_status");

                entity.Property(e => e.Logo).HasColumnName("logo");

                entity.Property(e => e.StatusComplete).HasColumnName("status_complete");

                entity.Property(e => e.StatusPromo).HasColumnName("status_promo");

                entity.Property(e => e.TitleGoal).HasColumnName("title_goal");

                entity.Property(e => e.HelpText).HasColumnName("help_text");

                entity.Property(e => e.TotalPoints).HasColumnName("total_points");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.SecondTitle).HasColumnName("second_title");

                entity.Property(e => e.Label).HasColumnName("label");

                entity.Property(e => e.DateCreated).HasColumnName("date_created");

                entity.Property(e => e.StepsJsonUI).HasColumnName("steps_json_ui");

                entity.Property(e => e.ProgramId).HasColumnName("programid");
                
                entity.Property(e => e.ProgramGroupId).HasColumnName("programGroupId");

                entity.Property(e => e.IsPublished).HasColumnName("is_published");

                entity.Property(e => e.StatusOnboarding).HasColumnName("status_onboarding");

                entity.Property(e => e.StatusScore).HasColumnName("status_score");

            });

            modelBuilder.Entity<GoalsCategory>(entity =>
            {
                entity.HasKey(e => e.IdGoalsCategory)
                    .HasName("PK_Stages");

                entity.ToTable("Goals_Category");

                entity.Property(e => e.IdGoalsCategory).HasColumnName("id_goals_category");

                entity.Property(e => e.LogoCategory).HasColumnName("logo_category");

                entity.Property(e => e.MoneyCategory).HasColumnName("money_category");

                entity.Property(e => e.NameGoalsCategory).HasColumnName("name_goals_category");

            });

            modelBuilder.Entity<GoalsCardFields>(entity =>
            {
                entity.HasKey(e => e.IdCard);
                entity.ToTable("Goals_Card_Fields");
                entity.Property(e => e.IdCard).HasColumnName("id_card");
                entity.Property(e => e.IdStep).HasColumnName("id_step");
                entity.Property(e => e.help_text).HasColumnName("help_text");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.FieldCode).HasColumnName("field_code");
                entity.Property(e => e.FieldCodeType).HasColumnName("field_code_type");
                entity.Property(e => e.HasOption).HasColumnName("has_option");
                entity.Property(e => e.Option).HasColumnName("options");
                entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
                entity.Property(e => e.SuccessMessage).HasColumnName("success_message");
                entity.Property(e => e.Placeholder).HasColumnName("placeholder");
                entity.Property(e => e.TextButton).HasColumnName("text_button");
                entity.Property(e => e.Type).HasColumnName("type");
                entity.Property(e => e.TypeRule).HasColumnName("type_rule");
                entity.Property(e => e.Label).HasColumnName("label");
                entity.Property(e => e.Value).HasColumnName("value");
                entity.Property(e => e.IsRequired).HasColumnName("is_required");
                entity.Property(e => e.IsDisabled).HasColumnName("is_disabled");
                entity.Property(e => e.MaxData).HasColumnName("max_data");
                entity.Property(e => e.AllowMultiple).HasColumnName("allow_multiple");
                entity.Property(e => e.FilesType).HasColumnName("files_type");
                entity.Property(e => e.QuestionID).HasColumnName("question_id");
                entity.Property(e => e.Score).HasColumnName("score");
                entity.Property(e => e.Visible).HasColumnName("visible");
            });

            modelBuilder.Entity<GoalsStep>(entity =>
            {
                entity.HasKey(e => e.IdGoalStep)
                    .HasName("PK_Goas_Step");

                entity.ToTable("Goals_Step");

                entity.Property(e => e.IdGoalStep).HasColumnName("id_goal_step");

                entity.Property(e => e.GoalStepDateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("goal_step_date_created");

                entity.Property(e => e.IdGoal).HasColumnName("id_goal");

                entity.Property(e => e.IdStep).HasColumnName("id_step");
                entity.Property(e => e.Title).HasColumnName("title");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.OrderStep).HasColumnName("order_step");
                entity.Property(e => e.Visible).HasColumnName("visible");
            });

            modelBuilder.Entity<GoalsTemplates>(entity =>
            {
                entity.HasKey(e => e.id_template);

                entity.ToTable("Goals_Templates");

                entity.Property(e => e.id_template).HasColumnName("id_template");

                entity.Property(e => e.photo_template).HasColumnName("photo_template");

                entity.Property(e => e.id_level).HasColumnName("id_level");

                entity.Property(e => e.status).HasColumnName("status");
            });

            modelBuilder.Entity<HangOutLink>(entity =>
            {
                entity.ToTable("HangOutLink");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Mentorsessionid).HasColumnName("mentorsessionid");

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("url")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<HistoryCompanySteps>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_History_Company_Steps");

                entity.ToTable("History_Company_Steps");

                entity.Property(e => e.Id).HasColumnName("id_history_step");

                entity.Property(e => e.IdGoalStep).HasColumnName("id_goal_step");

                entity.Property(e => e.IdCompanyGoals).HasColumnName("id_company_goals");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IsStepComplete).HasColumnName("is_step_complete");

                entity.Property(e => e.IsGoalComplete).HasColumnName("is_goal_complete");

                entity.Property(e => e.TSBCoin).HasColumnName("tsb_coin");

                entity.Property(e => e.DateTimeHistory)
                    .HasColumnType("datetime")
                    .HasColumnName("datetime_history");

            });

            modelBuilder.Entity<InvestmentInvestorBuyShare>(entity =>
            {
                entity.HasKey(e => e.BuyId);

                entity.ToTable("Investment_Investor_Buy_Shares");

                entity.Property(e => e.BuyId).HasColumnName("buy_id");

                entity.Property(e => e.AmountSharesBought).HasColumnName("amount_shares_bought");

                entity.Property(e => e.BuyDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("buy_datetime");

                entity.Property(e => e.CurrentPricePerShare)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("current_price_per_share");

                entity.Property(e => e.InvestorEmail)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("investor_email");

                entity.Property(e => e.InvestorUserid)
                    .HasMaxLength(128)
                    .HasColumnName("investor_userid");

                entity.Property(e => e.StartupSharesOwnerId).HasColumnName("startup_shares_owner_id");

                entity.Property(e => e.TotalCurrentSharesPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("total_current_shares_price");
            });

            modelBuilder.Entity<InvestmentInvestorOfferShare>(entity =>
            {
                entity.HasKey(e => e.OfferId)
                    .HasName("PK_Investment_Offer_Shares");

                entity.ToTable("Investment_Investor_Offer_Shares");

                entity.Property(e => e.OfferId).HasColumnName("offer_id");

                entity.Property(e => e.InvestorEmail)
                    .HasMaxLength(250)
                    .HasColumnName("investor_email");

                entity.Property(e => e.InvestorUserid)
                    .HasMaxLength(128)
                    .HasColumnName("investor_userid");

                entity.Property(e => e.OfferToStartupid).HasColumnName("offer_to_startupid");

                entity.Property(e => e.PricePerShareOffer)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price_per_share_offer");

                entity.Property(e => e.ShareAmountOffer).HasColumnName("share_amount_offer");

                entity.Property(e => e.TimeOffer)
                    .HasColumnType("datetime")
                    .HasColumnName("time_offer");

                entity.Property(e => e.TotalShareOffer)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("total_share_offer");
            });

            modelBuilder.Entity<InvestmentInvestorSellShare>(entity =>
            {
                entity.HasKey(e => e.SellId);

                entity.ToTable("Investment_Investor_Sell_Shares");

                entity.Property(e => e.SellId).HasColumnName("sell_id");

                entity.Property(e => e.InvestorEmail)
                    .HasMaxLength(250)
                    .HasColumnName("investor_email");

                entity.Property(e => e.InvestorUserid)
                    .HasMaxLength(128)
                    .HasColumnName("investor_userid")
                    .HasDefaultValueSql("(N'((\"\"))')");

                entity.Property(e => e.PricePerShareSell)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price_per_share_sell");

                entity.Property(e => e.SellToStartupid).HasColumnName("sell_to_startupid");

                entity.Property(e => e.ShareAmountSell).HasColumnName("share_amount_sell");

                entity.Property(e => e.TimeSell)
                    .HasColumnType("datetime")
                    .HasColumnName("time_sell");

                entity.Property(e => e.TotalShareSell)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("total_share_sell");
            });

            modelBuilder.Entity<InvestmentSellShare>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("Investment_Sell_Shares");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PricePerShare)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("price_per_share");

                entity.Property(e => e.SellDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("sell_datetime");

                entity.Property(e => e.Startupid).HasColumnName("startupid");

                entity.Property(e => e.VirtualSharesAmount).HasColumnName("virtual_shares_amount");
            });

            modelBuilder.Entity<Investor>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Investor");

                entity.Property(e => e.UserId).HasMaxLength(128);

                entity.Property(e => e.DoubleMoney).HasColumnName("double_money");

                entity.Property(e => e.Invesmentdeclare)
                    .HasColumnName("invesmentdeclare")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Invesmentstage)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("invesmentstage")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Investmentcategories)
                    .HasColumnName("investmentcategories")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Investmentregions)
                    .HasColumnName("investmentregions")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Investortype)
                    .HasMaxLength(500)
                    .HasColumnName("investortype")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Isapproved).HasColumnName("isapproved");

                entity.Property(e => e.Marketfocus)
                    .HasMaxLength(500)
                    .HasColumnName("marketfocus")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MaxInvestment).HasColumnName("maxInvestment");

                entity.Property(e => e.MinInvestment).HasColumnName("minInvestment");

                entity.Property(e => e.Minimumrevenue).HasColumnName("minimumrevenue");

                entity.Property(e => e.Vcname)
                    .HasMaxLength(500)
                    .HasColumnName("vcname")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Virtualmoney)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("virtualmoney")
                    .HasDefaultValueSql("((5000000))");
            });

            modelBuilder.Entity<InvestorMember>(entity =>
            {
                entity.HasKey(e => e.IdInvestorMember);

                entity.ToTable("Investor_Member");

                entity.Property(e => e.IdInvestorMember).HasColumnName("id_investor_member");

                entity.Property(e => e.MemberEmail)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("member_email")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MemberName)
                    .HasMaxLength(250)
                    .HasColumnName("member_name")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UseridTeamOwner)
                    .HasMaxLength(150)
                    .HasColumnName("userid_team_owner")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<InvestorMission>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("Investor_Mission");

                entity.Property(e => e.Userid)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("userid");

                entity.Property(e => e.Displayorder).HasColumnName("displayorder");

                entity.Property(e => e.Missionid).HasColumnName("missionid");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.HasKey(e => e.IdLevel);
                entity.Property(e => e.IdLevel).HasColumnName("id_level");

                entity.Property(e => e.IdGoalsCategory).HasColumnName("id_goals_category");

                entity.Property(e => e.NameLevel).HasColumnName("name_level");

                entity.Property(e => e.LogoLevel).HasColumnName("logo_level");

                entity.Property(e => e.CodeLevel).HasColumnName("code_level");
                entity.Property(e => e.What).HasColumnName("what");

                entity.Property(e => e.Why).HasColumnName("why");
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.ToTable("Membership");

                entity.Property(e => e.MembershipId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("membership_id")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.PlanDuration)
                    .HasColumnName("plan_duration")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PlanId).HasColumnName("plan_id");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Mentor>(entity =>
            {
                entity.ToTable("Mentor");

                entity.Property(e => e.Mentorid).HasColumnName("mentorid");

                entity.Property(e => e.Communication)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("communication")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Description)
                    .HasColumnType("ntext")
                    .HasColumnName("description")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Photo)
                    .HasColumnType("image")
                    .HasColumnName("photo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("remarks")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasColumnName("title")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<MentorMission>(entity =>
            {
                entity.HasKey(e => new { e.Mentorid, e.Missionid });

                entity.ToTable("mentor_mission");

                entity.Property(e => e.Mentorid).HasColumnName("mentorid");

                entity.Property(e => e.Missionid).HasColumnName("missionid");
            });

            modelBuilder.Entity<MentorSession>(entity =>
            {
                entity.HasKey(e => e.Sessionid);

                entity.ToTable("Mentor_Session");

                entity.Property(e => e.Sessionid).HasColumnName("sessionid");

                entity.Property(e => e.Endtime)
                    .HasColumnType("datetime")
                    .HasColumnName("endtime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Hangout)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("hangout")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Mentorid).HasColumnName("mentorid");

                entity.Property(e => e.Starttime)
                    .HasColumnType("datetime")
                    .HasColumnName("starttime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();
            });

            modelBuilder.Entity<MetaDatum>(entity =>
            {
                entity.ToTable("Meta_data");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Data)
                    .HasColumnType("text")
                    .HasColumnName("data");

                entity.Property(e => e.MetaKey)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("meta_key");

                entity.Property(e => e.Page)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("page");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<MissionField>(entity =>
            {
                entity.HasKey(e => e.Fieldid);

                entity.ToTable("Mission_Field");

                entity.Property(e => e.Fieldid).HasColumnName("fieldid");

                entity.Property(e => e.Controltype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("controltype")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Displayorder)
                    .HasColumnName("displayorder")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Isdisable)
                    .HasColumnName("isdisable")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Isrequired)
                    .IsRequired()
                    .HasColumnName("isrequired")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Missionid).HasColumnName("missionid");

                entity.Property(e => e.Options)
                    .HasColumnType("text")
                    .HasColumnName("options")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Popovercontent)
                    .HasColumnType("text")
                    .HasColumnName("popovercontent")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Popovertitle)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("popovertitle")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Title)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("title")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<NetworkingProfile>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Networking_Profiles");

                entity.Property(e => e.Ispublic)
                    .IsRequired()
                    .HasColumnName("ispublic")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MyExpertise)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("my_expertise")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MyRole)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("my_role")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NetworkWithExpertiseAt)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("network_with_expertise_at")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NetworkWithRoles)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("network_with_roles")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProfileImgUrl)
                    .HasMaxLength(500)
                    .HasColumnName("profile_img_url")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<NetworkingConnect>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Networking_Connect");

                entity.Property(e => e.SenderId).HasColumnName("senderId");

                entity.Property(e => e.ReceiverId).HasColumnName("receiverId");

                entity.Property(e => e.RequestDate).HasColumnName("requestDate");

                entity.Property(e => e.AccepDate).HasColumnName("acceptDate");

                entity.Property(e => e.ConnectionStatus).HasColumnName("connectionStatus");

                entity.Property(e => e.RequestBy).HasColumnName("requestBy");
            });


            //modelBuilder.Entity<Notification>(entity =>
            //{
            //    entity.ToTable("Notification");

            //    entity.Property(e => e.Notificationid).HasColumnName("notificationid");

            //    entity.Property(e => e.Button)
            //        .HasMaxLength(250)
            //        .HasColumnName("button")
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.Datecreated)
            //        .HasColumnType("datetime")
            //        .HasColumnName("datecreated")
            //        .HasDefaultValueSql("(getdate())");

            //    entity.Property(e => e.Expirydate)
            //        .HasColumnType("datetime")
            //        .HasColumnName("expirydate")
            //        .HasDefaultValueSql("(getdate())");

            //    entity.Property(e => e.Link)
            //        .HasMaxLength(250)
            //        .IsUnicode(false)
            //        .HasColumnName("link")
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.Message)
            //        .HasMaxLength(2000)
            //        .HasColumnName("message")
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.Usertype).HasColumnName("usertype");
            //});

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.ToTable("Notifications");

                entity.Property(e => e.ID).HasColumnName("id_notification");

                entity.Property(e => e.StartupId)
                    .HasColumnName("startupid")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId)
                    .HasColumnName("userid");

                entity.Property(e => e.IsAction)
                    .HasColumnName("is_action")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Message)
                    .HasMaxLength(1000)
                    .HasColumnName("message")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Link)
                    .HasMaxLength(1000)
                    .HasColumnName("link")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReadStatus)
                    .HasColumnName("read_status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.JoinedAs)
                    .HasMaxLength(50)
                    .HasColumnName("joinedas");

                entity.Property(e => e.Startdate)
                    .HasColumnType("datetime")
                    .HasColumnName("startdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Enddate)
                    .HasColumnType("datetime")
                    .HasColumnName("enddate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HasButton)
                    .HasColumnName("has_button")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ButtonLabel)
                    .HasMaxLength(500)
                    .HasColumnName("button_label")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SuccessMessage)
                    .HasMaxLength(1000)
                    .HasColumnName("success_message")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Redirect)
                    .HasMaxLength(500)
                    .HasColumnName("redirect")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NotifyToTeam)
                    .HasColumnName("notify_to_team")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Clicked)
                    .HasColumnName("clicked")
                    .HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<PortalUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Profile");

                entity.Property(e => e.Questionid).HasColumnName("questionid");

                entity.Property(e => e.Userid)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Value)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<ProgramCode>(entity =>
            {
                entity.ToTable("Program");

                entity.Property(e => e.Programid).HasColumnName("programid");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("code")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Enddate)
                    .HasColumnType("datetime")
                    .HasColumnName("enddate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Limit).HasColumnName("limit");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Permission).HasColumnName("permission");

                entity.Property(e => e.ProgramType)
                    .HasColumnName("program_type")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Startdate)
                    .HasColumnType("datetime")
                    .HasColumnName("startdate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusGoal).HasColumnName("status_goal");
            });

            modelBuilder.Entity<ProgramGroup>(entity =>
            {
                entity.HasKey(e => e.ProgramGroupId);

                entity.ToTable("Program_Group");

                entity.Property(e => e.ProgramGroupId).HasColumnName("programGroupId");
                entity.Property(e => e.Programid).HasColumnName("programid");
                entity.Property(e => e.GroupName).HasColumnName("groupName");
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdDate")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Question");

                entity.Property(e => e.Controltype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("controltype");

                entity.Property(e => e.Displayorder).HasColumnName("displayorder");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Isdisable).HasColumnName("isdisable");

                entity.Property(e => e.Options)
                    .HasColumnType("text")
                    .HasColumnName("options");

                entity.Property(e => e.Qid).HasColumnName("qid");

                entity.Property(e => e.Tab).HasColumnName("tab");

                entity.Property(e => e.Title)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<QuestionCategoryScoreValue>(entity =>
            {
                entity.HasKey(e => e.Scorevalueid);

                entity.ToTable("Question_Category_ScoreValue");

                entity.Property(e => e.Scorevalueid).HasColumnName("scorevalueid");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Eligiblity).HasColumnName("eligiblity");

                entity.Property(e => e.Questionid).HasColumnName("questionid");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("value");

                entity.Property(e => e.Valuefrom)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("valuefrom");

                entity.Property(e => e.Valuetext)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("valuetext");

                entity.Property(e => e.Valueto)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("valueto");

                entity.Property(e => e.Weight)
                    .HasColumnType("decimal(8, 2)")
                    .HasColumnName("weight");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Service");

                entity.Property(e => e.Category)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("category")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Clicks).HasColumnName("clicks");

                entity.Property(e => e.Createdat)
                    .HasColumnType("datetime")
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Investeurprice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("investeurprice");

                entity.Property(e => e.Isapproved).HasColumnName("isapproved");

                entity.Property(e => e.Missionid).HasColumnName("missionid");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Providerid).HasColumnName("providerid");

                entity.Property(e => e.Serviceid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("serviceid");

                entity.Property(e => e.Startupid).HasColumnName("startupid");

                entity.Property(e => e.Unit)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("unit")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Userid)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Views).HasColumnName("views");
            });

            modelBuilder.Entity<ServiceField>(entity =>
            {
                entity.HasKey(e => e.Fieldid);

                entity.ToTable("Service_Field");

                entity.Property(e => e.Fieldid).HasColumnName("fieldid");

                entity.Property(e => e.Fieldname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("fieldname")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Serviceid).HasColumnName("serviceid");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ServiceFile>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Service_File");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.Fileid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("fileid");

                entity.Property(e => e.Serviceid).HasColumnName("serviceid");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("type")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ServicePurchase>(entity =>
            {
                entity.HasKey(e => e.Purchaseid);

                entity.ToTable("Service_Purchase");

                entity.Property(e => e.Purchaseid).HasColumnName("purchaseid");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mobile")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Paymentid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("paymentid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Purchasetime)
                    .HasColumnType("datetime")
                    .HasColumnName("purchasetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Serviceid).HasColumnName("serviceid");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength();
            });

            modelBuilder.Entity<SessionRating>(entity =>
            {
                entity.HasKey(e => e.id_rating);

                entity.ToTable("Session_Rating");
                
                entity.Property(e => e.id_rating).HasColumnName("id_rating");

                entity.Property(e => e.id_calendar)
                    .HasMaxLength(150)
                    .HasColumnName("id_calendar")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.points).HasColumnName("points");

                entity.Property(e => e.comment_review).HasColumnName("comment_review");

                entity.Property(e => e.coin_bonus).HasColumnName("coin_bonus");

                entity.Property(e => e.startupid).HasColumnName("startupid");
            });

            modelBuilder.Entity<ShareLink>(entity =>
            {
                entity.HasKey(e => e.Linkid)
                    .HasName("PK_Links");

                entity.ToTable("ShareLink");

                entity.Property(e => e.Linkid).HasColumnName("linkid");

                entity.Property(e => e.Datecreated)
                    .HasColumnType("datetime")
                    .HasColumnName("datecreated")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Sharecontent)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sharecontent")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Startupid).HasColumnName("startupid");

                entity.Property(e => e.Token)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("token")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Url)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("url")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Validdays)
                    .HasColumnName("validdays")
                    .HasDefaultValueSql("((10))");
            });

            modelBuilder.Entity<ShopCoins>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Shop_Coins");
                
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.AmountOfCoins).HasColumnName("AmountOfCoins").HasDefaultValueSql("((0))");
                entity.Property(e => e.Price).HasColumnName("Price").HasDefaultValueSql("((0))");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasColumnName("Title")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Apply_to).HasColumnName("Apply_to").HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate).HasColumnName("CreatedDate").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Logo)
                    .HasColumnName("Logo")
                    .HasDefaultValueSql("('')");
                
                entity.Property(e => e.IsPublished).HasColumnName("isPuslished").HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Startup>(entity =>
            {
                entity.ToTable("Startup");

                entity.Property(e => e.Startupid).HasColumnName("startupid");

                entity.Property(e => e.BenefitTicket)
                    .HasColumnName("benefit_ticket")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Clicks).HasColumnName("clicks");

                entity.Property(e => e.CurrentFund)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("current_fund")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Description)
                    .HasColumnType("ntext")
                    .HasColumnName("description")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DoubleShares).HasColumnName("double_shares");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Ispublic).HasColumnName("ispublic");

                entity.Property(e => e.LastInvoice)
                    .HasColumnName("last_invoice")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Logo)
                    .HasColumnType("image")
                    .HasColumnName("logo");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("name")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PitchUrl)
                    .HasColumnName("pitch_url")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Publicprofilevisible).HasColumnName("publicprofilevisible");

                entity.Property(e => e.Startupstatus)
                    .HasMaxLength(100)
                    .HasColumnName("startupstatus")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TotalFund)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("total_fund")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.VideoUrl)
                    .HasColumnName("video_url")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Virtualshares)
                    .HasColumnName("virtualshares")
                    .HasDefaultValueSql("((1000000))");

                entity.Property(e => e.Website)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("website")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tsbCoin)
                    .HasColumnName("tsb_coin")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.priceConnect)
                    .HasColumnName("priceConnect")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.startupLogo).HasColumnName("startup_logo");
                
                entity.Property(e => e.startupScore).HasColumnName("startup_score");

                entity.Property(e => e.startupNote).HasColumnName("startup_note");
            });

            modelBuilder.Entity<StartupAchievement>(entity =>
            {
                entity.HasKey(e => e.Achievementid);

                entity.ToTable("Startup_Achievement");

                entity.Property(e => e.Achievementid).HasColumnName("achievementid");

                entity.Property(e => e.Change)
                    .HasMaxLength(50)
                    .HasColumnName("change")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Datetime)
                    .HasColumnType("datetime")
                    .HasColumnName("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Metric)
                    .HasMaxLength(1000)
                    .HasColumnName("metric")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Notes)
                    .HasColumnName("notes")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Startupid).HasColumnName("startupid");

                entity.Property(e => e.Week)
                    .HasColumnName("week")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<StartupBmc>(entity =>
            {
                entity.HasKey(e => e.Startupid);

                entity.ToTable("Startup_BMC");

                entity.Property(e => e.Startupid)
                    .ValueGeneratedNever()
                    .HasColumnName("startupid");

                entity.Property(e => e.Data)
                    .HasColumnType("ntext")
                    .HasColumnName("data");

                entity.Property(e => e.Lastupdated)
                    .HasColumnType("datetime")
                    .HasColumnName("lastupdated")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<StartupDemoDay>(entity =>
            {
                entity.ToTable("Startup_DemoDay");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Applicationdate)
                    .HasColumnType("datetime")
                    .HasColumnName("applicationdate");

                entity.Property(e => e.Chargeid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("chargeid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Startupid).HasColumnName("startupid");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid")
                    .IsFixedLength();
            });

            modelBuilder.Entity<StartupGoal>(entity =>
            {
                entity.HasKey(e => e.Goalid);

                entity.ToTable("Startup_Goal");

                entity.Property(e => e.Goalid).HasColumnName("goalid");

                entity.Property(e => e.AssigneeTo)
                    .HasMaxLength(250)
                    .HasColumnName("assignee_to");

                entity.Property(e => e.Datetime)
                    .HasColumnType("datetime")
                    .HasColumnName("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("due_date");

                entity.Property(e => e.Lastupdated)
                    .HasColumnType("datetime")
                    .HasColumnName("lastupdated")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Startupid).HasColumnName("startupid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UseridAssignee)
                    .HasMaxLength(128)
                    .HasColumnName("userid_assignee");

                entity.Property(e => e.Week)
                    .HasColumnName("week")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<StartupInvestor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Startup_Investor");

                entity.Property(e => e.DateTimeFav)
                    .HasColumnType("datetime")
                    .HasColumnName("date_time_fav");

                entity.Property(e => e.Startupid).HasColumnName("startupid");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<StartupMember>(entity =>
            {
                entity.HasKey(e => new { e.Startupid, e.Email });

                entity.ToTable("Startup_Member");

                entity.Property(e => e.Startupid).HasColumnName("startupid");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("email")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<StartupMission>(entity =>
            {
                entity.HasKey(e => e.Missionid);

                entity.ToTable("Startup_Mission");

                entity.Property(e => e.Missionid).HasColumnName("missionid");

                entity.Property(e => e.AssignTo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("assignTo");

                entity.Property(e => e.Defaultorder).HasColumnName("defaultorder");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Isbonus).HasColumnName("isbonus");

                entity.Property(e => e.Isdisable).HasColumnName("isdisable");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Percentage).HasColumnName("percentage");

                entity.Property(e => e.Subtitle)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("subtitle")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<StartupProgram>(entity =>
            {
                entity.HasKey(e => new { e.ProgramId, e.StartupId, e.JoinDate });

                entity.ToTable("Startup_Program");

                entity.Property(e => e.JoinDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProgramGroupId).HasColumnName("ProgramGroupId");
            });

            modelBuilder.Entity<StartupProgress>(entity =>
            {
                entity.HasKey(e => e.Progressid);

                entity.ToTable("Startup_Progress");

                entity.Property(e => e.Progressid).HasColumnName("progressid");

                entity.Property(e => e.Companyid).HasColumnName("companyid");

                entity.Property(e => e.Progress).HasColumnName("progress");

                entity.Property(e => e.Updatedat)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedat")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Userid)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<StartupPv>(entity =>
            {
                entity.HasKey(e => e.Startupid);

                entity.ToTable("Startup_PV");

                entity.Property(e => e.Startupid)
                    .ValueGeneratedNever()
                    .HasColumnName("startupid");

                entity.Property(e => e.Data)
                    .HasColumnType("ntext")
                    .HasColumnName("data")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Lastupdated)
                    .HasColumnType("datetime")
                    .HasColumnName("lastupdated")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<StartupVpc>(entity =>
            {
                entity.HasKey(e => e.Startupid);

                entity.ToTable("Startup_VPC");

                entity.Property(e => e.Startupid)
                    .ValueGeneratedNever()
                    .HasColumnName("startupid");

                entity.Property(e => e.Data)
                    .HasColumnType("ntext")
                    .HasColumnName("data");

                entity.Property(e => e.Lastupdated)
                    .HasColumnType("datetime")
                    .HasColumnName("lastupdated")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Step>(entity =>
            {
                entity.HasKey(e => e.IdStep);

                entity.Property(e => e.IdStep).HasColumnName("id_step");

                entity.Property(e => e.CreatedDateStep)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date_step");

                entity.Property(e => e.DescriptionStep).HasColumnName("description_step");

                entity.Property(e => e.TitleStep).HasColumnName("title_step");
            });

            modelBuilder.Entity<SubscriptionStartup>(entity =>
            {
                entity.ToTable("Subscription");

                entity.Property(e => e.Subscriptionid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("subscriptionid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Defaultplan)
                    .HasColumnName("defaultplan")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Enddate)
                    .HasColumnType("datetime")
                    .HasColumnName("enddate");

                entity.Property(e => e.Plantype).HasColumnName("plantype");

                entity.Property(e => e.Starttime)
                    .HasColumnType("datetime")
                    .HasColumnName("starttime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<SubscriptionInvestor>(entity =>
            {
                entity.HasKey(e => e.Subscriptionid);

                entity.ToTable("Subscription_Investor");

                entity.Property(e => e.Subscriptionid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("subscriptionid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Defaultplan)
                    .HasColumnName("defaultplan")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Enddate)
                    .HasColumnType("datetime")
                    .HasColumnName("enddate");

                entity.Property(e => e.Plantype).HasColumnName("plantype");

                entity.Property(e => e.Starttime)
                    .HasColumnType("datetime")
                    .HasColumnName("starttime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ToolkitLearning>(entity =>
            {
                entity.ToTable("ToolkitLearnings");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Title).HasColumnName("Title");

                entity.Property(e => e.Description).HasColumnName("Description");

                entity.Property(e => e.Url).HasColumnName("URL");

                entity.Property(e => e.Type).HasColumnName("Type");

                entity.Property(e => e.Tag).HasColumnName("Tag");

                entity.Property(e => e.Image).HasColumnName("Image");

                entity.Property(e => e.Exclusive).HasColumnName("Exclusive");

                entity.Property(e => e.ProgramId).HasColumnName("ProgramId");

                entity.Property(e => e.ContentType).HasColumnName("ContentType");

                entity.Property(e => e.ContentArticle).HasColumnName("ContentArticle");

                entity.Property(e => e.IdLevel).HasColumnName("IDLevel");

                entity.Property(e => e.Logo).HasColumnName("Logo");

                entity.Property(e => e.IsPublished).HasColumnName("IsPublished");
                
                entity.Property(e => e.Rewards).HasColumnName("Rewards");

                entity.Property(e => e.DateCreated).HasColumnName("DateCreated");

                entity.Property(e => e.IdCategory).HasColumnName("IdCategory");

                entity.Property(e => e.ProgramGroupId).HasColumnName("ProgramGroupId").HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .HasMaxLength(128)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.JoinDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LoginProvider)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProviderKey)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OnBoardingClicked).HasColumnName("OnBoardingClicked");

                entity.Property(e => e.UserOnBoardingStatus).HasColumnName("UserOnBoardingStatus");
            });

            //modelBuilder.Entity<Users>(entity =>
            //{
            //    entity.ToTable("User");

            //    entity.Property(e => e.UserId)
            //        .HasMaxLength(128)
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.City)
            //        .HasMaxLength(50)
            //        .IsUnicode(false)
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.Country)
            //        .HasMaxLength(50)
            //        .IsUnicode(false)
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.DisplayName)
            //        .HasMaxLength(250)
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.Email)
            //        .HasMaxLength(256)
            //        .IsUnicode(false)
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.JoinDate)
            //        .HasColumnType("datetime")
            //        .HasDefaultValueSql("(getdate())");

            //    entity.Property(e => e.LoginProvider)
            //        .HasMaxLength(50)
            //        .IsUnicode(false)
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.ProviderKey)
            //        .HasMaxLength(128)
            //        .IsUnicode(false)
            //        .HasDefaultValueSql("('')");

            //    entity.Property(e => e.Source)
            //        .HasMaxLength(50)
            //        .IsUnicode(false)
            //        .HasDefaultValueSql("('')");
            //    entity.Property(e => e.Photo)
            //        .HasColumnName("Photo")
            //        .IsUnicode(false)
            //        .HasDefaultValueSql("('')");
            //});

            modelBuilder.Entity<UserFieldFile>(entity =>
            {
                entity.HasKey(e => e.Fileid);

                entity.ToTable("User_Field_File");

                entity.Property(e => e.Fileid).HasColumnName("fileid");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.Fieldid).HasColumnName("fieldid");

                entity.Property(e => e.Filename)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("filename")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Lastupdated)
                    .HasColumnType("datetime")
                    .HasColumnName("lastupdated")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Startupid).HasColumnName("startupid");

                entity.Property(e => e.Type)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("type")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Userid)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<UserFieldValue>(entity =>
            {
                entity.HasKey(e => new { e.Userid, e.Fieldid, e.Companyid });

                entity.ToTable("User_Field_Value");

                entity.Property(e => e.Userid)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Fieldid).HasColumnName("fieldid");

                entity.Property(e => e.Companyid).HasColumnName("companyid");

                entity.Property(e => e.Fieldname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("fieldname")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Lastupdated)
                    .HasColumnType("datetime")
                    .HasColumnName("lastupdated")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Startupid).HasColumnName("startupid");

                entity.Property(e => e.Value)
                    .IsUnicode(false)
                    .HasColumnName("value")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<UserMission>(entity =>
            {
                entity.HasKey(e => new { e.Userid, e.Missionid });

                entity.ToTable("User_Mission");

                entity.Property(e => e.Userid)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Missionid).HasColumnName("missionid");

                entity.Property(e => e.Displayorder).HasColumnName("displayorder");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<UserNotification>(entity =>
            {
                entity.HasKey(e => new { e.Userid, e.Notificationid });

                entity.ToTable("User_Notification");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Notificationid).HasColumnName("notificationid");

                entity.Property(e => e.Datetime)
                    .HasColumnType("datetime")
                    .HasColumnName("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.Profileid)
                    .HasName("PK_Profile");

                entity.ToTable("UserProfile");

                entity.Property(e => e.BenefitTicket)
                    .HasColumnName("benefit_ticket")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Communication)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("communication")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Createdat)
                    .HasColumnType("datetime")
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasColumnType("ntext")
                    .HasColumnName("description")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Expertise)
                    .HasMaxLength(500)
                    .HasColumnName("expertise")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Investorstatus).HasColumnName("investorstatus");

                entity.Property(e => e.Ispaidmentor).HasColumnName("ispaidmentor");

                entity.Property(e => e.Joinedas)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("joinedas")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastInvoice)
                    .HasColumnName("last_invoice")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Linkedin)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("linkedin")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Mentoringstatus).HasColumnName("mentoringstatus");

                entity.Property(e => e.Mentorprice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("mentorprice");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Photo)
                    .HasColumnType("image")
                    .HasColumnName("photo")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("remarks")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasColumnName("title")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Userid)
                    .HasMaxLength(128)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Userlog>(entity =>
            {
                entity.HasKey(e => e.Logid);

                entity.ToTable("userlog");

                entity.Property(e => e.Logid).HasColumnName("logid");

                entity.Property(e => e.Action)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("action")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Datetime)
                    .HasColumnType("datetime")
                    .HasColumnName("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Userid)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("userid")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<CompanyGoals>(entity =>
            {
                entity.ToTable("Company_Goals");

                entity.HasKey(e => e.IdCompanyGoals);

                entity.Property(e => e.IdCompanyGoals).HasColumnName("id_company_goals");

                entity.Property(e => e.IdGoal).HasColumnName("id_goal");

                entity.Property(e => e.StartupId).HasColumnName("startupid");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IsRewardCollected).HasColumnName("is_reward_collected");
                entity.Property(e => e.DateCreated).HasColumnName("dateCreated");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("Events");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.EventName).HasColumnName("event_name");

                entity.Property(e => e.Logo).HasColumnName("logo");

                entity.Property(e => e.EventDescription).HasColumnName("event_description");

                entity.Property(e => e.EventInfo).HasColumnName("event_info");

                entity.Property(e => e.ApplyTo).HasColumnName("apply_to");
                entity.Property(e => e.EventDateStart).HasColumnName("event_date_start");
                entity.Property(e => e.EventDateEnd).HasColumnName("event_date_end");
                entity.Property(e => e.EventUrl).HasColumnName("event_url");
                entity.Property(e => e.EventType).HasColumnName("event_type");
                entity.Property(e => e.EventAddress).HasColumnName("event_address");
                entity.Property(e => e.EventPlatform).HasColumnName("event_platform");
                entity.Property(e => e.AssignToProgram).HasColumnName("assign_to_program");
                entity.Property(e => e.EmailNotification).HasColumnName("email_notification");
                entity.Property(e => e.ActionType).HasColumnName("action_type");
                entity.Property(e => e.PriceEvent).HasColumnName("price_event");
                entity.Property(e => e.IsPublished).HasColumnName("is_published");
                entity.Property(e => e.tags).HasColumnName("tags");
                entity.Property(e => e.AssignToLevel).HasColumnName("assign_to_level");
                entity.Property(e => e.AssignToCategory).HasColumnName("assign_to_category");
                entity.Property(e => e.CreateDate).HasColumnName("create_date");
                entity.Property(e => e.ProgramGroupId).HasColumnName("program_group_id").HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<CompanyGoalsStep>(entity =>
            {
                entity.ToTable("Company_Goals_Step");

                entity.HasKey(e => e.IdCompanyGoalsStep);

                entity.Property(e => e.IdCompanyGoalsStep).HasColumnName("id_company_goals_step");

                entity.Property(e => e.IdGoalStep).HasColumnName("id_goal_step");

                entity.Property(e => e.StartupId).HasColumnName("startupid");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IsStepComplete).HasColumnName("is_step_complete");

                entity.Property(e => e.IsGoalComplete).HasColumnName("is_goal_complete");

                entity.Property(e => e.TSBCoin).HasColumnName("TSBCoin");
                entity.Property(e => e.DateCreated).HasColumnName("dateCreated");
                entity.Property(e => e.IdGoal).HasColumnName("id_goal");
            });

            modelBuilder.Entity<CompanyGoalsCardFields>(entity =>
            {
                entity.ToTable("Company_Goals_Card_Fields");

                entity.HasKey(e => e.IdCompanyGoalsCard);

                entity.Property(e => e.IdCompanyGoalsCard).HasColumnName("id_company_goals_card");

                entity.Property(e => e.IdCard).HasColumnName("id_card");

                entity.Property(e => e.StartupId).HasColumnName("startupid");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.Property(e => e.DateCreated).HasColumnName("dateCreated");

                entity.Property(e => e.QuestionID).HasColumnName("question_id");
                
                entity.Property(e => e.Score).HasColumnName("score");
                
                entity.Property(e => e.IsCorrected).HasColumnName("isCorrected");
                
            });


            modelBuilder.Entity<VpcCustomerJob>(entity =>
            {
                entity.HasKey(e => e.Customerjobid);

                entity.ToTable("VPC_CustomerJob");

                entity.Property(e => e.Customerjobid).HasColumnName("customerjobid");

                entity.Property(e => e.Createdat)
                    .HasColumnType("datetime")
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Customertypeid).HasColumnName("customertypeid");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("name")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<VpcCustomerNeed>(entity =>
            {
                entity.HasKey(e => e.Customerneedid);

                entity.ToTable("VPC_CustomerNeed");

                entity.Property(e => e.Customerneedid).HasColumnName("customerneedid");

                entity.Property(e => e.Customertypeid).HasColumnName("customertypeid");

                entity.Property(e => e.Name)
                    .HasMaxLength(1500)
                    .HasColumnName("name");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<VpcCustomerType>(entity =>
            {
                entity.HasKey(e => e.Customertypeid);

                entity.ToTable("VPC_CustomerType");

                entity.Property(e => e.Customertypeid).HasColumnName("customertypeid");

                entity.Property(e => e.Createdat)
                    .HasColumnType("datetime")
                    .HasColumnName("createdat")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("name")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Startupid).HasColumnName("startupid");
            });

            modelBuilder.Entity<VpcFeature>(entity =>
            {
                entity.HasKey(e => e.Featureid);

                entity.ToTable("VPC_Feature");

                entity.Property(e => e.Featureid).HasColumnName("featureid");

                entity.Property(e => e.Customerneedid).HasColumnName("customerneedid");

                entity.Property(e => e.Name)
                    .HasMaxLength(1500)
                    .HasColumnName("name")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<VpcProduct>(entity =>
            {
                entity.HasKey(e => e.Productid);

                entity.ToTable("VPC_Product");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Customertypeid).HasColumnName("customertypeid");

                entity.Property(e => e.Name)
                    .HasMaxLength(2500)
                    .HasColumnName("name")
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<WeeklyMentoringHour>(entity =>
            {
                entity.HasKey(e => e.WeeklyMentorId);

                entity.ToTable("Weekly_Mentoring_Hour");

                entity.Property(e => e.WeeklyMentorId).HasColumnName("weekly_mentor_id");

                entity.Property(e => e.MentorId).HasColumnName("mentor_id");

                entity.Property(e => e.WeeklyDay).HasColumnName("weekly_day");

                entity.Property(e => e.WeeklyEndtime)
                    .HasColumnType("datetime")
                    .HasColumnName("weekly_endtime");

                entity.Property(e => e.WeeklyStarttime)
                    .HasColumnType("datetime")
                    .HasColumnName("weekly_starttime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
