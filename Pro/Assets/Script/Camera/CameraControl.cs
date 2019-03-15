/**  
* 标    题：   CameraControl.cs 
* 描    述：    
* 创建时间：   2018年03月08日 23:08 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class CameraControl : CameraControlBase
    {

        #region 参数
        /// <summary>
        /// 高度
        /// </summary>
        public float mHeight = 4.5f;
        /// <summary>
        /// 相机和目标的距离
        /// </summary>
        public float mDistance = 30.0f;
        /// <summary>
        /// 最小镜头距离
        /// </summary>
        public float mMinDistance = 5.0f;
        /// <summary>
        /// 最大镜头距离
        /// </summary>
        public float mMaxDistance = 30.0f;
        /// <summary>
        /// 摄像机竖直方向的角度
        /// </summary>
        public float mVerticalAngle = 50;
        /// <summary>
        /// 摄像机竖直方向的最小角度
        /// </summary>
        public float mMinVerticalAngle = 0;
        /// <summary>
        /// 摄像机竖直方向的最大角度
        /// </summary>
        public float mMaxVerticalAngle = 50;
        /// <summary>
        /// 摄像机水平环绕角色的角度
        /// </summary>
        public float mHorizontalAngle = 0 ;
        /// <summary>
        /// 鼠标滚轮拉近拉远速度系数
        /// </summary>
        public float mScrollFactor = 10.0f;
        /// <summary>
        /// 镜头旋转速度比率
        /// </summary>
        public float mRotateFactor = 10.0f;
        /// <summary>
        /// 持续抖动的时长
        /// </summary>
        private float mShake = 0.0f;
        /// <summary>
        /// 是否抖动摄像机
        /// </summary>
        public bool IsShaking
        {
            set
            {
                if (value) mShake = 1.5f;
                else mShake = 0.0f;
            }
        }
        /// <summary>
        /// 振幅越大抖动越厉害
        /// 抖动幅度（振幅）
        /// </summary>
        private float shakeAmount = 0.1f;
        /// <summary>
        /// 消长因素
        /// </summary>
        private float decreaseFactor = 1.0f;

        private float value = 100.0f;
        #endregion


        public CameraControl(Camera camera):base(camera)
        {
        }

        #region 重写函数
        public override void UpdateControl()
        {
            if (!Check()) return;
            UpdateCustom();
        }

        protected override bool Check()
        {
            if (Target == null)
            {
                MessageBox.Error("hs", "没有角色目标");
                return false;
            }
            else if (Target.name.ToString() == "null")
            {
                MessageBox.Error("hs", "角色目标已销毁");
                return false;
            }
            if (Camera == null)
            {
                MessageBox.Error("hs", "没有场景摄像机");
                return false;
            }
            return true;
        }

        protected override void UpdateCustom()
        {
            UpdateHorizontalAngle();
            UpdateVerticalAngle();
            UpdateDistance();
            UpdateCameraStatus();
            UpdateShake();
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 更新水平角度移动
        /// </summary>
        private void UpdateHorizontalAngle()
        {
            if (TouchConst.TouchIng()) return;
            float axis = 0f;
#if UNITY_EDITOR
            if (Input.GetMouseButton(1))
            {
                axis = Input.GetAxis("Mouse X");
            }
#elif UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_WP_8_1 || UNITY_BLACKBERRY
            if (Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                Vector2 delta = Input.GetTouch(0).deltaPosition;
                axis = delta.x;
            }
#endif
            mHorizontalAngle += axis * mRotateFactor;
        }

        /// <summary>
        /// 更新竖直角度移动
        /// </summary>
        private void UpdateVerticalAngle()
        {
            if (TouchConst.TouchIng()) return;
            float axis = 0f;
#if UNITY_EDITOR
            axis = Input.GetAxis("Mouse ScrollWheel");
#elif UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_WP_8_1 || UNITY_BLACKBERRY
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 delta = Input.GetTouch(0).deltaPosition;
                axis = delta.x;
            }
#endif
            mVerticalAngle += axis * mScrollFactor * 2;
            if (mVerticalAngle < mMinVerticalAngle) mVerticalAngle = mMinVerticalAngle;
            else if (mVerticalAngle > mMaxVerticalAngle) mVerticalAngle = mMaxVerticalAngle;
        }

        private void UpdateDistance()
        {
            float axis = 0f;
#if UNITY_EDITOR
            axis = Input.GetAxis("Mouse ScrollWheel");
#elif UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_WP_8_1 || UNITY_BLACKBERRY
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 delta = Input.GetTouch(0).deltaPosition;
                axis = delta.x;
            }
#endif
            mDistance += axis * mScrollFactor;
            if (mDistance < mMinDistance) mDistance = mMinDistance;
            else if (mDistance > mMaxDistance) mDistance = mMaxDistance;
        }

        /// <summary>
        /// 更新摄像机状态
        /// </summary>
        private void UpdateCameraStatus()
        {
            Quaternion rotation = Quaternion.Euler(mVerticalAngle, mHorizontalAngle, 0);
            var offset = rotation * Vector3.back * mDistance;
            Camera.transform.position = Target.position + offset + Vector3.up * mHeight;
            Camera.transform.rotation = rotation;
        }

        /// <summary>
        /// 相机震动
        /// </summary>
        private void UpdateShake()
        {
            if (mShake > 0)
            {
                Camera.position = Camera.position + UnityEngine.Random.insideUnitSphere * shakeAmount;
                mShake -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                mShake = 0f;
            }
        }
        #endregion

        #region 公开函数

        /// <summary>
        /// 更新摄像机锁定对象
        /// </summary>
        public override void UpdateTrans(Transform trans)
        {
            if (trans == null)
            {
                MessageBox.Error("HS", "传入角色摄像机控制的角色对象为null!!");
                return;
            }
            base.UpdateTrans(trans);
        }
        #endregion
    }
}