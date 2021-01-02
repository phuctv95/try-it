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
        private const string IncompleteFolderName = "Incomplete";
        private const string TorrentsFolderName = "Torrents";
        private const string SessionsFolderName = "Sessions";

        public void StartDownloading(string magnetLinkOrTorrentFile, string savingLocation, Action<string> writeLog,
            Action<IList<string>> onMetadataReceived, Action<TorrentDownloadStats> onDownloadingProgress, Action onDownloadFinished)
        {
            CreateProcessingFoldersIfNotExist();
            var opt = new Options
            {
                FolderComplete = savingLocation,
                FolderIncomplete = Path.Combine(Directory.GetCurrentDirectory(), IncompleteFolderName),
                FolderTorrents = Path.Combine(Directory.GetCurrentDirectory(), TorrentsFolderName),
                FolderSessions = Path.Combine(Directory.GetCurrentDirectory(), SessionsFolderName),
            };
            _bitSwarm = new BitSwarm(opt);

            _bitSwarm.MetadataReceived += (source, e) => OnMetadataReceived(e, writeLog, onMetadataReceived);
            _bitSwarm.StatsUpdated += (source, e) => OnStatsUpdated(e, writeLog, onDownloadingProgress);
            _bitSwarm.StatusChanged += (source, e) => OnStatusChanged(e, writeLog, onDownloadFinished);
            _bitSwarm.OnFinishing += (source, e) => writeLog($"[OnFinishing] Finishing...");

            _bitSwarm.Open(magnetLinkOrTorrentFile);
            _downloadedFiles.Clear();
            _bitSwarm.Start();
        }

        private void CreateProcessingFoldersIfNotExist()
        {
            var currentFolder = Directory.GetCurrentDirectory();
            if (Directory.Exists(Path.Combine(currentFolder, IncompleteFolderName)))
            {
                Directory.CreateDirectory(Path.Combine(currentFolder, IncompleteFolderName));
            }
            if (Directory.Exists(Path.Combine(currentFolder, TorrentsFolderName)))
            {
                Directory.CreateDirectory(Path.Combine(currentFolder, TorrentsFolderName));
            }
            if (Directory.Exists(Path.Combine(currentFolder, SessionsFolderName)))
            {
                Directory.CreateDirectory(Path.Combine(currentFolder, SessionsFolderName));
            }
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
                .Where(x => !_downloadedFiles.Contains(x) && File.Exists(Path.Combine(_torrent.data.folder ?? _bitSwarm.Options.FolderComplete, x)))
                .ToList();
            foreach (var newDownloadedFile in newDownloadedFiles)
            {
                _downloadedFiles.Add(newDownloadedFile);
                writeLog($"[{EventType}] Downloaded file {newDownloadedFile}.");
            }
            writeLog($"[{EventType}] {ByteSize.FromBytes(stats.BytesDownloadedPrevSession + stats.BytesDownloaded)}/{ByteSize.FromBytes(stats.BytesIncluded)} ({stats.Progress}%)"
                + $" | Download speed: {ByteSize.FromBytes(stats.DownRate)}/s (max: {ByteSize.FromBytes(stats.MaxRate)}/s)"
                + $" | ETA: {TimeSpan.FromSeconds(stats.ETA):hh\\:mm\\:ss} AvgETA: {TimeSpan.FromSeconds(stats.AvgETA):hh\\:mm\\:ss}"
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
                }),
                Progress = stats.Progress,
                TotalBytes = stats.BytesIncluded,
                DownloadedBytes = stats.BytesDownloadedPrevSession + stats.BytesDownloaded,
                DownloadSpeed = stats.DownRate,
                MaxSpeed = stats.MaxRate,
                ETA = TimeSpan.FromSeconds(stats.ETA),
                AvgETA = TimeSpan.FromSeconds(stats.AvgETA),
                PeersConnected = stats.PeersConnected,
                PeersTotal = stats.PeersTotal,
            });
        }

        private void OnStatusChanged(BitSwarm.StatusChangedArgs e, Action<string> writeLog, Action onDownloadFinished)
        {
            const string EventType = "StatusChanged";
            writeLog($"[{EventType}] Status code: {e.Status}.{(e.ErrorMsg != string.Empty ? $" Error message: {e.ErrorMsg}." : string.Empty)}.");

            writeLog($"[{EventType}] Disposing BitSwarm...");
            _bitSwarm.Dispose();
            CleanUpFiles();
            onDownloadFinished.Invoke();
            writeLog($"[{EventType}] Done.");
        }

        private void CleanUpFiles()
        {
            if (_torrent == null && _bitSwarm == null)
            {
                return;
            }
            var sessionFile = Path.Combine(_bitSwarm.Options.FolderSessions, $"{_torrent.file.infoHash}.bsf");
            var torrentFile = Path.Combine(_bitSwarm.Options.FolderTorrents, $"{_torrent.file.name}.torrent");
            if (File.Exists(sessionFile))
            {
                File.Delete(sessionFile);
            }
            if (File.Exists(torrentFile))
            {
                File.Delete(torrentFile);
            }
        }
    }
}
