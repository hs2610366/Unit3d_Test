/**  
* 标    题：   MapInfo.cs 
* 描    述：    
* 创建时间：   2018年07月14日 02:52 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Divak.Script.Game;

namespace Divak.Script.Game
{
    public enum NavType
    {
        [Description("清除区域")]
        None = 0,
        [Description("行走区域")]
        Walk = 1
    }


    public class MapInfo
    {
        public static NavType PosType = NavType.None;
#if UNITY_EDITOR
        public static int ColorType = 0;
        public static string[] Names = { "清除区域", "行走区域" };
#endif
        public static Color[] Colors = { Color.white, Color.green, Color.red , Color.gray, Color.black};


#if UNITY_EDITOR
        public static void UpdateColor(int index)
        {
            ColorType = index;
        }
#endif
    }
}
