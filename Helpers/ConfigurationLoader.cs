using Microsoft.Extensions.Configuration;


namespace Webcrawler.Helpers
{
    public static class ConfigurationLoader
    {
        public static IConfiguration Load() => new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
    }
}
