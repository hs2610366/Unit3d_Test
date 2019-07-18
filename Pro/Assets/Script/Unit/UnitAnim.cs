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
        [SerializeField]
        protected List<AnimInfo> AnimInfos = new List<AnimInfo>();
#if UNITY_EDITOR
        public List<AnimInfo> Anims { get { return AnimInfos; } }
#endif

        protected override void CustomUpdateAnim(Animator anim)
        {
            mAnim = anim;
        }

        #region 私有函数
        #endregion

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

        #region 公开函数
        public void UpdateAnims(ModelTemp temp)
        {
            List<AnimInfo> list = null;
#if UNITY_EDITOR
            string path = string.Format("{0}{1}{2}", Application.dataPath, PathTool.AssetsEditorResource, PathTool.Anim);
            list = Config.InputConfig<List<AnimInfo>>(path, temp.model, SuffixTool.Animation);
#else
            string path = string.Format("{0}{1}", PathTool.DataPath, PathTool.Anim);
            list = Config.InputConfig<List<AnimInfo>>(path, temp.model, SuffixTool.Animation);
#endif
            if (list == null) return;
            AnimInfos.AddRange(list);
        }
#endregion
    }
}