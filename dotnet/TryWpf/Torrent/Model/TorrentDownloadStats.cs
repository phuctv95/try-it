using System;
using System.Collections.Generic;

namespace Torrent.Model
{
    public class TorrentDownloadStats
    {
        public Dictionary<string, FileStatus> Files { get; set; }
        public int Progress { get; set; }
        public long TotalBytes { get; set; }
        public long DownloadedBytes { get; set; }
        public long DownloadSpeed { get; set; }
        public long MaxSpeed { get; set; }
        public TimeSpan ETA { get; set; }
        public TimeSpan AvgETA { get; set; }
        public int PeersConnected { get; set; }
        public int PeersTotal { get; set; }
    }
}
