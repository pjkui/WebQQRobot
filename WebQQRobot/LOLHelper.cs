using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace WebQQRobot
{
    public static class LOLHelper
    {

        public static string GetRank(string id, string region)
        {

            HttpWebRequest request = HttpWebRequest.Create("http://www.lolhelper.cn/rank/rank.php") as HttpWebRequest;
            request.Method = "POST";
            request.Host = "www.lolhelper.cn";
            request.Referer = "http://www.lolhelper.cn/rank/";
            request.UserAgent = @"Mozilla/5.0 (Windows NT 6.3; WOW64; rv:32.0) Gecko/20100101 Firefox/32.0";
            request.Accept = @"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.ContentType = "application/x-www-form-urlencoded";
            request.KeepAlive = true;

            string reg = getRegion(region);
            if(string.IsNullOrEmpty(reg))
            {
                return "";
            }

            string name = System.Web.HttpUtility.UrlEncode(id);
            string section = System.Web.HttpUtility.UrlEncode(reg);
            string postData = "daqu=" + section;
            postData += "&nickname=" + name;

            byte[] buff = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = buff.Length;
            Stream writeBuf = request.GetRequestStream();
            writeBuf.Write(buff, 0, buff.Length);
            writeBuf.Close();

            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string str = sr.ReadToEnd();
            sr.Close();
            response.Close();

            string duanwei = getField(str, "段位");
            string rank = getField(str, "隐藏分");
            string win = getField(str, "排位胜场");
            string winrate = getField(str, "排位胜率");
            string login = getField(str, "最近登录");

            string msg = string.Format(@"段位：{0}\\n隐藏分：{1}\\n排位胜场：{2}\\n排位胜率：{3}\\n最近登录：{4}", duanwei, rank, win, winrate, login);
            return msg;
        }

        private static string getRegion(string region)
        {
            string reg = "";
            switch (region)
            {
                case "电1":
                    reg = "电信一";
                    break;
                case "电2":
                    reg = "电信二";
                    break;
                case "电3":
                    reg = "电信三";
                    break;
                case "电4":
                    reg = "电信四";
                    break;
                case "电5":
                    reg = "电信五";
                    break;
                case "电6":
                    reg = "电信六";
                    break;
                case "电7":
                    reg = "电信七";
                    break;
                case "电8":
                    reg = "电信八";
                    break;
                case "电9":
                    reg = "电信九";
                    break;
            }

            if(!string.IsNullOrEmpty(reg))
            {
                return reg;
            }

            switch (region)
            {
                case "电10":
                    reg = "电信十";
                    break;
                case "电11":
                    reg = "电信十一";
                    break;
                case "电12":
                    reg = "电信十二";
                    break;
                case "电13":
                    reg = "电信十三";
                    break;
                case "电14":
                    reg = "电信十四";
                    break;
                case "电15":
                    reg = "电信十五";
                    break;
                case "电16":
                    reg = "电信十六";
                    break;
                case "电17":
                    reg = "电信十七";
                    break;
                case "电18":
                    reg = "电信十八";
                    break;
                case "电19":
                    reg = "电信十九";
                    break;
                case "网1":
                    reg = "网通一";
                    break;
                case "网2":
                    reg = "网通二";
                    break;
                case "网3":
                    reg = "网通三";
                    break;
                case "网4":
                    reg = "网通四";
                    break;
                case "网5":
                    reg = "网通五";
                    break;
                case "网6":
                    reg = "网通六";
                    break;
                case "教1":
                    reg = "教育一";
                    break;
            }

            return reg;
        }

        /// <summary>
        /// 根据名称匹配每个值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string getField(string str, string name)
        {
            string value = Regex.Match(str, @"<td>" + name + @"</td>([\s\S]{0,24})</td>").Groups[0].Value;
            return Regex.Replace(value, @"(<td>" + name + @"</td>([\s\S]{0,20})<td>)|(</td>)", "");
        }
    }
}
