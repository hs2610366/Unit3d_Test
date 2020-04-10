/**  
* 标    题：   SceneSelectTool.cs 
* 描    述：    
* 创建时间：   2018年07月12日 01:36 
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
    public class SceneSelectTool
    {
        public static bool IsRestrict = true;
        public static NavmashType NavmeshDrawType;
        public static string[] NavmeshDrawNames = {"无","可行走"};
        public static Color32[] NavmeshDrawColors = { Color.gray, Color.green };
        public static float NavmeshSize { get; set; }
        public static int NavmeshH { get; set; }
        public static int NavmeshV { get; set; }

        [InitializeOnLoadMethod]
        static void Start()
        {
            SceneView.duringSceneGui += OnSceneGUI;
        }

        static void OnSceneGUI(SceneView sceneview)
        {
            Event e = Event.current;

            int controlID = GUIUtility.GetControlID(FocusType.Passive);

            if (IsRestrict && e.type == EventType.Layout)
            {
                HandleUtility.AddDefaultControl(controlID);
            }
        }

        public static void Clear()
        {
            SceneNavmeshMgr.Instance.Clear();
        }
    }
}
