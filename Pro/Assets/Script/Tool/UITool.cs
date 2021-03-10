/**  
* 标    题：   UITool.cs 
* 描    述：    
* 创建时间：   2017年07月31日 05:26 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Divak.Script.Game 
{
	public class UITool
    {

        public static GameObject CreateCanvas(string name, Transform parent = null)
        {
            GameObject go = new GameObject(name);
            go.AddComponent<Canvas>();
            go.AddComponent<CanvasScaler>();
            go.AddComponent<GraphicRaycaster>();
            if (parent != null) go.transform.SetParent(parent);
            RectTransform rectTrans = go.GetComponent<RectTransform>();
            if(go != null)
            {
                SetDefaultRectTransform(rectTrans);
            }
            else
            {
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
            }
            
            return go;
        }

        /// <summary>
        /// 设置Canvas父节点 并设置默认值
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="parent"></param>
        public static void SetParentOfCanvas(Transform trans, Transform parent)
        {
            if(trans == null)
            {
                MessageBox.Error("传入的需要设置挂载父节点的对象为null!");
                return;
            }
            if(parent == null)
            {
                MessageBox.Error("传入的需要设置挂载的父节点对象为null!");
            }
            trans.SetParent(parent);
            RectTransform rectTrans = trans.GetComponent<RectTransform>();
            if(rectTrans == null)
            {
                MessageBox.Error("传入的需要挂载父节点的对象没有RectTransform!!");
                return;
            }
            SetDefaultRectTransform(rectTrans);
        }

        /// <summary>
        /// 设置RectTransform默认值
        /// </summary>
        public static void SetDefaultRectTransform(RectTransform rectTrans)
        {
            if(rectTrans == null)
            {
                MessageBox.Error("传入的需要设置的RectTransform为null!!");
                return;
            }
            rectTrans.anchorMin = Vector2.zero;
            rectTrans.anchorMax = Vector2.one;
            rectTrans.sizeDelta = Vector2.zero;
            rectTrans.anchoredPosition = Vector2.zero;
            rectTrans.localEulerAngles = Vector3.zero;
            rectTrans.localScale = Vector3.one;
        }
	}
}