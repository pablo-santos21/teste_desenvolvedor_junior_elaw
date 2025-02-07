using HtmlAgilityPack;
using System.Text.Json;
using System.Text.Encodings.Web;
using Webcrawler.Model;

var web = new HtmlWeb();

// inicializa o HAP para pegar a pagina solicitada 
var document = web.Load("https://proxyservers.pro/proxy/list/order/updated/order_dir/desc");

var proxies = new List<Proxy>();

var rows = document.DocumentNode.QuerySelectorAll("tr[valign='top']");

if (rows != null)
{
    foreach (var row in rows)
    {
        var ip_Address = row.QuerySelector("td:nth-child(2) a")?.InnerText.Trim();
        var port = row.QuerySelector("td:nth-child(3) span.port")?.InnerText.Trim();
        var country = row.QuerySelector("td:nth-child(4)")?.InnerText.Trim();
        var protocol = row.QuerySelector("td:nth-child(7)")?.InnerText.Trim();

        var proxy = new Proxy() { Ip = ip_Address, Port = port, Country = country, Protocol = protocol };
        proxies.Add(proxy);
    }
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

