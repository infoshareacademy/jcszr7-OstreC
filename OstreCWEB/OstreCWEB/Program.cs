using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OstreCWEB.Data.DataBase;
using OstreCWEB.Data.InitialData;
using OstreCWEB.Data.Repository.Characters;
using OstreCWEB.Data.Repository.Characters.Interfaces;
using OstreCWEB.Data.Repository.Fight;
using OstreCWEB.Data.Repository.Identity;
using OstreCWEB.Data.Repository.ManyToMany;
using OstreCWEB.Data.Repository.SuperAdmin;
using OstreCWEB.Data.ServiceRegistration;
using OstreCWEB.Services.Characters;
using OstreCWEB.Services.Factory;
using OstreCWEB.Services.Fight;
using OstreCWEB.Services.Game;
using OstreCWEB.Services.Identity;
using OstreCWEB.Services.Seed;
using OstreCWEB.Services.ServiceRegistration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Allows retrying CRUD operations in case of transient failures.
builder.Services.AddDbContext<OstreCWebContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("OstreCWEB")));

// for Identity
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<OstreCWebContext>()
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/LoginController/Login");

builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

builder.Services.AddTransient<IFightService,FightService>();
builder.Services.AddSingleton<IFightRepository, FightRepository>();
builder.Services.AddTransient<IFightFactory, FightFactory>(); 
builder.Services.AddTransient<IStatusRepository, StatusRepository>();
builder.Services.AddTransient<ICharacterActionsRepository, CharacterActionRepository>();
builder.Services.AddTransient<IPlayableCharacterRepository, PlayableCharacterRepository >();
builder.Services.AddTransient<IPlayableCharacterService, PlayableCharacterService>();
builder.Services.AddTransient<ISuperAdminRepository, SuperAdminRepository>();
builder.Services.AddTransient<IIdentityRepository, IdentityRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISeeder, SeedCharacters>(); 
builder.Services.AddTransient<CharacterFactory, CharacterFactory>(); 
builder.Services.AddTransient<IUserParagraphRepository, UserParagraphRepository>();
builder.Services.AddTransient<IItemCharacterRepository, ItemCharacterRepository>();
builder.Services.AddTransient<IActionCharacterRepository, ActionCharacterRepository>();
builder.Services.AddTransient<IGameService, GameService>();

//builder.Services.AddTransient<IEnemyRepository,  >();

//builder.Services.AddTransient<ICharacterClassRepository,  >();
//builder.Services.AddTransient<ICharacterRaceRepository,  >();
//builder.Services.AddTransient<IItemRepository,  >();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services
    .AddAutoMapper(typeof(Program))
    .AddControllersWithViews()
.AddRazorRuntimeCompilation();

builder.Services
    .AddRepositories();

builder.Services
    .AddServices();

builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo.Console();
    loggerConfiguration.WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("OstreCWEB"), new MSSqlServerSinkOptions
    {
        AutoCreateSqlTable = true,
        TableName = "OstreCWebLogs"
    },
    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning);
});

 
var app = builder.Build();

 

using (var scope = app.Services.CreateScope())
{ 
    var services = scope.ServiceProvider; 
     SeedStories.Initialize(services); 
}
//var test = new StaticLists();
//test.SeedData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Swagger}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "storyBuilder",
    pattern: "{controller=Home}/{action=Index}/{id?}/{paragraphId?}");
 
app.Run();