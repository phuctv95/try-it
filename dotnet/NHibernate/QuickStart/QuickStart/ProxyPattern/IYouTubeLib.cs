using System.Collections.Generic;

namespace QuickStart.ProxyPattern
{
    public interface IYoutubeLib
    {
        IList<string> GetListTrendingVideos();
    }
}
