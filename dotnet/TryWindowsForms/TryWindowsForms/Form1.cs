using System;
using System.Drawing;
using System.Windows.Forms;
using TryWindowsForms.DarkModeHelper;
using DMH = TryWindowsForms.DarkModeHelper.DarkModeHelper;

namespace TryWindowsForms
{
    public partial class Form1 : Form
    {
        private const string ApplicationName = "Windows Tools";
        private const string TimeFormat = "hh:mm tt";
        private bool UserClickedExitMenuItem = false;
        private readonly Icon ApplicationIcon = new Icon("Assets/icon.ico");
        private readonly Icon ApplicationIconLight = new Icon("Assets/icon-light.ico");
        private readonly Icon ApplicationIconDark = new Icon("Assets/icon-dark.ico");

        public Form1()
        {
            InitializeComponent();
            InitOthers();
            BindEventForContextMenuItems();
        }

        private void InitOthers()
        {
            if (DMH.IsEnableSchedule)
            {
                scheduleTicker.Start();
            }
            notifyIcon.Icon = ApplicationIconLight;
            Icon = ApplicationIcon;
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
            saveBtn.Click += (sender, e) => ClickSaveHandler();
            cancelBtn.Click += (sender, e) => Hide();
            scheduleTicker.Tick += (sender, e) => DMH.SwitchWindowsColorModeIfOnTime();
            FormClosing += (sender, e) => MinimizeToSysTrayIfClickClose(e);
            DMH.AfterToggleModeHandlers += AfterToggleModeHandler;
            lightModeTimeDtpkr.ValueChanged += (sender, e) => UpdateVisibleOfScheduleBtn();
            darkModeTimeDtpkr.ValueChanged += (sender, e) => UpdateVisibleOfScheduleBtn();
            enableScheduleRbtn.CheckedChanged += (sender, e) => UpdateVisibleOfScheduleControls();
            disableScheduleRbtn.CheckedChanged += (sender, e) => UpdateVisibleOfScheduleControls();
        }

        private void ClickSaveHandler()
        {
            WriteToSettingFileAndCloseDialog();
            StartOrStopTicker(enableScheduleRbtn.Checked, scheduleTicker.Enabled);
        }

        private void StartOrStopTicker(bool enableSchedule, bool tickerIsRunning)
        {
            if (enableSchedule && !tickerIsRunning)
            {
                scheduleTicker.Start();
            }
            else if (!enableSchedule && tickerIsRunning)
            {
                scheduleTicker.Stop();
            }
        }

        private void UpdateVisibleOfScheduleControls()
        {
            var enableSchedule = enableScheduleRbtn.Checked;
            lightModeTimeDtpkr.Enabled = enableSchedule;
            lightModeTimeLbl.Enabled = enableSchedule;
            darkModeTimeDtpkr.Enabled = enableSchedule;
            darkModeTimeLbl.Enabled = enableSchedule;
        }

        private void UpdateVisibleOfScheduleBtn()
        {
            var sameTime = lightModeTimeDtpkr.Value.Hour == darkModeTimeDtpkr.Value.Hour
                && lightModeTimeDtpkr.Value.Minute == darkModeTimeDtpkr.Value.Minute;
            saveBtn.Enabled = !sameTime;
        }

        private void AfterToggleModeHandler(WindowsColorMode mode)
        {
            ShowBalloonTipText($"Switched successfully to {mode} Mode.");
        }

        private void ExitApplication()
        {
            UserClickedExitMenuItem = true;
            scheduleTicker.Stop();
            scheduleTicker.Dispose();
            Application.Exit();
        }

        private void MinimizeToSysTrayIfClickClose(FormClosingEventArgs e)
        {
            e.Cancel = !UserClickedExitMenuItem;
            Hide();
        }

        private void WriteToSettingFileAndCloseDialog()
        {
            var settings = new DarkModeSetting
            {
                EnableSchedule = enableScheduleRbtn.Checked,
                LightTime = lightModeTimeDtpkr.Value.ToString(TimeFormat),
                DarkTime = darkModeTimeDtpkr.Value.ToString(TimeFormat)
            };
            DMH.WriteToSettingFile(settings);
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
            enableScheduleRbtn.Checked = settings.EnableSchedule;
            disableScheduleRbtn.Checked = !settings.EnableSchedule;
        }

        private void ShowBalloonTipText(string message)
        {
            notifyIcon.BalloonTipText = message;
            notifyIcon.ShowBalloonTip(500);
        }
    }
}
