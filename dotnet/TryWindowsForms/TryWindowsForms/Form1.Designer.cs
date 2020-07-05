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
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.scheduleDarkModeItm = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleDarkModeItm = new System.Windows.Forms.ToolStripMenuItem();
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
            this.notifyIconContextMenu.SuspendLayout();
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
            this.toggleDarkModeItm,
            this.exitItm});
            this.notifyIconContextMenu.Name = "notifyIconContextMenu";
            this.notifyIconContextMenu.Size = new System.Drawing.Size(217, 76);
            // 
            // scheduleDarkModeItm
            // 
            this.scheduleDarkModeItm.Name = "scheduleDarkModeItm";
            this.scheduleDarkModeItm.Size = new System.Drawing.Size(216, 24);
            this.scheduleDarkModeItm.Text = "Schedule Dark Mode";
            // 
            // toggleDarkModeItm
            // 
            this.toggleDarkModeItm.Name = "toggleDarkModeItm";
            this.toggleDarkModeItm.Size = new System.Drawing.Size(216, 24);
            this.toggleDarkModeItm.Text = "Toggle Dark Mode";
            // 
            // exitItm
            // 
            this.exitItm.Name = "exitItm";
            this.exitItm.Size = new System.Drawing.Size(216, 24);
            this.exitItm.Text = "Exit";
            // 
            // lightModeTimeLbl
            // 
            this.lightModeTimeLbl.AutoSize = true;
            this.lightModeTimeLbl.Location = new System.Drawing.Point(106, 116);
            this.lightModeTimeLbl.Name = "lightModeTimeLbl";
            this.lightModeTimeLbl.Size = new System.Drawing.Size(159, 20);
            this.lightModeTimeLbl.TabIndex = 1;
            this.lightModeTimeLbl.Text = "Turn on Light Mode at:";
            // 
            // darkModeTimeLbl
            // 
            this.darkModeTimeLbl.AutoSize = true;
            this.darkModeTimeLbl.Location = new System.Drawing.Point(108, 154);
            this.darkModeTimeLbl.Name = "darkModeTimeLbl";
            this.darkModeTimeLbl.Size = new System.Drawing.Size(157, 20);
            this.darkModeTimeLbl.TabIndex = 1;
            this.darkModeTimeLbl.Text = "Turn on Dark Mode at:";
            // 
            // lightModeTimeDtpkr
            // 
            this.lightModeTimeDtpkr.CustomFormat = "hh:mm tt";
            this.lightModeTimeDtpkr.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.lightModeTimeDtpkr.Location = new System.Drawing.Point(271, 111);
            this.lightModeTimeDtpkr.Name = "lightModeTimeDtpkr";
            this.lightModeTimeDtpkr.ShowUpDown = true;
            this.lightModeTimeDtpkr.Size = new System.Drawing.Size(112, 27);
            this.lightModeTimeDtpkr.TabIndex = 1;
            // 
            // darkModeTimeDtpkr
            // 
            this.darkModeTimeDtpkr.CustomFormat = "hh:mm tt";
            this.darkModeTimeDtpkr.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.darkModeTimeDtpkr.Location = new System.Drawing.Point(271, 149);
            this.darkModeTimeDtpkr.Name = "darkModeTimeDtpkr";
            this.darkModeTimeDtpkr.ShowUpDown = true;
            this.darkModeTimeDtpkr.Size = new System.Drawing.Size(112, 27);
            this.darkModeTimeDtpkr.TabIndex = 2;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(130, 205);
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
            this.cancelBtn.Location = new System.Drawing.Point(262, 205);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 251);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.disableScheduleRbtn);
            this.Controls.Add(this.enableScheduleRbtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.darkModeTimeDtpkr);
            this.Controls.Add(this.lightModeTimeDtpkr);
            this.Controls.Add(this.darkModeTimeLbl);
            this.Controls.Add(this.lightModeTimeLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.notifyIconContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private ContextMenuStrip notifyIconContextMenu;
        private ToolStripMenuItem exitItm;
        private ToolStripMenuItem toggleDarkModeItm;
        private ToolStripMenuItem scheduleDarkModeItm;
        private Label lightModeTimeLbl;
        private Label darkModeTimeLbl;
        private DateTimePicker lightModeTimeDtpkr;
        private DateTimePicker dateTimePicker2;
        private Button saveBtn;
        private DateTimePicker darkModeTimeDtpkr;
        private Timer scheduleTicker;
        private Button cancelBtn;
        private RadioButton enableScheduleRbtn;
        private RadioButton disableScheduleRbtn;
        private Label label3;
    }
}

