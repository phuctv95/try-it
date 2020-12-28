using Crawler.Crawler;

namespace Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            new ZingCrawler().StartCrawlingZingArticles(@"C:\src\zing.csv");
        }
    }
}
