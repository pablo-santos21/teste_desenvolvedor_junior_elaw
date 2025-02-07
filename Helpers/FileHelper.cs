using System.Text.Encodings.Web;
using System.Text.Json;
using Webcrawler.Model;

namespace Webcrawler.Helpers
{
    public class FileHelper
    {
        public static void SaveProxiesToJson(List<ProxyServer> proxies)
        {
            if (!Directory.Exists("JsonDirectory"))
            {
                Directory.CreateDirectory("JsonDirectory");
            }

            string timestamp = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            string filePath = Path.Combine("JsonDirectory", $"proxies-{timestamp}.json");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            File.WriteAllText(filePath, JsonSerializer.Serialize(proxies, options));
        }

        public static void SavePageHtml(string htmlContent)
        {
            File.WriteAllText("page.html", htmlContent);
        }
    }
}
