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
    public class CreateScriptBase : UnityEditor.Editor
    {
        #region 脚本枚举
        public static ScriptType CreateScriptType = ScriptType.None;
        #endregion

        #region 私有函数
        /// <summary>
        /// 创建新脚本
        /// </summary>
        /// <param name="templateName"></param>
        protected static void CreateNewScript(string path, string templateName)
        {
            string installPath = Config.PathConfig[ConfigKey.InstallPath];
            if (string.IsNullOrEmpty(installPath)) return;
            installPath = installPath + templateName;
            string tempPath = installPath;
            string fileName = installPath;
            if (Directory.Exists(tempPath))
            {
                if (EditorUtility.DisplayDialog("错  误", "模板路径<" + tempPath + ">没有找到指定模板！", "确定"))
                    return;
            }
            if (fileName.ToLower().Contains("-"))
            {
                string[] file = fileName.Split('-');
                if (file.Length > 0) fileName = file[file.Length - 1];
                fileName = fileName.Replace(".txt", "");
            }
            string extension = Path.GetExtension(fileName);
            Texture2D icon;
            if (extension != null)
            {
                if (extension == ".js")
                {
                    CreateScriptType = ScriptType.None;
                    icon = (EditorGUIUtility.IconContent("js Script Icon").image as Texture2D);
                    goto IL_16F;
                }
                if (extension == ".cs")
                {
                    CreateScriptType = ScriptType.C_Sharp;
                    icon = (EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D);
                    goto IL_16F;
                }
                if (extension == ".boo")
                {
                    icon = (EditorGUIUtility.IconContent("boo Script Icon").image as Texture2D);
                    goto IL_16F;
                }
                if (extension == ".shader")
                {
                    icon = (EditorGUIUtility.IconContent("Shader Icon").image as Texture2D);
                    goto IL_16F;
                }
            }
            icon = (EditorGUIUtility.IconContent("TextAsset Icon").image as Texture2D);
        IL_16F:
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, ScriptableObject.CreateInstance<MyProjectWindowUtil>(), string.Format("{0}/{1}", path, fileName), icon, tempPath);
        }
        #endregion
    }
}
