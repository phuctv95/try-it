using Crawler.Crawler;
using System;

namespace Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            new ZingCrawler().StartCrawlingArticlesRepeatedly(@"C:\src\zing.csv", TimeSpan.FromMinutes(30));
        }
    }
}
