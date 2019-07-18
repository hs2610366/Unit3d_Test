/**  
* 标    题：   AnimInfo.cs 
* 描    述：    
* 创建时间：   2019年05月20日 02:29 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game
{
    [Serializable]
    public partial class AnimInfo
    {
        /// <summary>
        /// 技能名
        /// </summary>
        public string Name { get { return mName; } set { mName = value; } }
        [SerializeField]
        private string mName = string.Empty;
        /// <summary>
        /// 攻击距离
        /// </summary>
        public float Distance { get { return mDistance; } set { mDistance = value; } }
        [SerializeField]
        private float mDistance = 0.0f;
        /// <summary>
        /// 动画组播放类型
        /// </summary>
        public AnimPlayType APT { get { return mApt; } set { mApt = value; } }
        [SerializeField]
        private AnimPlayType mApt = AnimPlayType.Continuity;
        /// <summary>
        /// 能否被其他技能中断
        /// </summary>
        public bool IsBreak { get { return mIsBreak; } set { mIsBreak = value; } }
        [SerializeField]
        private bool mIsBreak = false;
        /// <summary>
        /// 动作组
        /// </summary>
        public List<string> AnimGroup { get { return mAnimGroup; } set { mAnimGroup = value; } }
        [SerializeField]
        public List<string> mAnimGroup = new List<string>();
        /// <summary>
        /// 可中断当前动作的动作组
        /// </summary>
        public List<int> BreakGroup { get { return mBreakGroup; } set { mBreakGroup = value; } }
        [SerializeField]
        public List<int> mBreakGroup = new List<int>();


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