using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Crawler.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Crawler.Crawler
{
    public class Crawler
    {
        private const string DriverLocation = @"C:\src";

        private IWebDriver? _webDriver;
        public IWebDriver WebDriver => _webDriver ??= new ChromeDriver(DriverLocation);

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
