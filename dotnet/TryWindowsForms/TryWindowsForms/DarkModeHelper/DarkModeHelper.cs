using Microsoft.Win32;
using System;
using System.Drawing;

namespace TryWindowsForms.DarkModeHelper
{
    public static class DarkModeHelper
    {
        private const string BasePath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string AppsUseLightThemeValueName = "AppsUseLightTheme";
        private const string SystemUsesLightThemeValueName = "SystemUsesLightTheme";
        private const string EnableScheduleKey = "EnableSchedule";
        private const string LightModeTimeSettingKey = "TimeToTriggerLightMode";
        private const string DarkModeTimeSettingKey = "TimeToTriggerDarkMode";
        private const string ApplyForWindowsControlsKey = "ApplyForWindowsControls";
        private const string ApplyForAppsKey = "ApplyForApps";
        public delegate void AfterToggleModeEvent(DarkModeApplyArea area, WindowsColorMode mode);
        public static AfterToggleModeEvent AfterToggleModeHandlers;

        public static bool IsEnableSchedule => Settings.ReadBoolean(EnableScheduleKey);
        public static bool IsApplyForWindowsControls => Settings.ReadBoolean(ApplyForWindowsControlsKey);
        public static bool IsApplyForApps => Settings.ReadBoolean(ApplyForAppsKey);

        public static Icon GetSystemTrayIcon(Icon iconUseForDarkMode, 
            Icon iconUseForLightMode)
        {
            var currentMode = 
                GetCurrentWindowsColorMode(DarkModeApplyArea.ForWindowsControls);
            return currentMode == WindowsColorMode.Dark
                ? iconUseForDarkMode
                : iconUseForLightMode;
        }

        public static void ToggleColorMode(DarkModeApplyArea area)
        {
            var currentMode = GetCurrentWindowsColorMode(area);
            if (currentMode == WindowsColorMode.Dark)
            {
                SwitchColorModeTo(area, WindowsColorMode.Light);
            }
            else
            {
                SwitchColorModeTo(area, WindowsColorMode.Dark);
            }
        }

        public static void ToggleColorModeInBothAreas()
        {
            ToggleColorMode(DarkModeApplyArea.ForWindowsControls);
            ToggleColorMode(DarkModeApplyArea.ForApps);
        }

        public static WindowsColorMode GetCurrentWindowsColorMode(
            DarkModeApplyArea area)
        {
            var value = area == DarkModeApplyArea.ForWindowsControls
                ? Registry.GetValue(BasePath, SystemUsesLightThemeValueName, null)
                : Registry.GetValue(BasePath, AppsUseLightThemeValueName, null);
            if (value == null) { throw new Exception(); }

            return value as int? == 1 
                ? WindowsColorMode.Light 
                : WindowsColorMode.Dark;
        }

        public static void SwitchColorModeTo(DarkModeApplyArea area,
            WindowsColorMode target)
        {
            if (GetCurrentWindowsColorMode(area) == target)
            {
                return;
            }

            var value = target == WindowsColorMode.Light ? 1 : 0;
            var key = area == DarkModeApplyArea.ForWindowsControls
                ? SystemUsesLightThemeValueName
                : AppsUseLightThemeValueName;
            Registry.SetValue(BasePath, key, value);
            AfterToggleModeHandlers?.Invoke(area, target);
        }

        public static void SwitchModeIfOnTime()
        {
            if (!IsEnableSchedule) { return; }

            var lightModeTime = DateTime.Parse(Settings.Read(LightModeTimeSettingKey));
            var darkModeTime = DateTime.Parse(Settings.Read(DarkModeTimeSettingKey));
            SwitchModeIfOnTime(lightModeTime, WindowsColorMode.Light);
            SwitchModeIfOnTime(darkModeTime, WindowsColorMode.Dark);
        }

        private static void SwitchModeIfOnTime(DateTime scheduleTime, 
            WindowsColorMode target)
        {
            var now = DateTime.Now;
            var onTime = scheduleTime.Hour == now.Hour && scheduleTime.Minute == now.Minute;
            if (!onTime) { return; }

            if (IsApplyForWindowsControls)
            {
                SwitchColorModeTo(DarkModeApplyArea.ForWindowsControls, target);
            }
            if (IsApplyForApps)
            {
                SwitchColorModeTo(DarkModeApplyArea.ForApps, target);
            }
        }

        public static void WriteToSettingFile(DarkModeSetting settings)
        {
            Settings.Write(EnableScheduleKey, settings.EnableSchedule);
            Settings.Write(LightModeTimeSettingKey, settings.LightTime);
            Settings.Write(DarkModeTimeSettingKey, settings.DarkTime);
            Settings.Write(ApplyForWindowsControlsKey, settings.IsApplyForWindowsControls);
            Settings.Write(ApplyForAppsKey, settings.IsApplyForApps);
        }

        public static DarkModeSetting ReadConfigFile()
        {
            return new DarkModeSetting
            {
                EnableSchedule = IsEnableSchedule,
                LightTime = Settings.Read(LightModeTimeSettingKey),
                DarkTime = Settings.Read(DarkModeTimeSettingKey),
                IsApplyForWindowsControls = Settings.ReadBoolean(ApplyForWindowsControlsKey),
                IsApplyForApps = Settings.ReadBoolean(ApplyForAppsKey)
            };
        }
    }
}
