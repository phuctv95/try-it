using System.Windows.Controls;

namespace TryMvvm.View
{
    /// <summary>
    /// Interaction logic for TorrentDownloaderView.xaml
    /// </summary>
    public partial class TorrentDownloaderView : UserControl
    {
        public TorrentDownloaderView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox)?.ScrollToEnd();
        }
    }
}
