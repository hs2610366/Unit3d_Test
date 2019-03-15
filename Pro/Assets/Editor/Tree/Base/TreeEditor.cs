/**  
* 标    题：   ThreeEditor.cs 
* 描    述：    
* 创建时间：   2018年08月07日 00:14 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Divak.Script.Game;

namespace Divak.Script.Editor
{
    public class TreeEditor<T> : EditorWinBase<T>
    {
        #region 变量
        protected BtnGuidInfo[] TypeInfos;

        protected Vector2 TypeSV = Vector2.zero;

        protected const int TypeW = 200;
        protected const int TypeH = 50;
        #endregion

        #region 保护函数
        protected override void Init()
        {
            ContextRect = InfoTool.GetViewRect();
            base.Init();
        }

        protected override void CustomGUI()
        {
            NodeListNode();
            base.CustomGUI();
        }
        /// <summary>
        /// 节点列表窗口
        /// </summary>
        protected virtual void NodeListNode()
        {
            float w = TypeW + 20;
            float h = TypeH * 10;
            TypeSV = EditorGUILayout.BeginScrollView(TypeSV, true, true, GUILayout.Width(w), GUILayout.Height(h));
            GUILayout.BeginArea(new Rect(0,0,w, h), "EditorWindow");
            GUILayout.Toggle(true, "节点类型：", "dragtab", GUILayout.Width(TypeW));
            CustomNodeListNode();
            GUILayout.EndArea();
            EditorGUILayout.EndScrollView();
        }

        protected virtual void CustomNodeListNode() { }
        #region 私有函数
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
            // Config.PathConfigOutput();
            // AssetDatabase.Refresh();
            this.Close();
        }
        #endregion

        #region 重构
        protected override void CustomDestroy()
        {
        }
        #endregion
        #endregion
    }
}