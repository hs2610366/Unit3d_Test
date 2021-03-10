using System;
using UnityEngine;

namespace Divak.Script.Game
{
    public class ResManifest : AssetLoad
    {
        /// <summary>
        /// 资源清单名
        /// </summary>
        public const string BundleManifest = "AssetBundleManifest";
        /// <summary>
        /// 资源清单文件
        /// </summary>
        private AssetBundleManifest mAssetBundleManifest = null;
        /// <summary>
        /// 加载完成回调
        /// </summary>
        public Action finish;

        public void Init(Action callback)
        {
            finish = callback;
            var name = string.Empty;
#if UNITY_EDITOR
            name = UnityEditor.EditorUserBuildSettings.activeBuildTarget.ToString();
#else
            name = Application.platform.ToString();
#endif
            LoadBundleAsync(name, OnComplete);
        }

        private void OnComplete(string path, AssetBundle bundle)
        {
            if(bundle == null)
            {
                MessageBox.Error("加载资源清单失败：" + path);
                return;
            }
            mAssetBundleManifest = bundle.LoadAsset<AssetBundleManifest>(BundleManifest);
            if (mAssetBundleManifest == null)
            {
                MessageBox.Error("读取资源清单文件失败:" + BundleManifest);
                return;
            }
            bundle.Unload(false);
            bundle = null;
            if (finish != null) finish();
            finish = null;
        }

        public string[] GetAllDependencies(string name)
        {
            return mAssetBundleManifest.GetAllDependencies(name);
        }

        public void Dispose()
        {
            mAssetBundleManifest = null;
            finish = null;
        }
    }
}
