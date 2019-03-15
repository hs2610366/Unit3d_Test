/**  
* 标    题：   GameSetting.cs 
* 描    述：   游戏设置
* 创建时间：   2017年03月11日 03:52 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class GameSetting
    {
        /// <summary>
        /// 工程名
        /// </summary>
        public const string ProjectName = "Pro";
        /// <summary>
        /// 是否是debug版本
        /// </summary>
        public static bool IsDebug = true;
        /// <summary>
        /// 是否输出log
        /// </summary>
        public static bool IsOutputLog = true;
        /// <summary>
        /// 同步加载
        /// </summary>
        public static bool SynchroLoad = true;

        public const int ScreenW = 1334;
        public const int ScreenH = 750;
	}
}