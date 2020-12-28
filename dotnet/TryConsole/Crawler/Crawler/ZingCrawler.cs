using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Crawler.Model;
using CsvHelper;
using CsvHelper.Configuration;
using OpenQA.Selenium;

namespace Crawler.Crawler
{
    public class ZingCrawler
    {
        private const string ZingHomePageLink = "https://zingnews.vn/";

        private Crawler Crawler { get; } = new Crawler();
        private IWebDriver WebDriver => Crawler.WebDriver;

        public void StartCrawlingZingArticlesRepeatedly(string csvFilePath)
        {
            // For each amount of time (maybe 1 hour), try to crawling.
            // If article already crawled, base on link, update it.
        }

        public void StartCrawlingZingArticles(string csvFilePath)
        {
            var topArticleLinks = GetTopZingArticles();
            foreach (var articleLink in topArticleLinks)
            {
                var article = CrawlZingArticle(articleLink);
                WriteArticleToCsv(article, csvFilePath);
            }
        }

        private IList<string> GetTopZingArticles()
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

        private void WriteArticleToCsv(Article article, string csvFilePath)
        {
            using var writer = new StreamWriter(csvFilePath, true);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.Configuration.HasHeaderRecord = false;
            csv.WriteRecords(new List<Article> {article});
        }

        private Article CrawlZingArticle(string articleLink)
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
