/**  
* 标    题：   SkillEditor.cs 
* 描    述：    
* 创建时间：   2019年01月29日 02:50 
* 作    者：   by. T.Y.Divak 
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
            base.CustomGUI();

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
            menu.AddItem(new GUIContent("保存并导出动作状态"), false, SaveStateInfo, "Create");
            menu.AddItem(new GUIContent("保存并导出动作"), false, SaveAnimInfo, "Create");
            menu.AddItem(new GUIContent("保存并导出全部"), false, SaveInfo, "Create");
            menu.ShowAsContext();
        }
        #endregion

        #region 绘制UI
        /// <summary>
        /// 点击模型列表item
        /// </summary>
        private void ClickItemMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("展开"), false, CreateUnitView, "Create");
            menu.ShowAsContext();
        }

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
                        ClickItemMenu();
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
            GUILayout.Toggle(true, string.Format("{0}({1})", temp.name, temp.model), UIStyles.TTDD_White);
            EditorGUILayout.BeginHorizontal();
            DrawUnitState();
            DrawAnimList();
            DrawAnimPro();
            DrawAnimGroup();
            DrawPlay();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(458);
            DrawAnimBreakGroup();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制角色状态
        /// </summary>
        private void DrawUnitState()
        {
            GUILayout.BeginArea(StateRect, UIStyles.SelectionRect);
            GUILayout.Label("角色状态", UIStyles.DO_18_White_UpperCenter, GUILayout.Height(20));
            GUILayout.Space(4);
            StatePos = EditorGUILayout.BeginScrollView(StatePos);
            for(int i = 0; i < Player.States.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Button(Player.States[i].ToString(), UIStyles.FN1, GUILayout.Width(120));
                string[] names = StatesList.ToArray();
                int index = GetUnitStatesIndex(i);
                int select = EditorGUILayout.Popup(string.Empty, index, names, UIStyles.FN0, GUILayout.Width(100), GUILayout.Height(40));
                if(select != index)
                {
                    ChangeUnitStates(i, select);
                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(10);
            }
            EditorGUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        /// <summary>
        /// 绘制动作列表
        /// </summary>
        private void DrawAnimList()
        {
            // EditorUI.DrawBox("动作列表", ListRect.width, ListRect.height + 20, Tooltip_20_UpperCenter);
            GUILayout.BeginArea(ListRect, UIStyles.SelectionRect);
            GUILayout.Label("动作列表", UIStyles.DO_18_White_UpperCenter, GUILayout.Height(20));
            GUILayout.Space(4);
            ListPos = EditorGUILayout.BeginScrollView(ListPos);
            if(Player.Anims.Count > 0)
            {
                for(int i = 0; i < Player.Anims.Count; i ++)
                {
                    DrawSelectBtn(i);
                }
            }
            ChangeSelect();
            GUILayout.Space(4);
            if(GUILayout.Button("+", UIStyles.WO))
            {
                AnimInfoEditor info = new AnimInfoEditor();
                info.SetName(Player.Anims.Count + 1);
                Player.Anims.Add(info);
                GetUnitStates();
            }
            EditorGUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        private void DrawAnimPro()
        {
            if (SelectAnimInfo == null) return;
            //EditorUI.DrawBox("属性参数", BaseProRect.width, BaseProRect.height + 20, Tooltip_20_UpperCenter);
            GUILayout.BeginArea(BaseProRect, UIStyles.SelectionRect);
            GUILayout.Label("属性参数", UIStyles.DO_18_White_UpperCenter, GUILayout.Height(20));
            GUILayout.Space(4);
            SelectAnimInfo.DrawPropertyGUI(this);
            GUILayout.EndArea();
        }

        private void DrawAnimGroup()
        {
            if (SelectAnimInfo == null) return;
            //EditorUI.DrawBox("动作组", AnimGroupRect.width, AnimGroupRect.height + 20, Tooltip_20_UpperCenter);
            GUILayout.BeginArea(AnimGroupRect, UIStyles.SelectionRect);
            GUILayout.Label("动作组", UIStyles.DO_18_White_UpperCenter, GUILayout.Height(20));
            GUILayout.Space(4);
            SelectAnimInfo.DrawAnimGroup(this, AnimList, SelectAnimInfo.AnimGroup);
            GUILayout.EndArea();
        }
        private float preTime = 0;
        private void DrawPlay()
        {
            if (SelectAnimInfo == null) return;
            Player.Anim.Update((float)EditorApplication.timeSinceStartup - preTime);
            preTime = (float)EditorApplication.timeSinceStartup;
            if (GUILayout.Button(string.Empty, UIStyles.ProjectBrowserSubAssetExpandBtn, GUILayout.Width(40), GUILayout.Height(40)))
            {
                Player.Execute(Player.Anim.name);
            }   
        }

        private void DrawAnimBreakGroup()
        {
            if (SelectAnimInfo == null || SelectAnimInfo.IsBreak == false) return;
            //EditorUI.DrawBox("可中断当前动作的动作", BreakGroupRect.width, BreakGroupRect.height + 20, Tooltip_20_UpperCenter);
            GUILayout.BeginArea(BreakGroupRect, UIStyles.SelectionRect);
            GUILayout.Label("可中断当前动作的动作", UIStyles.DO_18_White_UpperCenter, GUILayout.Height(20));
            GUILayout.Space(4);
            SelectAnimInfo.DrawAnimGroup(this, AnimList, SelectAnimInfo.BreakGroup);
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
            GetUnitStates();
            GetUnitAnim();
        }

        private bool CreateUnit()
        {
            Player = Divak.Script.Game.UnitMgr.Instance.CreatePlayer(101, string.Empty, Vector3.zero);
            if (Player == null) return false;
            Selection.activeGameObject = Player.Oneself;
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