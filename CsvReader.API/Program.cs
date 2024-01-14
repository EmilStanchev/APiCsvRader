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

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IAuthorizationService, AuthorizationService>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IOrganizationService, OrganizationSerive>();
builder.Services.AddSingleton<ICountryService, CountryService>();

builder.Services.AddAuthentication().AddJwtBearer(
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

