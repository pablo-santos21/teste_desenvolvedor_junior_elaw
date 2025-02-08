using System.Text.Encodings.Web;
using System.Text.Json;
using Webcrawler.Model;

namespace Webcrawler.Helpers
{
    public class FileHelper
    {
        public static string LastSavedJsonFileName { get; private set; } = string.Empty;

        public static void SaveProxiesToJson(List<ProxyServer> proxies)
        {
            if (!Directory.Exists("JsonDirectory"))
            {
                Directory.CreateDirectory("JsonDirectory");
            }

            string timestamp = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            LastSavedJsonFileName = $"proxies-{timestamp}.json";
            string filePath = Path.Combine("JsonDirectory", LastSavedJsonFileName);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            File.WriteAllText(filePath, JsonSerializer.Serialize(proxies, options));
        }

        public static void SavePageHtml(string htmlContent)
        {
            string htmlPath = Path.Combine(Directory.GetCurrentDirectory(), "page.html");
            File.WriteAllText(htmlPath, htmlContent);
        }
    }
}
