using System.Collections.Generic;

namespace Torrent.Model
{
    public class TorrentDownloadStats
    {
        public Dictionary<string, FileStatus> Files { get; set; }
    }
}
