/**  
* 标    题：   Projection2.cs 
* 描    述：   角色识别标识
* 创建时间：   2018年10月27日 03:27 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Divak.Script.Game 
{
	public class Projection2
    {
        private const string mPath = "Projection2";
        private const string mTPath = "circle_";
        private CharacterController mRoot;
        private GameObject mGO;
        private Texture mTexture;
        private Projector mProjector;

        public Projection2()
        {
            LoadProjection();
        }

        #region 私有函数
        private void LoadProjection()
        {
            if (mGO == null)
            {
#if UNITY_EDITOR
                UnityEngine.Object prefab = Resources.Load("Effect/Projector/" + mPath);
                mGO = GameObject.Instantiate(prefab as GameObject);
                mGO.name = mGO.name.Replace("(Clone)", string.Empty);
#else
                mGO = AssetsMgr.Instance.LoadPrefab(mPath);
#endif
                if (mGO == null) return;
                mProjector = mGO.GetComponentInChildren<Projector>();
            }
            UpdateIdent();
        }

        private void UpdateIdent()
        {
            if (mProjector == null || mTexture == null) return;
            mProjector.material.mainTexture = mTexture;
        }

        private void UpdateParent()
        {
            if (mRoot == null || mGO == null) return;
            mGO.transform.parent = mRoot.transform;
            Vector3 offset = Vector3.up * mRoot.height;
            mGO.transform.localPosition = offset;
            mGO.transform.localEulerAngles = new Vector3( 90.0f, 180.0f, 0);
            if(mProjector != null)
            {
                mProjector.farClipPlane = offset.y + 1;
                mProjector.orthographicSize = mRoot.radius + 2;
            }

        }

        #endregion

        #region 公开函数
        public void Add(CharacterController c)
        {
            mRoot = c;
            UpdateParent();
        }

        public void SetIdent(int ident)
        {
            if (mTexture == null)
            {
#if UNITY_EDITOR
                UnityEngine.Object go = Resources.Load(string.Format("Effect/Texture/{0}{1}", mTPath, ident));
                mTexture = GameObject.Instantiate<Texture2D>(go as Texture2D);
                mTexture.name = mTexture.name.Replace("(Clone)", string.Empty);
#else
                mTexture = AssetsMgr.Instance.LoadTex(string.Format("{0}{1}", mTPath, ident));
#endif
            }
            UpdateIdent();
        }

        public void Dispose()
        {

        }
        #endregion

    }
}