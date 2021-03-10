using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace Divak.Script.Game
{
    public class AssetBase : AssetLoad
    {
       // public static AssetBase Instance = new AssetBase();
        /// <summary>
        /// 资源清单文件
        /// </summary>
        private ResManifest mResManifest = new ResManifest();
        /// <summary>
        /// Bundle数据结构
        /// </summary>
        private Dictionary<string, AssetBundleInfo> mBundleInfos = new Dictionary<string, AssetBundleInfo>();

        private Queue<AssetBundleInfo> mLoadQueue = new Queue<AssetBundleInfo>();

        private bool mLoading = false;
        private bool mWaitClear = false;

        public virtual void Init(Action callback)
        {
            mResManifest.Init(callback);
        }

        #region 加载
        protected void LoadReady(string name, BaseLoadFinish callback)
        {
            var dependencies = mResManifest.GetAllDependencies(name);
            for (int i = 0; i < dependencies.Length; i++)
            {
                var dependencie = dependencies[i];
                mLoadQueue.Enqueue(GetBundleInfo(dependencie));
            }
            var info = GetBundleInfo(name, callback);
            mLoadQueue.Enqueue(info);
            info.AddCallback(callback);
            if (mLoading == true) return;
            mLoading = true;
            Global.Instance.StartCoroutine(LoadQueue(name));
        }

        private AssetBundleInfo GetBundleInfo(string name, BaseLoadFinish callback = null)
        {
            AssetBundleInfo info = null;
            if (!mBundleInfos.ContainsKey(name))
            {
                mBundleInfos.Add(name, new AssetBundleInfo(name));
            }
            info = mBundleInfos[name];
            return info;
        }

        private IEnumerator LoadQueue(string name)
        {
            var loading = true;
            while (loading)
            {
                var info = mLoadQueue.Dequeue();
                info.Load();
                yield return new WaitUntil(info.LoadEnding);
                info.Get();
                loading = mLoadQueue.Count > 0;
            }
            mLoadQueue.Clear();
            mLoading = false;
            if(mWaitClear == true)
            {
                ClearBundleInfos();
            }
        }
        #endregion

        #region 卸载
        public void Unload(Object go)
        {
            Unload(go.name, go);
        }

        public void Unload(string name, Object go)
        {
            if(mBundleInfos.ContainsKey(name))
            {
                var info = mBundleInfos[name];
                info.Unload(go);
                if(info.NoReference())
                {
                    mBundleInfos.Remove(name);
                    info.Dispose();
                    info = null;
                }
            }
        }

        public void ClearBundle(string name)
        {
            if (mBundleInfos.ContainsKey(name))
            {
                var info = mBundleInfos[name];
                mBundleInfos.Remove(name);
                info.Dispose();
                info = null;
            }
        }
        #endregion

        public virtual void ClearBundleInfos()
        {
            mLoadQueue.Clear();
            //Global.Instance.StopCoroutine(LoadQueue);
            if (mLoading == true)
            {
                mWaitClear = true;
                return;
            }
            mWaitClear = false;;
            var list = Util.GetDicKeyList(mBundleInfos);
            if(list != null)
            {
                while(list.Count > 0)
                {
                    var name = list[0];
                    list.RemoveAt(0);
                    ClearBundle(name);
                }
            }
        }

        public virtual void Dispose()
        {
            ClearBundleInfos();
            mResManifest.Dispose();
            mResManifest = null;
        }
    }
}
