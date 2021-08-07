using System.Windows;

namespace WindowsTools.Services
{
    public class MessageBoxExtra
    {
        public void Info(string title, string message)
        {
            Show(title, message, MessageBoxImage.Information);
        }
        public void Error(string title, string message)
        {
            Show(title, message, MessageBoxImage.Error);
        }

        private void Show(string title, string message, MessageBoxImage messageBoxImage)
        {
            MessageBox.Show(
                message,
                title,
                MessageBoxButton.OK,
                messageBoxImage);
        }
    }
}
