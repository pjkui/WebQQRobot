using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using WebQQRobot.DataModel;
using System.Drawing;
using SKCommonLib.Security;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.JScript;
using System.Threading.Tasks;
using System.Threading;

namespace CoreComponent
{
    public class Core
    {
        public static CookieContainer QQCookie = new CookieContainer();
        public static Hashtable QQData = new Hashtable();
        public static QQGroupList GroupList = new QQGroupList();
        public static List<Type> PluginList = new List<Type>();

        /// <summary>
        /// 获取验证信息
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="password"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static string[] VerifyQQ(string qq, ref CookieContainer cookie)
        {
            HttpWebRequest request = HttpWebRequest.Create("https://ssl.ptlogin2.qq.com/check?uin=" + qq + "&appid=1003903&r=0.5534069863735969") as HttpWebRequest;
            request.Method = "GET";
            request.CookieContainer = cookie;

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string str = sr.ReadToEnd();
            sr.Close();
            response.Close();

            str = str.Replace("ptui_checkVC(", "").Replace(");", "");
            return str.Split(',');
        }

        /// <summary>
        /// 获取验证码流
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="vercode"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static Image GetVerImage(string qq, string vercode, ref CookieContainer cookie)
        {
            string key = vercode;
            HttpWebRequest request = HttpWebRequest.Create("http://captcha.qq.com/getimage?aid=1003903&r=0.9701901362277567&uin=" + qq + "&vc_type=" + vercode) as HttpWebRequest;
            request.CookieContainer = cookie;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(response.GetResponseStream());

            Bitmap VerImage = new Bitmap(sr.BaseStream);

            sr.Close();
            response.Close();

            return VerImage;
        }

        public static Image RefurbishVerImage(string qq, ref CookieContainer cookie)
        {
            HttpWebRequest request = HttpWebRequest.Create("http://captcha.qq.com/getimage?&uin=" + qq + "&aid=1003903&" + (new Random()).Next(100, 999) + "&cap_cd=true") as HttpWebRequest;
            request.CookieContainer = cookie;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(response.GetResponseStream());
            Bitmap VerImage = new Bitmap(sr.BaseStream);
            sr.Close();
            response.Close();

            return VerImage;
        }

        public static byte[] ToBytes(string str)
        {
            var bytes = new byte[8];
            for (var i = 0; i < 8; i++)
            {
                bytes[i] = byte.Parse(str.Substring((i * 4) + 2, 2), System.Globalization.NumberStyles.HexNumber);
            }
            return bytes;
        }

        public static byte[] JoinBytes(byte[] b1, byte[] b2)
        {
            var b3 = new byte[b1.Length + b2.Length];
            Array.Copy(b1, b3, b1.Length);
            Array.Copy(b2, 0, b3, 16, b2.Length);
            return b3;
        }

        public static string ByteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        public static byte[] HexChar2Bin(string p)
        {
            ArrayList arr = new ArrayList();
            for (int i = 0; i < p.Length; i = i + 2)
            {
                arr.Add(System.Convert.ToByte(p.Substring(i, 2), 16));
            }

            return arr.ToArray(typeof(byte)) as byte[];
        }

        public static string GetSubmitPassword(string pwd, string veriCode, string QQHex)
        {
            string m = pwd;
            byte[] j = HexChar2Bin(SKMD5.MD5(m));
            string h = SKMD5.MD5(JoinBytes(j, ToBytes(QQHex)));
            return SKMD5.MD5(h + veriCode.ToUpper());
        }

        public static CookieCollection Login(string qq, string pwd, string veriCode, string QQHex, ref CookieContainer loginCookie, ref string err)
        {
            string p = GetSubmitPassword(pwd, veriCode, QQHex);
            HttpWebRequest request = HttpWebRequest.Create("https://ssl.ptlogin2.qq.com/login?u=" + qq + "&p=" + p + "&verifycode=" + veriCode + "&webqq_type=10&remember_uin=1&login2qq=1&aid=1003903&u1=http%3A%2F%2Fweb2.qq.com%2Floginproxy.html%3Flogin2qq%3D1%26webqq_type%3D10&h=1&ptredirect=0&ptlang=2052&daid=164&from_ui=1&pttype=1&dumy=&fp=loginerroralert&action=3-60-105181&mibao_css=m_webqq&t=1&g=1&js_type=0&js_ver=10060&login_sig=KEbQayQQ43IwVBEsCKg12ja-YmRWXDms*fSzyURyn45DJz4d7D18KdijUufRz4re") as HttpWebRequest;
            request.CookieContainer = loginCookie;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string str = sr.ReadToEnd();
            sr.Close();
            response.Close();

            foreach (Cookie c in response.Cookies)
            {
                if (c.Name == "ptwebqq")
                {
                    QQData.Add(c.Name, c.Value);
                }
                else if (c.Name == "skey")
                {
                    QQData.Add(c.Name, c.Value);
                }
            }

            Match m = Regex.Match(str, "'登录成功！', '(.*?)'");
            if (m.Groups.Count > 0)
            {
                if (m.Groups[0].Value.ToString() != "")
                {
                    string name = m.Groups[0].Value.Remove(0, 10);
                    name = name.Substring(0, name.Length - 1);
                    QQData["qq_name"] = name;
                }
            }

            err = str;
            return response.Cookies;
        }

        public static CookieCollection GetSSL(string url)
        {
            CookieContainer checkCookie = new CookieContainer();
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.CookieContainer = checkCookie;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string str = sr.ReadToEnd();
            sr.Close();
            response.Close();

            return response.Cookies;
        }
        public static bool Login2(System.Collections.Hashtable hash, ref CookieContainer cookie1, ref CookieCollection cookie2)
        {
            HttpWebRequest request = HttpWebRequest.Create("http://d.web2.qq.com/channel/login2") as HttpWebRequest;
            request.Referer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET CLR 1.1.4322; .NET4.0C; .NET4.0E)";
            request.Accept = "*/*";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8"; ;
            request.CookieContainer = cookie1;
            request.Method = "POST";

            Random rand = new Random();
            string id = rand.Next(20000000, 99999999).ToString();

            // status=登录状态，在线=online，Q我吧=callme，离开=away，忙碌=busy，勿扰=silent，隐身=hidden，离线=offline
            string data = "r=" + System.Web.HttpUtility.UrlEncode("{\"status\":\"online\","
                + "\"ptwebqq\":\"" + hash["ptwebqq"].ToString() + "\","
                + "\"passwd_sig\":\"\","
                + "\"clientid\":\"" + id + "\","
                + "\"psessionid\":null}") + "&"
                + "clientid=" + id + "&psessionid=null";

            byte[] buff = Encoding.UTF8.GetBytes(data);
            request.ContentLength = buff.Length;
            Stream s = request.GetRequestStream(); ;
            s.Write(buff, 0, buff.Length);
            s.Close();

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string str = sr.ReadToEnd();
            sr.Close();
            response.Close();

            if (str.IndexOf("\"retcode\":0") == -1)
            {
                return false;
            }

            // 查找uin
            string mUin = Regex.Match(str, "\"uin\":(.*?),\"").Groups[0].Value.Replace("\"uin\":", "").Replace(",\"", "");

            // 查找vfwebqq
            string vfwebqq = Regex.Match(str, "\"vfwebqq\":\"(.*?)\"").Groups[0].Value.Replace("\"vfwebqq\":\"", "").Replace("\"", "");

            // 查找psessionid
            string psessionid = Regex.Match(str, "\"psessionid\":\"(.*?)\"").Groups[0].Value.Replace("\"psessionid\":\"", "").Replace("\"", "");



            QQData.Add("uin", mUin);
            QQData.Add("vfwebqq", vfwebqq);
            QQData.Add("psessionid", psessionid);
            QQData.Add("clientid", id);

            QQCookie = cookie1;

            return true;
        }

        public static string PostPoll()
        {
            string psessionid = QQData["psessionid"].ToString();
            string id = QQData["clientid"].ToString();

            string url = "http://d.web2.qq.com/channel/poll2";
            string data = "r=%7B%22clientid%22%3A%22" + id + "%22%2C%22psessionid%22%3A%22" + psessionid + "%22%2C%22key%22%3A0%2C%22ids%22%3A%5B%5D%7D&clientid=" + id + "&psessionid=" + psessionid;

            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Referer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=3";
            request.CookieContainer = QQCookie;
            request.Method = "POST";
            request.Accept = "*/*";
            request.KeepAlive = true;
            request.Timeout = 14 * 24 * 60 * 60 * 1000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Host = "d.web2.qq.com";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36";

            byte[] buff = Encoding.UTF8.GetBytes(data);
            request.ContentLength = buff.Length;
            Stream s = request.GetRequestStream(); ;
            s.Write(buff, 0, buff.Length);
            s.Close();

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string str = sr.ReadToEnd();
            sr.Close();
            response.Close();

            return str;
        }

        /// <summary>
        /// 反序列化QQ消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static object DeserializationStr(string msg, Type t)
        {
            return JsonConvert.DeserializeObject(msg, t);
        }

        public static void GetGroupList()
        {
            string psessionid = QQData["psessionid"].ToString();

            string url = "http://s.web2.qq.com/api/get_group_name_list_mask2";
            // QQData["vfwebqq"]
            string data = "r=%7B%22hash%22%3A%22" + getHash() + "%22%2C%20%22vfwebqq%22%3A%22" + QQData["vfwebqq"] + "%22%7D";

            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Referer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=3";
            request.CookieContainer = QQCookie;
            request.Method = "POST";
            request.Accept = "*/*";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Host = "s.web2.qq.com";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36";

            byte[] buff = Encoding.UTF8.GetBytes(data);
            request.ContentLength = buff.Length;
            Stream s = request.GetRequestStream(); ;
            s.Write(buff, 0, buff.Length);
            s.Close();

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string str = sr.ReadToEnd();
            sr.Close();
            response.Close();

            GroupList = DeserializationStr(str, typeof(QQGroupList)) as QQGroupList;
        }

        public static string getHash()
        {
            HttpWebRequest request = HttpWebRequest.Create("http://0.web.qstatic.com/webqqpic/pubapps/0/50/eqq.all.js") as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string funStr = sr.ReadToEnd();
            funStr.Clone();
            response.Close();

            Match m = Regex.Match(funStr, @"P=function\(b,j\)([\s\S]*)return i},b");
            funStr = m.Groups[0].Value;
            funStr = funStr.Replace(@"P=function(b,j){", "").Replace("},b", ";");

            // 动态编译从腾讯下载的js
            CodeDomProvider JSProvider = new JScriptCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateInMemory = true;     // 生成到内存中

            funStr = "package QQHash { public class JScript { "
                    + "public static function getHash(b, j){ " + funStr + "}}}";

            // 编译
            CompilerResults results = JSProvider.CompileAssemblyFromSource(parameters, funStr);

            // 使用反射调用js函数
            Assembly assembly = results.CompiledAssembly;
            Type _evaluateType = assembly.GetType("QQHash.JScript");

            object[] param = new object[] { QQData["qq"], QQData["ptwebqq"] };
            object result = _evaluateType.InvokeMember("getHash", BindingFlags.InvokeMethod,
                        null, null, param);

            JSProvider.Dispose();

            return result.ToString();
        }

        /// <summary>
        /// 发送群消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="gid"></param>
        /// <param name="msgId"></param>
        public static void QQGroupSendMsg(string msg, string gid, string msgId)
        {
            // 异步执行
            Task t = new Task(new Action(() =>
            {
                Thread.Sleep(500);
                string psessionid = QQData["psessionid"].ToString();

                // group_id 1, msg 2, msg_id 3, clientid 4, psessionid 5
                string url = "http://d.web2.qq.com/channel/send_qun_msg2";

                string data = "r=%7B%22group_uin%22%3A" + gid + "%2C%22content%22%3A%22%5B%5C%22" + System.Web.HttpUtility.UrlEncode(msg) + "%5C%22%2C%5C%22%5C%22%2C%5B%5C%22font%5C%22%2C%7B%5C%22name%5C%22%3A%5C%22%E5%AE%8B%E4%BD%93%5C%22%2C%5C%22size%5C%22%3A%5C%2210%5C%22%2C%5C%22style%5C%22%3A%5B0%2C0%2C0%5D%2C%5C%22color%5C%22%3A%5C%22000000%5C%22%7D%5D%5D%22%2C%22msg_id%22%3A" + msgId + "%2C%22clientid%22%3A%22" + QQData["clientid"].ToString() + "%22%2C%22psessionid%22%3A%22" + psessionid + "%22%7D&clientid=" + QQData["clientid"].ToString() + "&psessionid=" + psessionid;

                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Referer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=3";
                request.CookieContainer = QQCookie;
                request.Method = "POST";
                request.Accept = "*/*";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Host = "d.web2.qq.com";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36";

                byte[] buff = Encoding.UTF8.GetBytes(data);
                request.ContentLength = buff.Length;
                Stream s = request.GetRequestStream();
                s.Write(buff, 0, buff.Length);
                s.Close();

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                StreamReader sr = new StreamReader(response.GetResponseStream());
                string str = sr.ReadToEnd();
                sr.Close();
                response.Close();
            }));
            t.Start();
        }

        /// <summary>
        /// 消息分类
        /// </summary>
        /// <param name="msg"></param>
        public static void MassageAnalyze(QQMessage msg)
        {
            foreach (QQMessage.Result2 r in msg.Result)
            {
                switch (r.PollType)
                {
                    case "group_message":
                        {
                            object[] m = r.Value.Content as object[];
                            string str = m[1].ToString();
                           
                        }
                        break;
                    case "message":
                        {
                            object[] m = r.Value.Content as object[];
                            string str = m[1].ToString();
                            string[] s = new string[2];
                            s[0] = "message";
                            s[1] = str;
                            PostPlugin(s);
                        }
                        break;
                    default: break;
                }
            }
        }

        private static void PostMessage(string msg)
        {
            StreamWriter sw = new StreamWriter("info.txt");
            sw.WriteLine(msg);
            sw.Close();
        }

        /// <summary>
        /// 向所有插件发送消息
        /// </summary>
        private static void PostPlugin(string[] msg)
        {
            PostDelegate pd = new PostDelegate(PostMessage);
            foreach(Type t in PluginList)
            {
                // 为防止插件卡死，采用异步传递消息的形式
                Task T = new Task(new Action(() =>
                {
                    IPlugin p = (IPlugin)Activator.CreateInstance(t);
                    p.MessageCallback(pd, msg);
                }));
                T.Start();
            }
        }
    }
}
