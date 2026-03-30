namespace frmdriverbackup
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtDownloadUrl = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblOSInfo = new System.Windows.Forms.Label();
            this.btnDonate = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlMiddle = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblUrlLabel = new System.Windows.Forms.Label();
            this.pnlSpacer1 = new System.Windows.Forms.Panel();
            this.pnlSpacer2 = new System.Windows.Forms.Panel();
            this.pnlTop.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(20, 25, 20, 25);
            this.pnlTop.Size = new System.Drawing.Size(600, 80);
            this.pnlTop.TabIndex = 5;
            // 
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.lblTitle.Location = new System.Drawing.Point(120, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(310, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Backup/Restore Driver Tool";
            // 
            // pnlSpacer1
            // 
            this.pnlSpacer1.BackColor = System.Drawing.Color.White;
            this.pnlSpacer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpacer1.Location = new System.Drawing.Point(0, 80);
            this.pnlSpacer1.Name = "pnlSpacer1";
            this.pnlSpacer1.Size = new System.Drawing.Size(600, 15);
            this.pnlSpacer1.TabIndex = 8;
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.BackColor = System.Drawing.Color.White;
            this.pnlMiddle.Controls.Add(this.btnDownload);
            this.pnlMiddle.Controls.Add(this.txtDownloadUrl);
            this.pnlMiddle.Controls.Add(this.lblUrlLabel);
            this.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMiddle.Location = new System.Drawing.Point(0, 95);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.pnlMiddle.Size = new System.Drawing.Size(600, 150);
            this.pnlMiddle.TabIndex = 6;
            // 
            // lblUrlLabel
            // 
            this.lblUrlLabel.AutoSize = true;
            this.lblUrlLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUrlLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.lblUrlLabel.Location = new System.Drawing.Point(10, 10);
            this.lblUrlLabel.Name = "lblUrlLabel";
            this.lblUrlLabel.Size = new System.Drawing.Size(143, 19);
            this.lblUrlLabel.TabIndex = 4;
            this.lblUrlLabel.Text = "Liên kết tải DISM++:";
            // 
            // txtDownloadUrl
            // 
            this.txtDownloadUrl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtDownloadUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDownloadUrl.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDownloadUrl.Location = new System.Drawing.Point(30, 35);
            this.txtDownloadUrl.Multiline = true;
            this.txtDownloadUrl.Name = "txtDownloadUrl";
            this.txtDownloadUrl.ReadOnly = true;
            this.txtDownloadUrl.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDownloadUrl.Size = new System.Drawing.Size(480, 50);
            this.txtDownloadUrl.TabIndex = 1;
            this.txtDownloadUrl.Text = "https://github.com/Chuyu-Team/Dism-Multi-language/releases/download/v10.1.1002.2/" +
    "Dism++10.1.1002.1B.zip";
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownload.FlatAppearance.BorderSize = 0;
            this.btnDownload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(158)))));
            this.btnDownload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(140)))), ((int)(((byte)(230)))));
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.Color.White;
            this.btnDownload.Location = new System.Drawing.Point(255, 95);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(90, 35);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "⬇ Tải xuống";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // pnlSpacer2
            // 
            this.pnlSpacer2.BackColor = System.Drawing.Color.White;
            this.pnlSpacer2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpacer2.Location = new System.Drawing.Point(0, 245);
            this.pnlSpacer2.Name = "pnlSpacer2";
            this.pnlSpacer2.Size = new System.Drawing.Size(600, 15);
            this.pnlSpacer2.TabIndex = 9;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBottom.Controls.Add(this.btnDonate);
            this.pnlBottom.Controls.Add(this.lblOSInfo);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 260);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlBottom.Size = new System.Drawing.Size(600, 60);
            this.pnlBottom.TabIndex = 7;
            // 
            // lblOSInfo
            // 
            this.lblOSInfo.AutoSize = true;
            this.lblOSInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOSInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.lblOSInfo.Location = new System.Drawing.Point(0, 5);
            this.lblOSInfo.Name = "lblOSInfo";
            this.lblOSInfo.Size = new System.Drawing.Size(200, 19);
            this.lblOSInfo.TabIndex = 3;
            this.lblOSInfo.Text = "💻 Windows 10/11 (64-bit)";
            // 
            // btnDonate
            // 
            this.btnDonate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDonate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.btnDonate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDonate.FlatAppearance.BorderSize = 0;
            this.btnDonate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnDonate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.btnDonate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDonate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDonate.ForeColor = System.Drawing.Color.White;
            this.btnDonate.Location = new System.Drawing.Point(465, 10);
            this.btnDonate.Name = "btnDonate";
            this.btnDonate.Size = new System.Drawing.Size(115, 35);
            this.btnDonate.TabIndex = 4;
            this.btnDonate.Text = "❤ Tặng quà";
            this.btnDonate.UseVisualStyleBackColor = false;
            this.btnDonate.Click += new System.EventHandler(this.btnDonate_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 320);
            this.Controls.Add(this.pnlSpacer2);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlMiddle);
            this.Controls.Add(this.pnlSpacer1);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Backup/Restore Driver Tool";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMiddle.ResumeLayout(false);
            this.pnlMiddle.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtDownloadUrl;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label lblOSInfo;
        private System.Windows.Forms.Button btnDonate;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlMiddle;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblUrlLabel;
        private System.Windows.Forms.Panel pnlSpacer1;
        private System.Windows.Forms.Panel pnlSpacer2;
    }
}
