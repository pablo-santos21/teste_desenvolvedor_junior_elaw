namespace Webcrawler.Helpers
{
    public class HtmlFileHelper
    {
        public static void SavePageHtml(List<string> pageContents)
        {
            string directoryPath = "PagesDirectory";

            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }

            Directory.CreateDirectory(directoryPath);

            for (int i = 0; i < pageContents.Count; i++)
            {
                string filePath = Path.Combine(directoryPath, $"page-{i + 1}.html");
                File.WriteAllText(filePath, pageContents[i]);
            }
        }
    }
}
