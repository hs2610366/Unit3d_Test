/**  
* 标    题：   SkillEditor.cs 
* 描    述：    
* 创建时间：   2019年01月29日 02:50 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Divak.Script.Game;

namespace Divak.Script.Editor 
{
	public partial class RoleactionEditor : EditorWinBase<RoleactionEditor>
    {

        private Rect RectBaseInfo = new Rect(0, 18, 400, 90);
        private string[] AnimPaths;
        private List<string> AnimNames = new List<string>();


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

        /// <summary>
        /// 右键菜单
        /// </summary>
        protected override void CustomRightMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("创建"), false, CreateUnitView, "Create");
            menu.ShowAsContext();
        }

        protected override void CustomDestroy()
        {
            ResetUnitInfo();
            if (Temps != null) Temps.Clear();
            Temps = null;
        }
        #endregion

        #region DRAW GUI
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
            DrawBaseInfo();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制基础数据
        /// </summary>
        private void DrawBaseInfo()
        {
            EditorUI.DrawArea(RectBaseInfo, DrawBaseInfoCB);
        }

        private void DrawBaseInfoCB()
        {
            UnitInfo info = Player.ModInfo;
            if (info == null) return;
            string name = info.Name;
            float height = info.Height;
            Vector3 center = info.Center;
            if (string.IsNullOrEmpty(name)) name = Player.MTemp.name;
            GUILayout.Toggle(true, "基础数据", "dragtab", GUILayout.Width(RectBaseInfo.width));
            info.Name = EditorUI.DrawPrefabsTextField(name, "名字");
            info.Type = (UnitType)EditorUI.DrawEnumPopup<UnitType>(info.Type, "类型");
            GUILayout.Toggle(true, "角色控制器", "dragtab", GUILayout.Width(RectBaseInfo.width));
            info.Height = EditorUI.DrawPrefabsFloatField(height, "高度");
            info.Center = EditorUI.DrawPrefabsVector3Field(center, "中枢");
        }
        #endregion

        #region 私有函数

        /// <summary>
        /// 创建模型
        /// </summary>
        private void CreateUnitView(object obj = null)
        {
            Player = Divak.Script.Game.UnitMgr.Instance.CreatePlayer(101, string.Empty, Vector3.zero);
            if (Player == null) return;
            Selection.activeGameObject = Player.Model;
            SceneView view = SceneView.lastActiveSceneView;
            if(view != null)
            {
                view.pivot = Vector3.zero;
                view.size = 20f;
                view.orthographic = true;
                view.MoveToView(Player.Trans);
            }
            GetUnitAnim();
        }
        private void GetUnitAnim()
        {
            string path = string.Format("{0}/{1}/Animator/", UnitMgr.Instance.ModPath, Player.MTemp.model);
            path = Application.dataPath.Replace("Assets", path);
            string[] AnimPaths = PathTool.GetFiles(path);
            if (AnimPaths != null)
            {
                int length = AnimPaths.Length;
                for (int i = 0; i < length; i++)
                {
                    if (Path.GetExtension(AnimPaths[i]) == SuffixTool.Meta) continue;
                    AnimNames.Add(Path.GetFileNameWithoutExtension(AnimPaths[i]));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetUnitInfo()
        {
            UnitMgr.Instance.TempID = 0;
            if (Player != null) Player.Dispose();
            Player = null;
        }
        #endregion

        #region 公开函数

        #endregion

    }
}