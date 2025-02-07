using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Webcrawler.Services
{
    public class HtmlSelenium
    {
        public static string LoadPageSelenium(string url)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--disable-gpu");
            chromeOptions.AddArgument("--no-sandbox");

            using (var driver = new ChromeDriver(chromeOptions))
            {
                driver.Navigate().GoToUrl(url);

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(driver =>
                {
                    var portElements = driver.FindElements(By.CssSelector("td span.port"));
                    return portElements.Any(e => !string.IsNullOrWhiteSpace(e.Text));
                });

                return driver.PageSource;
            }
        } 
    }
}
