using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Crawler.Crawler
{
    public class Crawler
    {
        private const string ChromeDriverLocation = @"C:\src\chromedriver";
        private const string EdgeDriverLocation = @"C:\src\edgedriver";

        public IWebDriver WebDriver { get; } = new EdgeDriver(EdgeDriverLocation);

        public IWebElement FindElementByCss(string css)
        {
            return WebDriver.FindElement(By.CssSelector(css));
        }

        public ReadOnlyCollection<IWebElement> FindElementsByCss(string css)
        {
            return WebDriver.FindElements(By.CssSelector(css));
        }
    }
}
