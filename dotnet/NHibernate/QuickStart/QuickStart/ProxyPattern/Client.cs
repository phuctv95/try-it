using System;

namespace QuickStart.ProxyPattern
{
    public class Client
    {
        private readonly IYoutubeLib _youtubeLib;

        // Inject YouTubeProxy into here.
        public Client(IYoutubeLib youtubeLib)
        {
            _youtubeLib = youtubeLib;
        }

        public void Consume()
        {
            foreach (var video in _youtubeLib.GetListTrendingVideos())
            {
                Console.WriteLine(video);
            }
        }
    }
}
