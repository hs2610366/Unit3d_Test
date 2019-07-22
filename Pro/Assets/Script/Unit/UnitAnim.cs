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
        [NonSerialized]
        private Animator mAnim = null;
        public Animator Anim { get { return mAnim; } }
        [NonSerialized]
        private AnimInfo mCurInfo = null;

        [SerializeField]
        protected List<AnimInfo> mAnimInfos = new List<AnimInfo>();
        [SerializeField]
        protected Dictionary<UnitState, AnimInfo> mUnitState = new Dictionary<UnitState, AnimInfo>();
#if UNITY_EDITOR
        public List<AnimInfo> Anims { get { return mAnimInfos; } }
        public List<UnitState> States = new List<Game.UnitState>();
        public Dictionary<UnitState, AnimInfo> UnitStates { get { return mUnitState; } }
        public UnitAnim() : base()
        {
            if (mUnitState.Count == 0)
            {
                Array ary = Enum.GetValues(typeof(UnitState));  //array是数组的基类, 无法实例化
                foreach (int i in ary)  //列出枚举项对应的数字
                {
                    States.Add((UnitState)i);
                    mUnitState.Add((UnitState)i, null);
                }
            }
        }
#endif

        #region 私有函数
        private AnimInfo GetAnimForName(string name)
        {
            return mAnimInfos.Find(s => { return s.Name.Equals(name); });
        }

        private bool CheckAnim(string name)
        {
            if(mCurInfo != null)
            {
                if (mCurInfo.Name.Equals(name))
                {
                    if(mCurInfo.mIsPlay == true)
                        return false;
                }
                else
                {
                    if(mCurInfo.IsBreak)
                    {
                        if(IsCheckBreak(name) == true)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        #endregion

        #region 保护函数
        public bool Play(string actionName)
        {
            if (CheckAnim(actionName) == false) return false;
            mCurInfo = GetAnimForName(actionName);
            if(mCurInfo != null)
            {
                mCurInfo.Play();
            }
            return true;
        }

        public bool Undo(string actionName)
        {
            if (CheckAnim(actionName) == false) return false;
            mCurInfo = GetAnimForName(actionName);
            if (mCurInfo != null)
            {
                mCurInfo.Undo();
            }
            return true;
        }
        #endregion

        #region 重构函数
        protected override void Update()
        {
            base.Update();
            if(mCurInfo != null)
            {
                mCurInfo.Update();
            }
        }

        protected override void CustomUpdateAnim(Animator anim)
        {
            mAnim = anim;
        }

        public override void Dispose()
        {
            mAnim = null;
            mCurInfo = null;
            mAnimInfos.Clear();
            base.Dispose();
        }
        #endregion

        #region 私有函数
        #endregion

        #region 公开函数
        public void UpdateState(ModelTemp temp)
        {
            Dictionary<UnitState, AnimInfo> dic = null;
#if UNITY_EDITOR
            string path = string.Format("{0}{1}{2}", Application.dataPath, PathTool.AssetsEditorResource, PathTool.UnitState);
            dic = Config.InputConfig<Dictionary<UnitState, AnimInfo>>(path, temp.model, SuffixTool.Config);
#else
            string path = string.Format("{0}{1}", PathTool.DataPath, PathTool.UnitState);
            dic = Config.InputConfig<Dictionary<UnitState, AnimInfo>>(path, temp.model, SuffixTool.Config);
#endif
            if (dic == null || dic.Count == 0) return;
            mUnitState.Clear();
            mUnitState = dic;
        }
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
            mAnimInfos.AddRange(list);
            if(mAnimInfos.Count > 0)
            {
                for (int i = 0; i < mAnimInfos.Count; i++)
                {
                    mAnimInfos[i].SetAnim(mAnim);
                }
            }
        }
#endregion
    }
}