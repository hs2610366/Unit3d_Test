/**  
* 标    题：   AnimInfo.cs 
* 描    述：    
* 创建时间：   2019年05月20日 02:29 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class AnimInfo
    {
        /// <summary>
        /// 技能名
        /// </summary>
        public string Name = string.Empty;
        

        public AnimInfo()
        {

        }


        public void Dispose()
        {
#if UNITY_EDITOR
            Reset();
#endif
        }
#if UNITY_EDITOR
        protected virtual void Reset(){ }
#endif
    }
}