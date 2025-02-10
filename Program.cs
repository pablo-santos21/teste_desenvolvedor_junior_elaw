using Webcrawler.Helpers;
using Webcrawler.Services;
using Webcrawler.Database;
using Webcrawler.Models;
using Microsoft.Extensions.Configuration;

var config = ConfigurationLoader.Load();
string connectionString = config.GetConnectionString("DefaultConnection");
int MaxConcurrentTasks = int.Parse(config.GetSection("ScraperConfig:MaxDegreeOfParallelism").Value);
string BaseUrl = config.GetSection("ScraperConfig:BaseUrl").Value;

var singlePageScraper = new SinglePageScraper();
var totalPage = new TotalPage();
var scraper = new ProxyScraper(singlePageScraper, totalPage, MaxConcurrentTasks);
var dbService = new DatabaseService(connectionString);

DateTime startTime = DateTime.Now;

var (proxies, totalPages) = await scraper.GetProxiesFromPage(BaseUrl);

ProxyFileHelper.SaveProxiesToJson(proxies);

DateTime endTime = DateTime.Now;
int totalRows = proxies.Count;

var saveInfos = new SaveInfos
{
    StartTime = startTime,
    EndTime = endTime,
    TotalPages = totalPages,
    TotalRowsExtracted = totalRows,
    JsonFileName = ProxyFileHelper.LastSavedJsonFileName
};

dbService.SaveExecutionInfo(saveInfos);

Console.WriteLine("Scraping concluído e dados salvos.");