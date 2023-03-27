using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OstreCWEB.Repository.DataBase;
using OstreCWEB.Repository.RepositoryRegistration;
using OstreCWEB.DomainModels.Identity;
using OstreCWEB.Services.ServiceRegistration;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Text.Json.Serialization;
using OstreCWEB.Repository.InitialData;
using OstreCWEB.Services.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Allows retrying CRUD operations in case of transient failures.
//builder.Services.AddDbContext<OstreCWebContext>(
//    options => options.UseSqlServer(builder.Configuration.GetConnectionString("OstreCWEB")));
builder.Services.AddDbContext<OstreCWebContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OstreCWEB"));
    options.EnableSensitiveDataLogging();
});

// for Identity
builder.Services.AddIdentity<User, IdentityRole<int>>(options => { options.Stores.MaxLengthForKeys = 128; })
.AddEntityFrameworkStores<OstreCWebContext>()
.AddRoles<IdentityRole<int>>()
.AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Login/Login");

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services
    .AddAutoMapper(typeof(Program))
    .AddControllersWithViews()
    .AddJsonOptions(option => option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
    .AddRazorRuntimeCompilation();


builder.Services
    .AddRepositories();

builder.Services
    .AddServices();

builder.Services.AddHttpClient(TestRestApiConfiguration.TestRestApiClientName,
                client => { client.BaseAddress = new Uri("https://api.open5e.com"); });

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


app.Services.GetRequiredService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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

app.MapControllerRoute(
    name: "EnemyInParagraph",
    pattern: "{controller=Home}/{action=Index}/{fightParagraphId?}/{paragraphId?}/");

app.MapControllerRoute(
    name: "ChooseSecondParagraph",
    pattern: "{controller=Home}/{action=Index}/{storyId?}/{firstParagraphId?}/");

app.MapControllerRoute(
    name: "ChoiceCreator",
    pattern: "{controller=Home}/{action=Index}/{firstParagraphId?}/{secondParagraphId?}");

app.MapControllerRoute(
    name: "DeleteChoice",
    pattern: "{controller=Home}/{action=Index}/{choiceId?}/{storyId?}");

app.MapControllerRoute(
    name: "ChooseSecondParagraph",
    pattern: "{controller=Home}/{action=Index}/{storyId?}/{choiceId?}/");

app.MapControllerRoute(
    name: "ChangeSecondParagraph",
    pattern: "{controller=Home}/{action=Index}/{id?}/{secondParagraphId?}/");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<OstreCWebContext>();

    context.Database.Migrate();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = services.GetRequiredService<UserManager<User>>();


    if (!context.Users.Any())
    {
        SeedDevelopmentData.Initialize(context, userManager, roleManager).Wait();
    }
}

app.Run();