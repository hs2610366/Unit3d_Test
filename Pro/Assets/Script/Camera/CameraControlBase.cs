/**  
* 标    题：   CameraControl.cs 
* 描    述：    
* 创建时间：   2018年03月08日 23:01 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class CameraControlBase
    {
        private Camera mCamera;
        public Transform Camera { get { return mCamera.transform; } }
        private Transform mTrans;
        public Transform Target { get { return mTrans; } }

        public CameraControlBase(Camera camera)
        {
            mCamera = camera;
        }

        public virtual void UpdateTrans(Transform trans) { mTrans = trans; }

        public virtual void UpdateControl() { }

        protected virtual bool Check() { return false; }

        protected virtual void UpdateCustom() { }

    }
}