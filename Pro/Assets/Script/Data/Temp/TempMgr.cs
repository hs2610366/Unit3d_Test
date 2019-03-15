/**  
* 标    题：   TempMgr.cs 
* 描    述：    
* 创建时间：   2018年03月06日 01:42 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Divak.Script.Game;

namespace Divak.Script.Game 
{
	public class TempMgr
    {
        private static List<BaseTempMgr<Temp>> Temps = new List<BaseTempMgr<Temp>>();
        public static void Init()
        {
            Clear();
            ModelTempMgr.Instance.Init();
            CareerTempMgr.Instance.Init();
            SceneTempMgr.Instance.Init();
        }


        public static void Clear()
        {
            ModelTempMgr.Instance.Clear();
            CareerTempMgr.Instance.Clear();
            SceneTempMgr.Instance.Clear();
        }

        public static void Destroy()
        {
            Clear();
        }

	}
}