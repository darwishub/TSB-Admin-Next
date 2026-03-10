using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheStartupBuddyV3.Migrations
{
    public partial class tsbnext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "__MigrationHistory",
                columns: table => new
                {
                    MigrationId = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContextKey = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Model = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProductVersion = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.__MigrationHistory", x => new { x.MigrationId, x.ContextKey });
                });

            migrationBuilder.CreateTable(
                name: "AdminUser",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false, comment: "''"),
                    password = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, comment: "''"),
                    role = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false, comment: "''"),
                    platformaccess = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, comment: "''")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUser", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PlatformAccess = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Role = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    TwoFactorStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Benefits",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    benefit_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    logo = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "('')"),
                    worth = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, defaultValueSql: "('')"),
                    pack_type = table.Column<byte>(type: "tinyint", nullable: false),
                    benefit_description = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "('')"),
                    benefit_info = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "('')"),
                    apply_to = table.Column<byte>(type: "tinyint", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    id_card = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_step = table.Column<int>(type: "int", nullable: true),
                    help_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field_code = table.Column<int>(type: "int", nullable: true),
                    field_code_type = table.Column<int>(type: "int", nullable: true),
                    placeholder = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    type_rule = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    required = table.Column<bool>(type: "bit", nullable: true),
                    disabled = table.Column<bool>(type: "bit", nullable: true),
                    max_data = table.Column<int>(type: "int", nullable: true),
                    allow_multiple = table.Column<bool>(type: "bit", nullable: true),
                    display_order = table.Column<byte>(type: "tinyint", nullable: true),
                    success_message = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    has_option = table.Column<bool>(type: "bit", nullable: true),
                    options = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.id_card);
                });

            migrationBuilder.CreateTable(
                name: "Chosen_Benefit",
                columns: table => new
                {
                    chosen_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    benefit_owner_userid = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    benefit_chosen_id = table.Column<int>(type: "int", nullable: false),
                    benefit_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    benefit_owner_startupid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chosen_Benefit", x => x.chosen_id);
                });

            migrationBuilder.CreateTable(
                name: "Community_Startup",
                columns: table => new
                {
                    communityid = table.Column<int>(type: "int", nullable: false),
                    startupid = table.Column<int>(type: "int", nullable: false),
                    joineddate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Community_Startup", x => new { x.communityid, x.startupid });
                });

            migrationBuilder.CreateTable(
                name: "Community_User",
                columns: table => new
                {
                    communityid = table.Column<int>(type: "int", nullable: false),
                    userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Community_User", x => new { x.communityid, x.userid });
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    companyid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    companyname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, defaultValueSql: "('')"),
                    website = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    description = table.Column<string>(type: "text", nullable: false),
                    contact = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.companyid);
                });

            migrationBuilder.CreateTable(
                name: "Company_Goals",
                columns: table => new
                {
                    id_company_goals = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_goal_step = table.Column<int>(type: "int", nullable: false),
                    startupid = table.Column<int>(type: "int", nullable: true),
                    id_user = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_step_complete = table.Column<bool>(type: "bit", nullable: true),
                    is_goal_complete = table.Column<bool>(type: "bit", nullable: true),
                    TSBCoin = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_Goals", x => x.id_company_goals);
                });

            migrationBuilder.CreateTable(
                name: "CountClickedProfileStartup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clickedProfile = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountClickedProfileStartup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "emailtempalte",
                columns: table => new
                {
                    Column0 = table.Column<string>(name: "Column 0", type: "text", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "emailtempalte2",
                columns: table => new
                {
                    Column0 = table.Column<string>(name: "Column 0", type: "xml", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Enterprise",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    enteprise = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    expirydate = table.Column<DateTime>(type: "datetime", nullable: true),
                    source = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprise", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Funding_Field_File",
                columns: table => new
                {
                    userid = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    field_id = table.Column<int>(type: "int", nullable: false),
                    file_name = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    data = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    type = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    lastupdated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Funding_Field_Value",
                columns: table => new
                {
                    userid = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    field_id = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    lastupdated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Funding_Mission_Field",
                columns: table => new
                {
                    field_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mission_id = table.Column<int>(type: "int", nullable: false),
                    title_name = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    control_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    options = table.Column<string>(type: "text", nullable: true),
                    display_order = table.Column<short>(type: "smallint", nullable: true),
                    is_disable = table.Column<bool>(type: "bit", nullable: true),
                    is_required = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funding_Mission_Field", x => x.field_id);
                });

            migrationBuilder.CreateTable(
                name: "Funding_Mission_Title",
                columns: table => new
                {
                    mission_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    default_order = table.Column<short>(type: "smallint", nullable: false),
                    is_disable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funding_Mission_Title", x => x.mission_id);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    id_goal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title_goal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_level = table.Column<int>(type: "int", nullable: true),
                    is_required = table.Column<bool>(type: "bit", nullable: false),
                    logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    help_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    total_points = table.Column<int>(type: "int", nullable: true),
                    locked_status = table.Column<bool>(type: "bit", nullable: true),
                    status_complete = table.Column<bool>(type: "bit", nullable: true),
                    status_promo = table.Column<bool>(type: "bit", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    second_title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.id_goal);
                });

            migrationBuilder.CreateTable(
                name: "Goals_Card_Fields",
                columns: table => new
                {
                    id_card = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_step = table.Column<int>(type: "int", nullable: false),
                    help_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    field_code = table.Column<byte>(type: "tinyint", nullable: false),
                    field_code_type = table.Column<byte>(type: "tinyint", nullable: false),
                    has_option = table.Column<bool>(type: "bit", nullable: false),
                    options = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    display_order = table.Column<byte>(type: "tinyint", nullable: false),
                    success_message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    placeholder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type_rule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_required = table.Column<bool>(type: "bit", nullable: false),
                    is_disabled = table.Column<bool>(type: "bit", nullable: false),
                    max_data = table.Column<byte>(type: "tinyint", nullable: false),
                    allow_multiple = table.Column<bool>(type: "bit", nullable: false),
                    files_type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals_Card_Fields", x => x.id_card);
                });

            migrationBuilder.CreateTable(
                name: "Goals_Category",
                columns: table => new
                {
                    id_goals_category = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_goals_category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    logo_category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    money_category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.id_goals_category);
                });

            migrationBuilder.CreateTable(
                name: "Goals_Step",
                columns: table => new
                {
                    id_goal_step = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_goal = table.Column<int>(type: "int", nullable: false),
                    id_step = table.Column<int>(type: "int", nullable: false),
                    goal_step_date_created = table.Column<DateTime>(type: "datetime", nullable: false),
                    order_step = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goas_Step", x => x.id_goal_step);
                });

            migrationBuilder.CreateTable(
                name: "Goals_Templates",
                columns: table => new
                {
                    id_template = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    photo_template = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_level = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals_Templates", x => x.id_template);
                });

            migrationBuilder.CreateTable(
                name: "HangOutLink",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    mentorsessionid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangOutLink", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "History_Company_Steps",
                columns: table => new
                {
                    id_history_step = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_goal_step = table.Column<int>(type: "int", nullable: false),
                    id_company_goals = table.Column<int>(type: "int", nullable: false),
                    StartupId = table.Column<int>(type: "int", nullable: true),
                    id_user = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_step_complete = table.Column<bool>(type: "bit", nullable: true),
                    is_goal_complete = table.Column<bool>(type: "bit", nullable: true),
                    tsb_coin = table.Column<int>(type: "int", nullable: false),
                    datetime_history = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History_Company_Steps", x => x.id_history_step);
                });

            migrationBuilder.CreateTable(
                name: "Investment_Investor_Buy_Shares",
                columns: table => new
                {
                    buy_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startup_shares_owner_id = table.Column<int>(type: "int", nullable: false),
                    investor_userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    investor_email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    amount_shares_bought = table.Column<int>(type: "int", nullable: false),
                    current_price_per_share = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_current_shares_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    buy_datetime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investment_Investor_Buy_Shares", x => x.buy_id);
                });

            migrationBuilder.CreateTable(
                name: "Investment_Investor_Offer_Shares",
                columns: table => new
                {
                    offer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    investor_email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    investor_userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    offer_to_startupid = table.Column<int>(type: "int", nullable: false),
                    share_amount_offer = table.Column<int>(type: "int", nullable: false),
                    price_per_share_offer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_share_offer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    time_offer = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investment_Offer_Shares", x => x.offer_id);
                });

            migrationBuilder.CreateTable(
                name: "Investment_Investor_Sell_Shares",
                columns: table => new
                {
                    sell_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    investor_email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    investor_userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "(N'((\"\"))')"),
                    sell_to_startupid = table.Column<int>(type: "int", nullable: false),
                    share_amount_sell = table.Column<int>(type: "int", nullable: false),
                    price_per_share_sell = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    total_share_sell = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    time_sell = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investment_Investor_Sell_Shares", x => x.sell_id);
                });

            migrationBuilder.CreateTable(
                name: "Investment_Sell_Shares",
                columns: table => new
                {
                    userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    startupid = table.Column<int>(type: "int", nullable: false),
                    virtual_shares_amount = table.Column<int>(type: "int", nullable: false),
                    price_per_share = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sell_datetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    id = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investment_Sell_Shares", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "Investor",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    investortype = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    vcname = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    marketfocus = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    investmentregions = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "('')"),
                    investmentcategories = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "('')"),
                    minimumrevenue = table.Column<int>(type: "int", nullable: false),
                    minInvestment = table.Column<int>(type: "int", nullable: false),
                    maxInvestment = table.Column<int>(type: "int", nullable: false),
                    isapproved = table.Column<bool>(type: "bit", nullable: false),
                    invesmentstage = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, defaultValueSql: "('')"),
                    invesmentdeclare = table.Column<byte>(type: "tinyint", nullable: true, defaultValueSql: "((1))"),
                    virtualmoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValueSql: "((5000000))"),
                    double_money = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investor", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Investor_Member",
                columns: table => new
                {
                    id_investor_member = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid_team_owner = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, defaultValueSql: "((0))"),
                    member_email = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false, defaultValueSql: "('')"),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    member_name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investor_Member", x => x.id_investor_member);
                });

            migrationBuilder.CreateTable(
                name: "Investor_Mission",
                columns: table => new
                {
                    userid = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    missionid = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    displayorder = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investor_Mission", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    id_level = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_goals_category = table.Column<int>(type: "int", nullable: false),
                    description_level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    second_title_level = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    waiting_time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    logo_level = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.id_level);
                });

            migrationBuilder.CreateTable(
                name: "Membership",
                columns: table => new
                {
                    membership_id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    plan_id = table.Column<short>(type: "smallint", nullable: false),
                    plan_duration = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "((1))"),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership", x => x.membership_id);
                });

            migrationBuilder.CreateTable(
                name: "Mentor",
                columns: table => new
                {
                    mentorid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    description = table.Column<string>(type: "ntext", nullable: false, defaultValueSql: "('')"),
                    photo = table.Column<byte[]>(type: "image", nullable: false, defaultValueSql: "('')"),
                    communication = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false, defaultValueSql: "('')"),
                    remarks = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentor", x => x.mentorid);
                });

            migrationBuilder.CreateTable(
                name: "mentor_mission",
                columns: table => new
                {
                    mentorid = table.Column<int>(type: "int", nullable: false),
                    missionid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mentor_mission", x => new { x.mentorid, x.missionid });
                });

            migrationBuilder.CreateTable(
                name: "Mentor_Session",
                columns: table => new
                {
                    sessionid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mentorid = table.Column<int>(type: "int", nullable: false),
                    userid = table.Column<string>(type: "nchar(128)", fixedLength: true, maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    starttime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    endtime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    hangout = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentor_Session", x => x.sessionid);
                });

            migrationBuilder.CreateTable(
                name: "Meta_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    page = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    meta_key = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    data = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meta_data", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Mission_Field",
                columns: table => new
                {
                    fieldid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    missionid = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    controltype = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    options = table.Column<string>(type: "text", nullable: true, defaultValueSql: "('')"),
                    displayorder = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    isdisable = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    popovertitle = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    popovercontent = table.Column<string>(type: "text", nullable: false, defaultValueSql: "('')"),
                    isrequired = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mission_Field", x => x.fieldid);
                });

            migrationBuilder.CreateTable(
                name: "Networking_Profiles",
                columns: table => new
                {
                    userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    my_role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    my_expertise = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false, defaultValueSql: "('')"),
                    profile_img_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    network_with_roles = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    network_with_expertise_at = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false, defaultValueSql: "('')"),
                    ispublic = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    notificationid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false, defaultValueSql: "('')"),
                    button = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    link = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    datecreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    expirydate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    usertype = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.notificationid);
                });

            migrationBuilder.CreateTable(
                name: "PortalUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValueSql: "('')"),
                    Password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true, defaultValueSql: "('')"),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    userid = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    questionid = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Program",
                columns: table => new
                {
                    programid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    startdate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    enddate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    limit = table.Column<int>(type: "int", nullable: false),
                    program_type = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    permission = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.programid);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    controltype = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    tab = table.Column<int>(type: "int", nullable: false),
                    options = table.Column<string>(type: "text", nullable: false),
                    isdisable = table.Column<bool>(type: "bit", nullable: false),
                    displayorder = table.Column<int>(type: "int", nullable: false),
                    qid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Question_Category_ScoreValue",
                columns: table => new
                {
                    scorevalueid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    questionid = table.Column<int>(type: "int", nullable: false),
                    category = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    eligiblity = table.Column<bool>(type: "bit", nullable: false),
                    weight = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    value = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    valuetext = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    valuefrom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    valueto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_Category_ScoreValue", x => x.scorevalueid);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    serviceid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    description = table.Column<string>(type: "text", nullable: false, defaultValueSql: "('')"),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    discount = table.Column<short>(type: "smallint", nullable: false),
                    category = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    unit = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValueSql: "('')"),
                    investeurprice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    missionid = table.Column<int>(type: "int", nullable: false),
                    providerid = table.Column<int>(type: "int", nullable: false),
                    isapproved = table.Column<bool>(type: "bit", nullable: false),
                    startupid = table.Column<int>(type: "int", nullable: false),
                    views = table.Column<int>(type: "int", nullable: false),
                    clicks = table.Column<int>(type: "int", nullable: false),
                    createdat = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Service_Field",
                columns: table => new
                {
                    fieldid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serviceid = table.Column<int>(type: "int", nullable: false),
                    fieldname = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false, defaultValueSql: "('')"),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service_Field", x => x.fieldid);
                });

            migrationBuilder.CreateTable(
                name: "Service_File",
                columns: table => new
                {
                    fileid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serviceid = table.Column<int>(type: "int", nullable: false),
                    data = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Service_Purchase",
                columns: table => new
                {
                    purchaseid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<string>(type: "nchar(128)", fixedLength: true, maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    serviceid = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    mobile = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    purchasetime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    paymentid = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service_Purchase", x => x.purchaseid);
                });

            migrationBuilder.CreateTable(
                name: "ShareLink",
                columns: table => new
                {
                    linkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    token = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    startupid = table.Column<int>(type: "int", nullable: false),
                    datecreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    validdays = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((10))"),
                    sharecontent = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    url = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.linkid);
                });

            migrationBuilder.CreateTable(
                name: "Startup",
                columns: table => new
                {
                    startupid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    description = table.Column<string>(type: "ntext", nullable: false, defaultValueSql: "('')"),
                    website = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    publicprofilevisible = table.Column<bool>(type: "bit", nullable: false),
                    logo = table.Column<byte[]>(type: "image", nullable: true),
                    startupstatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    clicks = table.Column<int>(type: "int", nullable: false),
                    ispublic = table.Column<bool>(type: "bit", nullable: false),
                    virtualshares = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1000000))"),
                    double_shares = table.Column<byte>(type: "tinyint", nullable: false),
                    pitch_url = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "('')"),
                    video_url = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "('')"),
                    current_fund = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValueSql: "((0))"),
                    total_fund = table.Column<decimal>(type: "decimal(18,2)", nullable: true, defaultValueSql: "((0))"),
                    benefit_ticket = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    last_invoice = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Startup", x => x.startupid);
                });

            migrationBuilder.CreateTable(
                name: "Startup_Achievement",
                columns: table => new
                {
                    achievementid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startupid = table.Column<int>(type: "int", nullable: false),
                    metric = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, defaultValueSql: "('')"),
                    change = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "('')"),
                    datetime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    week = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Startup_Achievement", x => x.achievementid);
                });

            migrationBuilder.CreateTable(
                name: "Startup_BMC",
                columns: table => new
                {
                    startupid = table.Column<int>(type: "int", nullable: false),
                    data = table.Column<string>(type: "ntext", nullable: false),
                    lastupdated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Startup_BMC", x => x.startupid);
                });

            migrationBuilder.CreateTable(
                name: "Startup_DemoDay",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<string>(type: "nchar(128)", fixedLength: true, maxLength: 128, nullable: false),
                    applicationdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    startupid = table.Column<int>(type: "int", nullable: false),
                    chargeid = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Startup_DemoDay", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Startup_Goal",
                columns: table => new
                {
                    goalid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startupid = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "('')"),
                    datetime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    lastupdated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    week = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    assignee_to = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    due_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    userid_assignee = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Startup_Goal", x => x.goalid);
                });

            migrationBuilder.CreateTable(
                name: "Startup_Investor",
                columns: table => new
                {
                    startupid = table.Column<int>(type: "int", nullable: false),
                    userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    date_time_fav = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Startup_Member",
                columns: table => new
                {
                    startupid = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    type = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Startup_Member", x => new { x.startupid, x.email });
                });

            migrationBuilder.CreateTable(
                name: "Startup_Mission",
                columns: table => new
                {
                    missionid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValueSql: "('')"),
                    description = table.Column<string>(type: "text", nullable: false, defaultValueSql: "('')"),
                    isbonus = table.Column<bool>(type: "bit", nullable: false),
                    percentage = table.Column<short>(type: "smallint", nullable: false),
                    defaultorder = table.Column<short>(type: "smallint", nullable: false),
                    subtitle = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    isdisable = table.Column<bool>(type: "bit", nullable: false),
                    assignTo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Startup_Mission", x => x.missionid);
                });

            migrationBuilder.CreateTable(
                name: "Startup_Program",
                columns: table => new
                {
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    StartupId = table.Column<int>(type: "int", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Startup_Program", x => new { x.ProgramId, x.StartupId, x.JoinDate });
                });

            migrationBuilder.CreateTable(
                name: "Startup_Progress",
                columns: table => new
                {
                    progressid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    progress = table.Column<int>(type: "int", nullable: false),
                    updatedat = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    companyid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Startup_Progress", x => x.progressid);
                });

            migrationBuilder.CreateTable(
                name: "Startup_PV",
                columns: table => new
                {
                    startupid = table.Column<int>(type: "int", nullable: false),
                    data = table.Column<string>(type: "ntext", nullable: false, defaultValueSql: "('')"),
                    lastupdated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Startup_PV", x => x.startupid);
                });

            migrationBuilder.CreateTable(
                name: "Startup_VPC",
                columns: table => new
                {
                    startupid = table.Column<int>(type: "int", nullable: false),
                    data = table.Column<string>(type: "ntext", nullable: false),
                    lastupdated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Startup_VPC", x => x.startupid);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    id_step = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title_step = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_date_step = table.Column<DateTime>(type: "datetime", nullable: true),
                    description_step = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.id_step);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    subscriptionid = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    status = table.Column<int>(type: "int", nullable: false),
                    starttime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    plantype = table.Column<short>(type: "smallint", nullable: false),
                    enddate = table.Column<DateTime>(type: "datetime", nullable: true),
                    duration = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    defaultplan = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.subscriptionid);
                });

            migrationBuilder.CreateTable(
                name: "Subscription_Investor",
                columns: table => new
                {
                    subscriptionid = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    status = table.Column<int>(type: "int", nullable: false),
                    starttime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    plantype = table.Column<short>(type: "smallint", nullable: false),
                    enddate = table.Column<DateTime>(type: "datetime", nullable: true),
                    duration = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    defaultplan = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription_Investor", x => x.subscriptionid);
                });

            migrationBuilder.CreateTable(
                name: "ToolkitLearnings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Exclusive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolkitLearnings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    Email = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false, defaultValueSql: "('')"),
                    LoginProvider = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    ProviderKey = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    DisplayName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    JoinDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Source = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    Country = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    City = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "User_Field_File",
                columns: table => new
                {
                    fileid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    fieldid = table.Column<int>(type: "int", nullable: false),
                    filename = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    data = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    type = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    startupid = table.Column<int>(type: "int", nullable: false),
                    lastupdated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Field_File", x => x.fileid);
                });

            migrationBuilder.CreateTable(
                name: "User_Field_Value",
                columns: table => new
                {
                    userid = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    fieldid = table.Column<int>(type: "int", nullable: false),
                    companyid = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, defaultValueSql: "('')"),
                    lastupdated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    fieldname = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    startupid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Field_Value", x => new { x.userid, x.fieldid, x.companyid });
                });

            migrationBuilder.CreateTable(
                name: "User_Mission",
                columns: table => new
                {
                    userid = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    missionid = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    displayorder = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Mission", x => new { x.userid, x.missionid });
                });

            migrationBuilder.CreateTable(
                name: "User_Notification",
                columns: table => new
                {
                    userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    notificationid = table.Column<int>(type: "int", nullable: false),
                    datetime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Notification", x => new { x.userid, x.notificationid });
                });

            migrationBuilder.CreateTable(
                name: "userlog",
                columns: table => new
                {
                    logid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    action = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    datetime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userlog", x => x.logid);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Profileid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    description = table.Column<string>(type: "ntext", nullable: false, defaultValueSql: "('')"),
                    photo = table.Column<byte[]>(type: "image", nullable: false, defaultValueSql: "('')"),
                    communication = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false, defaultValueSql: "('')"),
                    remarks = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "('')"),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, defaultValueSql: "('')"),
                    linkedin = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    joinedas = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('')"),
                    mentoringstatus = table.Column<bool>(type: "bit", nullable: false),
                    investorstatus = table.Column<bool>(type: "bit", nullable: false),
                    ispaidmentor = table.Column<bool>(type: "bit", nullable: false),
                    expertise = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    mentorprice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    createdat = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    benefit_ticket = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    last_invoice = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Profileid);
                });

            migrationBuilder.CreateTable(
                name: "VPC_CustomerJob",
                columns: table => new
                {
                    customerjobid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    customertypeid = table.Column<int>(type: "int", nullable: false),
                    createdat = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VPC_CustomerJob", x => x.customerjobid);
                });

            migrationBuilder.CreateTable(
                name: "VPC_CustomerNeed",
                columns: table => new
                {
                    customerneedid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customertypeid = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    type = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VPC_CustomerNeed", x => x.customerneedid);
                });

            migrationBuilder.CreateTable(
                name: "VPC_CustomerType",
                columns: table => new
                {
                    customertypeid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, defaultValueSql: "('')"),
                    startupid = table.Column<int>(type: "int", nullable: false),
                    createdat = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VPC_CustomerType", x => x.customertypeid);
                });

            migrationBuilder.CreateTable(
                name: "VPC_Feature",
                columns: table => new
                {
                    featureid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerneedid = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VPC_Feature", x => x.featureid);
                });

            migrationBuilder.CreateTable(
                name: "VPC_Product",
                columns: table => new
                {
                    productid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customertypeid = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: false, defaultValueSql: "('')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VPC_Product", x => x.productid);
                });

            migrationBuilder.CreateTable(
                name: "Weekly_Mentoring_Hour",
                columns: table => new
                {
                    weekly_mentor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mentor_id = table.Column<int>(type: "int", nullable: false),
                    weekly_day = table.Column<byte>(type: "tinyint", nullable: false),
                    weekly_starttime = table.Column<DateTime>(type: "datetime", nullable: false),
                    weekly_endtime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weekly_Mentoring_Hour", x => x.weekly_mentor_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_dbo.UserRoles_dbo.Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo.UserRoles_dbo.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "AdminUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavouritedDataLog",
                columns: table => new
                {
                    fav_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startupid = table.Column<int>(type: "int", nullable: false),
                    userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    date_time_fav = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouritedDataLog", x => x.fav_id);
                    table.ForeignKey(
                        name: "FK_FavouritedDataLog_Startup",
                        column: x => x.startupid,
                        principalTable: "Startup",
                        principalColumn: "startupid");
                });

            migrationBuilder.CreateTable(
                name: "ClicksDataLog",
                columns: table => new
                {
                    click_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    latest_count = table.Column<int>(type: "int", nullable: false),
                    click_date_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    startupid = table.Column<int>(type: "int", nullable: false),
                    userid = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ClicksDa__92D409639FFA8329", x => x.click_id);
                    table.ForeignKey(
                        name: "FK__ClicksDat__start__5F141958",
                        column: x => x.startupid,
                        principalTable: "Startup",
                        principalColumn: "startupid");
                    table.ForeignKey(
                        name: "FK_ClicksDataLog_User",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClicksDataLog_startupid",
                table: "ClicksDataLog",
                column: "startupid");

            migrationBuilder.CreateIndex(
                name: "IX_ClicksDataLog_userid",
                table: "ClicksDataLog",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_FavouritedDataLog_startupid",
                table: "FavouritedDataLog",
                column: "startupid");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__MigrationHistory");

            migrationBuilder.DropTable(
                name: "AdminUser");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Benefits");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Chosen_Benefit");

            migrationBuilder.DropTable(
                name: "ClicksDataLog");

            migrationBuilder.DropTable(
                name: "Community_Startup");

            migrationBuilder.DropTable(
                name: "Community_User");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Company_Goals");

            migrationBuilder.DropTable(
                name: "CountClickedProfileStartup");

            migrationBuilder.DropTable(
                name: "emailtempalte");

            migrationBuilder.DropTable(
                name: "emailtempalte2");

            migrationBuilder.DropTable(
                name: "Enterprise");

            migrationBuilder.DropTable(
                name: "FavouritedDataLog");

            migrationBuilder.DropTable(
                name: "Funding_Field_File");

            migrationBuilder.DropTable(
                name: "Funding_Field_Value");

            migrationBuilder.DropTable(
                name: "Funding_Mission_Field");

            migrationBuilder.DropTable(
                name: "Funding_Mission_Title");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Goals_Card_Fields");

            migrationBuilder.DropTable(
                name: "Goals_Category");

            migrationBuilder.DropTable(
                name: "Goals_Step");

            migrationBuilder.DropTable(
                name: "Goals_Templates");

            migrationBuilder.DropTable(
                name: "HangOutLink");

            migrationBuilder.DropTable(
                name: "History_Company_Steps");

            migrationBuilder.DropTable(
                name: "Investment_Investor_Buy_Shares");

            migrationBuilder.DropTable(
                name: "Investment_Investor_Offer_Shares");

            migrationBuilder.DropTable(
                name: "Investment_Investor_Sell_Shares");

            migrationBuilder.DropTable(
                name: "Investment_Sell_Shares");

            migrationBuilder.DropTable(
                name: "Investor");

            migrationBuilder.DropTable(
                name: "Investor_Member");

            migrationBuilder.DropTable(
                name: "Investor_Mission");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Membership");

            migrationBuilder.DropTable(
                name: "Mentor");

            migrationBuilder.DropTable(
                name: "mentor_mission");

            migrationBuilder.DropTable(
                name: "Mentor_Session");

            migrationBuilder.DropTable(
                name: "Meta_data");

            migrationBuilder.DropTable(
                name: "Mission_Field");

            migrationBuilder.DropTable(
                name: "Networking_Profiles");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PortalUsers");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "Program");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Question_Category_ScoreValue");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Service_Field");

            migrationBuilder.DropTable(
                name: "Service_File");

            migrationBuilder.DropTable(
                name: "Service_Purchase");

            migrationBuilder.DropTable(
                name: "ShareLink");

            migrationBuilder.DropTable(
                name: "Startup_Achievement");

            migrationBuilder.DropTable(
                name: "Startup_BMC");

            migrationBuilder.DropTable(
                name: "Startup_DemoDay");

            migrationBuilder.DropTable(
                name: "Startup_Goal");

            migrationBuilder.DropTable(
                name: "Startup_Investor");

            migrationBuilder.DropTable(
                name: "Startup_Member");

            migrationBuilder.DropTable(
                name: "Startup_Mission");

            migrationBuilder.DropTable(
                name: "Startup_Program");

            migrationBuilder.DropTable(
                name: "Startup_Progress");

            migrationBuilder.DropTable(
                name: "Startup_PV");

            migrationBuilder.DropTable(
                name: "Startup_VPC");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "Subscription_Investor");

            migrationBuilder.DropTable(
                name: "ToolkitLearnings");

            migrationBuilder.DropTable(
                name: "User_Field_File");

            migrationBuilder.DropTable(
                name: "User_Field_Value");

            migrationBuilder.DropTable(
                name: "User_Mission");

            migrationBuilder.DropTable(
                name: "User_Notification");

            migrationBuilder.DropTable(
                name: "userlog");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "VPC_CustomerJob");

            migrationBuilder.DropTable(
                name: "VPC_CustomerNeed");

            migrationBuilder.DropTable(
                name: "VPC_CustomerType");

            migrationBuilder.DropTable(
                name: "VPC_Feature");

            migrationBuilder.DropTable(
                name: "VPC_Product");

            migrationBuilder.DropTable(
                name: "Weekly_Mentoring_Hour");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Startup");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "AdminUsers");
        }
    }
}
