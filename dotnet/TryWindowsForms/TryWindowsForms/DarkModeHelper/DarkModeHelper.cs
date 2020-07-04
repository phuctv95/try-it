using Microsoft.Win32;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace TryWindowsForms.DarkModeHelper
{
    public static class DarkModeHelper
    {
        private const string BasePath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string AppsUseLightThemeValueName = "AppsUseLightTheme";
        private const string LightModeTimeSettingKey = "TimeToTriggerLightMode";
        private const string DarkModeTimeSettingKey = "TimeToTriggerDarkMode";

        public static void ToggleWindowsColorMode()
        {
            var currentMode = GetCurrentWindowsColorMode();
            if (currentMode == WindowsColorMode.Dark)
            {
                SwitchWindowsColorModeTo(WindowsColorMode.Light);
            }
            else
            {
                SwitchWindowsColorModeTo(WindowsColorMode.Dark);
            }
        }

        public static WindowsColorMode GetCurrentWindowsColorMode()
        {
            var appUseLightTheme = Registry.GetValue(BasePath, AppsUseLightThemeValueName, null);
            if (appUseLightTheme == null) { throw new Exception(); }
            return appUseLightTheme as int? == 1 ? WindowsColorMode.Light : WindowsColorMode.Dark;
        }

        public static void SwitchWindowsColorModeTo(WindowsColorMode target)
        {
            var appsUseLightTheme = target == WindowsColorMode.Light ? 1 : 0;
            Registry.SetValue(BasePath, AppsUseLightThemeValueName, appsUseLightTheme);
        }

        public static void SwitchWindowsColorModeIfOnTime(NotifyIcon notifyIcon)
        {
            var config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            var lightModeTimeFromSettingFile =
                config.AppSettings.Settings[LightModeTimeSettingKey]?.Value;
            var darkModeTimeFromSettingFile =
                config.AppSettings.Settings[DarkModeTimeSettingKey]?.Value;
            var isScheduledInSettingFile =
                lightModeTimeFromSettingFile != null
                || darkModeTimeFromSettingFile != null;

            if (!isScheduledInSettingFile) { return; }

            var lightModeTime = DateTime.Parse(lightModeTimeFromSettingFile);
            var darkModeTime = DateTime.Parse(darkModeTimeFromSettingFile);
            var now = DateTime.Now;
            if (lightModeTime.Hour == now.Hour && lightModeTime.Minute == now.Minute)
            {
                var target = WindowsColorMode.Light;
                if (GetCurrentWindowsColorMode() == target)
                {
                    return;
                }
                SwitchWindowsColorModeTo(target);
                ShowBalloonTipText(notifyIcon, $"Switched successfully to {target} Mode.");
                return;
            }
            if (darkModeTime.Hour == now.Hour && darkModeTime.Minute == now.Minute)
            {
                var target = WindowsColorMode.Light;
                if (GetCurrentWindowsColorMode() == target)
                {
                    return;
                }
                SwitchWindowsColorModeTo(target);
                ShowBalloonTipText(notifyIcon, $"Switched successfully to {target} Mode.");
                return;
            }
        }

        public static void WriteToSettingFile(string lightModeTime, string darkModeTime)
        {
            var config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings.Remove(LightModeTimeSettingKey);
            config.AppSettings.Settings.Add(LightModeTimeSettingKey, lightModeTime);
            config.AppSettings.Settings.Remove(DarkModeTimeSettingKey);
            config.AppSettings.Settings.Add(DarkModeTimeSettingKey, darkModeTime);
            config.Save(ConfigurationSaveMode.Modified);
        }

        public static DarkModeSetting ReadConfigFile()
        {
            var config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            return new DarkModeSetting
            {
                LightTime = config.AppSettings.Settings[LightModeTimeSettingKey]?.Value,
                DarkTime = config.AppSettings.Settings[DarkModeTimeSettingKey]?.Value
            };
        }

        private static void ShowBalloonTipText(NotifyIcon notifyIcon, string message)
        {
            notifyIcon.BalloonTipText = message;
            notifyIcon.ShowBalloonTip(500);
        }
    }
}
