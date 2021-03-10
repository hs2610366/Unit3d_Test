using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Divak.Script.Game
{
    public abstract class AssetLoad
    {
        #region 异步加载AssetBundle
        public void LoadBundleAsync(string name, Action<string, AssetBundle> callback, uint crc = 0)
        {
            Global.Instance.StartCoroutine(LoadBundle(name, callback, crc));
        }

        protected IEnumerator LoadBundle(string name, Action<string, AssetBundle> callback, uint crc = 0)
        {
            var path = PathTool.DataPath + name;
            path = path.ToLower();
            using (var request = UnityWebRequestAssetBundle.GetAssetBundle(path))
            {
                var handler = new DownloadHandlerAssetBundle(request.url, crc);             //1
                request.downloadHandler = handler;                                          //1
                yield return request.SendWebRequest();
                if (request.isNetworkError)
                {
                    MessageBox.Error("加载AssetBundle失败：" + path);
                    callback(path, null);
                }
                else
                {
                    callback(path, handler.assetBundle);                //1
                    //callback(path, (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle);                //2
                    //callback(path, AssetBundle.LoadFromMemory(request.downloadHandler.data));                             //3
                }
                handler.Dispose();
                request.Abort();
                request.Dispose();
            }
        }

        #endregion

        #region 同步加载AssetBundle
        protected AssetBundle LoadBundleFile(string path)
        {
            return AssetBundle.LoadFromFile(path);
        }
        #endregion
    }
}

