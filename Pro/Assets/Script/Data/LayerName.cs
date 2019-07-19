/**  
* 标    题：   LayerName.cs 
* 描    述：    
* 创建时间：   2017年07月22日 02:39 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class LayerName
    {

        public const string Default = "Default";
        public const string TransparentFx = "TransparentFx";
        public const string IgnoreRaycast = "Igonre Raycast";
        /// <summary>
        /// 水
        /// </summary>
        public const string Water = "Water";
        /// <summary>
        /// UI
        /// </summary>
        public const string UI = "UI";
        /// <summary>
        /// 角色
        /// </summary>
        public const string Unit = "Unit";
        /// <summary>
        /// NPC
        /// </summary>
        public const string NPC = "NPC";
        /// <summary>
        /// 地形
        /// </summary>
        public const string Gound = "Gound";


        /// <summary>
        /// 獲得層級索引
        /// </summary>
        public static int GetIndexOfLayerName(string name)
        {
            if (name == Default) return 0;
            else if (name == TransparentFx) return 1;
            else if (name == IgnoreRaycast) return 2;
            else if (name == Water) return 4;
            else if (name == UI) return 5;
            else if (name == Unit) return 8;
            else if (name == NPC) return 9;
            else if (name == Gound) return 10;
            return 0;
        }

        /// <summary>
        /// 獲得層級名字
        /// </summary>
        public static string GetLayerNameOfIndex(int index)
        {
            if (index == 0) return Default;
            else if (index == 1) return TransparentFx;
            else if (index == 2) return IgnoreRaycast;
            else if (index == 4) return Water;
            else if (index == 5) return UI;
            else if (index == 8) return Gound;
            else if (index == 9) return Unit;
            else if (index == 10) return NPC;
            return Default;
        }
	}
}