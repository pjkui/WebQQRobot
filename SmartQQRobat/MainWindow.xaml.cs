using CoreComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WebQQRobot.DataModel;

namespace SmartQQRobat
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private delegate void MethodInvokerParam(QQMessage qqMsg);

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> list = LoadPlugin();

            // 心跳包
            Task T = new Task(new Action(() =>
            {
                while (true)
                {
                    string msg = Core.PostPoll();
                    try
                    {
                        if (msg.IndexOf("\"retcode\": 0,") == -1)
                        {
                            QQMessage qqMsg = Core.DeserializationStr(msg, typeof(QQMessage)) as QQMessage;
                            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new MethodInvokerParam((QQMessage qqMessage) =>
                            {
                                if (qqMessage.Result == null) return;
                                // if (qqMessage.Result[0].PollType != "group_message") return;
                                object[] m = qqMessage.Result[0].Value.Content as object[];
                                // string str = m[1].ToString();
                                Core.MassageAnalyze(qqMessage);
                            }), qqMsg);
                        }
                    }
                    catch (Exception er)
                    {
                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                        {
                            MessageBox.Show(er.Message);
                        }));

                    }


                }
            }));
            T.Start();
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        private List<string> LoadPlugin()
        {
            List<string> list = new List<string>();
            string path = System.Environment.CurrentDirectory;
            path = System.IO.Path.Combine(path, "plugin");
            foreach(string file in System.IO.Directory.GetFiles(path, "*.dll"))
            {
                list.Add(file.Substring(file.LastIndexOf("\\") + 1));
                Assembly a = Assembly.LoadFrom(file);
                Type[] types = a.GetTypes();
                foreach(Type t in types)
                {
                    if (null != t.GetInterface("IPlugin"))
                    {
                        Core.PluginList.Add(t);
                    }
                }
            }

            return list;
        }
    }
}
