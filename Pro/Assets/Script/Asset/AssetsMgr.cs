/**  
* 标    题：   AssetsMgr.cs 
* 描    述：   资源管理工具
* 创建时间：   2017年07月31日 03:28 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class AssetsMgr : ABAssets
    {
        public static readonly AssetsMgr Instance = new AssetsMgr();

        public Action<string, byte[]> LoadBytesCallback;
		#region 保护函数

		#endregion

		#region 私有函数

		#endregion

		#region 公开函数

        public void Init()
        {
            LoadMainAssetBundleManifest();
        }

        public GameObject LoadPrefab(string name)
        {
            name = name + SuffixTool.Prefab;
            GameObject go = LoadAB<GameObject>(name);
            go.name = go.name.Replace("(Clone)", string.Empty);
            return go;
        }

        public Texture LoadTex(string name)
        {
            name = name + SuffixTool.PNG;
            Texture tex = LoadAB<Texture>(name);
            tex.name = tex.name.Replace("(Clone)", string.Empty);
            return tex ;
        }

        public UnityEngine.Object Load(string name, string suffix)
        {
            return LoadAB(name + suffix);
        }

        public void DestoryPrefab(string name, bool isDerstoy = true)
        {
            name = name + SuffixTool.Prefab;
            Destory(name, isDerstoy);
        }

        public void LoadScene(string name)
        {
            name = name + SuffixTool.Scene;
           LoadSAB(name);
        }

        /// <summary>
        /// 加载配置表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        public void LoadTbl(string name, Action<string, byte[]> callback)
        {
            LoadBytesCallback += callback;
            string path = string.Empty;
#if UNITY_EDITOR
            path = Config.PathConfig[ConfigKey.AssetsPath];
            path = string.Format("{0}/{1}/{2}", path, UnityEditor.EditorUserBuildSettings.activeBuildTarget, name);
            LoadFileBytes(path, LoadBytesCallback);
#else
            path = Application.streamingAssetsPath + "/" + name;
            Global.Instance.StartCoroutine(LoadBytes(path, LoadBytesCallback));
#endif
        }
        #endregion

    }
}