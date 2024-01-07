// See https://aka.ms/new-console-template for more information
using Data;
using Data.Implementation;

Console.WriteLine("Hello, World!");
string databasePath = "D:\\VTU software engineering\\C#\\API\\CsvReader\\CsvReader\\database.db";
DatabaseService databaseService = new DatabaseService();

using (var dbContext = new ApplicationDbContext(databasePath,databaseService))
{
    dbContext.Start();
}