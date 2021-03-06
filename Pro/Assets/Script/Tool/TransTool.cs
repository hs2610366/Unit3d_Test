﻿/**  
* 标    题：   TransTool.cs 
* 描    述：    
* 创建时间：   2017年07月25日 02:39 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class TransTool
    {
        public static Transform Find(Transform trans, string path, string name = "")
        {
            Transform t = trans.Find(path);
            if(t == null)
            {
                MessageBox.Error(string.Format("UI[{0}]中路径[{1}获取Transform失败！！]", name, path), TitleName.TransformFindError);
            }
            return t;
        }

        public static bool IsNull(GameObject go)
        {
            if (go == null) return true;
            if (go.name == "null") return true;
            if (go.Equals(null)) return true;
            return false;
        }

        public static bool IsNull(Transform trans)
        {
            if (trans == null) return true;
            if (trans.name == "null") return true;
            if (trans.Equals(null)) return true;
            return false;
        }
    }
}