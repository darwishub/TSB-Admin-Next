using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;
using TheStartupBuddyV3.Helpers;
using TheStartupBuddyV3.Models;
using TheStartupBuddyV3.Hubs;
using TheStartupBuddyV3.Repository;
using TheStartupBuddyV3.Service;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TSBDatabase");

//builder.Services.AddDbContext<InvesteurContext>(options =>
//    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<InvesteurContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging();
}

    );

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequiredLength = 8;

}).AddEntityFrameworkStores<InvesteurContext>()
.AddDefaultTokenProviders()
.AddTokenProvider<EmailConfirmationTokenProvider<IdentityUser>>("emailconfirmation");
// Add services to the container.
builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromHours(24));
//token for email confirmation
builder.Services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
     opt.TokenLifespan = TimeSpan.FromHours(24));
builder.Services.AddControllers();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<IHelpers, Helpers>();
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);
builder.Services.AddHttpContextAccessor();
var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfig>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddScoped<IServiceWrapper, ServiceWrapper>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters()
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidAudience = builder.Configuration["JWT:ValidAudience"],
         ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
         ValidateLifetime = true,
         ClockSkew = TimeSpan.Zero
     };
     options.Events = new JwtBearerEvents();
     options.Events.OnMessageReceived = context =>
     {

         if (context.Request.Cookies.ContainsKey("X-Access-Token"))
         {
             context.Token = context.Request.Cookies["X-Access-Token"];
         }

         return Task.CompletedTask;
     };
 });
builder.Services.AddSignalR();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
    {
        builder.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins("https://localhost:3000");
    }));

//google calendar
builder.Services.Configure<GoogleCalendarSettings>(builder.Configuration.GetSection(nameof(GoogleCalendarSettings)));
builder.Services.AddSingleton<IGoogleCalendarSettings>(s => s.GetRequiredService<IOptions<GoogleCalendarSettings>>().Value);

builder.Services.AddScoped<IGoogleCalendarService, GoogleCalendarService>();
// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.MapHub<ChatHub>("api/ChatHub");
app.MapFallbackToFile("index.html");
app.Use(async (context, next) =>
    {
        var token = context.Request.Cookies["X-Access-Token"];
        if (!string.IsNullOrEmpty(token))
        {
            context.Response.Headers.Add("authorization", "Bearer " + token);
        }
        await next();
    });
StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<String>();
app.Run();
