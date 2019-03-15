/**  
* 标    题：   Test.cs 
* 描    述：    
* 创建时间：   2018年11月12日 01:09 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class Test : MonoBehaviour
    {

        public float Dis;
        public float H;
        public float V;

		// Use this for initialization
		void Start () {
				
		}
		
		// Update is called once per frame
		void Update ()
        {
            CameraControl cc = CameraMgr.CC;

            if (cc == null) return;
            Dis = cc.mDistance;
            H = cc.mHorizontalAngle;
            V = cc.mVerticalAngle;
		}

	}
}