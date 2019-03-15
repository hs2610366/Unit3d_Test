/**  
* 标    题：   UnitListView.cs 
* 描    述：    
* 创建时间：   2018年07月09日 02:00 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Divak.Script.Game;


namespace Divak.Script.Editor
{
    public partial class UnitListView : EditorWinBase<UnitListView>
    {
        private enum AType
        {
            Start,
            Await,
            Skills,
            Dead
        }

        #region 数据
        private List<ModelTemp> Temps;
        private UnitPlayer Player;
        private BtnGuidInfo[] AnimTypeInfos;
        private Vector2 AnimPos = Vector2.zero;
        private string[] AnimPaths;
        private List<string> AnimNames = new List<string>();
        #endregion

        #region 参数
        private const int WinW = 800;
        private const int WinH = 800;
        private const int BtnW = 200;
        private const int BtnH = 30;
        private Rect TypeRect;
        private Rect AnimRect;
        #endregion

        #region 保护函数
        protected override void Init()
        {
            Title = "动作编辑器";
            InitData();
            InitAnimType();
            base.Init();
            TempMgr.Init();
            Temps = ModelTempMgr.Instance.Temps;
            InitComplete();
        }

        protected override void CustomGUI()
        {
            EditorUI.SetLabelWidth(80);
            if (Player == null) DrawList();
            else DrawModelInfo();

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
            ResetAnimInfo();
            if (Temps != null) Temps.Clear();
            Temps = null;
        }

        #endregion

        #region 私有函数

        #region ui
        /// <summary>
        /// 绘制List
        /// </summary>
        private void DrawList()
        {
            if (Temps != null)
            {
                for (int i = 0; i < Temps.Count; i++)
                {
                    if (GUILayout.Button(Temps[i].name, "button", GUILayout.Height(BtnH)))
                    {
                        UnitMgr.Instance.TempID = Temps[i].id;
                        CustomRightMenu();
                    }
                }
            }
        }

        /// <summary>
        /// 绘制模型
        /// </summary>
        private void DrawModelInfo()
        {
            ModelTemp temp = Player.MTemp;
            EditorGUILayout.BeginVertical();
            GUILayout.Toggle(true, string.Format("{0}({1})", temp.name, temp.model), "dragtab");
            EditorGUILayout.BeginHorizontal();
            AType type = DrawAnim();
            DrawAnims(type);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        #endregion

        #region 逻辑
        private void InitData()
        {
            ContextRect = new Rect(100, 100, WinW, WinH);
        }

        private void OpenUnitView(object obj = null)
        {
            Player = Divak.Script.Game.UnitMgr.Instance.CreatePlayer(101, string.Empty, Vector3.zero);
            if (Player == null) return;
            Selection.activeGameObject = Player.Model;
            SceneView view = SceneView.lastActiveSceneView;
            view.pivot = Vector3.zero;
            view.size = 20f;
            view.orthographic = true;
            view.MoveToView(Player.Trans);
            GetUnitAnim();
        }

        private void GetUnitAnim()
        {
            string path = string.Format("{0}/{1}/Animator/", UnitMgr.Instance.ModPath, Player.MTemp.model);
            path = Application.dataPath.Replace("Assets", path);
            string[] AnimPaths = PathTool.GetFiles(path);
            if(AnimPaths != null)
            {
                int length = AnimPaths.Length;
                for(int i = 0; i < length; i ++)
                {
                    if (Path.GetExtension(AnimPaths[i]) == SuffixTool.Meta) continue;
                    AnimNames.Add(Path.GetFileNameWithoutExtension(AnimPaths[i]));
                }
            }
        }

        private void ResetAnimInfo()
        {
            UnitMgr.Instance.TempID = 0;
            if (Player != null) Player.Dispose();
            Player = null;
            AnimPaths = null;
            AnimNames.Clear();
        }
        #endregion

        #endregion
    }
}
