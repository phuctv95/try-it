using System.Drawing;
using System.Windows.Forms;

namespace TryWindowsForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.scheduleDarkModeItm = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleForBoth = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleForWindowsControlsItm = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleForAppsItm = new System.Windows.Forms.ToolStripMenuItem();
            this.exitItm = new System.Windows.Forms.ToolStripMenuItem();
            this.lightModeTimeLbl = new System.Windows.Forms.Label();
            this.darkModeTimeLbl = new System.Windows.Forms.Label();
            this.lightModeTimeDtpkr = new System.Windows.Forms.DateTimePicker();
            this.darkModeTimeDtpkr = new System.Windows.Forms.DateTimePicker();
            this.saveBtn = new System.Windows.Forms.Button();
            this.scheduleTicker = new System.Windows.Forms.Timer(this.components);
            this.cancelBtn = new System.Windows.Forms.Button();
            this.enableScheduleRbtn = new System.Windows.Forms.RadioButton();
            this.disableScheduleRbtn = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.applyForWindowsControlsChbx = new System.Windows.Forms.CheckBox();
            this.applyForAppsChbx = new System.Windows.Forms.CheckBox();
            this.scheduleControlsGrp = new System.Windows.Forms.GroupBox();
            this.notifyIconContextMenu.SuspendLayout();
            this.scheduleControlsGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyIconContextMenu;
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Visible = true;
            // 
            // notifyIconContextMenu
            // 
            this.notifyIconContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.notifyIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scheduleDarkModeItm,
            this.toggleForBoth,
            this.toggleForWindowsControlsItm,
            this.toggleForAppsItm,
            this.exitItm});
            this.notifyIconContextMenu.Name = "notifyIconContextMenu";
            this.notifyIconContextMenu.Size = new System.Drawing.Size(372, 124);
            // 
            // scheduleDarkModeItm
            // 
            this.scheduleDarkModeItm.Name = "scheduleDarkModeItm";
            this.scheduleDarkModeItm.Size = new System.Drawing.Size(371, 24);
            this.scheduleDarkModeItm.Text = "Schedule Dark Mode";
            // 
            // toggleForBoth
            // 
            this.toggleForBoth.Name = "toggleForBoth";
            this.toggleForBoth.Size = new System.Drawing.Size(371, 24);
            this.toggleForBoth.Text = "Toggle Dark Mode For Windows && For Apps";
            // 
            // toggleForWindowsControlsItm
            // 
            this.toggleForWindowsControlsItm.Name = "toggleForWindowsControlsItm";
            this.toggleForWindowsControlsItm.Size = new System.Drawing.Size(371, 24);
            this.toggleForWindowsControlsItm.Text = "Toggle Dark Mode For Windows";
            // 
            // toggleForAppsItm
            // 
            this.toggleForAppsItm.Name = "toggleForAppsItm";
            this.toggleForAppsItm.Size = new System.Drawing.Size(371, 24);
            this.toggleForAppsItm.Text = "Toggle Dark Mode For Apps";
            // 
            // exitItm
            // 
            this.exitItm.Name = "exitItm";
            this.exitItm.Size = new System.Drawing.Size(371, 24);
            this.exitItm.Text = "Exit";
            // 
            // lightModeTimeLbl
            // 
            this.lightModeTimeLbl.AutoSize = true;
            this.lightModeTimeLbl.Location = new System.Drawing.Point(16, 33);
            this.lightModeTimeLbl.Name = "lightModeTimeLbl";
            this.lightModeTimeLbl.Size = new System.Drawing.Size(159, 20);
            this.lightModeTimeLbl.TabIndex = 1;
            this.lightModeTimeLbl.Text = "Turn on Light Mode at:";
            // 
            // darkModeTimeLbl
            // 
            this.darkModeTimeLbl.AutoSize = true;
            this.darkModeTimeLbl.Location = new System.Drawing.Point(16, 71);
            this.darkModeTimeLbl.Name = "darkModeTimeLbl";
            this.darkModeTimeLbl.Size = new System.Drawing.Size(157, 20);
            this.darkModeTimeLbl.TabIndex = 1;
            this.darkModeTimeLbl.Text = "Turn on Dark Mode at:";
            // 
            // lightModeTimeDtpkr
            // 
            this.lightModeTimeDtpkr.CustomFormat = "hh:mm tt";
            this.lightModeTimeDtpkr.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.lightModeTimeDtpkr.Location = new System.Drawing.Point(188, 28);
            this.lightModeTimeDtpkr.Name = "lightModeTimeDtpkr";
            this.lightModeTimeDtpkr.ShowUpDown = true;
            this.lightModeTimeDtpkr.Size = new System.Drawing.Size(112, 27);
            this.lightModeTimeDtpkr.TabIndex = 1;
            // 
            // darkModeTimeDtpkr
            // 
            this.darkModeTimeDtpkr.CustomFormat = "hh:mm tt";
            this.darkModeTimeDtpkr.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.darkModeTimeDtpkr.Location = new System.Drawing.Point(188, 66);
            this.darkModeTimeDtpkr.Name = "darkModeTimeDtpkr";
            this.darkModeTimeDtpkr.ShowUpDown = true;
            this.darkModeTimeDtpkr.Size = new System.Drawing.Size(112, 27);
            this.darkModeTimeDtpkr.TabIndex = 2;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(125, 290);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(94, 29);
            this.saveBtn.TabIndex = 3;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            // 
            // scheduleTicker
            // 
            this.scheduleTicker.Interval = 3000;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(257, 290);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(94, 29);
            this.cancelBtn.TabIndex = 4;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // enableScheduleRbtn
            // 
            this.enableScheduleRbtn.AutoSize = true;
            this.enableScheduleRbtn.Location = new System.Drawing.Point(149, 77);
            this.enableScheduleRbtn.Name = "enableScheduleRbtn";
            this.enableScheduleRbtn.Size = new System.Drawing.Size(75, 24);
            this.enableScheduleRbtn.TabIndex = 5;
            this.enableScheduleRbtn.TabStop = true;
            this.enableScheduleRbtn.Text = "Enable";
            this.enableScheduleRbtn.UseVisualStyleBackColor = true;
            // 
            // disableScheduleRbtn
            // 
            this.disableScheduleRbtn.AutoSize = true;
            this.disableScheduleRbtn.Location = new System.Drawing.Point(257, 77);
            this.disableScheduleRbtn.Name = "disableScheduleRbtn";
            this.disableScheduleRbtn.Size = new System.Drawing.Size(80, 24);
            this.disableScheduleRbtn.TabIndex = 6;
            this.disableScheduleRbtn.TabStop = true;
            this.disableScheduleRbtn.Text = "Disable";
            this.disableScheduleRbtn.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe Print", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(101, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(285, 35);
            this.label3.TabIndex = 7;
            this.label3.Text = "Schedule Dark/Light Mode";
            // 
            // applyForWindowsControlsChbx
            // 
            this.applyForWindowsControlsChbx.AutoSize = true;
            this.applyForWindowsControlsChbx.Location = new System.Drawing.Point(16, 99);
            this.applyForWindowsControlsChbx.Name = "applyForWindowsControlsChbx";
            this.applyForWindowsControlsChbx.Size = new System.Drawing.Size(215, 24);
            this.applyForWindowsControlsChbx.TabIndex = 8;
            this.applyForWindowsControlsChbx.Text = "Apply for Windows controls";
            this.applyForWindowsControlsChbx.UseVisualStyleBackColor = true;
            // 
            // applyForAppsChbx
            // 
            this.applyForAppsChbx.AutoSize = true;
            this.applyForAppsChbx.Location = new System.Drawing.Point(16, 129);
            this.applyForAppsChbx.Name = "applyForAppsChbx";
            this.applyForAppsChbx.Size = new System.Drawing.Size(129, 24);
            this.applyForAppsChbx.TabIndex = 8;
            this.applyForAppsChbx.Text = "Apply for apps";
            this.applyForAppsChbx.UseVisualStyleBackColor = true;
            // 
            // scheduleControlsGrp
            // 
            this.scheduleControlsGrp.Controls.Add(this.applyForAppsChbx);
            this.scheduleControlsGrp.Controls.Add(this.lightModeTimeLbl);
            this.scheduleControlsGrp.Controls.Add(this.applyForWindowsControlsChbx);
            this.scheduleControlsGrp.Controls.Add(this.darkModeTimeLbl);
            this.scheduleControlsGrp.Controls.Add(this.lightModeTimeDtpkr);
            this.scheduleControlsGrp.Controls.Add(this.darkModeTimeDtpkr);
            this.scheduleControlsGrp.Location = new System.Drawing.Point(87, 107);
            this.scheduleControlsGrp.Name = "scheduleControlsGrp";
            this.scheduleControlsGrp.Size = new System.Drawing.Size(312, 165);
            this.scheduleControlsGrp.TabIndex = 9;
            this.scheduleControlsGrp.TabStop = false;
            this.scheduleControlsGrp.Text = "Schedule";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 333);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.disableScheduleRbtn);
            this.Controls.Add(this.enableScheduleRbtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.scheduleControlsGrp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.notifyIconContextMenu.ResumeLayout(false);
            this.scheduleControlsGrp.ResumeLayout(false);
            this.scheduleControlsGrp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private ContextMenuStrip notifyIconContextMenu;
        private ToolStripMenuItem exitItm;
        private ToolStripMenuItem toggleForAppsItm;
        private ToolStripMenuItem scheduleDarkModeItm;
        private Label lightModeTimeLbl;
        private Label darkModeTimeLbl;
        private DateTimePicker lightModeTimeDtpkr;
        private Button saveBtn;
        private DateTimePicker darkModeTimeDtpkr;
        private Timer scheduleTicker;
        private Button cancelBtn;
        private RadioButton enableScheduleRbtn;
        private RadioButton disableScheduleRbtn;
        private Label label3;
        private CheckBox applyForWindowsControlsChbx;
        private CheckBox applyForAppsChbx;
        private GroupBox scheduleControlsGrp;
        private ToolStripMenuItem toggleForWindowsControlsItm;
        private ToolStripMenuItem toggleForBoth;
    }
}

