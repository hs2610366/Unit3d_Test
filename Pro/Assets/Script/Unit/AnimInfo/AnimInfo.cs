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
        /// <summary>
        /// 攻击距离
        /// </summary>
        public float Distance = 0.0f;
        /// <summary>
        /// 动画组播放类型
        /// </summary>
        public AnimPlayType APT = AnimPlayType.Continuity;
        /// <summary>
        /// 重复播放能否中断
        /// </summary>
        public bool IsBreakRepeat = false;
        /// <summary>
        /// 能否被其他技能中断
        /// </summary>
        public bool IsBreak = false;
        /// <summary>
        /// 动作组
        /// </summary>
        public List<string> AnimGroup = new List<string>();
        /// <summary>
        /// 可中断当前动作的动作
        /// </summary>
        public List<int> BreakGroup = new List<int>();


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