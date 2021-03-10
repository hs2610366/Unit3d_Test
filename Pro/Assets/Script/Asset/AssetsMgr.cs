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
using Object = UnityEngine.Object;

namespace Divak.Script.Game 
{
	public class AssetsMgr : AssetBase
    {
        public static readonly AssetsMgr Instance = new AssetsMgr();

        public Action<string, byte[]> LoadBytesCallback;
		#region 保护函数

		#endregion

		#region 私有函数

		#endregion

		#region 公开函数

        public override void Init(Action callback)
        {
            base.Init(callback);
        }

        #region 异步加载
        public void LoadPrefab(string name, string suffix, Action<GameObject, string> callback)
        {
            var finish = new ObjLoadFinish();
            finish.AddName(name);
            finish.AddCallbak<GameObject, string>(callback);
            LoadAsync(name, suffix, finish);
        }

        public void LoadPrefab(string bundleName, Action<GameObject, string> callback)
        {
            var finish = new ObjLoadFinish();
            finish.AddName(bundleName);
            finish.AddCallbak<GameObject, string>(callback);
            LoadAsync(bundleName, finish);
        }
        public void LoadScene(string name, Action<string, bool> callback)
        {
            var path = name + SuffixTool.Scene;
            var finish = new SceneLoadFinish();
            finish.SetInstantiate(false);
            finish.AddName(name);
            finish.AddCallbak<string, bool>(callback);
            LoadAsync(path, finish);
        }

        public void LoadAsync(string name, string suffix, BaseLoadFinish callback)
        {
            LoadReady(name + suffix, callback);
        }

        public void LoadAsync(string bundleName, BaseLoadFinish callback)
        {
            LoadReady(bundleName, callback);
        }
        #endregion

        #region 卸载
        public void UnloadPrefab(string name, GameObject obj)
        {
            Unload(name, obj);
        }

        public void UnloadPrefab(string name, string suffix, GameObject obj)
        {
            Unload(name + suffix, obj);
        }
        #endregion


        #region 配置
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
          //  LoadFileBytes(path, LoadBytesCallback);
#else
            path = Application.streamingAssetsPath + "/" + name;
            Global.Instance.StartCoroutine(LoadBytes(path, LoadBytesCallback));
#endif
        }
        #endregion

        #endregion
    }
}