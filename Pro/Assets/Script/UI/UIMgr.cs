/**  
* 标    题：   UIMgr.cs 
* 描    述：    
* 创建时间：   2017年07月25日 03:16 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Divak.Script.Game 
{
	public class UIMgr : UIMgrBase
    {
        /// <summary>
        /// 所有UI字典
        /// </summary>
        private static Dictionary<string, UIBase> UIDic = new Dictionary<string, UIBase>();
        /// <summary>
        /// 当前打开的UI
        /// </summary>
        private static Dictionary<string, UIBase> OpenUIDic = new Dictionary<string, UIBase>();

        public static GameObject UIRoot;
        public static Camera UICamera;
        public static Camera UIFXCamera;
        public static Transform UITrans;
        public static Transform FXTrans;

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            GameObject go = GameObject.FindWithTag(TagName.UICamera);
            Canvas canvas = null;
            CanvasScaler scaler = null;
            if(go == null)
            {
                go = UITool.CreateCanvas("UIRoot");
                go.tag = TagName.UICamera;
                canvas = go.GetComponent<Canvas>();
                scaler = go.GetComponent<CanvasScaler>();
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.pixelPerfect = false;
                canvas.planeDistance = 0;
                canvas.sortingLayerID = 1 << LayerName.GetIndexOfLayerName(LayerName.Default);
                UICamera = CreateCamera("UICamera", 1, go.transform);
                UIFXCamera = CreateCamera("UIFXCamera", 2, go.transform);
                GameObject ui = new GameObject();
                ui.name = "UI";
                ui.transform.parent = go.transform;
                UITrans = ui.transform;
                GameObject fx = new GameObject();
                fx.name = "FX";
                fx.transform.parent = go.transform;
                FXTrans = fx.transform;
            }
            else
            {
                UICamera = ComTool.Find<Camera>(go, "UICamera");
                UIFXCamera = ComTool.Find<Camera>(go, "UIFXCamera");
                UITrans = TransTool.Find(go.transform, "UI");
                FXTrans = TransTool.Find(go.transform, "FX");
            }
            if(go != null)
            {
                GameObject.DontDestroyOnLoad(go.transform);
                UIRoot = go;
            }

            /**
            if (go == null)
            {
                go = UITool.CreateCanvas("UIRoot");
                go.tag = TagName.UICamera;
                canvas = go.GetComponent<Canvas>();
                scaler = go.GetComponent<CanvasScaler>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.pixelPerfect = false;
                canvas.sortingOrder = 0;
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(GameSetting.ScreenW, GameSetting.ScreenH);
                scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                scaler.matchWidthOrHeight = 1;
            }

            Transform UILayerTrans = TransTool.Find(go.transform, "UILayer");
            if (UILayerTrans == null) UILayer = UITool.CreateCanvas("UILayer", UIRoot.transform);
            else UILayer = UILayerTrans.gameObject;
            Transform TipLayerTrans = TransTool.Find(go.transform, "TipLayer");
            if (TipLayerTrans == null) TipLayer = UITool.CreateCanvas("TipLayer", UIRoot.transform);
            else TipLayer = UILayerTrans.gameObject;
            */
        }

        public static Camera CreateCamera(string name, int depth, Transform parent)
        {
            GameObject go = new GameObject(name);
            Camera cam = go.AddComponent<Camera>();
            cam.name = name;
            cam.clearFlags = CameraClearFlags.Depth;
            cam.cullingMask = 1 << LayerName.GetIndexOfLayerName(LayerName.UI);
            cam.orthographic = true;
            cam.orthographicSize = 3.75f;
            cam.nearClipPlane = -1;
            cam.farClipPlane = 1;
            cam.depth = depth;
            cam.transform.parent = parent;
            return cam;
        }

        #region 打开UI
        public static void Open(string uiName, Action<UIBase> callback = null)
        {
            UIBase ui = null;
            if (UIDic.ContainsKey(uiName)) ui = UIDic[uiName];
            if(ui == null)
            {
                AssetsMgr.Instance.LoadPrefab(uiName, SuffixTool.Prefab, OnLoadFinish);
            }
        }

        private static void OnLoadFinish(GameObject go, string name)
        {
            if (go == null)
            {
                MessageBox.Error(string.Format("加载资源{0}失败", name));
                return;
            }
            var ui = Instantiation(name, go);
            if (ui == null)
            {
                AssetsMgr.Instance.UnloadPrefab(name, go);
                return;
            }
            go.name = go.name.Replace("(Clone)", string.Empty);
            UITool.SetParentOfCanvas((go as GameObject).transform, UITrans);
            UIDic.Add(name, ui);

            if (ui == null) return;
            ui.Open();
            OpenUIDic.Add(name, ui);
            //if (callback == null) return;
            //callback(ui);
        }

        public static void Open<T>(Action<UIBase> callback = null)
        {
            Type t = typeof(T);
            string name = t.ToString();
            Open(name, callback);
        }
        #endregion

        #region 关闭UI
        public static void Close(string uiName,Action callback = null)
        {
            UIBase ui = null;
            if(UIDic.ContainsKey(uiName))
            {
                ui = UIDic[uiName];
                ui.Close();
                OpenUIDic.Remove(uiName);
            }
        }

        public static void Close<T>(Action callback = null)
        {
            Type t = typeof(T);
            string name = t.ToString();
            Close(name, callback);
        }
        #endregion

        #region 释放ui
        /// <summary>
        /// 通过名字释放UI
        /// </summary>
        /// <param name="uiName"></param>
        public static void Dispose(string uiName, bool isDestory = false)
        {
            if (OpenUIDic.ContainsKey(uiName)) OpenUIDic.Remove(uiName);
            if (UIDic.ContainsKey(uiName)) UIDic.Remove(uiName);
           // AssetsMgr.Instance.DestoryPrefab(uiName, isDestory);
        }

        /// <summary>
        /// 释放全部ui
        /// </summary>
        public static void DisposeAll(bool isDestory = false)
        {
            if (UIDic.Count == 0) return;
            string name = string.Empty;
            foreach (KeyValuePair<string, UIBase> data in UIDic)
            {
                if (OpenUIDic.ContainsKey(data.Key)) OpenUIDic.Remove(data.Key);
                data.Value.Dispose();
            //    AssetsMgr.Instance.DestoryPrefab(data.Key,isDestory);
            }
            OpenUIDic.Clear();
            UIDic.Clear();
        }
        #endregion
    }
}