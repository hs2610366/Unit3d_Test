/**  
* 标    题：   MapNavInfo.cs 
* 描    述：    
* 创建时间：   2018年07月15日 03:21 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Divak.Script.Game;

namespace Divak.Script.Game
{
    [Serializable]
    public class MapNavInfo
    {
        public uint SceneID;

        public int Size = 1;

        public int MapH = 0;

        public int MapV = 0;

        public MapPos[,] MapNav;

        public MapNavInfo() { }
    }
}
