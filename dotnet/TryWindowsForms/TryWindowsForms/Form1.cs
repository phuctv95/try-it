using System;
using System.Drawing;
using System.Windows.Forms;
using DMH = TryWindowsForms.DarkModeHelper.DarkModeHelper;

namespace TryWindowsForms
{
    public partial class Form1 : Form
    {
        private const string ApplicationName = "Windows Tools";
        private const string TimeFormat = "hh:mm tt";
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
            toggleDarkModeItm.Click += (sender, e) => DMH.ToggleWindowsColorMode();
            exitItm.Click += (sender, e) => ExitApplication();
            notifyIcon.DoubleClick += (sender, e) => ShowUiAndData();
            scheduleBtn.Click += (sender, e) => WriteToSettingFileAndCloseDialog();
            cancelBtn.Click += (sender, e) => Hide();
            timer.Tick += (sender, e) => DMH.SwitchWindowsColorModeIfOnTime(notifyIcon);
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

        private void WriteToSettingFileAndCloseDialog()
        {
            var lightModeTime = lightModeTimeDtpkr.Value.ToString(TimeFormat);
            var darkModeTime = darkModeTimeDtpkr.Value.ToString(TimeFormat);
            DMH.WriteToSettingFile(lightModeTime, darkModeTime);
            Hide();
        }

        private void ShowUiAndData()
        {
            Show();

            var settings = DMH.ReadConfigFile();
            var now = DateTime.Now;

            lightModeTimeDtpkr.Value = settings.LightTime != null
                ? DateTime.Parse(settings.LightTime)
                : new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);
            darkModeTimeDtpkr.Value = settings.DarkTime != null
                ? DateTime.Parse(settings.DarkTime)
                : new DateTime(now.Year, now.Month, now.Day, 18, 0, 0);
        }
    }
}
