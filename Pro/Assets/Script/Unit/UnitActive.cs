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
        private const float mSpeed = 1.2f;
        /// <summary>
        /// 旋转角度速率
        /// </summary>
        private const float mASpeed = 1000.0f;
        #endregion

        #region 重力
        private const float G = 0.098f;
        private float mGravity = 0.098f;
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
        private bool mIsDrop = true;
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
            float dis = Controller.height / 2;
            Vector3 dir = Vector3.down;
            Vector3 origin = Trans.position;
            origin.y = origin.y + dis;
            Ray ray = new Ray(origin, dir);
            Debug.DrawLine(origin, dir, Color.red, 2);
            RaycastHit hit;
            int layer = 1<<LayerMask.NameToLayer(LayerName.Gound);
            if (Physics.Raycast(ray, out hit, 100, layer))
            {
                if (hit.distance > dis)
                {
                    mGravity =  -G;
                }
                else if(hit.distance < dis)
                {
                    mGravity = G;
                }
                else
                {
                    mIsDrop = false;
                    return;
                }
                mIsDrop = true;
                return;
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

        #region 接收命令
        /// <summary> 接收命令 </summary>
        /// 
        public void EulerAngles()
        {
        }

        public void Move()
        {
            AnimInfo info = mUnitState[UnitState.Run];
            if (info == null) return;
            if (Execute(info.Name)) mIsMove = true;
        }

        public void UndoMove()
        {
            AnimInfo info = mUnitState[UnitState.Run];
            if (info == null) return;
            if (Undo(info.Name)) mIsMove = false;
        }

        public void Fight(UnitState state)
        {
            AnimInfo info = mUnitState[state];
            if (info == null) return;
            if(Execute(info.Name)) mIsMove = false;
        }

        public void UndoFight(UnitState state)
        {
            AnimInfo info = mUnitState[state];
            if (info == null) return;
            Execute(info.Name);
        }
        /// <summary> 接收命令 </summary>
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