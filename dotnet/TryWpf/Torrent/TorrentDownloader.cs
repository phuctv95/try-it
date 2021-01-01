using ByteSizeLib;
using SuRGeoNix.BitSwarmLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Torrent
{
    public class TorrentDownloader
    {
        private SuRGeoNix.BitSwarmLib.BEP.Torrent _torrent;
        private List<string> _downloadedFiles = new List<string>();
        public void StartDownloading(string magnetLinkOrTorrentFile, string savingLocation, Action<string> writeLog, Action onDownloadFinished)
        {
            var opt = new Options
            {
                FolderComplete = savingLocation,
                FolderIncomplete = savingLocation,
                FolderTorrents = savingLocation,
                FolderSessions = savingLocation,
            };
            var bitSwarm = new BitSwarm(opt);

            bitSwarm.MetadataReceived += (source, e) => OnMetadataReceived(e, writeLog);
            bitSwarm.StatsUpdated += (source, e) => OnStatsUpdated(e, writeLog);
            bitSwarm.StatusChanged += (source, e) => OnStatusChanged(e, bitSwarm, writeLog, onDownloadFinished);
            bitSwarm.OnFinishing += (source, e) => writeLog($"[OnFinishing] Finishing...");

            bitSwarm.Open(magnetLinkOrTorrentFile);
            _downloadedFiles.Clear();
            bitSwarm.Start();
        }

        private void OnMetadataReceived(BitSwarm.MetadataReceivedArgs e, Action<string> writeLog)
        {
            const string EventType = "MetadataReceived";
            _torrent = e.Torrent;
            for (int i = 0; i < e.Torrent.data.files.Length; i++)
            {
                var file = e.Torrent.data.files[i];
                if (file == null) { continue; }
                writeLog($"[{EventType}] {file.Filename} ({ByteSize.FromBytes(file.Size)})");
            }
        }

        private void OnStatsUpdated(BitSwarm.StatsUpdatedArgs e, Action<string> writeLog)
        {
            const string EventType = "StatsUpdated";
            var stats = e.Stats;
            var newDownloadedFiles = _torrent.data.files.Where(x => x != null && x.Created && !_downloadedFiles.Contains(x.Filename)).ToList();
            foreach (var newDownloadedFile in newDownloadedFiles)
            {
                _downloadedFiles.Add(newDownloadedFile.Filename);
                writeLog($"[{EventType}] Downloaded file {newDownloadedFile.Filename}.");
            }
            writeLog($"[{EventType}] {ByteSize.FromBytes(stats.BytesDownloadedPrevSession + stats.BytesDownloaded)}/{ByteSize.FromBytes(stats.BytesIncluded)} ({stats.Progress}%)"
                + $" | Download speed: {ByteSize.FromBytes(stats.DownRate)}/s (max: {ByteSize.FromBytes(stats.MaxRate)}/s)"
                + $" | ETA: {TimeSpan.FromSeconds((stats.ETA + stats.AvgETA)/2):hh\\:mm\\:ss}"
                + $" | Connected peers: {stats.PeersConnected}/{stats.PeersTotal}.");
        }

        private void OnStatusChanged(BitSwarm.StatusChangedArgs e, BitSwarm bitSwarm, Action<string> writeLog, Action onDownloadFinished)
        {
            const string EventType = "StatusChanged";
            writeLog($"[{EventType}] Status code: {e.Status}.{(e.ErrorMsg != string.Empty ? $" Error message: {e.ErrorMsg}." : string.Empty)}.");
            writeLog($"[{EventType}] Disposing BitSwarm...");
            bitSwarm.Dispose();
            onDownloadFinished.Invoke();
            writeLog($"[{EventType}] Done.");
        }
    }
}
