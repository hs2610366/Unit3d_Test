/**  
* 标    题：   BaseAssets.cs 
* 描    述：    
* 创建时间：   2017年07月25日 04:03 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Divak.Script.Game 
{
	public class BaseAssets
    { 
        #region 加载AssetsBundle

        /// <summary>
        /// 加载AssetBundle
        /// </summary>
        /// <param name="path"></param>
        public AssetBundle LoadAssetBundle(string path)
        {
            AssetBundle asset = AssetBundle.LoadFromFile(path);
            if (asset == null)
            {
                MessageBox.Error(string.Format("从{0}加载资源失败！！", path));
                return null;
            }
            return asset;
        }
        #endregion

        #region 加载资源

        protected IEnumerator LoadWWW(string path, Action<string, AssetBundleManifest> callback)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(path))
            {
                yield return request.SendWebRequest();
                if(request.isNetworkError)
                {
                    Debug.LogError("加载AB失败：" + path);
                }
                else
                {
                    AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
                    AssetBundleManifest manifest = (AssetBundleManifest)ab.LoadAsset("AssetBundleManifest");
                    if (manifest != null && callback != null) callback(path, manifest);
                    ab.Unload(false);
                    ab = null;
                }
                request.Abort();
                request.Dispose();
            }
        }

        /// <summary>
        /// 通过名字加载
        /// </summary>
        /// <param name="name"> 资源名 </param>
        /// <returns></returns>
        protected IEnumerator LoadBytes(string path, Action<string, byte[]> callback)
        {
            byte[] bytes = null;
#if UNITY_EDITOR || UNITY_IOS
            path = "file://" + path;
#endif
            WWW www = new WWW(path);
            yield return www;
            bytes = www.bytes;
            if (callback != null) callback(path, bytes);
            www.Dispose();
            www = null;
            yield return new WaitForFixedUpdate();
        }

        /// <summary>
        /// 通过名字加载
        /// </summary>
        /// <param name="name"> 资源名 </param>
        /// <returns></returns>
        protected void LoadFileBytes(string path, Action<string, byte[]> callback)
        {
            byte[] bytes = File.ReadAllBytes(path);
            if(bytes == null)
            {
                MessageBox.Error(string.Format("加载{0}资源失败！！", path));
                return;
            }
            if (callback != null) callback(Path.GetFileNameWithoutExtension(path), bytes);
        }
        #endregion

    }
}