/**  
* 标    题：   MapNavMeshPath.cs 
* 描    述：    
* 创建时间：   2018年06月11日 03:26 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using Divak.Script.Game;

namespace Divak.Script.Editor 
{
	public class MapNavMeshPath : EditorWinBase<MapNavMeshPath>
    {
        private SceneTemp Temp;
        private MapMgr Map;
        private bool IsPathfinding = false;

        #region 保护函数
        protected override void Init()
        {
            Title = "地图路径";
            ContextRect = new Rect(300, 300, 300, 800);
            Map = MapMgr.Instance;
            base.Init();
            InitScene();
            InitComplete();
        }

        protected override void CustomGUI()
        {
            base.CustomGUI();
            UpdateView();
        }

        #region 右键菜单
        protected override void CustomRightMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("保存"), false, OutputConfig, "Output");
            menu.ShowAsContext();
        }
        #endregion

        protected override void CustomDrawGizmos()
        {
        }

        protected override void CustomDestroy()
        {
            EditorSceneManager.sceneOpened -= OnSceneOpened;
            SceneView.onSceneGUIDelegate -= OnSceneGUI;
        }

        #endregion

        #region 私有函数
        private void InitScene()
        {
            Temp = SceneMgr.Instance.CurEditorTemp;
            if (Temp == null) return;
            InputConfig();
            string mod = Temp.modle;
            EditorSceneManager.sceneOpened += OnSceneOpened; 
            EditorSceneManager.OpenScene(string.Format("{0}{1}/{2}{3}", PathEditorTool.SceneModPath, mod, mod, SuffixTool.Scene));
        }

        private void OnSceneOpened(Scene scene, OpenSceneMode mode)
        {
            Map.Info.SceneID = Temp.id;
            EditorSceneManager.sceneOpened -= OnSceneOpened;
            SceneView.onSceneGUIDelegate += OnSceneGUI;
            SceneView view = SceneView.lastActiveSceneView;
            if (view)
            {
                view.rotation = Quaternion.Euler(90f, 0f, 0f);
                view.pivot = Vector3.zero;
                view.size = 5f;
                view.orthographic = true;
                GameObject go = GameObject.Find(scene.name);
                if (go != null)
                {
                    Selection.activeGameObject = go;
                   // view.MoveToView(go.transform);
                }
                Map.Init();
            }
        }
        private void OnSceneGUI(SceneView sceneview)
        {
            Event e = Event.current;
            switch(e.type)
            {
                case EventType.Layout:
                    int controlID = GUIUtility.GetControlID(FocusType.Passive);
                    HandleUtility.AddDefaultControl(controlID);
                    break;
                case EventType.MouseDown:
                case EventType.MouseDrag:
                    UpdateClickRay();
                    break;
            }
        }

        private void UpdateView()
        {
            if (Map == null)
            {
                MessageBox.Error("NULL");
                return;
            }
            GUILayout.BeginArea(new Rect(0,0,220,1000));
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("范围：", GUILayout.Width(40));
            int h = EditorUI.DrawPrefabsNumberField(Map.MapH, "行", 14, GUILayout.Width(50));
            int v = EditorUI.DrawPrefabsNumberField(Map.MapV, "列", 14, GUILayout.Width(50));
            EditorGUILayout.EndHorizontal();
            int s = EditorUI.DrawPrefabsNumberField(Map.Size, "尺寸：", 28, GUILayout.Width(50));
            if (h != Map.MapH || v != Map.MapV || s != Map.Size)
            {
                Map.Info.MapH = h;
                Map.Info.MapV = v;
                Map.Info.Size = s;
                Map.Reset();
            }
            if(GUILayout.Button("重置"))
            {
                Map.Reset();
            }
            GUILayout.Space(2f);
            EditorGUILayout.BeginHorizontal();
            int t = GUILayout.SelectionGrid((int)MapInfo.PosType, MapInfo.Names, 1, GUILayout.Width(120));
            UpdateColor();
            EditorGUILayout.EndHorizontal();
            if ((NavType)t != MapInfo.PosType)
            {
                MapInfo.PosType = (NavType)t;
                MapInfo.UpdateColor(t);
            }

            IsPathfinding = GUILayout.Toggle(IsPathfinding, "模拟寻路", "button");
            GUILayout.EndArea();
        }

        private void UpdateColor()
        {
            Color[] list = MapInfo.Colors;
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.BeginVertical();
            for(int i = 0; i < list.Length; i ++)
            {
                EditorGUILayout.ColorField(list[i], GUILayout.Width(100));
            }
            EditorGUILayout.EndVertical();
            EditorGUI.EndDisabledGroup();
        }

        private void UpdateClickRay()
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000, -1))
            {
                int layer = hit.transform.gameObject.layer;
                if (LayerName.GetLayerNameOfIndex(layer) == LayerName.Gound)
                {
                    if(!IsPathfinding)
                    {
                        if (MapInfo.PosType == NavType.Walk)
                        {
                            Map.Add(hit.point);
                        }
                        else
                        {
                            Map.Remove(hit.point);
                        }
                    }
                    else
                    {
                        Map.StartNavMesh(hit.point);
                    }
                }
            }
        }

        private void OutputConfig(object obj = null)
        {
            OutputConfig();
        }

        private void InputConfig()
        {
            string path = string.Format("{0}{1}", PathTool.DataPath, PathTool.Nav);
            string name = Temp.id.ToString();
            MapNavInfo info = Config.InputConfig<MapNavInfo>(path, name, SuffixTool.Nav);
            if (info == null) return;
            Map.Info = info;
        }

        private void OutputConfig()
        {
            string path = string.Format("{0}{1}", PathTool.DataPath, PathTool.Nav);
            string name = Temp.id.ToString();
            Config.OutputConfig<MapNavInfo>(path, name, Map.Info as MapNavInfo, SuffixTool.Nav);
        }

        #endregion

        #region 公开函数

        public static void ShowWindow()
        {
            MapArrayView.ShowWin();
        }
        #endregion
    }
}