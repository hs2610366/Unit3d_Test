/**  
* 标    题：   UnitAnim.cs 
* 描    述：    
* 创建时间：   2018年08月03日 22:36 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game
{
    public class UnitAnim : UnitBase
    {
        private Animator mAnim;

        private UnitAnimInfo mAnimInfo;

        private UnitAnimInfo mStartAnim = new UnitAnimInfo("出生动作");
        private Dictionary<string, UnitAnimInfo> mSkillAnims = new Dictionary<string, UnitAnimInfo>();
        private UnitAnimInfo mDeadAnim = new UnitAnimInfo("死亡动作");

        private UnitAnimInfo mPlayAnim;
        private int mPlayIndex;
#if UNITY_EDITOR
        public Dictionary<string, UnitAnimInfo> SkillAnims { get { return mSkillAnims; } }
#endif

        protected override void CustomUpdateAnim(Animator anim)
        {
            mAnim = anim;
        }

        #region 保护函数

        protected void Play(string actionName, int value)
        {
            if (!mAnim) return;
            mAnim.SetInteger(actionName, value);
        }


        protected void Play(string actionName, float value)
        {
            if (!mAnim) return;
            mAnim.SetFloat(actionName, value);
        }


        protected void Play(string actionName, bool value)
        {
            if (!mAnim) return;
            mAnim.SetBool(actionName, value);
        }


        protected void Play(string actionName)
        {
            if (!mAnim) return;
            mAnim.SetTrigger(actionName);
        }

        #endregion

        #region 重构函数
        protected override void Update()
        {
            base.Update();

            if (mAnimInfo != null && mAnim != null && mPlayAnim != null)
            {
                AnimatorStateInfo info = mAnim.GetCurrentAnimatorStateInfo(0);

                if (info.normalizedTime >= mPlayAnim.Times)
                {
                    switch (mAnimInfo.Type)
                    {
                        //case AnimPlayType.Base:
                        //    mPlayIndex++;
                            // PlayBase(mAnimInfo.List, mPlayIndex);
                        //    break;
                        case AnimPlayType.Random:
                            //PlayRandom(mAnimInfo.List, UnityEngine.Random.Range(0, mAnimInfo.List.Count - 1));
                            break;
                        case AnimPlayType.Continuity:
                            //PlayContinuity(mAnimInfo.List, mPlayIndex);
                            break;
                    }
                }
            }
        }
        #endregion

        #region 私有函数
        /**
        private void PlayBase(List<AnimInfo> list, int index)
        {
            if (index >= list.Count) return;
            AnimInfo anim = list[mPlayIndex];
            Play(anim.Name, true);
        }
        private void PlayRandom(List<AnimInfo> list, int index)
        {
            if (index >= list.Count) return;
            AnimInfo anim = list[mPlayIndex];
            Play(anim.Name, true);
        }

        private void PlayContinuity(List<AnimInfo> list, int index)
        {
            if (index >= list.Count) return;
            AnimInfo anim = list[mPlayIndex];
            Play(anim.Name, true);
        }
        #endregion

        public void Play(UnitAnimInfo info)
        {
            if (info == null) return;
            if (!mAnim) return;
            if (info != null && mAnimInfo != null && info.Title != mAnimInfo.Title)
            {
                Regain(mPlayInfo);
            }
            mAnimInfo = info;

            List<AnimInfo> list = info.List;
            if (list == null || list.Count == 0) return;


            AnimType type = info.Type;
            switch (type)
            {
                case AnimType.Base:
                    PlayBase(list, mPlayIndex);
                    break;
                case AnimType.Random:
                    PlayRandom(list, UnityEngine.Random.Range(0, list.Count - 1));
                    break;
                case AnimType.Continuity:
                    PlayContinuity(list, mPlayIndex);
                    break;
            }
        }
        public void Regain(UnitAnimInfo info)
        {
            if (info == null) return;
            if (!mAnim) return;
            if (string.IsNullOrEmpty(info.Name)) return;
            mAnim.SetBool(info.Name, false);
        }
        */
        #endregion
    }
}