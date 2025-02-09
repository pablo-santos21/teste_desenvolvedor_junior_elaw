using HtmlAgilityPack;
using Webcrawler.Model;

namespace Webcrawler.Services
{
    public class ProxyScraper
    {
        private readonly SinglePageScraper _scraper;
        private readonly TotalPage _totalPage;

        public ProxyScraper(SinglePageScraper scraper, TotalPage totalPage)
        {
            _scraper = scraper;
            _totalPage = totalPage;
        }

        public (List<ProxyServer> Proxies, int TotalPages) GetProxiesFromPage(string baseUrl)
        {
            var proxies = new List<ProxyServer>();

            var pageSource = HtmlSelenium.LoadPageSelenium(baseUrl);

            var document = new HtmlDocument();
            document.LoadHtml(pageSource);

            int totalPages = _totalPage.GetTotalPages(document);

            for (int page = 1; page <= totalPages; page++ )
            {
                string pageUrl = $"{baseUrl}/page/{page}";
                Console.WriteLine($"Acessando página: {pageUrl}");

                var pageProxies = _scraper.GetProxiesFromSinglePage(pageUrl);
                proxies.AddRange(pageProxies);
            }

            return (proxies, totalPages);
        }
    }
}
