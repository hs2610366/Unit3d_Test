/**  
* 标    题：   UIStyles.cs 
* 描    述：    
* 创建时间：   2019年06月14日 21:49 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Divak.Script.Editor 
{
	public class UIStyles
    {
        //Tooltip
        public static GUIStyle Tooltip_20_UpperCenter = new GUIStyle("Tooltip");
        //dockareaOverlay
        public static GUIStyle DO_18_White_UpperCenter = new GUIStyle("dockareaOverlay");
        //SelectionRect
        public static GUIStyle SelectionRect = new GUIStyle("SelectionRect");
        //ChannelStripAttenuationMarkerSquare
        public static GUIStyle CSAMS = new GUIStyle("ChannelStripAttenuationMarkerSquare");
        //TL SelectionBarCloseButton
        public static GUIStyle TSBCB = new GUIStyle("TL SelectionBarCloseButton");
        //flow node X
        public static GUIStyle FN0 = new GUIStyle("flow node 0");
        public static GUIStyle FN1 = new GUIStyle("flow node 1");
        public static GUIStyle FN0_18_White_UpperCenter = new GUIStyle("flow node 0");
        public static GUIStyle FN1_18_White_UpperCenter = new GUIStyle("flow node 1");


        public static void Init()
        {

            Tooltip_20_UpperCenter.fontSize = 20;
            Tooltip_20_UpperCenter.alignment = TextAnchor.UpperCenter;

            DO_18_White_UpperCenter.fontSize = 18;
            DO_18_White_UpperCenter.normal.textColor = Color.white;
            DO_18_White_UpperCenter.alignment = TextAnchor.UpperCenter;


            FN0_18_White_UpperCenter.fontSize = 18;
            FN0_18_White_UpperCenter.normal.textColor = Color.white;
            FN0_18_White_UpperCenter.alignment = TextAnchor.UpperCenter;


            FN1_18_White_UpperCenter.fontSize = 18;
            FN1_18_White_UpperCenter.normal.textColor = Color.white;
            FN1_18_White_UpperCenter.alignment = TextAnchor.UpperCenter;
        }
    }
}