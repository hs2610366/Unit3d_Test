using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Divak.Script.Game;

namespace Divak.Script.Game
{
    public class AssetsHotUpdateMgr
    {
        public static readonly AssetsHotUpdateMgr Instance = new AssetsHotUpdateMgr();

        private Action mCallback;
        /// <summary>
        /// 本地版本控制文件
        /// </summary>
        private Dictionary<string,string> mAssetsVersionFiles;
        /// <summary>
        /// 服务器版本控制文件
        /// </summary>
        private Dictionary<string, string> mAssetsVersionOfServerFiles;
        /// <summary>
        /// 等待下载的资源路径
        /// </summary>
        private List<string> mWaitDownLoad;

        public void StartUpdate(Action callback)
        {
            this.mCallback = callback;
#if UNITY_EDITOR
            OnUpdateAssetsComplete();
#else
            mAssetsVersionFiles = new Dictionary<string, string>();
            mAssetsVersionOfServerFiles = new Dictionary<string, string>();
            CheckExtractResource();
#endif
        }

        #region 释放资源
        private void CheckExtractResource()
        {
            MessageBox.Log("检查资源文件及版本控制文本");
            bool isExists = Directory.Exists(PathTool.DataPath) && File.Exists(PathTool.AssetsVersionPath);
            if (!isExists)
            {
                Global.Instance.StartCoroutine(OnGetAssetsVersionFile());
            }
            else
            {
                Global.Instance.StartCoroutine(CheckUpdateAssets());
                return;
            }
        }
#endregion

#region 获取资源版本文件
        /// <summary>
        /// 获取资源版本文件
        /// </summary>
        /// <returns></returns>
        IEnumerator OnGetAssetsVersionFile()
        {
            if (!Directory.Exists(PathTool.DataPath))
                Directory.CreateDirectory(PathTool.DataPath);

            //开始下载被指定的路径
            //WWW www  =  WWW.LoadFromCacheOrDownload((PathTool.AssetsVersionOfServerPath, 0);
            WWW www = WWW.LoadFromCacheOrDownload("http://www.duowan.com/public/s/i/navbar/navbar.js", 0);
            //定义www为WWW类型并且等于被下载的内容。
            // Wait for download to complete
            //等待www全部下载完毕
            yield return www;
            if (www.isDone)
            {
                if (www.error != null)
                {
                    MessageBox.Error(string.Format("版本文件{0}更新失败!>", PathTool.AssetsVersionOfServerPath));
                    yield break;
                }
                Util.AnalysisAssetsVersion(ref mAssetsVersionOfServerFiles, www.text);
                if(mAssetsVersionOfServerFiles != null && mAssetsVersionOfServerFiles.Count > 0)
                {
                    File.WriteAllBytes(PathTool.AssetsVersionPath, www.bytes);
                }
                else
                {
                    MessageBox.Error("服务未获取到资源版本信息！！");
                    www.Dispose();
                    yield break;
                }
            }
            www.Dispose();
            yield return new WaitForEndOfFrame();
            Global.Instance.StartCoroutine(CheckUpdateAssets());
        }
#endregion

#region 检查并更新资源
        /// <summary>
        /// 检查并更新资源
        /// </summary>
        /// <returns></returns>
        IEnumerator CheckUpdateAssets()
        {
            if (mWaitDownLoad == null) mWaitDownLoad = new List<string>();
            WWW www = WWW.LoadFromCacheOrDownload(PathTool.AssetsVersionPath, 0);
            yield return www;
            if (www.isDone)
            {
                if (www.error != null)
                {
                    MessageBox.Error(string.Format("版本文件{0}获取失败!>", PathTool.AssetsVersionPath));
                    yield break;
                }
                Util.AnalysisAssetsVersion(ref mAssetsVersionFiles, www.text);

                if(mAssetsVersionFiles != null && mAssetsVersionFiles.Count > 0)
                {

                    foreach(string key in mAssetsVersionOfServerFiles.Keys)
                    {
                        if(mAssetsVersionFiles.ContainsKey(key) && mAssetsVersionFiles[key] == mAssetsVersionOfServerFiles[key])
                        {
                            continue;
                        }
                        else
                        {
                            mWaitDownLoad.Add(key);
                        }
                    }
                    Global.Instance.StartCoroutine(OnDownLoadAssets());
                    yield break;
                }
                else
                {
                    MessageBox.Error("本地未获取到资源版本信息！！");
                }
                OnUpdateAssetsComplete();
            }
            yield return new WaitForEndOfFrame();
        }
#endregion

#region 读取资源
        IEnumerator OnDownLoadAssets()
        {
            string inFile = string.Empty;
            string outFile = string.Empty;
            string name = string.Empty;
            if (!Directory.Exists(PathTool.DataPath)) Directory.CreateDirectory(PathTool.DataPath);
            while (mWaitDownLoad.Count > 0)
            {
                inFile = mWaitDownLoad[0];
                name = Path.GetDirectoryName(inFile);
                outFile = PathTool.DataPath + name;
                WWW www = WWW.LoadFromCacheOrDownload(inFile, 0);
                yield return www;

                if (www.isDone)
                {
                    File.WriteAllBytes(outFile, www.bytes);
                }
                mWaitDownLoad.RemoveAt(0);
                yield return new WaitForEndOfFrame();
            }
            OnUpdateAssetsComplete();
            yield return new WaitForSeconds(0.1f);
        }
#endregion

#region 资源更新完成
        /// <summary>
        /// 更新资源完成
        /// </summary>
        private void OnUpdateAssetsComplete()
        {
            if(mWaitDownLoad != null)
            {
                mWaitDownLoad.Clear();
                mWaitDownLoad = null;
            }
            if (mAssetsVersionFiles != null)
            {
                mAssetsVersionFiles.Clear();
                mAssetsVersionFiles = null;
            }
            if (mAssetsVersionOfServerFiles != null)
            {
                mAssetsVersionOfServerFiles.Clear();
                mAssetsVersionOfServerFiles = null;
            }
            if (mCallback == null)
            {
                return;
            }
            mCallback();
        }
#endregion
    }
}
