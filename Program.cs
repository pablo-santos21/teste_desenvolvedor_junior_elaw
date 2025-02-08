    using Webcrawler.Helpers;
    using Webcrawler.Services;
    using Webcrawler.Database;
    using Webcrawler.Models;
    using Microsoft.Extensions.Configuration;

    var config = ConfigurationLoader.Load();
    string connectionString = config.GetConnectionString("DefaultConnection");

    var scraper = new ProxyScraper();
    var infos = new SaveInfos();
    var dbService = new DatabaseService(connectionString);

    DateTime startTime = DateTime.Now;

    var (proxies, totalPages) = scraper.GetProxiesFromPage("https://proxyservers.pro/proxy/list/order/updated/order_dir/desc");

    FileHelper.SaveProxiesToJson(proxies);

    DateTime endTime = DateTime.Now;
    int totalRows = proxies.Count;

    var saveInfos = new SaveInfos
    {
        StartTime = startTime,
        EndTime = endTime,
        TotalPages = totalPages,
        TotalRowsExtracted = totalRows,
        JsonFileName = FileHelper.LastSavedJsonFileName
    };

    dbService.SaveExecutionInfo(saveInfos);

    Console.WriteLine("Scraping concluído e dados salvos.");