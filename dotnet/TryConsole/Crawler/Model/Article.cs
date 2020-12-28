using System.Collections;
using System.Collections.Generic;

namespace Crawler.Model
{
    public class Article
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Author { get; set; }
        public string PublishDateTime { get; set; }
        public string Category { get; set; }
    }
}
