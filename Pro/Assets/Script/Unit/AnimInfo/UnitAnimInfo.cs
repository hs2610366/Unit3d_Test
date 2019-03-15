/**  
* 标    题：   UnitAnimInfo.cs 
* 描    述：    
* 创建时间：   2018年08月10日 00:40 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class UnitAnimInfo
    {
        public string Title = "";
        //public AnimType Type = AnimType.Base;

        public bool IsTtermination = true;
        #region 目标动画
        /// <summary>
        /// 动作队列
        /// </summary>
        public List<string> List = new List<string>();
        /// <summary>
        /// 时间
        /// </summary>
        /// <param name="title"></param>
        public float Times = 0.0f;
        #endregion

        public UnitAnimInfo(string title)
        {
            Title = title;
        }
    }
}