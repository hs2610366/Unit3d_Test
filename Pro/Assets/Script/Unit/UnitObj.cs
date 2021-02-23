/**  
* 标    题：   UnitObj.cs 
* 描    述：    
* 创建时间：   2019年12月11日 16:05 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class UnitObj : UnitBase
    {
        #region 预制体
        private GameObject mOneself;
        /// <summary>
        /// 模型预制体
        /// </summary>
        public GameObject Oneself { get { return mOneself; } }
        private Transform mTrans;
        /// <summary>
        /// 变换器
        /// </summary>
        public Transform Trans { get { return mTrans; } }
        #endregion

        #region 模型Root
        private Transform mRoot;
        /// <summary>
        /// 模型Root
        /// </summary>
        public Transform Root { get { return mRoot; } }
        #endregion

        #region 血条Root
        private Transform mHPRoot;
        /// <summary>
        /// 模型Root
        /// </summary>
        public Transform HPRoot { get { return mHPRoot; } }
        #endregion

        #region 角色控制器
        private CharacterController mController;
        /// <summary>
        /// 角色控制器
        /// </summary>
        public CharacterController Controller { get { return mController; } }
        #endregion

        #region 渲染器
        private Renderer mRendererTool;
        /// <summary>
        /// 渲染器
        /// </summary>
        public Renderer RendererTool{get { return mRendererTool; }}
        #endregion
        public UnitObj() : base() { }

        public UnitObj(string tag) : base(tag)
        {
        }

        #region 私有函数
        private void UpdateOnselfData()
        {
            if (TransTool.IsNull(mTrans)) return;
            mTrans.position = Pos;
            mTrans.eulerAngles = Angles;
            mTrans.localScale = Vector3.one * Scale;
        }

        #endregion

        #region 保护函数
        protected void CreateObj(string path)
        {
#if UNITY_EDITOR
            UnityEngine.Object prefab = Resources.Load(path);
            if (prefab == null)
            {
                MessageBox.Error(string.Format("error:{0} 未读取到", path));
                return;
            }
            GameObject go = GameObject.Instantiate(prefab as GameObject);
            //string path = UnityEditor.AssetDatabase.GetAssetPath(prefab);
            //ModPath = System.IO.Path.GetDirectoryName(path);
#else
            GameObject go = AssetsMgr.Instance.LoadPrefab(path);
#endif
            UpdateOneself(go);
        }


        protected virtual void UpdateOneself(GameObject go)
        {
            mOneself = go;
            mTrans = Oneself.transform;
            mRoot = TransTool.Find(mTrans, "Root");
            mHPRoot = TransTool.Find(mTrans, "Node/hp_root");
            mController = mTrans.GetComponent<CharacterController>();
            //mRendererTool = ConTool.Find<Renderer>(go,"");
            if (mController)
            {
                mController.center = mController.center * Scale;
                mController.height = mController.height * Scale;
                mController.radius = mController.radius * Scale;
            }
            UpdateOnselfData();
            CustomUpdateModel();
        }

        protected override void CustomUpdate()
        {
            base.CustomUpdate();
        }

        protected override void CustomRefreshGravity()
        {
            //base.CustomRefreshGravity();
            if (TransTool.IsNull(mTrans) == false)
            {
                bool isHit = false;
                Vector3 hitPos = TransTool.Raycast(mTrans, LayerName.Gound, out isHit);
                if(!isHit)
                {
                    if (Vector3.Distance(Pos, new Vector3(Pos.x, 0, Pos.z)) > 0)
                    {
                        isHit = true;
                    }
                }
                if (isHit)
                {
                    if (Vector3.Distance(Pos, hitPos) > 0)
                    {
                        if (GravityTabTime == 0 )
                        {
                            GravityTabTime = Time.realtimeSinceStartup;
                        }
                    }
                    else
                    {
                        GravityTabTime = 0;
                    }
                }
            }
        }
        #endregion

        #region 公有函数

        public Vector3 GetHPRootPos()
        {
            if (TransTool.IsNull(mTrans) == false)
                return HPRoot.transform.position;
            return Pos;
        }

        #endregion

        protected virtual void CustomUpdateModel()
        {

        }

        public override void Reset()
        {
            if (mController)
            {
                mController.center = mController.center / Scale;
                mController.height = mController.height / Scale;
                mController.radius = mController.radius / Scale;
            }
            base.Reset();
            UpdateOnselfData();
        }

        public override void Dispose()
        {
#if UNITY_EDITOR
            GameObject.DestroyImmediate(mOneself);
#else
            AssetsMgr.Instance.DestoryPrefab(mTrans.name);
#endif
            mController = null;
            mRoot = null;
            mTrans = null;
            mOneself = null;
            base.Dispose();
        }
    }
}