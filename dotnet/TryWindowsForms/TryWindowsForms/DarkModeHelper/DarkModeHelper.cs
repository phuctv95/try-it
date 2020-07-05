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
        private const string EnableScheduleKey = "EnableSchedule";
        private const string LightModeTimeSettingKey = "TimeToTriggerLightMode";
        private const string DarkModeTimeSettingKey = "TimeToTriggerDarkMode";
        public delegate void AfterToggleModeEvent(WindowsColorMode mode);
        public static AfterToggleModeEvent AfterToggleModeHandlers;

        public static bool IsEnableSchedule
        {
            get
            {
                return Settings.Read(EnableScheduleKey) == "1";
            } 
        }

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
            if (!IsEnableSchedule) { return; }

            var lightModeTime = DateTime.Parse(Settings.Read(LightModeTimeSettingKey));
            var darkModeTime = DateTime.Parse(Settings.Read(DarkModeTimeSettingKey));
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

        public static void WriteToSettingFile(DarkModeSetting settings)
        {
            Settings.Write(EnableScheduleKey, settings.EnableSchedule ? "1" : "0");
            Settings.Write(LightModeTimeSettingKey, settings.LightTime);
            Settings.Write(DarkModeTimeSettingKey, settings.DarkTime);
        }

        public static DarkModeSetting ReadConfigFile()
        {
            return new DarkModeSetting
            {
                EnableSchedule = IsEnableSchedule,
                LightTime = Settings.Read(LightModeTimeSettingKey),
                DarkTime = Settings.Read(DarkModeTimeSettingKey)
            };
        }
    }
}
