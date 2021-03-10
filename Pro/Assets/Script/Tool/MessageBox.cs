/**  
* 标    题：   MessageBox.cs 
* 描    述：   消息盒子
* 创建时间：   2017年03月11日 03:50 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBox
{
    public enum LogType
    {
        Log_Message,
        Warning_Message,
        Error_Message
    }

    /// <summary>
    /// 输出log
    /// </summary>
    /// <param name="title">标签</param>
    /// <param name="value">内容</param>
    public static void Log(string value, string title = null)
    {
        OutputLog(value, title, LogType.Log_Message);
    }

    /// <summary>
    /// 输出warning
    /// </summary>
    /// <param name="title">标签</param>
    /// <param name="value">内容</param>
    public static void Warning(string value, string title = null)
    {
        OutputLog(value, title, LogType.Warning_Message);
    }

    /// <summary>
    /// 输出warning
    /// </summary>
    /// <param name="title">标签</param>
    /// <param name="value">内容</param>
    public static void Error(string value, string title = null)
    {
        OutputLog(value, title, LogType.Error_Message);
    }

    private static void OutputLog(string value, string title, LogType type)
    {
        string message = string.Empty;
        if(string.IsNullOrEmpty(title))
        {
            message = value;
        }
        else
        {
            message = title + ": " + value;
        }
        switch (type)
        {
            case LogType.Log_Message:
                Debug.Log(message);
                break;
            case LogType.Warning_Message:
                Debug.LogWarning(message);
                break;
            case LogType.Error_Message:
                Debug.LogError(message);
                break;
        }
    }
}
