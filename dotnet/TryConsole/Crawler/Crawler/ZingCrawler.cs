using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        private IList<Article> CrawledArticles { get; set; } = new List<Article>();
        private IWebDriver WebDriver => Crawler.WebDriver;

        public void StartCrawlingArticlesRepeatedly(string csvFilePath, TimeSpan waitTime)
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Exiting...");
                WebDriver.Quit();
            };

            CrawledArticles = Csv.Read<Article>(csvFilePath);
            while (true)
            {
                StartCrawlingArticles(csvFilePath);
                Thread.Sleep(waitTime);
            }
        }

        public void StartCrawlingArticles(string csvFilePath)
        {
            var topArticleLinks = GetTopArticleLinks();
            foreach (var articleLink in topArticleLinks)
            {
                if (!ArticleIsExist(articleLink))
                {
                    var article = CrawlArticle(articleLink);
                    StoreArticle(article, csvFilePath);
                }
            }
        }

        private void StoreArticle(Article article, string csvFilePath)
        {
            CrawledArticles.Add(article);
            Csv.WriteAppend(article, csvFilePath);
        }

        private bool ArticleIsExist(string articleLink)
        {
            return CrawledArticles.Any(x => x.Link == articleLink);
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
                Author = Crawler.FindElementByCss("article header li.the-article-author").Text
            };
        }
    }
}
