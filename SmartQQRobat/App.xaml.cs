using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace SmartQQRobat
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application.Current.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
            LoginForm Login = new LoginForm();
            if (Login.ShowDialog() == true)
            {
                Application.Current.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
                Login.Close();
            }
            else
            {
                this.Shutdown();
            }
        }
    }
}
