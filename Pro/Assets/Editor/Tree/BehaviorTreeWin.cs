/**  
* 标    题：   BehaviorTreeWin.cs 
* 描    述：    
* 创建时间：   2018年08月07日 00:16 
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
    public class BehaviorTreeWin : TreeEditor<BehaviorTreeWin>
    {
        public enum BTType
        {
            COMPOSITE,
            DECORATOR,
            CONDITION,
            ACTION
        }

        #region 变量
        #endregion

        #region 初始化
        protected override void Init()
        {
            Title = "行为树";
            base.Init();
        }

        protected override void CustomInit()
        {
            base.CustomInit();
            TypeInfos = new BtnGuidInfo[]
            {
                new BtnGuidInfo("组合节点", Color.red),
                new BtnGuidInfo("修饰节点", Color.cyan),
                new BtnGuidInfo("条件节点（子节点）", Color.green),
                new BtnGuidInfo("动作节点（子节点）", Color.blue)
            };
            InitComplete();
        }
        #endregion

        #region 刷新GUI
        protected override void CustomNodeListNode()
        {
            if (TypeInfos != null)
            {
                BTType type = EditorUI.DrawSelectBtnGuid<BTType>(ref TypeInfos, TypeW, TypeH);
            }
        }
        #endregion

        #region 绘制编辑器GUI

        #endregion

        #region 重构
        protected override void CustomDestroy()
        {
        }
        #endregion
    }
}