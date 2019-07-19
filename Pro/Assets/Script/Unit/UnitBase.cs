/**  
* 标    题：   UnitBase.cs 
* 描    述：    
* 创建时间：   2018年03月06日 01:32 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class UnitBase
    {

        #region 预制体
        private GameObject mModel;
        /// <summary>
        /// 模型预制体
        /// </summary>
        public GameObject Model { get { return mModel; } }
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

        #region 角色控制器
        private CharacterController mController;
        /// <summary>
        /// 角色控制器
        /// </summary>
        public CharacterController Controller { get { return mController; } }
        #endregion

        public UnitBase()
        {
            Global.Instance.OnUpdate += Update;
        }

        public virtual void UpdateModel(GameObject go)
        {
            mModel = go;
            mTrans = Model.transform;
            mRoot = TransTool.Find(mTrans, "Root");
            mController = mTrans.GetComponent<CharacterController>();
            CustomUpdateAnim(ConTool.Find<Animator>(go, "Root"));
            CustomUpdateModel();
        }

        protected virtual void CustomUpdateAnim(Animator anim)
        {

        }

        protected virtual void CustomUpdateModel()
        {

        }

        protected virtual void Update()
        {

        }

        public virtual void Dispose()
        {
#if UNITY_EDITOR
            GameObject.DestroyImmediate(mModel);
#else
            AssetsMgr.Instance.DestoryPrefab(mTrans.name);
#endif
            mController = null;
            mRoot = null;
            mTrans = null;
            mModel = null;
        }
    }
}