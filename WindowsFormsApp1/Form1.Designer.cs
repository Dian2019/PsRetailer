namespace WindowsFormsApp1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.NotifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.chkQRCode = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnOpenTextFile = new System.Windows.Forms.Button();
            this.BtnOpenXml = new System.Windows.Forms.Button();
            this.lblXml = new System.Windows.Forms.Label();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.txtXml = new System.Windows.Forms.TextBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.lblFileWatcher = new System.Windows.Forms.Label();
            this.txtFileWatcherFilter = new System.Windows.Forms.TextBox();
            this.lblConnectionPath = new System.Windows.Forms.Label();
            this.txtConnectionPath = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.FileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // NotifyIcon1
            // 
            this.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.NotifyIcon1.BalloonTipText = "Tip";
            this.NotifyIcon1.BalloonTipTitle = "Balloon Title";
            this.NotifyIcon1.Text = "NotifyIcon1";
            this.NotifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
            // 
            // chkQRCode
            // 
            this.chkQRCode.AutoSize = true;
            this.chkQRCode.Checked = true;
            this.chkQRCode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkQRCode.Location = new System.Drawing.Point(21, 34);
            this.chkQRCode.Name = "chkQRCode";
            this.chkQRCode.Size = new System.Drawing.Size(155, 24);
            this.chkQRCode.TabIndex = 4;
            this.chkQRCode.Text = "Enable QR Code";
            this.chkQRCode.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BtnOpenTextFile);
            this.groupBox1.Controls.Add(this.BtnOpenXml);
            this.groupBox1.Controls.Add(this.lblXml);
            this.groupBox1.Controls.Add(this.lblFilePath);
            this.groupBox1.Controls.Add(this.txtXml);
            this.groupBox1.Controls.Add(this.txtFilePath);
            this.groupBox1.Controls.Add(this.lblFileWatcher);
            this.groupBox1.Controls.Add(this.txtFileWatcherFilter);
            this.groupBox1.Controls.Add(this.lblConnectionPath);
            this.groupBox1.Controls.Add(this.txtConnectionPath);
            this.groupBox1.Controls.Add(this.lblUrl);
            this.groupBox1.Controls.Add(this.txtUrl);
            this.groupBox1.Location = new System.Drawing.Point(35, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(818, 408);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File information";
            // 
            // BtnOpenTextFile
            // 
            this.BtnOpenTextFile.Location = new System.Drawing.Point(706, 277);
            this.BtnOpenTextFile.Name = "BtnOpenTextFile";
            this.BtnOpenTextFile.Size = new System.Drawing.Size(75, 34);
            this.BtnOpenTextFile.TabIndex = 23;
            this.BtnOpenTextFile.Text = "Open";
            this.BtnOpenTextFile.UseVisualStyleBackColor = true;
            this.BtnOpenTextFile.Click += new System.EventHandler(this.BtnOpenTextFile_Click);
            // 
            // BtnOpenXml
            // 
            this.BtnOpenXml.Location = new System.Drawing.Point(706, 325);
            this.BtnOpenXml.Name = "BtnOpenXml";
            this.BtnOpenXml.Size = new System.Drawing.Size(75, 34);
            this.BtnOpenXml.TabIndex = 22;
            this.BtnOpenXml.Text = "Open";
            this.BtnOpenXml.UseVisualStyleBackColor = true;
            this.BtnOpenXml.Click += new System.EventHandler(this.BtnOpenXml_Click);
            // 
            // lblXml
            // 
            this.lblXml.AutoSize = true;
            this.lblXml.Location = new System.Drawing.Point(29, 325);
            this.lblXml.Name = "lblXml";
            this.lblXml.Size = new System.Drawing.Size(110, 20);
            this.lblXml.TabIndex = 21;
            this.lblXml.Text = "XML file name";
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(29, 284);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(70, 20);
            this.lblFilePath.TabIndex = 20;
            this.lblFilePath.Text = "File path";
            // 
            // txtXml
            // 
            this.txtXml.Location = new System.Drawing.Point(167, 325);
            this.txtXml.Name = "txtXml";
            this.txtXml.Size = new System.Drawing.Size(533, 26);
            this.txtXml.TabIndex = 19;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(167, 278);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(533, 26);
            this.txtFilePath.TabIndex = 18;
            // 
            // lblFileWatcher
            // 
            this.lblFileWatcher.AutoSize = true;
            this.lblFileWatcher.Location = new System.Drawing.Point(29, 240);
            this.lblFileWatcher.Name = "lblFileWatcher";
            this.lblFileWatcher.Size = new System.Drawing.Size(103, 20);
            this.lblFileWatcher.TabIndex = 17;
            this.lblFileWatcher.Text = "Watcher filter";
            // 
            // txtFileWatcherFilter
            // 
            this.txtFileWatcherFilter.Location = new System.Drawing.Point(167, 234);
            this.txtFileWatcherFilter.Name = "txtFileWatcherFilter";
            this.txtFileWatcherFilter.Size = new System.Drawing.Size(614, 26);
            this.txtFileWatcherFilter.TabIndex = 16;
            // 
            // lblConnectionPath
            // 
            this.lblConnectionPath.AutoSize = true;
            this.lblConnectionPath.Location = new System.Drawing.Point(29, 190);
            this.lblConnectionPath.Name = "lblConnectionPath";
            this.lblConnectionPath.Size = new System.Drawing.Size(126, 20);
            this.lblConnectionPath.TabIndex = 15;
            this.lblConnectionPath.Text = "Connection path";
            // 
            // txtConnectionPath
            // 
            this.txtConnectionPath.Location = new System.Drawing.Point(167, 184);
            this.txtConnectionPath.Name = "txtConnectionPath";
            this.txtConnectionPath.Size = new System.Drawing.Size(614, 26);
            this.txtConnectionPath.TabIndex = 14;
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(29, 140);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(42, 20);
            this.lblUrl.TabIndex = 11;
            this.lblUrl.Text = "URL";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(167, 134);
            this.txtUrl.Multiline = true;
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(614, 26);
            this.txtUrl.TabIndex = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkQRCode);
            this.groupBox2.Location = new System.Drawing.Point(35, 497);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(818, 80);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Print setup";
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Location = new System.Drawing.Point(94, 612);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(158, 41);
            this.BtnUpdate.TabIndex = 11;
            this.BtnUpdate.Text = "Update File Setting";
            this.BtnUpdate.UseVisualStyleBackColor = true;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(575, 612);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(241, 41);
            this.Button2.TabIndex = 12;
            this.Button2.Text = "Manual Create Record";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // FileSystemWatcher1
            // 
            this.FileSystemWatcher1.EnableRaisingEvents = true;
            this.FileSystemWatcher1.SynchronizingObject = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 685);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.BtnUpdate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(932, 681);
            this.Name = "Form1";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon NotifyIcon1;
        private System.Windows.Forms.CheckBox chkQRCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button BtnUpdate;
        private System.Windows.Forms.Button Button2;
        private System.IO.FileSystemWatcher FileSystemWatcher1;
        private System.Windows.Forms.Label lblXml;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.TextBox txtXml;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label lblFileWatcher;
        private System.Windows.Forms.TextBox txtFileWatcherFilter;
        private System.Windows.Forms.Label lblConnectionPath;
        private System.Windows.Forms.TextBox txtConnectionPath;
        private System.Windows.Forms.Button BtnOpenXml;
        private System.Windows.Forms.Button BtnOpenTextFile;
    }
}

