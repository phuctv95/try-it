using ByteSizeLib;
using SuRGeoNix.BitSwarmLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Torrent.Model;

namespace Torrent
{
    public class TorrentDownloader
    {
        private BitSwarm _bitSwarm;
        private List<string> _downloadedFiles = new List<string>();
        private SuRGeoNix.BitSwarmLib.BEP.Torrent _torrent => _bitSwarm?.torrent;

        public void StartDownloading(string magnetLinkOrTorrentFile, string savingLocation, Action<string> writeLog,
            Action<IList<string>> onMetadataReceived, Action<TorrentDownloadStats> onDownloadingProgress, Action onDownloadFinished)
        {
            var opt = new Options
            {
                FolderComplete = savingLocation,
                FolderIncomplete = savingLocation,
                FolderTorrents = savingLocation,
                FolderSessions = savingLocation,
            };
            _bitSwarm = new BitSwarm(opt);

            _bitSwarm.MetadataReceived += (source, e) => OnMetadataReceived(e, writeLog, onMetadataReceived);
            _bitSwarm.StatsUpdated += (source, e) => OnStatsUpdated(e, writeLog, onDownloadingProgress);
            _bitSwarm.StatusChanged += (source, e) => OnStatusChanged(e, _bitSwarm, writeLog, onDownloadFinished);
            _bitSwarm.OnFinishing += (source, e) => writeLog($"[OnFinishing] Finishing...");

            _bitSwarm.Open(magnetLinkOrTorrentFile);
            _downloadedFiles.Clear();
            _bitSwarm.Start();
        }

        public void UpdateFilesToDownload(List<string> filesToDownload)
        {
            _bitSwarm.IncludeFiles(filesToDownload);
        }

        private void OnMetadataReceived(BitSwarm.MetadataReceivedArgs e, Action<string> writeLog, Action<IList<string>> onMetadataReceived)
        {
            const string EventType = "MetadataReceived";
            for (int i = 0; i < e.Torrent.data.files.Length; i++)
            {
                var file = e.Torrent.data.files[i];
                if (file == null) { continue; }
                writeLog($"[{EventType}] {file.Filename} ({ByteSize.FromBytes(file.Size)})");
            }
            onMetadataReceived(_torrent.file.paths);
        }

        private void OnStatsUpdated(BitSwarm.StatsUpdatedArgs e, Action<string> writeLog, Action<TorrentDownloadStats> onDownloadingProgress)
        {
            const string EventType = "StatsUpdated";
            var stats = e.Stats;
            var newDownloadedFiles = _torrent.data.filesIncludes
                .Where(x => !_downloadedFiles.Contains(x) && File.Exists(Path.Combine(_torrent.data.folder, x)))
                .ToList();
            foreach (var newDownloadedFile in newDownloadedFiles)
            {
                _downloadedFiles.Add(newDownloadedFile);
                writeLog($"[{EventType}] Downloaded file {newDownloadedFile}.");
            }
            writeLog($"[{EventType}] {ByteSize.FromBytes(stats.BytesDownloadedPrevSession + stats.BytesDownloaded)}/{ByteSize.FromBytes(stats.BytesIncluded)} ({stats.Progress}%)"
                + $" | Download speed: {ByteSize.FromBytes(stats.DownRate)}/s (max: {ByteSize.FromBytes(stats.MaxRate)}/s)"
                + $" | ETA: {TimeSpan.FromSeconds((stats.ETA + stats.AvgETA)/2):hh\\:mm\\:ss}"
                + $" | Connected peers: {stats.PeersConnected}/{stats.PeersTotal}.");

            onDownloadingProgress.Invoke(new TorrentDownloadStats
            {
                Files = _torrent.file.paths.ToDictionary(x => x, x => new FileStatus
                {
                    FileName = x,
                    DownloadStatus = _downloadedFiles.Contains(x)
                    ? DownloadStatus.Finished
                    : _torrent.data.filesIncludes.Contains(x)
                        ? DownloadStatus.Downloading
                        : DownloadStatus.Canceled
                })
            });
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
