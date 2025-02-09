    using Webcrawler.Helpers;
    using Webcrawler.Services;
    using Webcrawler.Database;
    using Webcrawler.Models;
    using Microsoft.Extensions.Configuration;
using HtmlAgilityPack;
using System;

    var config = ConfigurationLoader.Load();
    string connectionString = config.GetConnectionString("DefaultConnection");

    var singlePageScraper = new SinglePageScraper();
    var totalPage = new TotalPage();
    var scraper = new ProxyScraper(singlePageScraper, totalPage);
    var dbService = new DatabaseService(connectionString);

    DateTime startTime = DateTime.Now;

    var url = "https://proxyservers.pro/proxy/list/order/updated/order_dir/desc";
    var (proxies, totalPages) = scraper.GetProxiesFromPage(url);

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