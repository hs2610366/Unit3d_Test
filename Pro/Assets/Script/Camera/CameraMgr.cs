/**  
* 标    题：   CameraManager.cs 
* 描    述：   攝像機管理
* 创建时间：   2017年07月22日 02:25 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class CameraMgr
    {
        /// <summary>
        /// 主攝像機
        /// </summary>
        private static Camera mMain;
        public static Camera Main;

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            if (mMain == null) GetMainCamera();
            Global.Instance.OnUpdate += Update;
        }

        private static CameraControlBase mControl;
        public static CameraControl CC {get{ return mControl as CameraControl; }}

        #region 私有函數
        /// <summary>
        /// 获取主摄像机
        /// </summary>
        private static void GetMainCamera()
        {
            GameObject go = GameObject.FindWithTag(TagName.MainCamera);
            if(go == null)
            {
                go = new GameObject(TagName.MainCamera);
                go.tag = TagName.MainCamera;
            }
            go.name = "CameraRoot";
            mMain = go.GetComponent<Camera>();
            if (mMain == null) mMain = go.AddComponent<Camera>();
            mMain.clearFlags = CameraClearFlags.Nothing;
            mMain.cullingMask = GetLayerMask();
            mMain.orthographic = false;
            mMain.nearClipPlane = 0.01f;
            mMain.farClipPlane = 100.0f;
            GameObject.DontDestroyOnLoad(mMain);
        }

        private static int GetLayerMask()
        {
            return 1 << LayerMask.NameToLayer(LayerName.Unit) | 1 << LayerMask.NameToLayer(LayerName.Gound);
        }

        #endregion

        #region 公开函数
        public static void UpdatePlay(Transform trans)
        {
            if (mControl == null) mControl = new CameraControl(mMain);
            mControl.UpdateTrans(trans);
        }

        public static void Update()
        {
            if (mControl != null) mControl.UpdateControl();
        }
        #endregion
    }
}