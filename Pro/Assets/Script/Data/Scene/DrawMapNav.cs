/**  
* 标    题：   DrawMapNav.cs 
* 描    述：    
* 创建时间：   2018年11月16日 01:44 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Divak.Script.Game
{

    [ExecuteInEditMode]
    public class DrawMapNav : MonoBehaviour {

        private MapMgr mMap;

		// Use this for initialization
		void Start () {
            mMap = MapMgr.Instance;
		}
		
		// Update is called once per frame
		void Update () {
				
		}

        void OnDrawGizmos()
        {
            if (mMap == null) return;
            Gizmos.color = Color.white;
            Gizmos.DrawLine(Vector3.zero, Vector3.right * mMap.MapH);
            Gizmos.DrawLine(Vector3.zero, Vector3.forward * mMap.MapV);
            Gizmos.DrawLine(Vector3.right * mMap.MapH, new Vector3(mMap.MapH, 0, mMap.MapV));
            Gizmos.DrawLine(Vector3.forward * mMap.MapV, new Vector3(mMap.MapH, 0, mMap.MapV));
            for (int i = 0; i < mMap.MapH; i++)
            {
                for (int j = 0; j < mMap.MapV; j++)
                {
                    float offset = mMap.Size / 2.0f;
                    MapPos pos = mMap.MapNav[i, j];
                    if (pos == null) continue;
                    float posH = pos.H + offset;
                    float posV = pos.V + offset;
                    Gizmos.color = pos.GetColor();
                    Vector3 origin = new Vector3(posH, 0, posV);
                    Vector3 pos1 = new Vector3(posH - offset, 0, posV - offset);
                    Vector3 pos2 = new Vector3(posH + offset, 0, posV - offset);
                    Vector3 pos3 = new Vector3(posH + offset, 0, posV + offset);
                    Vector3 pos4 = new Vector3(posH - offset, 0, posV + offset);
                    Gizmos.DrawLine(origin, pos1);
                    Gizmos.DrawLine(origin, pos2);
                    Gizmos.DrawLine(origin, pos3);
                    Gizmos.DrawLine(origin, pos4);
                    Gizmos.DrawLine(pos1, pos2);
                    Gizmos.DrawLine(pos2, pos3);
                    Gizmos.DrawLine(pos3, pos4);
                    Gizmos.DrawLine(pos4, pos1);
                }
            }
        }


    }
}