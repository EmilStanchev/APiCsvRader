using ApiDatabaseServices.Implementation;
using ApiDatabaseServices.Interfaces;
using ApiServices.Implementation;
using ApiServices.Interfaces;
using CsvReader.API.Middlewares;
using CsvReaderAPI.Services.Implementation;
using CsvReaderAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Quartz;
using CsvReader.API.Jobs;
using ApiDatabaseServices.Helpers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

//db services 

builder.Services.AddSingleton<ITableService, TableService>();
builder.Services.AddSingleton<IDeleteService, DeleteService>();
builder.Services.AddSingleton<ITableCreator, TableCreator>();
builder.Services.AddSingleton<IDataInserter, DataInserter>();
builder.Services.AddSingleton<IStatisticService, StatisticSerive>();
builder.Services.AddSingleton<IDatabaseConfiguration, DatabaseConfiguration>();
string relativePath = Path.Combine("D:\\VTU software engineering\\C#\\API\\CsvUniProject\\APiCsvRader\\CsvReader", "database.db");
builder.Services.AddSingleton<IDatabaseService>(provider =>
{
    var config = provider.GetRequiredService<IDatabaseConfiguration>();
    return new DatabaseService($"Data Source={relativePath}", config);
});

// services for controllers 
builder.Services.AddSingleton<IApiPrinter, ApiPrinter>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IAuthorizationService, AuthorizationService>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IOrganizationService, OrganizationSerive>();
builder.Services.AddSingleton<ICountryService, CountryService>();

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("InfoFileJob");
    q.AddJob<InfoFileJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("InfoFileJob-trigger")
         // this is for 10 secs and its for test.WithCronSchedule("0/10 * * ? * *"));
         .WithCronSchedule("0 0 0 * *  ?"));

});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true); builder.Services.AddAuthentication().AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("SymmetricKey"))),
        };
    });

// swagger configuration for bearer token
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<IpRestrictionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<HeaderMiddleware>(); 
app.MapControllers();

app.Run();

