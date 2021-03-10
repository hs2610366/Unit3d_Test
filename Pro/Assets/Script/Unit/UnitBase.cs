/**  
* 标    题：   UnitBase.cs 
* 描    述：   Unit 基础参数 和 函数
* 创建时间：   2018年03月06日 01:32 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game
{
    public class UnitBase
    {
        #region 属性
        /**                                     基础属性                                     */
        #region 标识
        /// <summary>
        /// 标识
        /// </summary>
        private string mTag;
        /// <summary>
        /// 标识
        /// </summary>
        public string Tag { get { return mTag; } set { mTag = value; } }
        #endregion

        #region 模型路径
        /// <summary>
        /// 模型路径
        /// </summary>
        private string mModPath = string.Empty;
        /// <summary>
        /// 模型路径
        /// </summary>
        public string ModPath { get { return mModPath; } set { mModPath = value; } }
        #endregion

        #region 缩放大小
        /// <summary>
        /// 缩放大小
        /// </summary>
        private float mScale = 1.0f;
        /// <summary>
        /// 缩放大小
        /// </summary>
        public float Scale
        {
            get { return mScale; }
            set { mScale = value; }
        }
        #endregion

        #region 移动速度
        /// <summary>
        /// 移动速度
        /// </summary>
        private float mMovingSpeed = 0;
        /// <summary>
        /// 移动速度
        /// </summary>
        public float MovingSpeed
        {
            get
            {
                return mMovingSpeed;
            }
            set
            {
                mMovingSpeed = value;
            }
        }
        #endregion

        #region 转身速率
        /// <summary>
        /// 转身速率
        /// </summary>
        private float mRotateRate = 0;
        /// <summary>
        /// 转身速率
        /// </summary>
        public float RotateRate
        {
            get
            {
                return mRotateRate;
            }
            set
            {
                mRotateRate = value;
            }
        }
        #endregion

        #region 重力
        private float mGravity = 0.098f;
        public float Gravity { get { return mGravity; } set { mGravity = value; } }
        #endregion

        /**                                     基础属性                                     */

        #region 坐标
        /// <summary>
        /// 当前坐标
        /// </summary>
        private Vector3 mPos = Vector3.zero;
        /// <summary>
        /// 目标坐标
        /// </summary>
        private Vector3 mTarPos = Vector3.zero;
        /// <summary>
        /// 当前坐标
        /// </summary>
        public Vector3 Pos
        {
            get
            {
                return mPos;
            }
        }
        /// <summary>
        /// 目标坐标
        /// </summary>
        public Vector3 TarPos
        {
            get
            {
                return mTarPos;
            }
            set
            {
                mTarPos = value;
            }
        }
        #endregion

        #region 旋转欧拉角角度
        /// <summary>
        /// 当前旋转欧拉角角度
        /// </summary>
        private Vector3 mAngles = Vector3.zero;
        /// <summary>
        /// 目标旋转欧拉角角度
        /// </summary>
        private Vector3 mTarAngles = Vector3.zero;
        /// <summary>
        /// 当前旋转欧拉角角度
        ///// </summary>
        public Vector3 Angles
        {
            get
            {
                return mAngles;
            }
        }
        /// <summary>
        /// 当前旋转欧拉角角度
        /// </summary>
        public Vector3 TarAngles
        {
            get { return mTarAngles; }
            set { mTarAngles = value; }
        }
        #endregion

        #region 移动方向
        /// <summary>
        /// 移动方向向量
        /// </summary>
        private Vector3 mMovingDir = Vector3.zero;
        #endregion

        #region 移动距离
        /// <summary>
        /// 当前剩余移动距离
        /// </summary>
        private float mMovingDis;
        /// <summary>
        /// 总移动距离
        /// </summary>
        private float mLimitMovingDis;
        #endregion

        #region 等待时间
        /// <summary>
        /// 等待时间
        /// </summary>
        private float mWaitTime = 0;
        /// <summary>
        /// 等待时间
        /// </summary>
        public float WaitTime
        {
            get
            {
                return mWaitTime;
            }
            set
            {
                mWaitTime = value;
            }
        }
        #endregion

        #region 时间标识
        /// <summary>
        /// 重力加速度
        /// </summary>
        private float mGravityTabTime = 0;
        /// <summary>
        /// 重力加速度
        /// </summary>
        public float GravityTabTime
        {
            get
            {
                return mGravityTabTime;
            }
            set
            {
                mGravityTabTime = value;
            }
        }
        /// <summary>
        /// 转身时间标识
        /// </summary>
        private float mRotateTabTime = 0;
        /// <summary>
        /// 等待时间标识
        /// </summary>
        private float mWaitTabTime = 0;
        #endregion

        #region 攻击范围
        /// <summary>
        /// 事件响应范围
        /// </summary>
        private float mRangeRadius = 0;
        /// <summary>
        /// 事件响应范围
        /// </summary>
        private float RangeRadius
        {
            get
            {
                return mRangeRadius;
            }
            set
            {
                mRangeRadius = value;
            }
        }
        #endregion

        #region 播放执行
        /// <summary>
        /// 是否执行刷新
        /// </summary>
        private bool mIsPlay = false;
        #endregion
        #endregion

        #region 构造函数
        public UnitBase()
        {
#if UNITY_ANDROID || UNITY_IPHONE
            Global.Instance.OnUpdate += Update;
#endif
        }

        public UnitBase(string tag)
        {
            mTag = tag;
#if UNITY_ANDROID || UNITY_IPHONE
            Global.Instance.OnUpdate += Update;
#endif
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 刷新
        /// </summary>
        public virtual void Update()
        {
            if (mIsPlay == false) return;
            CustomRefreshGravity();
            CustomUpdate();
        }
        #endregion

        #region 保护函数

        /// <summary>
        /// 刷新自由落体
        /// </summary>
        protected virtual void CustomRefreshGravity()
        {
            bool isHit = false;
            Vector3 hitPos = RaycastTool.Raycast(mPos, Vector3.down, LayerName.Gound, out isHit);
            if(isHit)
            {
                var dis = Vector3.Distance(mPos, hitPos);
                if (dis > 0)
                {
                    mGravityTabTime = Time.realtimeSinceStartup;
                }
                else if(dis < 0)
                {
                    mGravityTabTime = 0;
                    mPos = hitPos;
                }
            }
        }

        /// <summary>
        /// 自定义刷新
        /// </summary>
        protected virtual void CustomUpdate() { }

        /// <summary>
        /// 刷新位置
        /// </summary>
        protected virtual void RefreshPos()
        {
            if(mMovingDis > 0)
            {
                Vector3 delta = Moveing();
                mPos += delta;
                mMovingDis -= delta.magnitude;
            }
            else
            {
                mMovingDis = 0;
            }
            if (mGravityTabTime > 0)
            {
                float gravityDis = GravityDistance();
                mPos.y -= gravityDis;
            }
        }

        /// <summary>
        /// 刷新欧拉角角度
        /// </summary>
        protected virtual void RefreshAngles()
        {
            if(mRotateRate != 0)
            {
                if(mRotateTabTime > 0)
                {
                    float cur = Time.realtimeSinceStartup;
                    float off = cur - mRotateTabTime;
                    if (mRotateRate - off >= 0)
                    {
                        Mathex.GetRangeAngles(ref mAngles, ref mTarAngles);
                        if (off == 0) off = mRotateRate;
                        Vector3 angles = Mathex.GetLinearPoint(mAngles, mTarAngles, off);
                        SetEulerAngle(angles);
                    }
                    else
                    {
                        SetEulerAngle(mTarAngles);
                        mRotateTabTime = 0;
                    }

                } 
            }
            else
            {
                SetEulerAngle(mTarAngles);
            }
        }

        /// <summary>
        /// 通过目标坐标刷新欧拉角
        /// </summary>
        protected virtual void UpdateEulerAnges()
        {
            SetMoveTarget(mTarPos);
            RefreshAngles();
        }
        
        /// <summary>
        /// 移动数据
        /// </summary>
        protected Vector3 Moveing()
        {
            Vector3 delta = mMovingDir * Time.deltaTime * mMovingSpeed;
            return delta;
        }

        protected float GravityDistance()
        {
            return mGravityTabTime * mGravity;
        }

        /// <summary>
        /// 是否在等待时间
        /// </summary>
        /// <returns></returns>
        protected bool IsWait()
        {
            if(mWaitTabTime > 0 && mWaitTime > 0)
            {
                float off = Time.realtimeSinceStartup - mWaitTabTime;
                if (mWaitTime > off)
                {
                    return true;
                }
                else
                {
                    mWaitTime = 0;
                    mWaitTabTime = 0;
                }
            }
            return false;
        }

        /// <summary>
        /// 目标点是否在事件响应范围
        /// </summary>
        /// <param name="off"> 偏移距离 </param>
        /// <returns></returns>
        protected bool IsActionRange(float off)
        {
            return IsActionRange(mTarPos, off);
        }

        /// <summary>
        /// 目标点是否在事件响应范围
        /// </summary>
        /// <param name="off"> 偏移距离 </param>
        /// <returns></returns>
        protected bool IsActionRange(Vector3 tar, float off = 0)
        {
            if(mRangeRadius > 0)
            {
                return Vector3.Distance(mPos, tar) <= mRangeRadius;
            }
            return false;
        }
        #endregion

        #region 公有函数

        /// <summary>
        /// 设置欧拉角角度
        /// </summary>
        public void SetEulerAngle(Vector3 vec3)
        {
            vec3.y = Mathex.GetModAngles(vec3.y);
            mAngles = vec3;
        }

        /// <summary>
        /// 设置移动目标参数
        /// </summary>
        /// <param name="vec3"> 目标坐标 </param>
        /// <param name="offsetY"> Y轴相对偏移值（旋转角度计算 某些模型朝向非z轴正向） </param>
        /// <param name="axisY"> 旋转角度是否计算Y轴 </param>
        public void SetMoveTarget(Vector3 vec3, float offsetY = 0, bool axisY = false)
        {
            bool rotate = false;
            TarPos = vec3;
            Vector3 movingDir = mTarPos - mPos;
            rotate = movingDir != mMovingDir;
            mMovingDis = movingDir.magnitude;
            mLimitMovingDis = movingDir.magnitude;
            if (mMovingDis > 0)
            {
                mMovingDir = movingDir.normalized;
            }
            Vector3 angles = Vector3.zero;
            if (axisY == false)
            {
                angles = Mathex.GetAnglesNoY(mPos, vec3);
            }
            else
            {
                angles = Mathex.GetAngles(mPos, vec3);
            }
            angles.y += offsetY;
            mTarAngles = new Vector3(angles.z, angles.y, angles.x);  //项目用法  不知是否是模型偏移了90°导致的
            if (rotate == true)
            {
                if (mRotateRate == 0) return;
                mRotateTabTime = Time.realtimeSinceStartup;
            }
        }

        /// <summary>
        /// 作用位置
        /// </summary>
        /// <returns></returns>
        public virtual Vector3 ActionPos()
        {
            return mPos;
        }

        public virtual void Play()
        {
            mIsPlay = true;
        }

        public virtual void SetPaused(bool state)
        {
            mIsPlay = !state;
        }

        public virtual void Resume()
        {
            mIsPlay = false;
        }

        public virtual void Stop()
        {
            mIsPlay = false;
        }

        public virtual void Reset()
        {
            mIsPlay = false;
            mPos = mTarPos = Vector3.zero;
            mAngles = mTarAngles = Vector3.zero;
            mScale = 1.0f;
            mMovingDir = Vector3.zero;
            mMovingDis = 0;
            mLimitMovingDis = 0;
            mMovingSpeed = 0;
            mRotateRate = 0;
            mWaitTime = 0;
            mRangeRadius = 0;
            mRotateRate = 0;
            mWaitTabTime = 0;
            mRotateTabTime = 0;
        }

        public virtual void Dispose()
        {
        }
        #endregion
    }
}