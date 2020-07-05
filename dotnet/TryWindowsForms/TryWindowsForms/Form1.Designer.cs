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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lightModeTimeDtpkr = new System.Windows.Forms.DateTimePicker();
            this.darkModeTimeDtpkr = new System.Windows.Forms.DateTimePicker();
            this.scheduleBtn = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.cancelBtn = new System.Windows.Forms.Button();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Turn on Light Mode at:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Turn on Dark Mode at:";
            // 
            // lightModeTimeDtpkr
            // 
            this.lightModeTimeDtpkr.CustomFormat = "hh:mm tt";
            this.lightModeTimeDtpkr.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.lightModeTimeDtpkr.Location = new System.Drawing.Point(270, 32);
            this.lightModeTimeDtpkr.Name = "lightModeTimeDtpkr";
            this.lightModeTimeDtpkr.ShowUpDown = true;
            this.lightModeTimeDtpkr.Size = new System.Drawing.Size(112, 27);
            this.lightModeTimeDtpkr.TabIndex = 1;
            // 
            // darkModeTimeDtpkr
            // 
            this.darkModeTimeDtpkr.CustomFormat = "hh:mm tt";
            this.darkModeTimeDtpkr.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.darkModeTimeDtpkr.Location = new System.Drawing.Point(270, 70);
            this.darkModeTimeDtpkr.Name = "darkModeTimeDtpkr";
            this.darkModeTimeDtpkr.ShowUpDown = true;
            this.darkModeTimeDtpkr.Size = new System.Drawing.Size(112, 27);
            this.darkModeTimeDtpkr.TabIndex = 2;
            // 
            // scheduleBtn
            // 
            this.scheduleBtn.Location = new System.Drawing.Point(130, 130);
            this.scheduleBtn.Name = "scheduleBtn";
            this.scheduleBtn.Size = new System.Drawing.Size(94, 29);
            this.scheduleBtn.TabIndex = 3;
            this.scheduleBtn.Text = "Schedule";
            this.scheduleBtn.UseVisualStyleBackColor = true;
            // 
            // timer
            // 
            this.timer.Interval = 3000;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(262, 130);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(94, 29);
            this.cancelBtn.TabIndex = 4;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 190);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.scheduleBtn);
            this.Controls.Add(this.darkModeTimeDtpkr);
            this.Controls.Add(this.lightModeTimeDtpkr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
        private Label label1;
        private Label label2;
        private DateTimePicker lightModeTimeDtpkr;
        private DateTimePicker dateTimePicker2;
        private Button scheduleBtn;
        private DateTimePicker darkModeTimeDtpkr;
        private Timer timer;
        private Button cancelBtn;
    }
}

