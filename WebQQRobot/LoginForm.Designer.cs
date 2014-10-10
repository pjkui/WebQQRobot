namespace WebQQRobot
{
    partial class LoginForm
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
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtQQCode = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.pbCheckCode = new System.Windows.Forms.PictureBox();
            this.cbbRemember = new System.Windows.Forms.CheckBox();
            this.txtVerCode = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheckCode)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(154, 213);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtQQCode
            // 
            this.txtQQCode.Location = new System.Drawing.Point(154, 101);
            this.txtQQCode.Name = "txtQQCode";
            this.txtQQCode.Size = new System.Drawing.Size(163, 21);
            this.txtQQCode.TabIndex = 1;
            this.txtQQCode.Text = "1919192334";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(154, 128);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(163, 21);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Text = "wsc.blue6358";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "QQ号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "密码：";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(242, 213);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // pbCheckCode
            // 
            this.pbCheckCode.Location = new System.Drawing.Point(323, 101);
            this.pbCheckCode.Name = "pbCheckCode";
            this.pbCheckCode.Size = new System.Drawing.Size(100, 50);
            this.pbCheckCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCheckCode.TabIndex = 6;
            this.pbCheckCode.TabStop = false;
            this.pbCheckCode.Click += new System.EventHandler(this.pbCheckCode_Click);
            // 
            // cbbRemember
            // 
            this.cbbRemember.AutoSize = true;
            this.cbbRemember.Checked = true;
            this.cbbRemember.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbbRemember.Location = new System.Drawing.Point(245, 157);
            this.cbbRemember.Name = "cbbRemember";
            this.cbbRemember.Size = new System.Drawing.Size(72, 16);
            this.cbbRemember.TabIndex = 7;
            this.cbbRemember.Text = "记住密码";
            this.cbbRemember.UseVisualStyleBackColor = true;
            // 
            // txtVerCode
            // 
            this.txtVerCode.Location = new System.Drawing.Point(154, 152);
            this.txtVerCode.MaxLength = 4;
            this.txtVerCode.Name = "txtVerCode";
            this.txtVerCode.Size = new System.Drawing.Size(85, 21);
            this.txtVerCode.TabIndex = 8;
            this.txtVerCode.Visible = false;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.ForeColor = System.Drawing.Color.Red;
            this.lblInfo.Location = new System.Drawing.Point(152, 187);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(54, 12);
            this.lblInfo.TabIndex = 9;
            this.lblInfo.Text = "lblInfo";
            this.lblInfo.Visible = false;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 248);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtVerCode);
            this.Controls.Add(this.cbbRemember);
            this.Controls.Add(this.pbCheckCode);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtQQCode);
            this.Controls.Add(this.btnLogin);
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            ((System.ComponentModel.ISupportInitialize)(this.pbCheckCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtQQCode;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox pbCheckCode;
        private System.Windows.Forms.CheckBox cbbRemember;
        private System.Windows.Forms.TextBox txtVerCode;
        private System.Windows.Forms.Label lblInfo;
    }
}