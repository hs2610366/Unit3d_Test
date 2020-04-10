/**  
* 标    题：   MapArrayView.cs 
* 描    述：    
* 创建时间：   2018年06月12日 02:17 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Divak.Script.Game;

namespace Divak.Script.Editor 
{

    public class SceneArrayBase<T> : EditorWinBase<T>
    {
        #region 数据
        protected List<SceneTemp> sList;
        #endregion

        #region 参数
        private int btnW = 296;
        private int btnH = 30;
        #endregion


        #region 保护函数
        protected override void Init()
        {
            Title = "场景列表";
            ContextRect = new Rect(300, 300, 300, 300);
            base.Init();
            TempMgr.Init();
            sList = SceneTempMgr.Instance.Temps;
            InitComplete();
        }

        protected override void CustomGUI()
        {
            if (sList!= null)
            {
                for (int i = 0; i < sList.Count; i++)
                {
                    if(GUILayout.Button(sList[i].name, "button", GUILayout.Width(btnW), GUILayout.Height(btnH)))
                    {
                        SceneMgr.Instance.CurEditorTemp = sList[i];
                        CustomRightMenu();
                    }
                }
            }
        }

        #region 右键菜单
        protected override void CustomRightMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("打开"), false, OpenUnitView, "Open");
            menu.ShowAsContext();
        }
        #endregion

        protected override void CustomDestroy()
        {
        }

        #endregion

        #region 私有函数

        #region 逻辑
        protected virtual void OpenUnitView(object obj = null)
        {
            AssetDatabase.Refresh();

            this.Close();
        }
        #endregion

        #endregion

        #region 公开函数
        #endregion
    }
}