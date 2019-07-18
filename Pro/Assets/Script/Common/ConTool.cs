/**  
* 标    题：   ComponetTool.cs 
* 描    述：    
* 创建时间：   2017年07月25日 02:31 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class ConTool
    {

        /// <summary>
        /// 通过类型名字符串获得控件
        /// </summary>
        /// <param name="go"> 父节点 </param>
        /// <param name="path"> 控件路径 </param>
        /// <param name="type"> 类型名字符串 </param>
        /// <param name="name"> UI名 </param>
        /// <param name="isChild"> 是否从子节点中获取 </param>
        /// <returns></returns>
        [Obsolete("可能没用")]
        public static Component Find(GameObject go, string path, string type, string name = "", bool isChild = false)
        {
            Transform child = TransTool.Find(go.transform, path, name);
            if (child == null)
            {
                MessageBox.Error(string.Format("UI[{0}]中路径[{1}获取Transform失败！！]", name, path), TitleName.TransformFindError);
                return null;
            }
            Component c = child.GetComponent(type);
            if (c == null && isChild == true)
            {
                c = go.GetComponentInChildren(Type.GetType(type));
            }
            if (c == null)
            {
                MessageBox.Error(string.Format("UI[{0}]中路径[{1}获取{3}失败！！]", name, path, type), TitleName.TransformFindError);
                return null;
            }
            return c;
        }

        /// <summary>
        /// 获得控件
        /// </summary>
        /// <typeparam name="T"> 控件类型 </typeparam>
        /// <param name="go"> 父节点 </param>
        /// <param name="path"> 控件路径 </param>
        /// <param name="name"> UI名 </param>
        /// <param name="isChild"> 是否从子节点中获取 </param>
        /// <returns></returns>
        public static T Find<T>(GameObject go, string path, string name = "", bool isChild = false)
        {
            Transform child = TransTool.Find(go.transform, path, name);
            if (child == null)
            {
                MessageBox.Error(string.Format("UI[{0}]中路径[{1}获取Transform失败！！]", name, path), TitleName.TransformFindError);
                return default(T);
            }
            T t = child.GetComponent<T>();
            if(t == null && isChild == true)
            {
                t = go.GetComponentInChildren<T>();
            }
            if (t == null)
            {
                MessageBox.Error(string.Format("UI[{0}]中路径[{1}获取{3}失败！！]", name, path, typeof(T)), TitleName.TransformFindError);
                return default(T);
            }
            return t;
        }
    }
}