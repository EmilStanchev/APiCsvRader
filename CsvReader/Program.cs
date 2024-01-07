// See https://aka.ms/new-console-template for more information
using Data;
using Data.Implementation;
using Services;
using Services.Implementations;

Console.WriteLine("Hello, World!");
string databasePath = "D:\\VTU software engineering\\C#\\API\\CsvReader\\CsvReader\\database.db";
DatabaseService databaseService = new DatabaseService();
CsvReaderService csvReaderService = new CsvReaderService();
CsvReaderController controller = new CsvReaderController(csvReaderService);

var list = controller.Read();
foreach (var item in list)
{
    Console.WriteLine(item.ToString());
}
/*
using (var dbContext = new ApplicationDbContext(databasePath,databaseService))
{
    dbContext.Start();
}*/