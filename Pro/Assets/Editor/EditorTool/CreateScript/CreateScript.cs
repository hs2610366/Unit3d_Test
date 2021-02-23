/**  
 * 标题：   Schedule.java 
 * 描述：   编辑器监视
 * 创建：   2017-03-02  
 * 作者：   by T.Y.Divak
 * 详细：   自定义修改Unity3d编辑器创建脚本
 */
using System.IO;
using UnityEditor;
using UnityEngine;
using Divak.Script.Game;


namespace Divak.Script.Editor
{
    public class CreateScript : CreateScriptBase
    {
        #region 模板名
        private const string ScriptName = "/81-C# Script-NewBehaviourScript.cs.txt";
        private const string TemplateName = "/93-C# Script-NewTemp.cs.txt";
        private const string UxmlName = "/94-UXML-NewUXMLTemplate.uxml.txt";
        #endregion

        #region 创建脚本
        /// <summary>
        /// 创建C#脚本
        /// </summary>
        [MenuItem("Assets/Create/C# Script (Custom)", false, 51)]
        public static void CreateCSharpScript()
        {
            if(Config.Init())
            {
                string path = PathEditorTool.GetSelectedPath();
                if (PathEditorTool.AssetSavePathIsNullOrEmpty(path)) return;
                if (PathEditorTool.IsNotScriptPath(path)) return;
                CreateNewScript(path, ScriptName);
            }
        }

        /// <summary>
        /// 创建C#配置表脚本
        /// </summary>
        [MenuItem("Assets/Create/C# Temp (Custom)", false, 52)]
        public static void CreateCSharpTemp()
        {
            if (Config.Init())
            {
                string path = PathEditorTool.GetSelectedPath();
                if (PathEditorTool.AssetSavePathIsNullOrEmpty(path)) return;
                if (PathEditorTool.IsNotScriptPath(path)) return;
                CreateNewScript(path, TemplateName);
            }
        }


        /// <summary>
        /// 创建UXML脚本
        /// </summary>
        [MenuItem("Assets/Create/UI Elements UXML (Custom)", false, 53)]
        public static void CreateUIElementsUXML()
        {
            if (Config.Init())
            {
                string path = PathEditorTool.GetSelectedPath();
                if (PathEditorTool.AssetSavePathIsNullOrEmpty(path)) return;
                if (PathEditorTool.IsNotScriptPath(path)) return;
                CreateNewScript(path, UxmlName);
            }
        }
        #endregion
    }
}
