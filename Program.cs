using Webcrawler.Helpers;
using Webcrawler.Services;

var scraper = new ProxyScraper();

var proxies = scraper.GetProxiesFromPage("https://proxyservers.pro/proxy/list/order/updated/order_dir/desc");

FileHelper.SaveProxiesToJson(proxies);

Console.WriteLine("Scraping concluído e dados salvos.");