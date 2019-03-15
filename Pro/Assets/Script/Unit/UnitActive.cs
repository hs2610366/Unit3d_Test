/**  
* 标    题：   UnitActive.cs 
* 描    述：    
* 创建时间：   2018年03月17日 22:57 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class UnitActive : UnitAnim
    {

        #region 速度
        /// <summary>
        /// 移动速度
        /// </summary>
        private float mSpeed = 1.0f;
        public float Speed
        {
            get { return mSpeed; }
            set { mSpeed = value; }
        }
        /// <summary>
        /// 旋转角度
        /// </summary>
        private float mASpeed = 1000.0f;
        #endregion

        #region 重力
        private float mGravity = - 9.8f;
        public float Gravity
        {
            set { mGravity = value; }
            get { return mGravity; }
        }
        #endregion

        #region 角度
        private float mAngle = 0.0f;
        public float Angle
        {
            set { mAngle = value; }
            get { return mAngle; }
        }
        #endregion

        #region 是否移动
        /// <summary>
        /// 是否移动
        /// </summary>
        private bool mIsMove = false;
        public bool IsMove
        {
            set { mIsMove = value; }
            get { return IsMove; }
        }

        /// <summary>
        /// 坠落
        /// </summary>
        private bool mIsDrop = false;
        public bool IsDrop
        {
            set { mIsDrop = value; }
            get { return mIsDrop; }
        }
        #endregion


        public UnitActive():base()
        {
        }

        #region 私有函数
        protected override void Update()
        {
            base.Update();
            if (!Controller) return;
            if (!Trans) return;
            if (!Controller) return;
            UpdateGravity();
            UpateEuler();
            //if (Trans.eulerAngles.y != mAngle) return;
            UpdateMove();
        }

        private void UpdateGravity()
        {
            Vector3 dir = Trans.position + Vector3.down;
            Ray ray = new Ray(Trans.position, dir);
            Debug.DrawLine(Trans.position, dir, Color.red, 2);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.layer == LayerName.GetIndexOfLayerName(LayerName.Gound))
                {
                    mIsDrop = true;
                    return;
                }
            }
            mIsDrop = false;
        }

        private void UpateEuler()
        {
            float speed = mASpeed * Time.deltaTime;
            float angle = Mathf.MoveTowardsAngle(Trans.eulerAngles.y, mAngle, speed);
            Trans.eulerAngles = new Vector3(0, angle, 0);
        }

        private void UpdateMove()
        {
            float speed = 0;
            float g = 0;
            if (mIsMove) speed = mSpeed / 7.0f;
            if (mIsDrop) g = mGravity;
            if (g != 0) g -= g * Time.deltaTime;
            Controller.Move(Trans.TransformDirection(0, g, -speed));
        }
        #endregion


        public void UpdateEuler(Vector3 euler)
        {
            if (Trans == null) return;
            Trans.eulerAngles = euler;
        }

        public void UpdatePos(Vector3 pos)
        {
            if (Trans == null) return;
            Trans.position = pos;
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}