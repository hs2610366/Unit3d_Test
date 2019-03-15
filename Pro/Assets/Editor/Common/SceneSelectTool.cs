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

namespace Divak.Script.Editor
{
    public class SceneSelectTool
    {
        public static bool IsRestrict = true;

        [InitializeOnLoadMethod]
        static void Start()
        {
            SceneView.onSceneGUIDelegate = OnSceneGUI;
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
    }
}
