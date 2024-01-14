using ApiDatabaseServices.Interfaces;
using ApiServices.Interfaces;
using Quartz;

namespace CsvReader.API.Jobs
{
    public class InfoFileJob: IJob
    {
        private string directoryPath = "D:\\VTU software engineering\\C#\\CsvFiles\\readed";
        private string saveFilePath = $"D:\\VTU software engineering\\C#\\API\\CsvUniProject\\APiCsvRader\\CsvReader.API\\Logs\\";
        private readonly IDatabaseService _databaseService;
        private readonly IApiPrinter _apiPrinter;
        public InfoFileJob(IDatabaseService databaseService, IApiPrinter apiPrinter)
        {
            _databaseService = databaseService;
            _apiPrinter = apiPrinter;
        }

        public Task Execute(IJobExecutionContext ctx)
        {
            try
            {
                SaveFileWithInfo();
                return Task.CompletedTask;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.CompletedTask;
            }
        }
        private int CountFilesCreatedToday()
        {
            if (!Directory.Exists(directoryPath))
            {
                _apiPrinter.Print($"Directory '{directoryPath}' not found.");
                return 0;
            }
            string[] files = Directory.GetFiles(directoryPath);
            int count = 0;
            DateTime today = DateTime.Today;

            foreach (string filePath in files)
            {
                DateTime fileCreationTime = File.GetLastAccessTime(filePath);

                if (fileCreationTime.Date == today)
                {
                    count++;
                }
            }
            return count;
        }
        private void SaveFileWithInfo()
        {
            try
            {
                int fileCount = CountFilesCreatedToday();
                string fileName = $"{DateTime.Today.ToString("yyyy-MM-dd")}_Info.json";
                string filePath = Path.Combine(saveFilePath, fileName);
                var organization = _databaseService.GetOrganizationWithMaxEmployees();
                int countriesCount = _databaseService.CountCountries();
                int organizationsCount = _databaseService.CountOrganizations();
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"File Count: {fileCount} for : {DateTime.Today.ToString("dd-MM-yyyy")}");
                    writer.WriteLine($"The organizations with the most number of employee is:");
                    writer.WriteLine(organization.ToString());
                    writer.WriteLine($"There are {countriesCount} countries in the db");
                    writer.WriteLine($"There are {organizationsCount} organizations in the db");

                }
                _apiPrinter.Print($"{filePath} is saved in {directoryPath}");

            }
            catch (Exception ex)
            {
                _apiPrinter.Print($"Error saving file count to {saveFilePath}: {ex.Message}");
            }
        }
    }
}
