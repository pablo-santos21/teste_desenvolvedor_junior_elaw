using HtmlAgilityPack;
using Webcrawler.Helpers;
using Webcrawler.Model;
using Webcrawler.Services;

public class ProxyScraper
{
    private readonly SinglePageScraper _scraper;
    private readonly TotalPage _totalPage;
    private readonly int _maxConcurrentTasks;

    public ProxyScraper(SinglePageScraper scraper, TotalPage totalPage, int maxConcurrentTasks)
    {
        _scraper = scraper;
        _totalPage = totalPage;
        _maxConcurrentTasks = maxConcurrentTasks;
    }

    public async Task<(List<ProxyServer> Proxies, int TotalPages)> GetProxiesFromPage(string baseUrl)
    {
        var proxies = new List<ProxyServer>();
        var allPageContents = new List<string>();

        var pageSource = HtmlSelenium.LoadPageSelenium(baseUrl);

        var document = new HtmlDocument();
        document.LoadHtml(pageSource);

        int totalPages = _totalPage.GetTotalPages(document);

        var semaphore = new SemaphoreSlim(_maxConcurrentTasks);
        var tasks = new List<Task>();

        for (int page = 1; page <= totalPages; page++)
        {
            await semaphore.WaitAsync();
            string pageUrl = $"{baseUrl}/page/{page}";
            Console.WriteLine($"Acessando página: {pageUrl}");

            var task = Task.Run(async () =>
            {
                try
                {
                    string pageContent = HtmlSelenium.LoadPageSelenium(pageUrl);
                    lock (allPageContents)
                    {
                        allPageContents.Add(pageContent);
                    }

                    var pageProxies = _scraper.GetProxiesFromSinglePage(pageUrl);
                    lock (proxies)
                    {
                        proxies.AddRange(pageProxies);
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            });

            tasks.Add(task);
        }
        await Task.WhenAll(tasks);
        HtmlFileHelper.SavePageHtml(allPageContents);

        return (proxies, totalPages);
    }
}