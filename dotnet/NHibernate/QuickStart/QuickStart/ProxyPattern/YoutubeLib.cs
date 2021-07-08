using System.Collections.Generic;

namespace QuickStart.ProxyPattern
{
    public class YoutubeLib : IYoutubeLib
    {
        public IList<string> GetListTrendingVideos()
        {
            return new List<string>
            {
                "Video 1",
                "Video 2",
                "Video 3",
            };
        }
    }
}
