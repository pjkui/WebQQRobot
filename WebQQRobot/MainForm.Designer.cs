namespace WebQQRobot
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtEdit = new System.Windows.Forms.TextBox();
            this.plMain = new System.Windows.Forms.Panel();
            this.lstMessage = new System.Windows.Forms.ListBox();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.plMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEdit
            // 
            this.txtEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtEdit.Location = new System.Drawing.Point(0, 304);
            this.txtEdit.Multiline = true;
            this.txtEdit.Name = "txtEdit";
            this.txtEdit.Size = new System.Drawing.Size(668, 60);
            this.txtEdit.TabIndex = 1;
            // 
            // plMain
            // 
            this.plMain.Controls.Add(this.lstMessage);
            this.plMain.Controls.Add(this.txtInfo);
            this.plMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plMain.Location = new System.Drawing.Point(0, 0);
            this.plMain.Name = "plMain";
            this.plMain.Size = new System.Drawing.Size(668, 304);
            this.plMain.TabIndex = 2;
            // 
            // lstMessage
            // 
            this.lstMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMessage.FormattingEnabled = true;
            this.lstMessage.ItemHeight = 12;
            this.lstMessage.Location = new System.Drawing.Point(0, 0);
            this.lstMessage.Name = "lstMessage";
            this.lstMessage.Size = new System.Drawing.Size(433, 304);
            this.lstMessage.TabIndex = 3;
            // 
            // txtInfo
            // 
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtInfo.Location = new System.Drawing.Point(433, 0);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(235, 304);
            this.txtInfo.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 364);
            this.Controls.Add(this.plMain);
            this.Controls.Add(this.txtEdit);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QQ机器人";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.plMain.ResumeLayout(false);
            this.plMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEdit;
        private System.Windows.Forms.Panel plMain;
        private System.Windows.Forms.TextBox txtInfo;
        public System.Windows.Forms.ListBox lstMessage;

    }
}

