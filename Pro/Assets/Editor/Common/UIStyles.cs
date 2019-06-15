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
        //Font
        public static GUIStyle Label_White = new GUIStyle("Label");
        public static GUIStyle Label_13_White = new GUIStyle("Label");
        //ProjectBrowserSubAssetExpandBtn
        public static GUIStyle ProjectBrowserSubAssetExpandBtn = new GUIStyle("ProjectBrowserSubAssetExpandBtn");
        //WarningOverlay
        public static GUIStyle WO = new GUIStyle("WarningOverlay");
        //BoldToggle
        public static GUIStyle BoldToggle_White = new GUIStyle("BoldToggle");
        //Tooltip
        public static GUIStyle Tooltip_20_UpperCenter = new GUIStyle("Tooltip");
        //dockareaOverlay
        public static GUIStyle DO_18_White_UpperCenter = new GUIStyle("dockareaOverlay");
        //SelectionRect
        public static GUIStyle SelectionRect = new GUIStyle("SelectionRect");
        //ChannelStripAttenuationMarkerSquare
        public static GUIStyle CSAMS = new GUIStyle("ChannelStripAttenuationMarkerSquare");
        public static GUIStyle CSAMS_12_White = new GUIStyle("ChannelStripAttenuationMarkerSquare");
        //TL SelectionBarCloseButton
        public static GUIStyle TSBCB = new GUIStyle("TL SelectionBarCloseButton");
        //TE ToolbarDropDown
        public static GUIStyle TTDD_White = new GUIStyle("TE ToolbarDropDown");
        //LargeTextField
        public static GUIStyle LTF_14_White = new GUIStyle("LargeTextField");
        //DropDownButton
        public static GUIStyle DDB_White = new GUIStyle("DropDownButton");
        //Icon.ClipSelected
        public static GUIStyle ICS_16_White_UpperLeft = new GUIStyle("Icon.ClipSelected");
        //flow node X
        public static GUIStyle FN0 = new GUIStyle("flow node 0");
        public static GUIStyle FN1 = new GUIStyle("flow node 1");
        public static GUIStyle FN0_18_White_UpperCenter = new GUIStyle("flow node 0");
        public static GUIStyle FN1_18_White_UpperCenter = new GUIStyle("flow node 1");
        public static GUIStyle FN2_White_UpperLeft = new GUIStyle("flow node 2");
        public static GUIStyle FN2_18_White_UpperLeft = new GUIStyle("flow node 2");



        public static void Init()
        {
            InitFontStyle();
            InitUIStyle();
        }

        private static void InitFontStyle()
        {
            Label_White.normal.textColor = Color.white;
            Label_13_White.fontSize = 13;
            Label_13_White.normal.textColor = Color.white;
        }

        private static void InitUIStyle()
        {
            //BoldToggle
            BoldToggle_White.normal.textColor = Color.white;
            //Tooltip
            Tooltip_20_UpperCenter.fontSize = 20;
            Tooltip_20_UpperCenter.alignment = TextAnchor.UpperCenter;
            //dockareaOverlay
            DO_18_White_UpperCenter.fontSize = 18;
            DO_18_White_UpperCenter.normal.textColor = Color.white;
            DO_18_White_UpperCenter.alignment = TextAnchor.UpperCenter;
            //ChannelStripAttenuationMarkerSquare
            CSAMS_12_White.fontSize = 12;
            CSAMS_12_White.normal.textColor = Color.white;
            //TE ToolbarDropDown
            TTDD_White.normal.textColor = Color.white;
            //LargeTextField
            LTF_14_White.fontSize = 12;
            LTF_14_White.normal.textColor = Color.white;
            //DropDownButton
            DDB_White.normal.textColor = Color.white;
            //Icon.ClipSelected
            ICS_16_White_UpperLeft.fontSize = 16;
            ICS_16_White_UpperLeft.normal.textColor = Color.white;
            ICS_16_White_UpperLeft.alignment = TextAnchor.MiddleLeft;
            //flow node X
            FN0_18_White_UpperCenter.fontSize = 18;
            FN0_18_White_UpperCenter.normal.textColor = Color.white;
            FN0_18_White_UpperCenter.alignment = TextAnchor.UpperCenter;

            FN1_18_White_UpperCenter.fontSize = 18;
            FN1_18_White_UpperCenter.normal.textColor = Color.white;
            FN1_18_White_UpperCenter.alignment = TextAnchor.UpperCenter;

            FN2_White_UpperLeft.fontSize = 14;
            FN2_White_UpperLeft.normal.textColor = Color.white;
            FN2_White_UpperLeft.alignment = TextAnchor.UpperLeft;
            FN2_18_White_UpperLeft.fontSize = 18;
            FN2_18_White_UpperLeft.normal.textColor = Color.white;
            FN2_18_White_UpperLeft.alignment = TextAnchor.UpperLeft;
        }
    }
}