/**  
* 标    题：   AssetBundleInfo.cs 
* 描    述：    
* 创建时间：   2018年03月03日 19:00 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.IO;
using System.Collections.Generic;
using UnityEngine;

using Object = UnityEngine.Object;

namespace Divak.Script.Game 
{
	public class AssetBundleInfo : AssetLoad
    {
        /// <summary>
        /// 资源路径名
        /// </summary>
        private string mName = string.Empty;
        public string Name { get { return mName; } }
        /// <summary>
        /// 资源包
        /// </summary>
        private AssetBundle mBundle = null;
        /// <summary>
        /// 引用计数
        /// </summary>
        private int mRefCount;
        /// <summary>
        /// 加载结束
        /// </summary>
        private bool mLoadEnding = false;
        /// <summary>
        /// 回调
        /// </summary>
        private Queue<BaseLoadFinish> OnLoadFinish = new Queue<BaseLoadFinish>();

        private readonly object locker = new object();

        public AssetBundleInfo(string name)
        {
            mName = name;
        }

        #region 设置
        public void AddCallback(BaseLoadFinish callback)
        {
            OnLoadFinish.Enqueue(callback);
        }
        #endregion

        #region 获取bundle
        public void Load()
        {
            if(mBundle == null)
            {
                LoadBundleAsync(mName, OnLoadBundleFinish);
                return;
            }
            mLoadEnding = true;
        }

        private void OnLoadBundleFinish(string name, AssetBundle bundle)
        {
            mBundle = bundle;
            mLoadEnding = true;
        }
        #endregion

        #region 获取对象
        public void Get()
        {
            QuoteAdd();
            if (OnLoadFinish.Count == 0) return;
            var callback = OnLoadFinish.Dequeue();
            callback.SetLoadFinish(mBundle != null);
            if(callback.Instantiate)
            {
                var assetName = mName.Replace(SuffixTool.AssetName, string.Empty);
                assetName = Path.GetFileNameWithoutExtension(assetName);
                var obj = mBundle.LoadAsset(assetName);
                if(obj == null)
                {
                    MessageBox.Error("AssetBundle中获取对象失败：{AssetBundle:" + mName + "}, {Res:" + assetName + "}");
                }
                else
                {
                    callback.UpdateData(Object.Instantiate(obj));
                }
            }
            callback.OnCallback();
            callback = null;
        }
        #endregion

        #region 操作对象池

        public void Unload(Object obj)
        {
            Object.Destroy(obj);
            QuoteRemove();
        }
        #endregion

        #region 引用
        public void QuoteAdd()
        {
            mRefCount++;
        }

        public void QuoteRemove()
        {
            mRefCount--;
            if (mRefCount < 0)
            {
                mRefCount = 0;
            }
        }
        #endregion

        #region 状态 

        public bool LoadEnding()
        {
            return mLoadEnding;
        }

        public bool NoReference()
        {
            return mRefCount == 0;
        }
        #endregion

        #region 清除

        public void Clear(bool unloadAllLoadedObjects = false)
        {
            if (mBundle != null)
            {
                mBundle.Unload(unloadAllLoadedObjects);
                mBundle = null;
            }
            mLoadEnding = false;
            mRefCount = 0;
            mName = string.Empty;
            OnLoadFinish = null;
        }

        public void Dispose()
        {
            Clear();
        }
        #endregion
    }
}