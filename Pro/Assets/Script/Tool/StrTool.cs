/**  
* 标    题：   StrTool.cs 
* 描    述：    
* 创建时间：   2019年02月27日 02:35 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class StrTool
    {

        public static int IndexOfToStr(string[] strs, string str)
        {
            if (strs != null)
            {
                for(int i = 0; i < strs.Length; i ++)
                {
                    if (String.Equals(strs[i],str)) return i;
                }
            }
            return -1;
        }

	}
}