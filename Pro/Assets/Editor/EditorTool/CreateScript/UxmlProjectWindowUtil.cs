/**  
 * 标    题：   NewBehaviourScript.cs
 * 描    述：   
 * 创建时间：
 * 作    者：   by T.Y.Divak
 * 详    细：
 */
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using Divak.Script.Game;

namespace Divak.Script.Editor
{
    class UxmlProjectWindowUtil : EndNameEditAction
    {

        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            UnityEngine.Object o = CreateScriptAssetFromTemplate(pathName, resourceFile);
            ProjectWindowUtil.ShowCreatedAsset(o);
        }
        internal static void DuplicateSelectedAssets()
        {
            return;
        }

        #region 创建脚本
        internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName, string resourceFile)
        {
            string fullPath = Path.GetFullPath(pathName);
            string text = File.ReadAllText(resourceFile);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName);
            string text2 = fileNameWithoutExtension.Replace(" ", "");
            text = text.Replace("#SCRIPTNAME#", text2);
            if (char.IsUpper(text2, 0))
            {
                text2 = char.ToLower(text2[0]) + text2.Substring(1);
                text = text.Replace("#SCRIPTNAME_LOWER#", text2);
            }
            else
            {
                text2 = "my" + char.ToUpper(text2[0]) + text2.Substring(1);
                text = text.Replace("#SCRIPTNAME_LOWER#", text2);
            }
            UTF8Encoding encoding = new UTF8Encoding(true);
            File.WriteAllText(fullPath, text, encoding);
            AssetDatabase.ImportAsset(pathName);
            CreateScript.CreateScriptType = ScriptType.None;
            return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
        }

        /// <summary>
        /// 设置导入包
        /// </summary>
        /// <param name="pathName"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string SetImportPackage(string pathName, string text)
        {
            switch (CreateScript.CreateScriptType)
            {
                case ScriptType.C_Sharp:
                    if (pathName.Contains("/Editor/") && !text.Contains("using UnityEditor;"))
                    {
                        text = text.Replace("\n\npublic class", "\nusing UnityEditor;\n\npublic class");
                    }
                    if (pathName.Contains("/Script/") && !text.Contains("using UnityEngine;"))
                    {
                        text = text.Replace("\n\npublic class", "\nusing UnityEngine;\n\npublic class");
                    }
                    break;
            }
            return text;
        }

        /// <summary>
        /// 设置命名空间
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string SetNamespace(string pathName, string text)
        {
            switch (CreateScript.CreateScriptType)
            {
                case ScriptType.C_Sharp:
                    string key = "Game";
                    if (pathName.Contains("Assets/Editor/"))
                    {
                        key = "Editor";
                    }
                    text = text.Replace("\n\npublic class", "\n\nnamespace Divak.Script." + key + " \n{\npublic class");
                    text = text.Replace("\t", "\t\t");
                    text = text.Remove(text.Length - 2);
                    text += "\n\t}\n}";
                    text = text.Replace("\npublic class", "\n\tpublic class");
                    text = text.Replace("[Serializable]", "\n[Serializable]");
                    break;
            }
            return text;
        }
        #endregion
    }
}
