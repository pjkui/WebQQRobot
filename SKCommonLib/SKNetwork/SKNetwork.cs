using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SKCommonLib
{
    // 网络操作类
    public class SKNetwork
    {
        private string PostRequest(string url)
        {
            string result = "";
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                using(WebResponse response = request.GetResponse())
                {
                    using(StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        result = sr.ReadToEnd();
                    }
                }
                return result;
            }
            catch(Exception except)
            {
                result = except.Message;
                return result;
            }
        }
    }
}
