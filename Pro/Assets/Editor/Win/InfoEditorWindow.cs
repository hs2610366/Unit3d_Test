/**  
* 标    题：   EditorWinBase.cs 
* 描    述：   脚本编辑器基类
* 创建时间：   2017年11月19日 02:58 
* 作    者：   by. T.Y.Divak
* 详    细：   创建脚本相关编辑器
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
	public class InfoEditorWindow : EditorWinBase<InfoEditorWindow>
    {
        #region 变量
        #endregion

        #region 初始化
        protected override void Init()
        {
            base.Init();
            ContextRect = position;
        }
        protected override void CustomInit()
        {
            base.CustomInit();
            if(!Config.IsInt)
            {
                if (Config.Init())
                {
                    CreateDicKey();
                    InitComplete();
                }
                else
                {
                    CreateDicKey();
                    InitComplete();
                }
            }
            else
            {
                InitComplete();
            }
        }

        private void CreateDicKey()
        {
            Dictionary<string, string> dic = Config.PathConfig;
            if (!dic.ContainsKey(ConfigKey.CreateName))
                Config.PathConfig.Add(ConfigKey.CreateName,string.Empty);
            if (!dic.ContainsKey(ConfigKey.InstallPath))
                Config.PathConfig.Add(ConfigKey.InstallPath, string.Empty);
            if (!dic.ContainsKey(ConfigKey.AssetsPath))
                Config.PathConfig.Add(ConfigKey.AssetsPath, string.Empty);
            if (!dic.ContainsKey(ConfigKey.TablePath))
                Config.PathConfig.Add(ConfigKey.TablePath, string.Empty);
            if (!dic.ContainsKey(ConfigKey.CSharpDllPath))
                Config.PathConfig.Add(ConfigKey.CSharpDllPath, string.Empty);
        }
        #endregion

        #region 刷新GUI
        protected override void CustomGUI()
        {
            OnSetScriptCreatorGUI();
            base.CustomGUI();
        }
        #endregion

        #region 绘制编辑器GUI
        /// <summary>
        /// 绘制设置脚本创建者名字GUI
        /// </summary>
        private void OnSetScriptCreatorGUI()
        {
            if(Config.PathConfig.ContainsKey(ConfigKey.CreateName))
            {
                EditorGUILayout.Space();
                Config.PathConfig[ConfigKey.CreateName] = EditorUI.DrawPrefabsTextField(Config.PathConfig[ConfigKey.CreateName], "脚本创建者");
            }
            if(Config.PathConfig.ContainsKey(ConfigKey.InstallPath))
            {
                EditorGUILayout.Space();
                Config.PathConfig[ConfigKey.InstallPath] = EditorUI.DrawPrefabTextFieldPath(Config.PathConfig[ConfigKey.InstallPath], "预设脚本路径");
            }
            if (Config.PathConfig.ContainsKey(ConfigKey.AssetsPath))
            {
                EditorGUILayout.Space();
                Config.PathConfig[ConfigKey.AssetsPath] = EditorUI.DrawPrefabTextFieldPath(Config.PathConfig[ConfigKey.AssetsPath], "资源路径");
            }
            if (Config.PathConfig.ContainsKey(ConfigKey.TablePath))
            {
                EditorGUILayout.Space();
                Config.PathConfig[ConfigKey.TablePath] = EditorUI.DrawPrefabTextFieldPath(Config.PathConfig[ConfigKey.TablePath], "配置表路径");
            }
            if (Config.PathConfig.ContainsKey(ConfigKey.CSharpDllPath))
            {
                EditorGUILayout.Space();
                Config.PathConfig[ConfigKey.CSharpDllPath] = EditorUI.DrawPrefabTextFieldPath(Config.PathConfig[ConfigKey.CSharpDllPath], "工程dll路径");
            }
            //EditorUI.PrefabsObjectField("key", "脚本", typeof(MonoScript), SuffixTool.CS);
            //EditorGUILayout.ObjectField(obj, typeof(ScriptableObject));
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            if(GUILayout.Button("关    闭"))
            {
                this.Close();
            }
        }
        #endregion

        #region 右键菜单
        protected override void CustomRightMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("保存并关闭"), false, SaveInfoAndClose, "Save");
            menu.ShowAsContext();
        }
        #endregion

        #region 逻辑
        private void SaveInfoAndClose(object obj = null)
        {
            Config.PathConfigOutput();
            AssetDatabase.Refresh();
            this.Close();
        }
        #endregion

        #region 重构
        protected override void CustomDestroy()
        {
        }
        #endregion
    }
}