/**  
* 标    题：   EventProgress.cs 
* 描    述：    
* 创建时间：   2021年03月09日 15:14 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class EventProgress
    {
        #region 委托
        public delegate void EventUpdate(EventProgress progressInfo);
        public EventUpdate Update;
        #endregion

        #region 参数
        public int ID { get; set; }
        public bool Finish { get; set; }
        public int Cur { get; set; }
        public int Total { get; set; }
        public string Content { get; set; }
        #endregion

        #region 公开函数
        public virtual void Reset()
        {
            ID = -1;
            Cur = 0;
            Total = 0;
            Finish = false;
            Content = string.Empty;
            Update = null;
        }
        #endregion

    }
}