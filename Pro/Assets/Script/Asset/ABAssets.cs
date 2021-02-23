/**  
* 标    题：   ABAssets.cs 
* 描    述：    
* 创建时间：   2018年03月03日 16:50 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Divak.Script.Game;

namespace Divak.Script.Game 
{
	public class ABAssets : BaseAssets
    {
        private Dictionary<string, AssetBundle> mSceneDic = new Dictionary<string, AssetBundle>();
        /// <summary>
        /// 存储加载的manifest资源信息
        /// </summary>
        private Dictionary<string,string[]> mDependsDic = new Dictionary<string, string[]>();
        /// <summary>
        /// 存储加载的AssetsBundle资源信息
        /// </summary>
        private Dictionary<string, AssetBundleInfo> mABDic = new Dictionary<string, AssetBundleInfo>();

        public AssetBundleManifest mAbm = null;

        #region LoadMainAssetBundleManifestComplete
        /// <summary>
        /// LoadMainAssetBundleManifestComplete
        /// </summary>
        public event Fun LoadMABMComplete;
        public event StringFun LoadSceneComplete;
        #endregion


        #region 加载manifest文件
        protected void LoadMainAssetBundleManifest()
        {
            string path = string.Empty;
#if UNITY_EDITOR
            path = PathTool.DataPath + UnityEditor.EditorUserBuildSettings.activeBuildTarget.ToString();
#else
            path = PathTool.DataPath + Application.platform.ToString();
#endif
            Global.Instance.StartCoroutine(LoadWWW(path, LMABMCallback));
        }

        protected void LMABMCallback(string p, AssetBundleManifest manifest)
        {
            mAbm = manifest;
            if (manifest == null)
            {
                MessageBox.Error(string.Format("从{0}加载manifest失败！！", p));
                return;
            }
            LoadMABMComplete();
        }

        /// <summary>
        /// 获取依赖
        /// </summary>
        /// <param name="name"></param>
        protected AssetBundle GetDepends(string assetBundleName)
        {
            if (mAbm == null)
            {
                MessageBox.Error("mainManifest is null!!!");
                return null;
            }
            //获取依赖文件列表;    
            string[] depends = mAbm.GetAllDependencies(assetBundleName);
            if(depends != null)
            {
                if (!mDependsDic.ContainsKey(assetBundleName)) mDependsDic[assetBundleName] = depends;
                for(int i = 0; i < depends.Length; i ++)
                {
                    GetAssetBundle(depends[i], isDepends: true);
                }
            }

            return GetAssetBundle(assetBundleName);
        }
#endregion

#region 加载assetsBundle
        public void LoadSAB(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Error("加载的资源名为null!!");
            }
            name = name.ToLower();
            AssetBundle ab = GetDepends(name + SuffixTool.AssetName);
            if (!mSceneDic.ContainsKey(name)) mSceneDic.Add(name, ab);
            if (ab == null)
            {
                MessageBox.Error(string.Format("场景[{0}]加载完成", name));
                return;
            }
            string n = Path.GetFileNameWithoutExtension(name);
            LoadSceneComplete(n);
        } 

        /// <summary>
        /// 通过名字加载资源
        /// </summary>
        /// <param name="name"> 资源名 </param>
        /// <returns></returns>
        protected UnityEngine.Object LoadAB(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Error("加载的资源名为null!!");
                return null;
            }
            name = name.ToLower();

            AssetBundle ab = GetDepends(name + SuffixTool.AssetName);
            UnityEngine.Object obj = ab.LoadAsset(name);
            if (obj == null)
            {
                MessageBox.Error(string.Format("从{0}中获取资源{1}失败！！", ab.name, name));
                return null;
            }
            return GameObject.Instantiate(obj);
        }

        /// <summary>
        /// 通过类型 和 名字 加载资源
        /// </summary>
        /// <typeparam name="T"> 资源类型 </typeparam>
        /// <param name="nam"> 资源名字 </param>
        /// <returns></returns>
        protected T LoadAB<T>(string name) where T : UnityEngine.Object
        {
            T t = LoadAB(name) as T;
            if(t == null)
            {
                MessageBox.Error("加载资源[{0}]为空！！", name);
                return null;
            }
            return t;
        }

        protected AssetBundle GetAssetBundle(string abName, bool isDepends = false)
        {
            string extension = Path.GetExtension(abName);
            string path = PathTool.DataPath + abName;
            AssetBundle ab = GetAsset(abName);
            if (ab == null)
            {
                ab = LoadAssetBundle(path);
                if (!mABDic.ContainsKey(abName))
                {
                    mABDic.Add(abName, new AssetBundleInfo(abName, ab));
                }
                mABDic[abName].Add();
            }
            if (ab == null) return null;
            if (isDepends) return null;
            return ab;
        }

#endregion

#region 获取assetBundle中的资源
        /// <summary>
        /// 获取assetBundle中的资源
        /// </summary>
        /// <param name="name"> 资源名 </param>
        /// <returns></returns>
        private AssetBundle GetAsset(string name)
        {
            if(mABDic.ContainsKey(name))
            {
                return mABDic[name].AB;
            }
            return null;
        }
#endregion

#region 释放资源
        public void DestoryInfo(string name, bool isDerstoy)
        {
            AssetBundleInfo info = null;
            if (mABDic.ContainsKey(name))
            {
                info = mABDic[name];
                mABDic.Remove(name);
                info.Reduce();
                if (info.Count <= 0)
                    info.Clear(isDerstoy);
                info = null;
            }
        }

        /// <summary>
        /// 通过名字释放assetbundle 会把assetbundle里所有资源释放
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isDerstoy"></param>
        public void Destory(string name, bool isDerstoy = true)
        {
            string abName =  name + SuffixTool.Prefab + SuffixTool.AssetName;
            abName = abName.ToLower();
            string [] depends = mDependsDic[abName];
            mDependsDic.Remove(abName);
            string path = string.Empty;
            for(int i = 0; i < depends.Length; i ++)
            {
                path = depends[i];
                string n = Path.GetFileName(path);
                DestoryInfo(n, isDerstoy);
            }
            DestoryInfo(name, isDerstoy);
        }

        public void DisposeAll(bool isDerstoy = true)
        {
            if (mABDic == null || mABDic.Count == 0) return;
            AssetBundleInfo info = null;
            List<string> list = Util.GetDicKeyList(mABDic);
            string key = string.Empty;
            for(int i = 0; i < list.Count; i ++)
            {
                key = list[i];
                info = mABDic[key];
                mABDic.Remove(key);
                info.Clear(isDerstoy);
                info = null;
                if (mDependsDic.ContainsKey(key))
                {
                    mDependsDic.Remove(key);
                }
            }
            mABDic.Clear();
            mDependsDic.Clear();
        }
#endregion
    }
}