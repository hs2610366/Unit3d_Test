/**  
* 标    题：   TouchManager.cs 
* 描    述：    
* 创建时间：   2018年03月16日 03:24 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class TouchManager
    {
        public static readonly TouchManager Instance = new TouchManager();

        private event TransEvent _touchEvent;

        private Dictionary<Transform, TransEvent> _btnBackFunction = new Dictionary<Transform, TransEvent>();

        private Camera mCamera;
        private Transform mTrans;

        // Use this for initialization
        public TouchManager()
        {
            mCamera =  UIMgr.UICamera;
            mTrans = mCamera.transform;
            Global.Instance.OnUpdate += TouchUpdate;
        }

        // Update is called once per frame
        private void TouchUpdate()
        {
            if (TouchConst.TouchEnd())
            {

                RaycastHit hit;
                Ray ray = mCamera.ScreenPointToRay(TouchConst.TouchPosition());
                Debug.DrawLine(ray.origin, mTrans.position, Color.red, 2);
                if (Physics.Raycast(ray, out hit))//射线发出并碰撞到外壳，那么手臂就应该朝向碰撞点  
                {
                    GameObject gameObject = hit.collider.gameObject;
                    Debug.DrawLine(ray.origin, new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100), Color.red, 2);
                    DispatchTouchEvent(gameObject.transform);
                }
                /**
                RaycastHit objhit;
                //Ray ray = _uiCamera.ScreenPointToRay(Input.mousePosition);
                Vector3 originPos = mTrans.position;
                originPos.z -= 1;
                Ray ray = new Ray(originPos, Vector3.forward);
                Debug.DrawLine(ray.origin, Vector3.forward, Color.red, 2);
                if (Physics.Raycast(ray, out objhit) == true)
                {
                    GameObject gameObject = objhit.collider.gameObject;
                    Debug.DrawLine(ray.origin, new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100), Color.red, 2);
                    DispatchTouchEvent(gameObject.transform);
                }
    */
            }
        }

        public void AddTouchEvent(Transform trans, TransEvent function)
        {
            if (_btnBackFunction.ContainsKey(trans) == false)
            {
                _btnBackFunction.Add(trans, _touchEvent);
            }
            _btnBackFunction[trans] += function;
        }
        public void RemoveTouchEvent(Transform trans, TransEvent function)
        {
            if (_btnBackFunction.ContainsKey(trans) == true)
            {
                _btnBackFunction[trans] -= function;
            }
            if (_btnBackFunction[trans] == null) _btnBackFunction.Remove(trans);
        }

        public void DispatchTouchEvent(Transform trans)
        {
            if (_btnBackFunction.ContainsKey(trans) == true)
            {
                _touchEvent = _btnBackFunction[trans] as TransEvent;
                _touchEvent(trans);
            }
        }

    }
}