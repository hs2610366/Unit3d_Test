/**  
* 标    题：   ThreadEventProgress.cs 
* 描    述：    
* 创建时间：   2021年03月09日 15:51 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game
{
	public class ThreadEventProgress : EventProgress
    {
        #region 参数
        public string Error { get; set; }
        #endregion

        public void UpdateData(int id, bool finish, int cur, int total, string content, string error)
        {
            this.ID = id;
            this.Finish = finish;
            this.Cur = cur;
            this.Total = total;
            this.Content = content;
            this.Error = error;
        }

    }
}