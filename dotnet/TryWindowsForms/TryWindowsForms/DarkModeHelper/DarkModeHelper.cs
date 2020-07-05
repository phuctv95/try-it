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
        public delegate void AfterToggleModeEvent(WindowsColorMode mode);
        public static AfterToggleModeEvent AfterToggleModeHandlers;

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
            AfterToggleModeHandlers?.Invoke(target);
        }

        public static void SwitchWindowsColorModeIfOnTime()
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
                return;
            }
        }

        public static void WriteToSettingFile(string lightModeTime, string darkModeTime)
        {
            Settings.Write(LightModeTimeSettingKey, lightModeTime);
            Settings.Write(DarkModeTimeSettingKey, darkModeTime);
        }

        public static DarkModeSetting ReadConfigFile()
        {
            return new DarkModeSetting
            {
                LightTime = Settings.Read(LightModeTimeSettingKey),
                DarkTime = Settings.Read(DarkModeTimeSettingKey)
            };
        }
    }
}
