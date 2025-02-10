namespace Webcrawler.Helpers
{
    internal class Logger
    {
        public static void Log(string message)
        {
            string logFilePath = $"Logs/log_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)); 
            File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
        }

    }
}
