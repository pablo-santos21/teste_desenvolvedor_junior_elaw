using HtmlAgilityPack;
using Webcrawler.Helpers;
using Webcrawler.Model;

namespace Webcrawler.Services
{
    public class ProxyScraper
    {
        public List<ProxyServer> GetProxiesFromPage(string url)
        {
            var proxies = new List<ProxyServer>();
            var pageSource = HtmlSelenium.LoadPageSelenium(url);

            FileHelper.SavePageHtml(pageSource);

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

                    proxies.Add(new ProxyServer { Ip = ip_Address, Port = port, Country = country, Protocol = protocol });
                }
            }

            return proxies;
        }
    }
}
