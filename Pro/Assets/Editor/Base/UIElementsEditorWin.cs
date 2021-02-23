/**  
* 标    题：   UIElementsEditorWin.cs 
* 描    述：    
* 创建时间：   2020年12月03日 16:48 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/
using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Divak.Script.Editor;

namespace Divak.Script.Editor 
{
	public class UIElementsEditorWin<T> : EditorWindow
    {
        protected bool mIsInit = false;
        protected string mUXML = string.Empty;
        protected string mUSS = string.Empty;
        protected StyleSheet mCommonStyle;
        protected VisualTreeAsset mVisualTree;
        protected TemplateContainer mContainer;
        protected VisualElement mRoot;

        protected Action<object> Callback;

        public static void ShowWin(string title = null, Action<object> callback = null)
        {
            Type t = typeof(T);
            EditorWindow win = EditorWindow.GetWindow(t, false, string.IsNullOrEmpty(title) ? t.Name : title, true);
            if (win != null && win is UIElementsEditorWin<T>)
            {
                if (callback != null) (win as UIElementsEditorWin<T>).Callback = callback;
                (win as UIElementsEditorWin<T>).Init();
            }
        }

        #region 私有函数

        /// <summary>
        /// ui
        /// </summary>
        private void OnGUI()
        {
            if (mIsInit) return;
            CustomGUI();
        }

        /// <summary>
        /// 无效
        /// </summary>
        private void OnDrawGizmos()
        {
            if (mIsInit) return;
            CustomDrawGizmos();
        }

        /// <summary>
        /// 更新
        /// </summary>
        private void Update()
        {
            CustomUpdate();
        }/// <summary>
         /// 当窗口获得焦点时调用一次
         /// </summary>
        private void OnFocus()
        {
        }

        /// <summary>
        /// 当窗口丢失焦点时调用一次
        /// </summary>
        private void OnLostFocus()
        {
        }

        /// <summary>
        /// 当Hierarchy视图中的任何对象发生改变时调用一次
        /// </summary>
        private void OnHierarchyChange()
        {
        }

        /// <summary>
        /// 当Project视图中的资源发生改变时调用一次
        /// </summary>
        private void OnProjectChange()
        {
        }

        /// <summary>
        /// 窗口面板的更新 
        /// 这里开启窗口的重绘，不然窗口信息不会刷新
        /// </summary>
        private void OnInspectorUpdate()
        {
            this.Repaint();
        }

        /// <summary>
        /// 当窗口出去开启状态，并且在Hierarchy视图中选择某游戏对象时调用
        /// 有可能是多选，这里开启一个循环打印选中游戏对象的名称
        /// </summary>
        private void OnSelectionChange()
        {
            foreach (Transform t in Selection.transforms)
            {
                Debug.Log("OnSelectionChange" + t.name);
            }
        }


        private void OnDisable()
        {
            CustomClose();
        }

        /// <summary>
        /// 当销毁
        /// </summary>
        private void OnDestroy()
        {
            CloseNotification();
            CustomDestroy();
        }
        #endregion

        #region 保护函数
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {
            SetSize();
            // this.position = ContextRect;
            UIStyles.Init();
            if (!EditorUI.GetVisualTreeAsset(mUXML, out mVisualTree))
            {
                UpdateNotification("编辑器描述文件读取失败");
                return;
            }
            if(!string.IsNullOrEmpty(mUSS))
            {
                if(!EditorUI.GetStyleSheet(mUSS, out mCommonStyle))
                {
                    UpdateNotification("通用样式读取失败");
                    return;
                }
            }
            mContainer = mVisualTree.CloneTree();
            mRoot = rootVisualElement;
            /** 不能初始化直接add  ToolbarMenu后续事件无法添加
            mRoot.Add(mContainer);
            if(mCommonStyle != null)
            {
                mContainer.styleSheets.Add(mCommonStyle);
            }
            */
            CustomInit();
        }
        /// <summary>
        /// 更新消息
        /// </summary>
        protected virtual void UpdateNotification(string msg)
        {
            if (string.IsNullOrEmpty(msg)) return;
            ShowNotification(new GUIContent(msg));
        }

        protected virtual T Load<T>(string path, string suffix) where T : UnityEngine.Object
        {
            UnityEngine.Object obj = EditorGUIUtility.Load(path + suffix);
            return obj as T;
        }

        /// <summary>
        /// 关闭消息
        /// </summary>
        protected virtual void CloseNotification()
        {
            RemoveNotification();
        }

        protected virtual void SetSize()
        {
            this.minSize = this.maxSize = Vector2.one * 800;
        }

        /// <summary>
        /// 自定义初始化
        /// </summary>
        protected virtual void CustomInit()
        {
            mIsInit = true;
            UpdateNotification("正在初始化数据...");
        }

        protected virtual void InitComplete()
        {
            UpdateNotification("初始化数据完成");
            mIsInit = false;
        }

        protected virtual void SetWinCfg(string uss = null, string uxml = null)
        {
            if(!string.IsNullOrEmpty(uss))
            {
                if (!EditorUI.GetStyleSheet(uss, out mCommonStyle))
                {
                    UpdateNotification("通用样式读取失败");
                    return;
                }
            }
            if (!string.IsNullOrEmpty(uxml))
            {
                if (!EditorUI.GetVisualTreeAsset(uxml, out mVisualTree))
                {
                    UpdateNotification("编辑器描述文件读取失败");
                    return;
                }
            }
        }

        protected virtual VisualElement AddVisualElement(string name)
        {
            if (mContainer == null) return null;
            if (mRoot == null) return null;
            VisualElement ve = mContainer.Q(name);
            mRoot.Add(ve);
            return ve;
        }

        /// <summary>
        /// 自定义UI
        /// </summary>
        protected virtual void CustomGUI()
        {
            RightMenu();
        }

        protected virtual void CustomDrawGizmos()
        {

        }

        protected virtual void CustomUpdate()
        {

        }

        /// <summary>
        /// 自定义关闭
        /// </summary>
        protected virtual void CustomClose()
        {

        }

        /// <summary>
        /// 自定义销毁
        /// </summary>
        protected virtual void CustomDestroy()
        {

        }

        /// <summary>
        /// 右键菜单
        /// </summary>
        protected virtual void RightMenu()
        {
            Event evt = Event.current;
            if (evt.type == EventType.ContextClick)
            {
                // Vector2 mousePos = Event.current.mousePosition;
                CustomRightMenu();
            }
            //evt.Use();
        }

        /// <summary>
        /// 自定义右键菜单
        /// </summary>
        protected virtual void CustomRightMenu()
        {

        }
        #endregion

    }
}