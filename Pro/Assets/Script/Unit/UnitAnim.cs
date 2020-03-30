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
    public class UnitAnim : UnitObj
    {

        #region 动画管理器
        [NonSerialized]
        private Animator mAnim = null;
        public Animator Anim { get { return mAnim; } }
        #endregion
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

        private bool CheckAnim(string name, bool isUndo = false)
        {
            if (mCurInfo != null)
            {
                if (!mCurInfo.Name.Equals(name))
                {
                    if (mCurInfo.IsPlay())
                    {
                        if (mCurInfo.IsBreak)
                        {
                            if (mCurInfo.IsCheckBreak(name) == true)
                            {
                                if(isUndo == false)
                                {
                                    //這個設置可以避免undo執行endAnim動作使不同動作的切換更加平滑
                                    mCurInfo.NextAnim = name;
                                }
                                return false;
                            }
                        }
                    }
                    mCurInfo.Reset();
                }
            }
            return true;
        }
        private void ClearNextAnim(string actionName)
        {
            foreach(AnimInfo info in mAnimInfos)
            {
                if (info != null && 
                    !string.IsNullOrEmpty(info.NextAnim) && 
                    info.NextAnim.Equals(actionName))
                {
                    info.NextAnim = string.Empty;
                }
            }
        }
        #endregion

        #region 保护函数
        protected override void UpdateOneself(GameObject go)
        {
            base.UpdateOneself(go);
            mAnim = ConTool.Find<Animator>(go, "Root");
        }

        public bool Execute(string actionName)
        {
            if (CheckAnim(actionName) == false) return false;
            mCurInfo = GetAnimForName(actionName);
            if(mCurInfo != null)
            {
                mCurInfo.Execute();
            }
            return true;
        }

        public bool Undo(string actionName)
        {
            ClearNextAnim(actionName);
            if (CheckAnim(actionName, true) == false) return false;
            mCurInfo = GetAnimForName(actionName);
            if (mCurInfo != null)
            {
                mCurInfo.Undo();
            }
            return true;
        }
        #endregion

        #region 重构函数

        protected override void CustomUpdate()
        {
            base.CustomUpdate();
            if(mCurInfo != null)
            {
                mCurInfo.Update();
            }
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
                    mAnimInfos[i].SetAnim(Anim);
                }
            }
        }
#endregion
    }
}