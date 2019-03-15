/**  
* 标    题：   PathEditorTool.cs 
* 描    述：   后缀工具
* 创建时间：   2017年04月13日 01:07 
* 作    者：   by. T.Y.Divak 
* 详    细：   方便添加字符串后缀
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Divak.Script.Game;

public class PathEditorTool
{

    /// <summary>
    /// 需要打包的預製體路徑
    /// </summary>
    public static string AssetsFolderName = "Assets/Scene/";
    public static string AssetsPrefabName = "Scene/Prefab/";
    public static string AssetsEditorResource = "/Editor Default Resources/";
    public static string SceneModPath = "Assets/Resource/Scene/";

    #region 当前选中路径为NULL
    /// <summary>
    /// 当前资源路径为NULL
    /// </summary>
    /// <returns></returns>
    public static bool AssetSavePathIsNullOrEmpty()
    {
        string path = GetSelectedPath();
        return AssetSavePathIsNullOrEmpty(path);
    }

    /// <summary>
    /// 当前资源路径为NULL
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns></returns>
    public static bool AssetSavePathIsNullOrEmpty(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            EditorUtility.DisplayDialog("错  误", "请选择资源保存路径！", "确定");
            return true;
        }
        return false;
    }
    #endregion

    #region 脚本路径
    /// <summary>
    /// 不是脚本路径
    /// </summary>
    /// <returns></returns>
    public static bool IsNotScriptPath()
    {
        string path = GetSelectedPath();
        return IsNotScriptPath(path);
    }

    /// <summary>
    /// 不是脚本路径
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns></returns>
    public static bool IsNotScriptPath(string path)
    {
        if (!path.Contains("/Editor") && !path.Contains("/Script"))
        {
            EditorUtility.DisplayDialog("错  误", "该路径下不能创建脚本！", "确定");
            return true;
        }
        return false;
    }
    #endregion

    #region 获取选中对象的路径
    /// <summary>
    /// 获取选中对象的路径
    /// </summary>
    /// <returns></returns>
    public static string GetSelectedPath()
    {
        //默认路径为Assets
        string selectedPath = "Assets";

        //获取选中的资源
        Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);

        //遍历选中的资源以返回路径
        foreach (Object obj in selection)
        {
            selectedPath = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(selectedPath) && File.Exists(selectedPath))
            {
                selectedPath = Path.GetDirectoryName(selectedPath);
                break;
            }
        }

        return selectedPath;
    }

    /// <summary>
    /// 獲得資源相對路徑
    /// </summary>
    /// <param name="name"> 資源名字 </param>
    /// <returns></returns>
    public static string GetAssetsPath(string name)
    {
        string path = Application.dataPath.Substring(Application.dataPath.LastIndexOf('/') + 1) + "/" + name;
        return path;
    }
    #endregion

    #region 获取资源的相对路径
    /// <summary>
    /// 通过资源名字获得相对路径
    /// </summary>
    /// <param name="name"></param>
    public static string GetRelativePathForName(string name)
    {
        string suffix = Path.GetExtension(name);
        AssetType type = EnumEditorTool.GetAssetType(suffix);
        return AssetsFolderName + type.ToString() + "/" + name;
    }
    #endregion

}
