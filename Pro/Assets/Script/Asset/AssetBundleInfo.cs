/**  
* 标    题：   AssetBundleInfo.cs 
* 描    述：    
* 创建时间：   2018年03月03日 19:00 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class AssetBundleInfo
    {
        public string AssetBundleName { get; set; }
        public int Count { get; set; }
        public AssetBundle AB = null;

        public AssetBundleInfo(string name, AssetBundle ab)
        {
            AssetBundleName = name;
            AB = ab;
            Count = 0;
        }

        public void Add()
        {
            Count++;
        }

        public void Reduce()
        {
            Count--;
        }

        public void Clear(bool isDerstoy)
        {
            if(AB != null)
            {
                AB.Unload(isDerstoy);
                AB = null;
            }
        }
	}
}