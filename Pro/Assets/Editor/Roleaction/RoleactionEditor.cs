/**  
* 标    题：   SkillEditor.cs 
* 描    述：    
* 创建时间：   2019年01月29日 02:50 
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
	public partial class RoleactionEditor : EditorWinBase<RoleactionEditor>
    {

        #region 保护函数
        protected override void Init()
        {
            Title = "动作编辑器";
            InitData();
            base.Init();
            InitComplete();
        }

        /// <summary>
        /// 绘制UI
        /// </summary>
        protected override void CustomGUI()
        {
            EditorUI.SetLabelWidth(80);
            if (Player == null) DrawList();
            else DrawModelInfo();

        }

        protected override void CustomUpdate()
        {
            if (SelectAnimInfo == null) return;
            SelectAnimInfo.OnUpdate();
        }

        /// <summary>
        /// 右键菜单
        /// </summary>
        protected override void CustomRightMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("创建"), false, CreateUnitView, "Create");
            menu.ShowAsContext();
        }
        #endregion

        #region 绘制UI

        /// <summary>
        /// 绘制List
        /// </summary>
        private void DrawList()
        {
            if (Temps != null)
            {
                for (int i = 0; i < Temps.Count; i++)
                {
                    if (GUILayout.Button(Temps[i].model, "button", GUILayout.Height(BtnH)))
                    {
                        UnitMgr.Instance.TempID = Temps[i].id;
                        CustomRightMenu();
                    }
                }
            }
        }

        /// <summary>
        /// 绘制模型list 
        /// </summary>
        private void DrawModelInfo()
        {
            ModelTemp temp = Player.MTemp;
            EditorGUILayout.BeginVertical();
            GUILayout.Toggle(true, string.Format("{0}({1})", temp.name, temp.model), "dragtab");
            EditorGUILayout.BeginHorizontal();
            DrawAnimList();
            DrawAnimPro();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制动作列表
        /// </summary>
        private void DrawAnimList()
        {
            GUILayout.Box(new GUIContent("动作组"), ListStyle, new[] { GUILayout.Width(ListRect.width), GUILayout.Height(ListRect.height + 20)});
            GUILayout.BeginArea(ListRect);
            ListPos = EditorGUILayout.BeginScrollView(ListPos);
            if(AnimInfos.Count > 0)
            {
                for(int i = 0; i < AnimInfos.Count; i ++)
                {
                    DrawSelectBtn(i);
                }
            }
            ChangeSelect();
            if(GUILayout.Button("+"))
            {
                AnimInfoEditor info = new AnimInfoEditor();
                AnimInfos.Add(info);
            }
            EditorGUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        private void DrawAnimPro()
        {
            if (SelectAnimInfo == null) return;
            GUILayout.Box(new GUIContent("基础属性"), ListStyle, new[] { GUILayout.Width(ListRect.width), GUILayout.Height(ListRect.height + 20) });
            GUILayout.BeginArea(BaseProRect);
            SelectAnimInfo.DrawPropertyGUI();
            GUILayout.EndArea();
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 创建模型
        /// </summary>
        private void CreateUnitView(object obj = null)
        {
            if (CreateUnit() == false)
            {
                UpdateNotification("创建模型失败！！！！");
                return;
            }
            GetUnitAnim();
        }

        private bool CreateUnit()
        {
            Player = Divak.Script.Game.UnitMgr.Instance.CreatePlayer(101, string.Empty, Vector3.zero);
            if (Player == null) return false;
            Selection.activeGameObject = Player.Model;
            SceneView view = SceneView.lastActiveSceneView;
            if(view != null)
            {
                view.pivot = Vector3.zero;
                view.size = 20f;
                view.orthographic = true;
                view.MoveToView(Player.Trans);
            }
            return true;
        }
        #endregion

        #region 公开函数

        #endregion

    }
}