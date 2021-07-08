using System.Collections.Generic;
using System.Linq;

namespace QuickStart.ProxyPattern
{
    public class YoutubeLibProxy : IYoutubeLib
    {
        private readonly YoutubeLib _youtubeProxy;
        private readonly List<string> _trendingVideosCache = new List<string>();

        public YoutubeLibProxy(YoutubeLib youtubeProxy)
        {
            _youtubeProxy = youtubeProxy;
        }

        public IList<string> GetListTrendingVideos()
        {
            if (!_trendingVideosCache.Any())
            {
                _trendingVideosCache.AddRange(_youtubeProxy.GetListTrendingVideos());
            }
            return _trendingVideosCache;
        }
    }
}
