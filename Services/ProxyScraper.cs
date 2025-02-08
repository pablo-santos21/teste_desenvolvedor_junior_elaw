using HtmlAgilityPack;
using Webcrawler.Helpers;
using Webcrawler.Model;

namespace Webcrawler.Services
{
    public class ProxyScraper
    {
        public (List<ProxyServer> Proxies, int TotalPages) GetProxiesFromPage(string url)
        {
            var proxies = new List<ProxyServer>();
            int totalPages = 1;

            var pageSource = HtmlSelenium.LoadPageSelenium(url);
            FileHelper.SavePageHtml(pageSource);

            var document = new HtmlDocument();
            document.LoadHtml(pageSource);

            var rows = document.DocumentNode.QuerySelectorAll("tr[valign='top']");
            var paginations = document.DocumentNode.QuerySelector("ul.pagination");

            if (paginations != null)
            {
                var page = paginations.QuerySelectorAll("li.page-item a.page-link");
                    if (page != null && page.Count > 0)
                    {
                        totalPages = page
                            .Select(node => int.TryParse(node.InnerText.Trim(), out int pageNumber) ? pageNumber : 0)
                            .Max(); 
                    }
            }

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

            return (proxies, totalPages);
        }
    }
}
