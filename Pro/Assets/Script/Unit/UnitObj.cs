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
	public class UnitObj : UnitAttr
    {
        #region 属性
        #region bundle名
        private string mAssetName;
        #endregion
        #endregion

        #region 引用对象

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
        #endregion

        #endregion

        #region 构造函数
        public UnitObj() : base() { }

        public UnitObj(string tag) : base(tag)
        {
        }
        #endregion

        #region 私有函数
        private void UpdateOnselfData()
        {
            if (TransTool.IsNull(mTrans)) return;
            mTrans.position = Pos;
            mTrans.eulerAngles = Angles;
            mTrans.localScale = Vector3.one * Scale;
        }

        private void OnLoadFinish(GameObject go, string name)
        {
            UpdateOneself(go);
        }

        #endregion

        #region 保护函数
        protected override void Instantiate()
        {
            mAssetName = mModelTemp.model;
            CreateObj(mAssetName);
        }

        protected void CreateObj(string path)
        {
            AssetsMgr.Instance.LoadPrefab(path, OnLoadFinish);
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
                Vector3 hitPos = RaycastTool.Raycast(mTrans, Vector3.down, LayerName.Gound, out isHit);
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

        protected virtual void CustomUpdateModel()
        {

        }
        #endregion

        #region 公开函数

        public virtual void UpdateOneself(GameObject go)
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

        public Vector3 GetHPRootPos()
        {
            if (TransTool.IsNull(mTrans) == false)
                return HPRoot.transform.position;
            return Pos;
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
            if (string.IsNullOrEmpty(mAssetName))
            {
                MessageBox.Error("UnitObj.Dispose : 资源名为空");
            }
            else
            {
                AssetsMgr.Instance.UnloadPrefab(mAssetName, mOneself);
            }
            mAssetName = null;
            mController = null;
            mRoot = null;
            mTrans = null;
            mOneself = null;
            base.Dispose();
        }

        #endregion

    }
}