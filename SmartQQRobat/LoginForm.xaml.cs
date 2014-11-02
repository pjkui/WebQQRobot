using CoreComponent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartQQRobat
{
    /// <summary>
    /// LoginForm.xaml 的交互逻辑
    /// </summary>
    public partial class LoginForm : Window
    {
        private CookieContainer LoginCookie = new CookieContainer();
        public System.Collections.Hashtable CookieHash = new System.Collections.Hashtable();
        private string[] VerInfo;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtVerCode.Visibility == System.Windows.Visibility.Visible)
            {
                string err = "";
                CookieCollection cookie = Core.Login(txtQQCode.Text, txtPassword.Password, txtVerCode.Text, VerInfo[2].Replace("'", ""), ref LoginCookie, ref err);
                // ptuiCB('4','0','','0','您输入的验证码不正确，请重新输入。', '1919192334');
                string t = err.Replace("ptuiCB(", "").Replace(";\r\n", "");
                string[] arr = t.Split(',');

                if (arr[0] != "'0'")
                {
                    if (arr[0] == "'4'")
                    {
                        pbCheckCode_MouseLeftButtonDown(sender, null);
                    }
                    MessageBox.Show(arr[4].Replace("'", ""));
                }
                else
                {
                    foreach (Cookie c in cookie)
                    {
                        CookieHash.Add(c.Name, c.Value);
                    }

                    // SSL验证，更新cookie
                    CookieCollection pskyCookie = Core.GetSSL(arr[2].Replace("'", ""));
                    LoginCookie.Add(pskyCookie);

                    // 二次登录
                    if (Core.Login2(CookieHash, ref LoginCookie, ref cookie))
                    {
                        Core.QQData.Add("qq", txtQQCode.Text);
                        DialogResult = true;
                        this.Close();
                        return;
                    }

                    MessageBox.Show("登录失败！");
                    lblInfo.Content = "";
                }
            }
            else
            {
                VerInfo = Core.VerifyQQ(txtQQCode.Text, ref LoginCookie);
                if (VerInfo[0] == "'1'")
                {
                    lblInfo.Content = "请输入验证码！";
                    lblInfo.Visibility = System.Windows.Visibility.Visible;
                    txtVerCode.Visibility = System.Windows.Visibility.Visible;
                    string Key = VerInfo[1].Replace("'", "");
                    MemoryStream ms = new MemoryStream();
                    System.Drawing.Image img = Core.GetVerImage(txtQQCode.Text, Key, ref LoginCookie);
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    BitmapImage bit = new BitmapImage();
                    bit.BeginInit();
                    bit.StreamSource = new MemoryStream(ms.ToArray());
                    bit.EndInit();
                    pbCheckCode.Source = bit;
                    ms.Close();

                    txtVerCode.Focus();

                }
                else
                {
                    lblInfo.Content = "";
                    lblInfo.Visibility = System.Windows.Visibility.Hidden;
                    txtVerCode.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void pbCheckCode_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            System.Drawing.Image img = Core.RefurbishVerImage(txtQQCode.Text, ref LoginCookie);
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bit = new BitmapImage();
            bit.BeginInit();
            bit.StreamSource = new MemoryStream(ms.ToArray());
            bit.EndInit();
            pbCheckCode.Source = bit;
            txtVerCode.Text = "";
            txtVerCode.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnLogin_Click(sender, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // SQLHelper.ExecSql("INSERT INTO question_answer(question, answer, insert_datetime) VALUES('女仆', '主人~人家在呢', '" + DateTime.Now + "')");
        }
    }
}
