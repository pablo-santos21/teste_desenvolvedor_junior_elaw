using HtmlAgilityPack;
using System.Text.Json;
using System.Text.Encodings.Web;
using Webcrawler.Model;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;


var chromeOptions = new ChromeOptions();
chromeOptions.AddArgument("--headless");
chromeOptions.AddArgument("--disable-gpu");
chromeOptions.AddArgument("--no-sandbox");

var proxies = new List<ProxyServer>();

using (var driver = new ChromeDriver(chromeOptions))
{
    driver.Navigate().GoToUrl("https://proxyservers.pro/proxy/list/order/updated/order_dir/desc");

    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    wait.Until(driver =>
    {
        var portElements = driver.FindElements(By.CssSelector("td span.port"));
        return portElements.Any(e => !string.IsNullOrWhiteSpace(e.Text));
    });

    var pageSource = driver.PageSource;

    var document = new HtmlDocument();
    document.LoadHtml(pageSource);

    var rows = document.DocumentNode.QuerySelectorAll("tr[valign='top']");

    if (rows != null)
    {
        foreach (var row in rows)
        {
            var ip_Address = row.QuerySelector("td:nth-child(2) a")?.InnerText.Trim();
            var port = row.QuerySelector("td:nth-child(3) span.port")?.InnerText.Trim();
            var country = row.QuerySelector("td:nth-child(4)")?.InnerText.Trim();
            var protocol = row.QuerySelector("td:nth-child(7)")?.InnerText.Trim();

            var proxy = new ProxyServer() { Ip = ip_Address, Port = port, Country = country, Protocol = protocol };
            proxies.Add(proxy);
        }
    }
    else
    {
        Console.WriteLine("A lista está vazia!");
    }

    // importando para JSON
    var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    File.WriteAllText("proxies.json", JsonSerializer.Serialize(proxies, options));
    File.WriteAllText("page.html", document.DocumentNode.OuterHtml);

    Console.WriteLine("Página salva para diagnóstico.");
    Console.WriteLine("Scraping completed!");

}