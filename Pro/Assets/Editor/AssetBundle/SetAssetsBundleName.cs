/**  
* 标    题：   SetAssetsBundleName.cs 
* 描    述：   设置assetBundleName
* 创建时间：   2017年07月15日 02:35 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Divak.Script.Game;

namespace Divak.Script.Editor 
{
	public class SetAssetsBundleName
    {


        #region 功能
        public static void SetAssetBundleName()
        {
            UnityEngine.Object obj =  Selection.activeObject;
            string path = AssetDatabase.GetAssetPath(obj);
            UpdateAssetImporter(path);

        }

        /// <summary>
        ///  递归更新AssetImporter
        /// </summary>
        /// <param name="path"></param>
        public static void UpdateAssetImporter(string path)
        {
            if(CheckAssetsDuplicationNname.Check(path))
            {
                return;
            }
            AssetImporter ai = AssetImporter.GetAtPath(path);
            if (ai == null)
            {
                EditorUtility.DisplayDialog("错误", string.Format("AssetImporter未成功找到路径：{0}", path), "确定");
                return;
            }
            string name = Path.GetFileName(path);
            string suffix = Path.GetExtension(name);
            AssetType type = EnumEditorTool.GetAssetType(suffix);
            if (!EnumEditorTool.IsAllowSetAssetBundle(type)) return;
            SetAssetBundleName(ref ai, name);
            //获得关联资源路径
            string[] dependencys = AssetDatabase.GetDependencies(path, false);
            string dependency = string.Empty;
            for (int i = 0; i < dependencys.Length; i++)
            {
                dependency = dependencys[i];
                if (string.IsNullOrEmpty(dependency)) continue;
                string dependencyName = Path.GetFileName(dependency);
                if (dependencyName.Contains(name)) continue;
                UpdateAssetImporter(dependency);
            }
        }

        /// <summary>
        /// 递归更新AssetImporter
        /// </summary>
        /// <param name="path"> 资源路径 </param>
        /// <param name="parentName"> 关联更节点资源AssetBundleName </param>
        /// <param name="isDependency"> 是否是关联资源 </param>
        public static void UpdateAssetImporter(string path, string parentName, bool isDependency = false, Func<string, string, bool, string> callback = null)
        {
            AssetImporter ai = AssetImporter.GetAtPath(path);
            if (ai == null)
            {
                EditorUtility.DisplayDialog("错误", string.Format("AssetImporter未成功找到路径：{0}", path), "确定");
                return;
            }
            string name = Path.GetFileName(path);
            string suffix = Path.GetExtension(name);
            AssetType type = EnumEditorTool.GetAssetType(suffix);
            if (!EnumEditorTool.IsAllowSetAssetBundle(type)) return;
            if(callback != null) parentName = callback(name, parentName, isDependency);
            if (string.IsNullOrEmpty(parentName))
                SetAssetBundleName(ref ai, name);
            else
                SetAssetBundleName(ref ai, parentName);
            //获得关联资源路径
            string[] dependencys = AssetDatabase.GetDependencies(path, false);
            string dependency = string.Empty;
            for (int i = 0; i < dependencys.Length; i++)
            {
                dependency = dependencys[i];
                if (string.IsNullOrEmpty(dependency)) continue;
                string dependencyName = Path.GetFileName(dependency);
                if (dependencyName.Contains(name)) continue;
                UpdateAssetImporter(dependency, isDependency == false ? name : ai.assetBundleName, true, callback);
            }

        }


        /// <summary>
        /// 设置AssetBundleName
        /// </summary>
        /// <param name="assetBundle"> 需要设置的资源 </param>
        /// <param name="name"> 设置的AssetBundleName </param>
        /// <param name="type"> 资源类型 </param>
        /// <returns></returns>
        public static string SetAssetBundleName(ref AssetImporter assetBundle, string name)
        {
            string path = name;
            assetBundle.assetBundleName = path.ToLower();
            assetBundle.assetBundleVariant = AssetType.DEMO.ToString().ToLower();
            return path + SuffixTool.AssetName;
        }

        #endregion
    }
}