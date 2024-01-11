using ApiDatabaseServices.Implementation;
using ApiDatabaseServices.Interfaces;
using ApiServices.Implementation;
using ApiServices.Interfaces;
using CsvReader.API.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITableService, TableService>();
builder.Services.AddSingleton<ITableCreator, TableCreator>();
builder.Services.AddSingleton<IDataInserter, DataInserter>();
builder.Services.AddSingleton<IDatabaseConfiguration, DatabaseConfiguration>();
string relativePath = Path.Combine("D:\\VTU software engineering\\C#\\API\\CsvUniProject\\APiCsvRader\\CsvReader", "database.db");
builder.Services.AddSingleton<IDatabaseService>(provider =>
{
    var config = provider.GetRequiredService<IDatabaseConfiguration>();
    return new DatabaseService($"Data Source={relativePath}", config);
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<IpRestrictionMiddleware>();
app.UseAuthorization();
app.UseMiddleware<HeaderMiddleware>(); 
app.MapControllers();

app.Run();

