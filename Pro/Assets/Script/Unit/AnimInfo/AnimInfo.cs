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
        /// 混合时间
        /// </summary>
        public int BlendTime { get { return mBlendTime; } set { mBlendTime = value; } }
        [SerializeField]
        private int mBlendTime = 20;
        /// <summary>
        /// 结束动画混合时间
        /// </summary>
        public int EndBlendTime { get { return mEndBlendTime; } set { mEndBlendTime = value; } }
        [SerializeField]
        private int mEndBlendTime = 20;
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
        /// <summary>
        /// 控制重复执行
        /// </summary>
        [NonSerialized]
        public bool mIsExecute = false;
        [NonSerialized]
        private int mOffsetTime = 0;
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

        public void Execute(bool loop = false)
        {
            mIsExecute = true;
            if (mIsPlay == true) return;
            Debug.LogError("---------------------->>> " + Name + " " + mIsPlay.ToString() + " " + mIsExecute.ToString());
            mIsExecute = false;
            mIsPlay = true;
            mIndex = 0;
            string name = mAnimGroup[mIndex];
            if (loop == false)
            {
                mBlendTime = 20;
                mOffsetTime = mBlendTime;
            }
            PlayAnim(name, mOffsetTime, mOffsetTime);
        }

        public void Undo()
        {
            mIsExecute = false;
            mIsPlay = false;
            mIndex = 0;
            string name = mEndAnim;
            if (string.IsNullOrEmpty(name)) name = "Idea2";
            mOffsetTime = mEndBlendTime;
            PlayAnim(name, mOffsetTime);
        }

        private void PlayAnim(string name, int blendTime, float offsetTime = 0)
        {
            if(blendTime == 0)
            {
                mAnim.Play(name,0, offsetTime);
            }
            else
            {
                float time = blendTime * 0.01f;
                mAnim.CrossFadeInFixedTime(name, time);
            }
        }

        public void Update()
        {
            if(mIsPlay)
            {
                if (mAnim != null && mAnim != null)
                {
                    AnimatorStateInfo info = mAnim.GetCurrentAnimatorStateInfo(0);
                    //if (info.normalizedTime > 1.0f && info.IsName(mAnimGroup[mIndex]))
                    if (info.normalizedTime > 1.0f)
                    {
                        mOffsetTime = 0;
                        mIsPlay = false;
                        if (mAnimGroup.Count > mIndex + 1)
                        {
                            mIndex++;
                            Execute();
                        }
                        else
                        {
                            if (!mIsExecute)
                                Undo();
                            else
                                Execute(true);
                        }
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
        public virtual void Reset()
        {
            mIndex = 0;
            mIsPlay = false;
            mIsExecute = false;
            mOffsetTime = 0;
            mAnim.Update(0);
        }

        public void Dispose()
        {
            Reset();
        }
    }
}