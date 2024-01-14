using Quartz;

namespace CsvReader.API.Jobs
{
    public class InfoFileJob: IJob
    {
        private string directoryPath = "D:\\VTU software engineering\\C#\\CsvFiles\\readed";
        private string saveFilePath = "D:\\VTU software engineering\\C#\\API\\CsvUniProject\\APiCsvRader\\CsvReader.API\\Logs\\info.txt";

        public Task Execute(IJobExecutionContext ctx)
        {
            try
            {
                SaveFileWithInfo();
                Console.WriteLine($"{saveFilePath} is saved in {directoryPath}");
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
                Console.WriteLine($"Directory '{directoryPath}' not found.");
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
                using (StreamWriter writer = new StreamWriter(saveFilePath))
                {
                    writer.WriteLine($"File Count: {fileCount} for : {DateTime.Today}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file count to {saveFilePath}: {ex.Message}");
            }
        }
    }
}
