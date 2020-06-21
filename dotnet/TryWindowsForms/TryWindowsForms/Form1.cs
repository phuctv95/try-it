using Microsoft.Win32;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace TryWindowsForms
{
    public partial class Form1 : Form
    {
        private const string ApplicationName = "Windows Tools";
        private const string BasePath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string AppsUseLightThemeValueName = "AppsUseLightTheme";
        private const string TimeFormat = "hh:mm tt";
        private const string LightModeTimeSettingKey = "TimeToTriggerLightMode";
        private const string DarkModeTimeSettingKey = "TimeToTriggerDarkMode";
        private bool UserClickedExitMenuItem = false;

        public Form1()
        {
            InitializeComponent();
            InitOthers();
            BindEventForContextMenuItems();
        }

        private void InitOthers()
        {
            notifyIcon.Icon = SystemIcons.Asterisk;
            timer.Start();
            notifyIcon.Text = ApplicationName;
            Text = ApplicationName;
        }

        private void BindEventForContextMenuItems()
        {
            Shown += (sender, e) => Hide();
            scheduleDarkModeItm.Click += (sender, e) => ShowUiAndData();
            toggleDarkModeItm.Click += (sender, e) => ToggleWindowsColorMode();
            exitItm.Click += (sender, e) => ExitApplication();
            notifyIcon.DoubleClick += (sender, e) => ShowUiAndData();
            scheduleBtn.Click += (sender, e) => WriteToSettingFileAndCloseDialog();
            cancelBtn.Click += (sender, e) => Hide();
            timer.Tick += (sender, e) => SwitchWindowsColorModeIfOnTime();
            FormClosing += (sender, e) => MinimizeToSysTrayIfClickClose(e);
        }

        private void ExitApplication()
        {
            UserClickedExitMenuItem = true;
            timer.Stop();
            timer.Dispose();
            Application.Exit();
        }

        private void MinimizeToSysTrayIfClickClose(FormClosingEventArgs e)
        {
            e.Cancel = !UserClickedExitMenuItem;
            Hide();
        }

        private void SwitchWindowsColorModeIfOnTime()
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
                if (GetCurrentWindowsColorMode() == WindowsColorMode.Light)
                {
                    return;
                }
                SwitchWindowsColorModeTo(WindowsColorMode.Light);
                return;
            }
            if (darkModeTime.Hour == now.Hour && darkModeTime.Minute == now.Minute)
            {
                if (GetCurrentWindowsColorMode() == WindowsColorMode.Dark)
                {
                    return;
                }
                SwitchWindowsColorModeTo(WindowsColorMode.Dark);
                return;
            }
        }

        private void WriteToSettingFile()
        {
            var lightModeTime = lightModeTimeDtpkr.Value.ToString(TimeFormat);
            var darkModeTime = darkModeTimeDtpkr.Value.ToString(TimeFormat);
            var config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings.Remove(LightModeTimeSettingKey);
            config.AppSettings.Settings.Add(LightModeTimeSettingKey, lightModeTime);
            config.AppSettings.Settings.Remove(DarkModeTimeSettingKey);
            config.AppSettings.Settings.Add(DarkModeTimeSettingKey, darkModeTime);
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void WriteToSettingFileAndCloseDialog()
        {
            WriteToSettingFile();
            Hide();
        }

        private void ShowUiAndData()
        {
            Show();

            var config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            var lightModeTimeFromSettingFile =
                config.AppSettings.Settings[LightModeTimeSettingKey]?.Value;
            var darkModeTimeFromSettingFile =
                config.AppSettings.Settings[DarkModeTimeSettingKey]?.Value;
            var now = DateTime.Now;

            lightModeTimeDtpkr.Value = lightModeTimeFromSettingFile != null
                ? DateTime.Parse(lightModeTimeFromSettingFile)
                : new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);
            darkModeTimeDtpkr.Value = darkModeTimeFromSettingFile != null
                ? DateTime.Parse(darkModeTimeFromSettingFile)
                : new DateTime(now.Year, now.Month, now.Day, 18, 0, 0);
        }

        private void ToggleWindowsColorMode()
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

        private WindowsColorMode GetCurrentWindowsColorMode()
        {
            var appUseLightTheme = Registry.GetValue(BasePath, AppsUseLightThemeValueName, null);
            if (appUseLightTheme == null) { throw new Exception(); }
            return appUseLightTheme as int? == 1 ? WindowsColorMode.Light : WindowsColorMode.Dark;
        }

        private void SwitchWindowsColorModeTo(WindowsColorMode target)
        {
            var appsUseLightTheme = target == WindowsColorMode.Light ? 1 : 0;
            Registry.SetValue(BasePath, AppsUseLightThemeValueName, appsUseLightTheme);
            ShowBalloonTipText($"Switched successfully to {target} Mode.");
        }

        private void ShowBalloonTipText(string message)
        {
            notifyIcon.BalloonTipText = message;
            notifyIcon.ShowBalloonTip(500);
        }
    }
}
