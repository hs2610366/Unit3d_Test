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
	public class SceneNavMeshPath : EditorWinBase<SceneNavMeshPath>
    {
    //    private SceneTemp Temp;
    //    private SceneNavmeshMgr Mgr;
    //    private bool IsReadNavmeshEnd = false;
    //    private bool IsPathfinding = false;

    //    #region 保护函数
    //    protected override void Init()
    //    {
    //        Title = "场景寻路网格编辑";
    //        ContextRect = new Rect(300, 300, 300, 300);
    //        base.Init();
    //        InitScene();
    //        InitNavmesh();
    //        InitComplete();
    //    }

    //    protected override void CustomGUI()
    //    {
    //        base.CustomGUI();
    //        DrawGridPath();
    //    }

    //    #region 右键菜单
    //    protected override void CustomRightMenu()
    //    {
    //        GenericMenu menu = new GenericMenu();
    //        menu.AddItem(new GUIContent("保存"), false, OutputConfig, "Output");
    //        menu.ShowAsContext();
    //    }
    //    #endregion

    //    protected override void CustomDrawGizmos()
    //    {
    //    }

    //    protected override void CustomDestroy()
    //    {
    //        SceneSelectTool.Clear();
    //        EditorSceneManager.sceneOpened -= OnSceneOpened;
    //        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    //    }

    //    #endregion

    //    #region 私有函数
    //    private void InitScene()
    //    {
    //        Temp = SceneMgr.Instance.CurEditorTemp;
    //        if (Temp == null) return;
    //        InputConfig();
    //        string mod = Temp.modle;
    //        EditorSceneManager.sceneOpened += OnSceneOpened; 
    //        EditorSceneManager.OpenScene(string.Format("{0}{1}/{2}{3}", PathEditorTool.SceneModPath, mod, mod, SuffixTool.Scene));
    //    }

    //    private void InitNavmesh()
    //    {
    //        if (Temp == null) return;
    //        NavMeshIO.Instance.OnReadFinish += OnReadFinish;
    //        NavMeshIO.Instance.OnReadFail += OnReadFail;
    //        NavMeshIO.Instance.ReadBinaryFile(SavePath(), Temp.id.ToString(), SuffixTool.Nav);
    //    }

    //    private void OnReadFinish(string fineName)
    //    {
    //        NavMeshIO.Instance.OnReadFinish -= OnReadFinish;
    //        NavMeshIO.Instance.OnReadFail -= OnReadFail;

    //        Mgr = SceneNavmeshMgr.Instance;
    //        SceneSelectTool.NavmeshSize = Mgr.Navmesh.Size;
    //        SceneSelectTool.NavmeshH = Mgr.Navmesh.NavmeshH;
    //        SceneSelectTool.NavmeshV = Mgr.Navmesh.NavmeshV;

    //        IsReadNavmeshEnd = true;
    //    }

    //    private void OnReadFail(string fineName, string err)
    //    {
    //        NavMeshIO.Instance.OnReadFinish -= OnReadFinish;
    //        NavMeshIO.Instance.OnReadFail -= OnReadFail;

    //        Mgr = SceneNavmeshMgr.Instance;
    //        IsReadNavmeshEnd = true;
    //    }

    //    #region 路径
    //    private static string SavePath()
    //    {
    //        string outputPath = string.Format("{0}{1}", PathTool.DataPath, PathTool.Nav);
    //        return outputPath;
    //    }
    //    #endregion

    //    private void OnSceneOpened(Scene scene, OpenSceneMode mode)
    //    {
    //        EditorSceneManager.sceneOpened -= OnSceneOpened;
    //        SceneView.onSceneGUIDelegate += OnSceneGUI;
    //        SceneView view = SceneView.lastActiveSceneView;
    //        if (view)
    //        {
    //            view.rotation = Quaternion.Euler(90f, 0f, 0f);
    //            view.pivot = Vector3.zero;
    //            view.size = 5f;
    //            view.orthographic = true;
    //            view.isRotationLocked = true;
    //            GameObject go = GameObject.Find(scene.name);
    //            if (go != null)
    //            {
    //                Selection.activeGameObject = go;
    //                //view.MoveToView(go.transform);
    //            }
    //        }
    //    }
    //    private void OnSceneGUI(SceneView sceneview)
    //    {
    //        Event e = Event.current;
    //        switch(e.type)
    //        {
    //            case EventType.Layout:
    //                int controlID = GUIUtility.GetControlID(FocusType.Passive);
    //                HandleUtility.AddDefaultControl(controlID);
    //                break;
    //            case EventType.MouseUp:
    //            case EventType.MouseDrag:
    //                if (e.button == 0)
    //                {
    //                    UpdateClickRay();
    //                }
    //                break;
    //        }
    //    }



    //    #region 绘制路径网格
    //    private void DrawGridPath()
    //    {
    //        NavmeshMgr navmesh = Mgr.Navmesh;
    //        if (navmesh != null) GUI.enabled = false;
    //        EditorGUILayout.BeginHorizontal();
    //        GUILayout.Label("规格：", GUILayout.Width(40));
    //        int h = EditorUI.DrawIntField(SceneSelectTool.NavmeshH, "行", 30, options: GUILayout.Width(50));
    //        int v = EditorUI.DrawIntField(SceneSelectTool.NavmeshV, "列", 30, options: GUILayout.Width(50));
    //        EditorGUILayout.EndHorizontal();
    //        float s = EditorUI.DrawFloatField(SceneSelectTool.NavmeshSize, "尺寸", 30, 48, GUILayout.Width(50));
    //        if (h != SceneSelectTool.NavmeshH || v != SceneSelectTool.NavmeshV || s != SceneSelectTool.NavmeshSize)
    //        {
    //            SceneSelectTool.NavmeshH = h;
    //            SceneSelectTool.NavmeshV = v;
    //            SceneSelectTool.NavmeshSize = s;
    //        }
    //        GUILayout.Space(2);
    //        if (navmesh != null) GUI.enabled = true;
    //        if (navmesh == null)
    //        {
    //            if (GUILayout.Button("生成"))
    //            {
    //                Mgr.Init(true);
    //                Mgr.Navmesh.Size = SceneSelectTool.NavmeshSize;
    //                Mgr.Navmesh.NavmeshH = SceneSelectTool.NavmeshH;
    //                Mgr.Navmesh.NavmeshV = SceneSelectTool.NavmeshV;
    //                Mgr.Navmesh.Meshs = new Navmesh[SceneSelectTool.NavmeshH, SceneSelectTool.NavmeshV];
    //                Mgr.CreateNavmesh();
    //            }
    //            return;
    //        }
    //        {

    //        }
    //        bool isFog = EditorUI.DrawToggle(navmesh.IsFog, "迷雾(可行走半径)", 100, options: GUILayout.Width(50));
    //        if(navmesh.IsFog != isFog)
    //        {
    //            navmesh.IsFog = isFog;
    //        }
    //        if (navmesh.IsFog)
    //        {
    //            int raidus = EditorUI.DrawIntField(navmesh.FogRaidus, "迷雾半径：", 28, 48, GUILayout.Width(50));
    //            if (raidus != navmesh.FogRaidus)
    //            {
    //                navmesh.FogRaidus = raidus;
    //            }
    //        }
    //        if (GUILayout.Button("重置"))
    //        {
    //            if (EditorUtility.DisplayDialog("提示", "重置地图网格数据？", "重置", "取消") == true)
    //                navmesh.Reset();
    //        }
    //        GUILayout.Space(2f);
    //        EditorGUILayout.BeginHorizontal();
    //        int t = GUILayout.SelectionGrid((int)SceneSelectTool.NavmeshDrawType, SceneSelectTool.NavmeshDrawNames, 1, GUILayout.Width(120));
    //        UpdateColor();
    //        EditorGUILayout.EndHorizontal();
    //        if ((NavmashType)t != SceneSelectTool.NavmeshDrawType)
    //        {
    //            SceneSelectTool.NavmeshDrawType = (NavmashType)t;
    //        }
    //        GUI.enabled = false;
    //        IsPathfinding = GUILayout.Toggle(IsPathfinding, "模拟寻路", "button");
    //        if (GUILayout.Button("重置路径"))
    //        {
    //            navmesh.ResetPath();
    //        }
    //        GUI.enabled = true;
    //    }
    //    #endregion

    //    private void UpdateColor()
    //    {
    //        Color32[] list = SceneSelectTool.NavmeshDrawColors;
    //        EditorGUI.BeginDisabledGroup(true);
    //        EditorGUILayout.BeginVertical();
    //        for(int i = 0; i < list.Length; i ++)
    //        {
    //            EditorGUILayout.ColorField(list[i], GUILayout.Width(100));
    //        }
    //        EditorGUILayout.EndVertical();
    //        EditorGUI.EndDisabledGroup();
    //    }

    //    private void UpdateClickRay()
    //    {
    //        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit, 10000, -1))
    //        {
    //            int layer = hit.transform.gameObject.layer;
    //            if (LayerName.GetLayerNameOfIndex(layer) == LayerName.Gound)
    //            {

    //                NavmeshMgr navmesh = Mgr.Navmesh;
    //                if (navmesh == null) return;

    //                if (!IsPathfinding)
    //                {
    //                    if (SceneSelectTool.NavmeshDrawType != NavmashType.NONE)
    //                    {
    //                        navmesh.Add(hit.point, SceneSelectTool.NavmeshDrawType);
    //                    }
    //                    else
    //                    {
    //                        navmesh.Remove(hit.point);
    //                    }
    //                }
    //                else
    //                {
    //                    navmesh.StartNavMesh(hit.point, Vector3.zero);
    //                }
    //            }
    //        }
    //    }

    //    private void OutputConfig(object obj = null)
    //    {
    //        OutputConfig();
    //    }

    //    private void InputConfig()
    //    {
    //        string path = string.Format("{0}{1}", PathTool.DataPath, PathTool.Nav);
    //        string name = Temp.id.ToString();
    //        NavmeshInfo info = Config.InputConfig<NavmeshInfo>(path, name, SuffixTool.Nav);
    //        if (info == null) return;
    //        Mgr.Navmesh = (NavmeshMgr)info;
    //    }

    //    private void OutputConfig()
    //    {
    //        string path = string.Format("{0}{1}", PathTool.DataPath, PathTool.Nav);
    //        string name = Temp.id.ToString();
    //        Config.OutputConfig<NavmeshInfo>(path, name, Mgr.Navmesh as NavmeshInfo, SuffixTool.Nav);
    //    }

    //    #endregion
    }
}