/**  
* 标    题：   Util.cs 
* 描    述：   一些通用函数
* 创建时间：   2017年07月29日 02:53 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{

    /// <summary>
    /// 解析资源版本信息
    /// </summary>
    public static void AnalysisAssetsVersion(ref Dictionary<string, string> dic, string text)
    {
        string[] files = text.Split('\n');
        if (files == null || files.Length == 0) return;
        string file = string.Empty;
        string[] data = null;
        for(int i = 0; i < files.Length; i ++)
        {
            file = files[i];
            data = file.Split('|');
            if (data.Length < 2)
            {
                MessageBox.Error("资源版本信息解析失败！");
                continue;
            }
            if (!dic.ContainsKey(data[0])) dic[data[0]] = data[1];
            else MessageBox.Error(string.Format("重复资源版本信息{0}！！", data[0]));
        }
    }

    public static List<T> GetDicKeyList<T,K>(Dictionary<T,K> dic)
    {
        List<T> list = new List<T>();
        foreach(T k in dic.Keys)
        {
            list.Add(k);
        }
        if (list.Count == 0) return null;
        return list;
    }
}
