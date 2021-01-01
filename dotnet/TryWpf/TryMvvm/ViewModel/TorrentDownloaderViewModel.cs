using System;
using Torrent;

namespace TryMvvm.ViewModel
{
    public class TorrentDownloaderViewModel : BaseViewModel
    {
        public string MagnetLinkOrTorrentFile { get; set; } = string.Empty;
        public CommandHandler DownloadCommand { get; }
        public CommandHandler SelectSavingLocationCommand { get; }
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
        private TorrentDownloader TorrentDownloader { get; } = new TorrentDownloader();

        public TorrentDownloaderViewModel()
        {
            DownloadFinished = true;
            SelectSavingLocationCommand = new CommandHandler(SelectSavingLocation);
            DownloadCommand = new CommandHandler(Download, () => DownloadFinished);
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
                () => System.Windows.Application.Current.Dispatcher.Invoke(() => DownloadFinished = true));
        }

        private void WriteLog(string msg)
        {
            LogContent += $"[{DateTime.Now}] {msg}{Environment.NewLine}";
        }
    }
}
