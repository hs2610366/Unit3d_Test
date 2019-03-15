/**  
* 标    题：   EventMgr.cs 
* 描    述：    
* 创建时间：   2018年06月14日 02:05 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMgr
{
    public static readonly EventMgr Instance = new EventMgr();
    private Dictionary<string, Action<object[]>> Events = new Dictionary<string, Action<object[]>>();

    public void Trigger(string key, params object[] objs)
    {
        if(Events.ContainsKey(key))
        {
            Events[key](objs);
        }
        else
        {
            MessageBox.Error("EventMgr", string.Format("未找到类型【{0}】事件。", key));
        }
    }

    public void Add(string key, Action<object[]> func)
    {
        if(!Events.ContainsKey(key))
        {
            Action<object[]> callback = func;
            Events.Add(key, callback);
        }
        Events[key] += func;
    }

    public void Remove(string key, Action<object[]> func)
    {
        if (!Events.ContainsKey(key)) return;
        Events[key] -= func;
        Delegate[] list = Events[key].GetInvocationList();
        if (list == null || list.Length == 0) Events.Remove(key);
    }
}
