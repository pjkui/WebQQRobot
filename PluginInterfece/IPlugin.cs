using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public delegate void PostDelegate(string msg);

/// <summary>
/// 插件接口
/// </summary>
public interface IPlugin
{
    /// <summary>
    /// 获取插件信息
    /// </summary>
    /// <returns></returns>
    Dictionary<string, string> GetPluginInfo();

    /// <summary>
    /// 消息回调
    /// </summary>
    /// <param name="msg">消息内容</param>
    void MessageCallback(PostDelegate func, string[] msg);

}
