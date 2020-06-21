namespace TryMachineLearningWinForms
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
            this.feelBtn = new System.Windows.Forms.Button();
            this.sentenceTxtBx = new System.Windows.Forms.TextBox();
            this.messageLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // feelBtn
            // 
            this.feelBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.feelBtn.Location = new System.Drawing.Point(292, 190);
            this.feelBtn.Name = "feelBtn";
            this.feelBtn.Size = new System.Drawing.Size(217, 84);
            this.feelBtn.TabIndex = 0;
            this.feelBtn.Text = "How do I feel about that sentence?";
            this.feelBtn.UseVisualStyleBackColor = true;
            this.feelBtn.Click += new System.EventHandler(this.feelBtn_Click);
            // 
            // sentenceTxtBx
            // 
            this.sentenceTxtBx.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sentenceTxtBx.Location = new System.Drawing.Point(226, 125);
            this.sentenceTxtBx.Name = "sentenceTxtBx";
            this.sentenceTxtBx.Size = new System.Drawing.Size(348, 27);
            this.sentenceTxtBx.TabIndex = 1;
            this.sentenceTxtBx.Text = "That\'s a nice motobike!";
            // 
            // messageLbl
            // 
            this.messageLbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.messageLbl.Location = new System.Drawing.Point(166, 296);
            this.messageLbl.Name = "messageLbl";
            this.messageLbl.Size = new System.Drawing.Size(468, 29);
            this.messageLbl.TabIndex = 2;
            this.messageLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.messageLbl);
            this.Controls.Add(this.sentenceTxtBx);
            this.Controls.Add(this.feelBtn);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button feelBtn;
        private System.Windows.Forms.TextBox sentenceTxtBx;
        private System.Windows.Forms.Label messageLbl;
    }
}

