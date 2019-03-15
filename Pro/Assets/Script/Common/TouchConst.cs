/**  
* 标    题：   TouchConst.cs 
* 描    述：    
* 创建时间：   2018年03月16日 00:04 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Divak.Script.Game 
{
	public class TouchConst
    {

        public static DateTime times;
        /**射线*/
        private static Ray mRay;
        /// <summary>
        /// 点击对象的名字
        /// </summary>
        private static string mClickName;

        public static void Init()
        {
            Global.Instance.OnUpdate += Update;
        }

        public static bool TouchBegin()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0)) return true;
#else
            if ( Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) return true;
#endif
            return false;
        }

        public static bool TouchEnd()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonUp(0)) return true;
#else
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) return true;
#endif
            return false;
        }

        public static bool TouchIng()
        {
            if (DateTime.Now - times < new TimeSpan(0, 0, 0, 0, 100))
            {
                return false;
            }
#if UNITY_EDITOR
            if (Input.GetMouseButton(0)) return true;
#else
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)  return true;
#endif
            return false;
        }

        /**获得View坐标*/
        public static Vector2 TouchPosition()
        {
#if UNITY_EDITOR
            return Input.mousePosition;
#else
            return Input.GetTouch(0).position;
#endif
        }

        private static bool IsAllow(out RaycastHit hit)
        {
            if (Physics.Raycast(mRay, out hit, 50))
            {
                int mask = 1 << LayerMask.GetMask(LayerName.Gound);
                if (hit.collider.gameObject.layer == mask)
                    return true;
            }
            return false;
        }

        private static void Update()
        {

            RaycastHit hit;
            if (TouchConst.TouchBegin())
            {
                TouchConst.times = DateTime.Now;
                mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (IsAllow(out hit))
                {
                    mClickName = hit.collider.name;
                }
            }
            if (TouchConst.TouchEnd())
            {
                if (IsAllow(out hit))
                {
                    if(hit.collider.name.Contains(mClickName))
                    {
                        mClickName = string.Empty;
                        Debug.Log(hit.point);
                    }
                }
            }
            else if (TouchConst.TouchIng())
            {
            }
            else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
            }

        }

    }
}