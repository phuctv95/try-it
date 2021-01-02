using ByteSizeLib;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Torrent;
using Torrent.Model;
using TryMvvm.Base;
using TryMvvm.Model;

namespace TryMvvm.ViewModel
{
    public class TorrentDownloaderViewModel : BaseNotifyPropertyChanged
    {
        public ObservableCollection<FileModel> Files { get; set; } = new ObservableCollection<FileModel>();
        public CommandHandler DownloadCommand { get; }
        public CommandHandler SelectSavingLocationCommand { get; }
        public CommandHandler OnCheckChangedCommand { get; }
        public CommandHandler SelectTorrentFileCommand { get; }
        public string MagnetLinkOrTorrentFile
        {
            get => Get<string>();
            set => SetAndRaiseChangedNotify(value);
        }
        public string LogContent
        {
            get => Get<string>();
            set => SetAndRaiseChangedNotify(value);
        }
        public string SavingLocation
        {
            get => Get<string>();
            set => SetAndRaiseChangedNotify(value);
        }
        public bool DownloadFinished
        {
            get => Get<bool>();
            set
            {
                SetAndRaiseChangedNotify(value);
                DownloadCommand?.RaiseCanExecuteChanged();
            }
        }
        public int DownloadProgress
        {
            get => Get<int>();
            set => SetAndRaiseChangedNotify(value);
        }
        public string DownloadedBytes
        {
            get => Get<string>();
            set => SetAndRaiseChangedNotify(value);
        }
        public string TotalBytes
        {
            get => Get<string>();
            set => SetAndRaiseChangedNotify(value);
        }
        public string DownloadSpeed
        {
            get => Get<string>();
            set => SetAndRaiseChangedNotify(value);
        }
        public string MaxDownloadSpeed
        {
            get => Get<string>();
            set => SetAndRaiseChangedNotify(value);
        }
        public string ETA
        {
            get => Get<string>();
            set => SetAndRaiseChangedNotify(value);
        }
        public string AvgETA
        {
            get => Get<string>();
            set => SetAndRaiseChangedNotify(value);
        }
        public int ConnectedPeers
        {
            get => Get<int>();
            set => SetAndRaiseChangedNotify(value);
        }
        public int TotalPeers
        {
            get => Get<int>();
            set => SetAndRaiseChangedNotify(value);
        }
        private TorrentDownloader TorrentDownloader { get; } = new TorrentDownloader();

        public TorrentDownloaderViewModel()
        {
            MagnetLinkOrTorrentFile = string.Empty;
            DownloadFinished = true;
            SelectSavingLocationCommand = new CommandHandler(SelectSavingLocation);
            DownloadCommand = new CommandHandler(Download, () => DownloadFinished);
            OnCheckChangedCommand = new CommandHandler(OnCheckChanged);
            SelectTorrentFileCommand = new CommandHandler(SelectTorrentFile);
            SavingLocation = GetDefaultSavingLocation();
        }

        private static string GetDefaultSavingLocation()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void SelectSavingLocation()
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog() ?? false)
            {
                SavingLocation = dialog.SelectedPath;
            }
        }

        private void Download()
        {
            WriteLog($"Start getting {MagnetLinkOrTorrentFile}...");
            DownloadFinished = false;
            TorrentDownloader.StartDownloading(MagnetLinkOrTorrentFile, SavingLocation, WriteLog,
                listFiles => Application.Current.Dispatcher.Invoke(() =>
                {
                    Files.Clear();
                    foreach (var file in listFiles)
                    {
                        Files.Add(new FileModel { FileName = file, Selected = true });
                    }
                }),
                downloadStats => Application.Current.Dispatcher.Invoke(() => UpdateStats(downloadStats)),
                () => Application.Current.Dispatcher.Invoke(() => DownloadFinished = true));
        }

        private void UpdateStats(TorrentDownloadStats stats)
        {
            DownloadProgress = stats.Progress;
            DownloadedBytes = ByteSize.FromBytes(stats.DownloadedBytes).ToString();
            TotalBytes = ByteSize.FromBytes(stats.TotalBytes).ToString();
            DownloadSpeed = $"{ByteSize.FromBytes(stats.DownloadSpeed)}/s";
            MaxDownloadSpeed = $"{ByteSize.FromBytes(stats.MaxSpeed)}/s";
            ETA = $"{stats.ETA:hh\\:mm\\:ss}s";
            AvgETA = $"{stats.AvgETA:hh\\:mm\\:ss}s";
            ConnectedPeers = stats.PeersConnected;
            TotalPeers = stats.PeersTotal;
            foreach (var file in Files)
            {
                if (stats.Files.TryGetValue(file.FileName, out FileStatus? fileStats))
                {
                    var status = fileStats.DownloadStatus;
                    file.Status = status switch
                    {
                        DownloadStatus.Finished => $"{status} ✔",
                        _ => status.ToString()
                    };
                }
            }
        }

        private void OnCheckChanged()
        {
            TorrentDownloader.UpdateFilesToDownload(Files.Where(x => x.Selected).Select(x => x.FileName).ToList());
        }

        private void SelectTorrentFile()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Torrent Files (*.torrent)|*.torrent|All files (*.*)|*.*";
            if (dialog.ShowDialog() ?? false)
            {
                MagnetLinkOrTorrentFile = dialog.FileName;
            }
        }

        private void WriteLog(string msg)
        {
            LogContent += $"[{DateTime.Now}] {msg}{Environment.NewLine}";
        }
    }
}
