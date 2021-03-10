/**  
* 标    题：   PathTool.cs 
* 描    述：   路径工具
* 创建时间：   2017年04月13日 01:07 
* 作    者：   by. T.Y.Divak 
* 详    细：   
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Divak.Script.Game
{
    public class PathTool
    {
        /// <summary>
        /// CDN路径
        /// </summary>
        public const string CDNURL = "http://192.168.0.1/";
        /// <summary>
        /// 版本控制文件名
        /// </summary>
        public const string AssetsVersionName = "files.txt";
        /// <summary>
        /// 资源资产
        /// </summary>
        public const string Res = "_Res";
        /// <summary>
        /// 配置表路径
        /// </summary>
        public const string Temp = "Temp/";
        /// <summary>
        /// 地图路径
        /// </summary>
        public const string Nav = "Nav/";
        /// <summary>
        /// 动作文件路径
        /// </summary>
        public const string Anim = "Anim/";
        /// <summary>
        /// 动作状态路径
        /// </summary>
        public const string UnitState = "UnitState/";
        /// <summary>
#if UNITY_EDITOR
        /// <summary>
        /// 编辑器资源名
        /// </summary>
        public static string AssetsEditorResource = "/Editor Default Resources/";
#endif
        /// <summary>
        ///  资源服务器里版本控制文件
        /// </summary>
        public static string AssetsVersionOfServerPath = CDNURL + AssetsVersionName + "/";
        /// <summary>
        /// 版本控制文件路径
        /// </summary>
        public static string AssetsVersionPath = DataPath + AssetsVersionName;

        #region 获取指定路径下的所有文件名
        /// <summary>
        /// 获取指定路径下的所有文件名
        /// </summary>
        /// <returns></returns>
        public static string[] GetFiles(string path)
        {
            string[] fileNames = Directory.GetFiles(path);
            if (fileNames != null && fileNames.Length > 0)
                return fileNames;
            return null;
        }
        #endregion


        #region 取得数据存放目录  
        /// <summary>
        /// 取得数据存放目录
        /// </summary>
        public static string DataPath
        {
            get
            {
                string game = Config.AppName.ToLower();
                //persistentDataPath
                //该文件存在手机沙盒中，因此不能直接存放文件，
                //1.通过服务器直接下载保存到该位置，也可以通过Md5码比对下载更新新的资源
                //2.没有服务器的，只有间接通过文件流的方式从本地读取并写入Application.persistentDataPath文件下，然后再通过Application.persistentDataPath来读取操作。
                //注：在Pc/Mac电脑 以及Android跟Ipad、ipone都可对文件进行任意操作，另外在IOS上该目录下的东西可以被iCloud自动备份。  
                if (Application.isMobilePlatform)
                {
                    return Application.persistentDataPath + "/" + game + "/";
                }
                string path = Application.dataPath;
                if (GameSetting.IsDebug)
                {
                    string outputPath = Application.dataPath;
                    outputPath = outputPath.Replace(string.Format("{0}/", GameSetting.ProjectName), "");
#if UNITY_EDITOR
                    outputPath = outputPath + string.Format("/{0}/", UnityEditor.EditorUserBuildSettings.activeBuildTarget.ToString());
#else
                outputPath = outputPath + string.Format("/{0}/", Application.platform.ToString());
#endif
                    return outputPath;
                }
                path = path.Replace("Assets", "");
                return path + "resource/";
            }
        }
        #endregion

        #region 检查资源路径
        /// <summary>
        /// 检查资源路径
        /// </summary>
        /// <param name="path"> 路径 </param>
        /// <param name="isCreate"> 是否创建路径 </param>
        /// <returns></returns>
        public static bool IsCheckPathDirname(string path, bool isCreate = false)
        {
            if (!Directory.Exists(path))
            {
                MessageBox.Log("资源路径不存在！", path);
                if (isCreate) return false;
                MessageBox.Log("创建资源路径！", path);
                Directory.CreateDirectory(path);
            }
            return true;
        }
        #endregion
    }
}
