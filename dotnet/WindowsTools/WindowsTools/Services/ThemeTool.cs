using Microsoft.Win32;
using System.Windows;

namespace WindowsTools.Services
{
    public class ThemeTool
    {
        private const string BasePath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string AppsLightThemeName = "AppsUseLightTheme";
        private const string SystemLightThemeName = "SystemUsesLightTheme";

        private readonly MessageBoxExtra _messageBox = new MessageBoxExtra();

        public void ToggleSystemLightTheme()
        {
            ToggleLightTheme(SystemLightThemeName);
        }

        public void ToggleAppsLightTheme()
        {
            ToggleLightTheme(AppsLightThemeName);
        }

        private void ToggleLightTheme(string lightThemeName)
        {
            var current = Registry.GetValue(BasePath, lightThemeName, null) as int?;
            if (current == null)
            {
                _messageBox.Error("Error", $@"Not found: {BasePath}\{lightThemeName}");
                return;
            }
            Registry.SetValue(
                BasePath,
                lightThemeName,
                current == 1 ? 0 : 1);
        }
    }
}
