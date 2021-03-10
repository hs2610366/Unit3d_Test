/**  
* 标    题：   RaycastTool.cs 
* 描    述：    
* 创建时间：   2021年02月23日 20:42 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class RaycastTool
    {

        public static Vector3 Raycast(GameObject go, Vector3 dir, string layerName, out bool isHit)
        {
            return Raycast(go.transform, dir, layerName, out isHit);
        }

        public static Vector3 Raycast(Transform trans, Vector3 dir, string layerName, out bool isHit)
        {
            Ray ray = new Ray(dir, trans.position);
            RaycastHit hit;
            isHit = Physics.Raycast(ray, out hit, 10000, 1 << LayerName.GetIndexOfLayerName(layerName));
            if (isHit)
            {
                return hit.point;
            }
            return trans.position;
        }

        public static Vector3 Raycast(Vector3 oragin, Vector3 dir, string layerName, out bool isHit)
        {
            Ray ray = new Ray(dir, oragin);
            RaycastHit hit;
            isHit = Physics.Raycast(ray, out hit, 10000, 1 << LayerName.GetIndexOfLayerName(layerName));
            if (isHit)
            {
                return hit.point;
            }
            return oragin;
        }
    }
}