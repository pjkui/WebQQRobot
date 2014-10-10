using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKCommonLib.SKText
{
    // 字符串处理类
    // 提供各种字符串转换、排序、搜索等算法
    public static class SKText
    {
        /// <summary>
        /// 十六进制转字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 字符串转十六进制
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static byte[] HexChar2Bin(string p)
        {
            ArrayList arr = new ArrayList();
            for (int i = 0; i < p.Length; i = i + 2)
            {
                arr.Add(Convert.ToByte(p.Substring(i, 2), 16));
            }

            return arr.ToArray(typeof(byte)) as byte[];
        }
    }
}
