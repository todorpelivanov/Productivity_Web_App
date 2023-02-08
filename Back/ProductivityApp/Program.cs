using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProducitivityApp.Helpers.DependencyInjection;
using ProductivityApp.Shared;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Serilog;
using ProductivityApp.Shared.Timer;
using ProductivityApp.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSignalR();

//builder.Services.

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme, e.g" +
        "\"bearer {token} \"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
    //  add the security definition, enable the swagger UI to add the bearer token
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
            GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

//read from appsettings.json, find the property AppSettings from the main object, 
//make instance of the class in order to call the token and connection string property

var appSettings = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure <AppSettings>(appSettings);
AppSettings appSettingsObject = appSettings.Get<AppSettings>();

//Dependency Injection
DependencyInjectionHelper.InjectDbContext(builder.Services, appSettingsObject.ConnectionString);
DependencyInjectionHelper.InjectRepositories(builder.Services);
DependencyInjectionHelper.InjectServices(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
}

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/Productivity_App_Log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();



app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();

});

//builder.Services.AddCors(options => {
//    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod()
//    .AllowAnyHeader()
//    .AllowCredentials()
//    .SetIsOriginAllowed((hosts) => true));
//});

//app.UseEndpoints(endpoints => {
//    endpoints.MapControllers();
//    endpoints.MapHub<MessageHub>("/offers");
//});

//builder.Services.AddCrojJob<>

var autoEvent = new AutoResetEvent(false);

var statusChecker = new StatusChecker(10);

var stateTimer = new Timer(statusChecker.CheckStatus,
                                   autoEvent, 1000, 250);

// When autoEvent signals, change the period to every half second.
autoEvent.WaitOne();
stateTimer.Change(0, 500);
Console.WriteLine("\nChanging period to .5 seconds.\n");

// When autoEvent signals the second time, dispose of the timer.
autoEvent.WaitOne();
stateTimer.Dispose();
Console.WriteLine("\nDestroying timer.");

app.Run();
