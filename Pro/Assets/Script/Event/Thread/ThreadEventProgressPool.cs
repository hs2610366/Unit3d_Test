/**  
* 标    题：   TreadProgressEventPool.cs 
* 描    述：    
* 创建时间：   2021年03月09日 15:43 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class ThreadEventProgressPool : EventProgressPool<ThreadEventProgressPool>
    {

        public EventProgress.EventUpdate ProgressUpdate;


        /// <summary>
        /// 线程调用 示例
        /// </summary>
        public void UpdateEventProgress(int id, bool finish, int cur, int total, string content, string error = "")
        {
            if (!string.IsNullOrEmpty(error)) Debug.LogError(error);
            //Debug.LogError(";;;;;; " + cur.ToString() + "/" + total.ToString());
            ThreadEventProgress progress = ThreadEventProgressPool.Instance.Get<ThreadEventProgress>();
            progress.UpdateData(id, finish, cur, total, content, error);
            progress.Update += ProgressUpdate;
            ThreadEventProgressPool.Instance.SetProgress(progress);
            EventData eData = ThreadEventProgressPool.Instance.GetEventData();
            ThreadDispatch.PushEvent(eData);
            ThreadEventProgressPool.Instance.SetEventData(eData);
        }

    }
}