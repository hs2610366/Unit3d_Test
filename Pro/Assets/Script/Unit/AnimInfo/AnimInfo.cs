/**  
* 标    题：   AnimInfo.cs 
* 描    述：    
* 创建时间：   2019年05月20日 02:29 
* 作    者：   by. T.Y.Divak 
* 详    细：   
*               Animator中可以获取三种不同的状态：
*                   GetCurrentAnimatorStateInfo 获取正确的状态机状态
*                   GetNextAnimatorStateInfo 获取下一个状态机的状态
*                   GetAnimatorTransitionInfo 获取状态机的过渡状态
*               动画同步是在帧最前，而协程是在帧的最后调用。所以切换状态后在协程获取状态机状态要yield return null在获取。
*               ---------------------------------------------------------------------------------------------------------
*               如果状态机有过渡的状态的话（A—》B），切换后
*                   GetCurrentAnimatorStateInfo 仍会获取切换前的状态 (A)
*                   GetNextAnimatorStateInfo 会获取切换后的状态（B）
*                   并且此时GetAnimatorTransitionInfo 可以获得过渡状态
*                   切换完毕后GetAnimatorTransitionInfo 会返回一个null
*                   通过normalizedTime计算动画播放时间时候，处于过渡状态下，仍会进行下一个状态的动画，
*                   所以过渡状态后获取的正确的GetCurrentAnimatorStateInfo （B状态）的normalizedTime不是从0开始而是根据过渡状态下经过的时间开始的
*               如果状态机没过渡状态的话（A—》B），切换后
*                   GetCurrentAnimatorStateInfo 会获得切换后的状态（B）
*                   GetAnimatorTransitionInfo 会返回一个null
*                   这是通过GetCurrentAnimatorStateInfo（B状态）获取的normalizedTime则是从0开始
*               如果动画是可以循环的话，貌似下一次该状态的normalizedTime不会归0
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
        /// 持续播放
        /// </summary>
        public bool IsLoop { get { return mIsLoop; } set { mIsLoop = value; } }
        [SerializeField]
        private bool mIsLoop = false;
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
        private int mIndex = 0;
        public string NextAnim { get { return mNextAnim; } set { mNextAnim = value; } }
        [NonSerialized]
        private string mNextAnim = string.Empty;
        /// <summary>
        ///播放状态
        /// </summary>
        [NonSerialized]
        private UnitAnimState mState = UnitAnimState.None;
        [NonSerialized]
        private int mOffsetTime = 0;
        [NonSerialized]
        private Dictionary<string, AnimationClip> mClipDic = null;
        

        public AnimInfo()
        {

        }

        public void SetAnim(Animator anim)
        {
            mAnim = anim;
            if(mClipDic == null) mClipDic = new Dictionary<string, AnimationClip>();
            AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            { 
                if (mAnimGroup.Find( s=> { return s.Equals(clip.name); }) != null)
                {
                    mClipDic.Add(clip.name, clip);
                }
            }
        }

        public void Execute(bool isLoop = false)
        {
            if (mState != UnitAnimState.None) return;
            mIndex = 0;
            string name = mAnimGroup[mIndex];
            if (isLoop == false)
            {
                mBlendTime = 20;
                mOffsetTime = mBlendTime;
            }
            PlayAnim(name, mOffsetTime, mOffsetTime);
        }

        public void Undo()
        {
            string name = mEndAnim;
            if (string.IsNullOrEmpty(name)) name = "Idea2";
            mOffsetTime = mEndBlendTime;
            PlayAnim(name, mOffsetTime);
        }

        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="name"></param>
        /// <param name="blendTime"></param>
        /// <param name="offsetTime"></param>
        private void PlayAnim(string name, int blendTime, float offsetTime = 0)
        {
            if(blendTime == 0)
            {
                mState = UnitAnimState.Play;
                mAnim.Play(name,0, offsetTime);
                Debug.LogError("----------------------->>>> Play: " + name);
            }
            else
            {
                Debug.LogError("----------------------->>>> CossFade: " + name);
                mState = UnitAnimState.CossFade;
                float time = blendTime * 0.01f;
                mAnim.CrossFadeInFixedTime(name, time);
            }
        }

        public void Update()
        {
            if(mState != UnitAnimState.None)
            {
                if (mAnim != null && mAnim != null)
                {
                    if(mState == UnitAnimState.Play)
                    {
                        AnimEndFun();
                    }
                    else if(mState == UnitAnimState.CossFade)
                    {
                        if (mAnim.IsInTransition(0) == true)
                        {
                            return;
                        }
                        AnimEndFun();
                    }
                }
            }
        }

        /// <summary>
        /// 动画播放完成
        /// </summary>
        private void AnimEndFun()
        {
            AnimatorStateInfo info = mAnim.GetCurrentAnimatorStateInfo(0);
            if (info.IsName(mEndAnim))
            {
                Reset();
                return;
            }
            if (info.normalizedTime > 1.0f)
            {
                mOffsetTime = 0;
                if (info.IsName(mAnimGroup[mIndex]))
                {
                    if (mAnimGroup.Count > mIndex + 1)
                    {
                        mIndex++;
                        Execute();
                    }
                    else
                    {
                        if (!mIsLoop)
                        {
                            mState = UnitAnimState.None;
                            if (!string.IsNullOrEmpty(mNextAnim)) return;
                            Undo();
                        }
                        else
                        {
                            Reset();
                            Execute(mIsLoop);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 检查当前动画能否中断
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// 当前动画是否在播放
        /// </summary>
        /// <returns></returns>
        public bool IsPlay()
        {
            return mState != UnitAnimState.None;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public virtual void Reset()
        { 
            mState = UnitAnimState.None;
            mNextAnim = string.Empty;
            mIndex = 0;
            mOffsetTime = 0;
            mAnim.Update(0);
        }

        public void Dispose()
        {
            Reset();
        }
    }
}