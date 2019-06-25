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
            this.txtRecordCount = new System.Windows.Forms.TextBox();
            this.mTxtLastNum = new System.Windows.Forms.MaskedTextBox();
            this.lblRecCount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnOpenXml = new System.Windows.Forms.Button();
            this.chckMinimize = new System.Windows.Forms.CheckBox();
            this.lblXml = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.txtXml = new System.Windows.Forms.TextBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.lblConnectionPath = new System.Windows.Forms.Label();
            this.txtConnectionPath = new System.Windows.Forms.TextBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.lblUrl = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.lblLastNum = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUpdate = new System.Windows.Forms.Button();
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
            // txtRecordCount
            // 
            this.txtRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecordCount.Location = new System.Drawing.Point(167, 28);
            this.txtRecordCount.Name = "txtRecordCount";
            this.txtRecordCount.ReadOnly = true;
            this.txtRecordCount.Size = new System.Drawing.Size(341, 26);
            this.txtRecordCount.TabIndex = 6;
            // 
            // mTxtLastNum
            // 
            this.mTxtLastNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mTxtLastNum.Location = new System.Drawing.Point(167, 81);
            this.mTxtLastNum.Name = "mTxtLastNum";
            this.mTxtLastNum.ReadOnly = true;
            this.mTxtLastNum.Size = new System.Drawing.Size(341, 26);
            this.mTxtLastNum.TabIndex = 7;
            this.mTxtLastNum.ValidatingType = typeof(int);
            // 
            // lblRecCount
            // 
            this.lblRecCount.AutoSize = true;
            this.lblRecCount.Location = new System.Drawing.Point(29, 34);
            this.lblRecCount.Name = "lblRecCount";
            this.lblRecCount.Size = new System.Drawing.Size(108, 20);
            this.lblRecCount.TabIndex = 8;
            this.lblRecCount.Text = "Record Count";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BtnOpenXml);
            this.groupBox1.Controls.Add(this.chckMinimize);
            this.groupBox1.Controls.Add(this.lblXml);
            this.groupBox1.Controls.Add(this.lblFileName);
            this.groupBox1.Controls.Add(this.txtXml);
            this.groupBox1.Controls.Add(this.txtFileName);
            this.groupBox1.Controls.Add(this.lblFilePath);
            this.groupBox1.Controls.Add(this.txtFilePath);
            this.groupBox1.Controls.Add(this.lblConnectionPath);
            this.groupBox1.Controls.Add(this.txtConnectionPath);
            this.groupBox1.Controls.Add(this.lblMsg);
            this.groupBox1.Controls.Add(this.txtMsg);
            this.groupBox1.Controls.Add(this.lblUrl);
            this.groupBox1.Controls.Add(this.txtUrl);
            this.groupBox1.Controls.Add(this.lblLastNum);
            this.groupBox1.Controls.Add(this.lblRecCount);
            this.groupBox1.Controls.Add(this.txtRecordCount);
            this.groupBox1.Controls.Add(this.mTxtLastNum);
            this.groupBox1.Location = new System.Drawing.Point(35, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(818, 463);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File information";
            // 
            // BtnOpenXml
            // 
            this.BtnOpenXml.Location = new System.Drawing.Point(706, 379);
            this.BtnOpenXml.Name = "BtnOpenXml";
            this.BtnOpenXml.Size = new System.Drawing.Size(75, 34);
            this.BtnOpenXml.TabIndex = 22;
            this.BtnOpenXml.Text = "Open";
            this.BtnOpenXml.UseVisualStyleBackColor = true;
            this.BtnOpenXml.Click += new System.EventHandler(this.BtnOpenXml_Click);
            // 
            // chckMinimize
            // 
            this.chckMinimize.AutoSize = true;
            this.chckMinimize.Checked = true;
            this.chckMinimize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chckMinimize.Location = new System.Drawing.Point(167, 422);
            this.chckMinimize.Name = "chckMinimize";
            this.chckMinimize.Size = new System.Drawing.Size(172, 24);
            this.chckMinimize.TabIndex = 5;
            this.chckMinimize.Text = "Minimize on startup";
            this.chckMinimize.UseVisualStyleBackColor = true;
            // 
            // lblXml
            // 
            this.lblXml.AutoSize = true;
            this.lblXml.Location = new System.Drawing.Point(29, 379);
            this.lblXml.Name = "lblXml";
            this.lblXml.Size = new System.Drawing.Size(110, 20);
            this.lblXml.TabIndex = 21;
            this.lblXml.Text = "XML file name";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(29, 338);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(78, 20);
            this.lblFileName.TabIndex = 20;
            this.lblFileName.Text = "File name";
            // 
            // txtXml
            // 
            this.txtXml.Location = new System.Drawing.Point(167, 379);
            this.txtXml.Name = "txtXml";
            this.txtXml.Size = new System.Drawing.Size(533, 26);
            this.txtXml.TabIndex = 19;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(167, 332);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(533, 26);
            this.txtFileName.TabIndex = 18;
            this.txtFileName.Text = "mcb.txt";
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(29, 294);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(70, 20);
            this.lblFilePath.TabIndex = 17;
            this.lblFilePath.Text = "File path";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(167, 288);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(614, 26);
            this.txtFilePath.TabIndex = 16;
            this.txtFilePath.Text = "D:\\PointSoft Dn\\Probation Project\\";
            // 
            // lblConnectionPath
            // 
            this.lblConnectionPath.AutoSize = true;
            this.lblConnectionPath.Location = new System.Drawing.Point(29, 244);
            this.lblConnectionPath.Name = "lblConnectionPath";
            this.lblConnectionPath.Size = new System.Drawing.Size(126, 20);
            this.lblConnectionPath.TabIndex = 15;
            this.lblConnectionPath.Text = "Connection path";
            // 
            // txtConnectionPath
            // 
            this.txtConnectionPath.Location = new System.Drawing.Point(167, 238);
            this.txtConnectionPath.Name = "txtConnectionPath";
            this.txtConnectionPath.Size = new System.Drawing.Size(614, 26);
            this.txtConnectionPath.TabIndex = 14;
            this.txtConnectionPath.Text = "D:\\PointSoft Dn\\Probation Project\\20190321\\";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(29, 195);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(74, 20);
            this.lblMsg.TabIndex = 13;
            this.lblMsg.Text = "Message";
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(167, 185);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(614, 30);
            this.txtMsg.TabIndex = 12;
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
            this.txtUrl.Text = "www.pointsoft.com.my";
            // 
            // lblLastNum
            // 
            this.lblLastNum.AutoSize = true;
            this.lblLastNum.Location = new System.Drawing.Point(29, 87);
            this.lblLastNum.Name = "lblLastNum";
            this.lblLastNum.Size = new System.Drawing.Size(98, 20);
            this.lblLastNum.TabIndex = 9;
            this.lblLastNum.Text = "Last number";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkQRCode);
            this.groupBox2.Location = new System.Drawing.Point(35, 515);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(818, 80);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Print setup";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(94, 612);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(158, 41);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Update File Setting";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
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
            this.Controls.Add(this.btnUpdate);
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
        private System.Windows.Forms.TextBox txtRecordCount;
        private System.Windows.Forms.MaskedTextBox mTxtLastNum;
        private System.Windows.Forms.Label lblRecCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label lblLastNum;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button Button2;
        private System.IO.FileSystemWatcher FileSystemWatcher1;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.CheckBox chckMinimize;
        private System.Windows.Forms.Label lblXml;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.TextBox txtXml;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label lblConnectionPath;
        private System.Windows.Forms.TextBox txtConnectionPath;
        private System.Windows.Forms.Button BtnOpenXml;
    }
}

