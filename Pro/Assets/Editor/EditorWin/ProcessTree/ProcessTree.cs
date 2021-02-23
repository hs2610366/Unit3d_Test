/**  
* 标    题：   ProcessTree.cs 
* 描    述：    
* 创建时间：   2020年12月21日 17:45 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Divak.Script.Game;

namespace Divak.Script.Editor
{
    public class ProcessTree : UIElementsEditorWin<ProcessTree>
    {
        private bool mIsMoveScrollView = false;
        private Vector2 mMousePos = Vector2.zero;
        private ScrollView mScrollView;
        private VisualElement mDragRect;

        protected override void SetSize()
        {
            this.minSize = this.maxSize = new Vector2(1800, 1000);
        }

        protected override void Init()
        {
            mUXML = "ProcessTree";
            base.Init();
        }

        protected override void CustomInit()
        {
            base.CustomInit();

            if (!Config.IsInt)
            {
                if (Config.Init())
                {
                    //if (!Analysis()) return;
                }
                else
                {
                    mIsInit = false;
                    return;
                }
            }
            else
            {
                //if (!Analysis()) return;
            }
            SetWinCfg();
            InitComplete();
            DrawUI();
           // DecodeExcel(0);
        }

        #region 绘制UI
        private void DrawUI()
        {
            VisualElement toolRoot = AddVisualElement("tool_root");
            ToolbarMenu menu = toolRoot.Q<ToolbarMenu>("menu");
            if(menu != null)
            {
                menu.menu.AppendAction("读取", MenuLoadCfg, DropdownMenuAction.Status.Normal);
                menu.menu.AppendAction("保存", MenuSaveCfg, DropdownMenuAction.Status.Normal);
            }
            VisualElement mainRoot = AddVisualElement("main_root");
            ListView lv = mainRoot.Q<ListView>("root_node_list");
            InitNodes(lv.contentContainer);
            ScrollView sv = mainRoot.Q<ScrollView>("scroll_view");
            sv.RegisterCallback<MouseDownEvent>(OnMouseDownEventCallback);
            sv.RegisterCallback<MouseUpEvent>(OnMouseUpEventCallback);
            sv.RegisterCallback<MouseMoveEvent>(OnMouseMoveEventCallback);
            mScrollView = sv;
            mDragRect = sv.Q("drag_rect");

        }


        #endregion

        #region 私有函数
        private void InitNodes(VisualElement ve)
        {
            var count = ve.childCount;
            if(count > 0)
            {
                for (int i = 0; i < count; i ++)
                {
                    var node = ve.ElementAt(i);
                    if(node == null) continue;
                    node.RegisterCallback<MouseUpEvent, VisualElement>(OnSelectNodeCallback, node);
                }
            }
        }
        #endregion

        #region 事件回调
        private void MenuLoadCfg(DropdownMenuAction action)
        {
            Debug.LogError("============== LoadCfg");
        }

        private void MenuSaveCfg(DropdownMenuAction action)
        {
            Debug.LogError("============== SaveCfg");
        }

        private void OnMouseDownEventCallback(MouseDownEvent evt)
        {
            mMousePos = evt.localMousePosition;
            mIsMoveScrollView = true;
        }

        private void OnMouseUpEventCallback(MouseUpEvent evt)
        {
            mIsMoveScrollView = false;
        }

        private void OnMouseMoveEventCallback(MouseMoveEvent evt)
        {
            if (mIsMoveScrollView == true)
            {
                var offset = evt.localMousePosition - mMousePos;
                mMousePos = evt.localMousePosition;
                mScrollView.scrollOffset -= offset;
            }
        }

        private void OnSelectNodeCallback(MouseUpEvent evt, VisualElement btn)
        {
            var ve = TreeMgr.Instance.Create(btn.name);
            mDragRect.Add(ve);
        }
        #endregion

    }
}