/**  
* 标    题：   MapNavMgr.cs 
* 描    述：    
* 创建时间：   2018年12月04日 01:56 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game
{

    public class NavMeshBase
    {

        public MapNavInfo Info = new MapNavInfo();
        public int Size { get { return Info.Size; } }

        public int MapH { get { return Info.MapH; } }

        public int MapV { get { return Info.MapV; } }

        public MapPos[,] MapNav { get { return Info.MapNav; } }

        public virtual void Init()
        {

        }
    }
}