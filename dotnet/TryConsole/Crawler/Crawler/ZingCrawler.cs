using System.Collections.Generic;
using System.Linq;
using Crawler.Model;
using Crawler.Tool;
using OpenQA.Selenium;

namespace Crawler.Crawler
{
    public class ZingCrawler
    {
        private const string ZingHomePageLink = "https://zingnews.vn/";

        private Crawler Crawler { get; } = new Crawler();
        private Csv Csv { get; } = new Csv();
        private IWebDriver WebDriver => Crawler.WebDriver;

        public void StartCrawlingArticlesRepeatedly(string csvFilePath)
        {
            // For each amount of time (maybe 1 hour), try to crawling.
            // If article already crawled, base on link, update it.
        }

        public void StartCrawlingArticles(string csvFilePath)
        {
            var topArticleLinks = GetTopArticleLinks();
            foreach (var articleLink in topArticleLinks)
            {
                var article = CrawlArticle(articleLink);
                Csv.WriteAppend(article, csvFilePath);
            }
            WebDriver.Quit();
        }

        private IList<string> GetTopArticleLinks()
        {
            WebDriver.Navigate().GoToUrl(ZingHomePageLink);
            var featuredNewsLinks = Crawler
                .FindElementsByCss(@"#section-featured div[data-content='newstrending'] article p[class='article-title'] a")
                .Select(x => x.GetProperty("href")).ToList();
            var trendingNewsLinks = Crawler
                .FindElementsByCss(@"#section-featured div[data-content='newsfeatured'] article p[class='article-title'] a")
                .Select(x => x.GetProperty("href")).ToList();

            return featuredNewsLinks.Concat(trendingNewsLinks).ToList();
        }

        private Article CrawlArticle(string articleLink)
        {
            WebDriver.Navigate().GoToUrl(articleLink);
            return new Article
            {
                Link = articleLink,
                Title = Crawler.FindElementByCss("article header h1.the-article-title").Text,
                Category = Crawler.FindElementByCss("article header p.the-article-category a").Text,
                PublishDateTime = Crawler.FindElementByCss("article header li.the-article-publish").Text,
                Authors = Crawler.FindElementsByCss("article header li.the-article-author a").Select(x => x.Text).ToList(),
            };
        }
    }
}
