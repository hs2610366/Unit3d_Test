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
        private List<string> mAnimGroup = new List<string>();
        /// <summary>
        /// 可中断当前动作的动作组
        /// </summary>
        public List<string> BreakGroup { get { return mBreakGroup; } set { mBreakGroup = value; } }
        [SerializeField]
        private List<string> mBreakGroup = new List<string>();
        /// <summary>
        /// 结束动作
        /// </summary>
        public string EndAnim { get { return mEndAnim; } set { mEndAnim = value; } }
        [SerializeField]
        private string mEndAnim = string.Empty;



        [NonSerialized]
        private Animator mAnim = null;
        [NonSerialized]
        public int mIndex = 0;
        [NonSerialized]
        public bool mIsPlay = false;
        [NonSerialized]
        private Dictionary<string, AnimationClip> mClipDic = new Dictionary<string, AnimationClip>();

        public AnimInfo()
        {

        }

        public void SetAnim(Animator anim)
        {
            mAnim = anim;
            AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            { 
                if (mAnimGroup.Find( s=> { return s.Equals(clip.name); }) != null)
                {
                    mClipDic.Add(clip.name, clip);
                }
            }
        }

        public void Play()
        {
            if (mIsPlay == true) return;
            mIsPlay = true;
            string name = mAnimGroup[mIndex];
            mAnim.CrossFadeInFixedTime(name, 0f);
        }

        public void Undo()
        {
            if (mIsPlay == false) return;
            mIsPlay = false;
            string name = mEndAnim;
            if (string.IsNullOrEmpty(name)) name = "Idea2";
            mAnim.CrossFadeInFixedTime(name, 0.15f);
        }

        public void Update()
        {
            if (mAnim != null && mAnim != null )
            {
                AnimatorStateInfo info = mAnim.GetCurrentAnimatorStateInfo(0);
                if (info.normalizedTime > 1.0f && info.IsName(mAnimGroup[mIndex]))
                {
                    mIsPlay = false;
                    if (mAnimGroup.Count > mIndex + 1)
                    {
                        mIndex++;
                        Play();
                    } 
                    else
                    {
                        //Undo();
                    }
                }
            }
        }

        public bool IsCheckBreak(string name)
        {
            if(mBreakGroup.Count > 0)
            {
                for(int i = 0; i < mBreakGroup.Count; i ++)
                {
                    if (mBreakGroup[i].Equals(name)) return true;
                }
            }
            else
            {
                return true;
            }
            return false;
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