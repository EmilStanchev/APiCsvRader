using ApiServices.Implementation;
using ApiServices.Interfaces;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITableService, TableService>();
string relativePath = Path.Combine("D:\\VTU software engineering\\C#\\API\\CsvUniProject\\APiCsvRader\\CsvReader", "database.db");
builder.Services.AddSingleton<IDatabaseService>(provider =>
{
    // Resolve ITableService from the service provider
    ITableService tableService = provider.GetRequiredService<ITableService>();
    return new DatabaseService($"Data Source={relativePath}", tableService);
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
