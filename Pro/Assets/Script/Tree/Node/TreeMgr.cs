/**  
* 标    题：   BehaviorTreeMgr.cs 
* 描    述：   行为树管理
* 创建时间：   2018年08月07日 01:10 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEngine.UIElements;
#endif

namespace Divak.Script.Game 
{
    /// <summary>
    /// 树状态
    /// </summary>
    public enum BTStatus
    {
        Success,
        Failure,
        Running
    }

    public class TreeMgr
    {
        public static readonly TreeMgr Instance = new TreeMgr();


#if UNITY_EDITOR
        public Dictionary<string, BaseNode> Nodes = new Dictionary<string, BaseNode>();
        private Vector2 offetMove = Vector2.zero;


        public VisualElement Create(string name)
        {
            BaseNode node = null;
            switch (name)
            {
                case "process_node":
                    node = new ProcessRootNode();
                    break;
            }
            if(node != null)
            {
                node.Create();
                if (string.IsNullOrEmpty(node.MD5Code)) return null;
                VisualElement ve = node.VE;
                ve.RegisterCallback<MouseDownEvent, VisualElement>(OnMouseDownEventCallback, ve);
                ve.RegisterCallback<MouseUpEvent, VisualElement>(OnMouseUpEventCallback, ve);
                ve.RegisterCallback<MouseMoveEvent, VisualElement>(OnMouseMoveEventCallback, ve);
                Nodes.Add(node.MD5Code, node);
                return node.VE;
            }
            return null;
        }

        private void OnMouseDownEventCallback(MouseDownEvent evt, VisualElement ve)
        {
            offetMove =  evt.localMousePosition;
           // mIsMoveScrollView = true;
        }

        private void OnMouseUpEventCallback(MouseUpEvent evt, VisualElement ve)
        {
           // mIsMoveScrollView = false;
        }

        private void OnMouseMoveEventCallback(MouseMoveEvent evt, VisualElement ve)
        {
          //  if (mIsMoveScrollView == true)
            {
             //   var offset = evt.localMousePosition - mMousePos;
            //    mMousePos = evt.localMousePosition;
           //     mScrollView.scrollOffset -= offset;
            }
        }
#endif
    }
}