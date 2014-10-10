using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace WebQQRobot
{
    public partial class LoginForm : Form
    {
        private CookieContainer LoginCookie = new CookieContainer();
        public System.Collections.Hashtable CookieHash = new System.Collections.Hashtable();
        private string[] VerInfo;

        public LoginForm()
        {
            InitializeComponent();

            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            /*DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();*/

            if (txtVerCode.Visible)
            {
                string err = "";
                CookieCollection cookie = Core.Login(txtQQCode.Text, txtPassword.Text, txtVerCode.Text, VerInfo[2].Replace("'", ""), ref LoginCookie, ref err);
                // ptuiCB('4','0','','0','您输入的验证码不正确，请重新输入。', '1919192334');
                string t = err.Replace("ptuiCB(", "").Replace(";\r\n", "");
                string[] arr = t.Split(',');
                
                if(arr[0] != "'0'")
                {
                    if(arr[0] == "'4'")
                    {
                        pbCheckCode_Click(sender, e);
                    }
                    MessageBox.Show(arr[4].Replace("'", ""));
                }
                else
                {
                    foreach(Cookie c in cookie)
                    {
                        CookieHash.Add(c.Name, c.Value);
                    }

                    // SSL验证，更新cookie
                    CookieCollection pskyCookie = Core.GetSSL(arr[2].Replace("'", ""));
                    LoginCookie.Add(pskyCookie);

                    // 二次登录
                    if(Core.Login2(CookieHash, ref LoginCookie, ref cookie))
                    {
                        Core.QQData.Add("qq", txtQQCode.Text);
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                        return;
                    }
                    
                    MessageBox.Show("登录失败！");
                    lblInfo.Text = "";

                }
            }
            else
            {
                VerInfo = Core.VerifyQQ(txtQQCode.Text, ref LoginCookie);
                if (VerInfo[0] == "'1'")
                {
                    lblInfo.Text = "请输入验证码！";
                    lblInfo.Visible = true;
                    txtVerCode.Visible = true;
                    string Key = VerInfo[1].Replace("'", "");
                    pbCheckCode.Image = Core.GetVerImage(txtQQCode.Text, Key, ref LoginCookie);

                    txtVerCode.Focus();

                }
                else
                {
                    lblInfo.Text = "";
                    lblInfo.Visible = false;
                    txtVerCode.Visible = false;
                }
            }
            /*DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();*/
        }

        private void pbCheckCode_Click(object sender, EventArgs e)
        {
            pbCheckCode.Image = Core.RefurbishVerImage(txtQQCode.Text, ref LoginCookie);
            txtVerCode.Text = "";
            txtVerCode.Focus();
        }
    }
}
