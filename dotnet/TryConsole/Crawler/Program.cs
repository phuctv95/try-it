using Crawler.Crawler;

namespace Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            new ZingCrawler().StartCrawlingArticles(@"C:\src\zing.csv");
        }
    }
}
