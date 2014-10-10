using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Collections;
using System.Web;
using System.Globalization;
using WebQQRobot.DataModel;

namespace WebQQRobot
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
        }

        private delegate void MethodInvokerParam(QQMessage qqMsg);
        private void MainForm_Load(object sender, EventArgs e)
        {

            // 获取群列表
            Core.GetGroupList();

            foreach(QQGroupList.Gnamelist2 g in Core.GroupList.Result.Gnamelist)
            {
                txtInfo.AppendText(g.Name + " : gid:" + g.Gid + " code:" + g.Code + "\r\n");
            }

            // 心跳包
            Task T = new Task(new Action(() =>
            {
                while(true)
                {
                    string msg = Core.PostPoll();
                    try
                    {
                        if (msg.IndexOf("\"retcode\": 0,") == -1)
                        {
                            QQMessage qqMsg = Core.DeserializationStr(msg, typeof(QQMessage)) as QQMessage;
                            this.BeginInvoke(new MethodInvokerParam((QQMessage qqMessage) =>
                            {
                                // group_message
                                if (qqMessage.Result == null) return;
                                if (qqMessage.Result[0].PollType != "group_message") return;
                                object[] m = qqMessage.Result[0].Value.Content as object[];
                                string str = m[1].ToString();
                                Core.MassageAnalyze(qqMessage);
                                /*lstMessage.Items.Add(msg);*/
                            }), qqMsg);
                        }
                    }
                    catch(Exception er)
                    {
                        BeginInvoke(new MethodInvoker(() =>
                        {
                            MessageBox.Show(er.Message);
                        }));
                        
                    }

                    
                }
            }));
            T.Start();
        }

        private void lstMessage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtEdit.Text = lstMessage.Items[lstMessage.SelectedIndex].ToString();
            }
            catch(Exception err)
            {

            }
            
        }

    }
}
