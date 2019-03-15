/**  
* 标    题：   Config.cs 
* 描    述：    
* 创建时间：   2018年02月25日 02:58 
* 作    者：   by.  T.Y.Divak 
* 详    细：    
*/

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class Config
    {
        /// <summary>
        /// 配置文件名
        /// </summary>
        public static string Name = "Config";
        /// <summary>
        /// 工程名
        /// </summary>
        public static string ProjectName = "Pro";
        /// <summary>
        /// 应用程序名
        /// </summary>
        public const string AppName = "DemoProject";

        public static bool IsInt = false;

        #region 变量
        public static Dictionary<string, string> PathConfig = new Dictionary<string, string>();
        #endregion

        public static Boolean Init()
        {
            string path = string.Format("{0}/{1}/", PathTool.DataPath, Name);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            PathConfig = InputConfig<string, string>(path, Name);
            if (PathConfig == null || PathConfig.Count == 0)
            {
                if (PathConfig == null) PathConfig = new Dictionary<string, string>();
                return false;
            }
            IsInt = true;
            return true;
        }

        public static void PathConfigOutput()
        {
            string path = string.Format("{0}/{1}/", PathTool.DataPath, Name);
            OutputConfig<string, string>(path, Name, PathConfig);
        }

        #region 导入
        /// <summary>
        /// 导入配置表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<T, K> InputConfig<T, K>(string path, string name)
        {
            name = name.ToLower();
            Dictionary<T, K> dic = null;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string rName = string.Format("{0}{1}", name, SuffixTool.Config);
            byte[] bytes = BinaryFile.ReadBinaryFile(path, rName, TypeEnum.ByteArray);
            if (bytes != null && bytes.Length != 0)
            {
                dic = BinaryFile.GetDicByByte<T, K>(bytes);
            }
            return dic;
        }

        /// <summary>
        /// 导入配置
        /// </summary>
        /// <returns></returns>
        public static T InputConfig<T>(string path, string name, string suffix)
        {
            name = name.ToLower();
            T t = default(T);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string rName = string.Format("{0}{1}", name, suffix);
            byte[] bytes = BinaryFile.ReadBinaryFile(path, rName, TypeEnum.ByteArray);
            if (bytes != null && bytes.Length != 0)
            {
                t = BinaryFile.GetObjByByte<T>(bytes);
            }
            return t;
        }
        #endregion

        #region 对象

        /// <summary>
        /// 导出配置表
        /// </summary>
        public static void OutputConfig<T, K>(string path, string name, Dictionary<T, K> dic)
        {
            name = name.ToLower();
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            byte[] buff = BinaryFile.GetByteByDic<T, K>(dic);
            if (buff == null || buff.Length == 0) return;
            string rName = string.Format("{0}{1}", name, SuffixTool.Config);
            BinaryFile.SaveBinaryFile(path, rName, buff, TypeEnum.ByteArray);
        }

        public static void OutputConfig<T>(string path, string name, List<T> list, string suffix)
        {
            name = name.ToLower();
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            byte[] buff = BinaryFile.GetByteByList<T>(list);
            if (buff == null || buff.Length == 0) return;
            string rName = string.Format("{0}{1}", name, suffix);
            BinaryFile.SaveBinaryFile(path, rName, buff, TypeEnum.ByteArray);
        }

        public static void OutputConfig<T>(string path, string name, T obj, string suffix)
        {
            name = name.ToLower();
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            byte[] buff = BinaryFile.GetByteByObj<T>(obj);
            if (buff == null || buff.Length == 0) return;
            string rName = string.Format("{0}{1}", name, suffix);
            BinaryFile.SaveBinaryFile(path, rName, buff, TypeEnum.ByteArray);
        }
        #endregion
    }
}