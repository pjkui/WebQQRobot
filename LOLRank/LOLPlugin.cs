using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOLRank
{
    public class LOLPlugin : IPlugin
    {
        public Dictionary<string, string> GetPluginInfo()
        {
            Dictionary<string, string> info = new Dictionary<string, string>();
            info.Add("Name", "LOL助手");
            return info;
        }

        /// <summary>
        /// 消息回调
        /// </summary>
        /// <param name="func">发送消息的委托</param>
        /// <param name="msg">字符串数组， msg[0]消息类型， msg[1]消息内容</param>
        public void MessageCallback(PostDelegate func, string[] msg)
        {
            func(msg[1]);
        }

    }
}
