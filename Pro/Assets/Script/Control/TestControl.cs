/**  
* 标    题：   TestControl.cs 
* 描    述：    
* 创建时间：   2019年07月19日 01:15 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class TestControl : MonoBehaviour {


        public Transform mTrans;

        public Transform mLArea;
        public Transform mAreaBg;
        public Transform mCircle;

        public Transform mRArea;

        private UnitPlayer mPlayer;
        /// <summary>
        /// 定点
        /// </summary>
        private bool mIsFixedPos = false;
        /// <summary>
        /// 是否是点击区域
        /// </summary>
        private bool mIsClickArea = false;
        /// <summary>
        /// 遥感距离
        /// </summary>
        private float DISTANCE = 400.0f;
        private float ACTION_DISTANCE = 95.0f;
        private Vector2 Offset = new Vector2(250.0f, 250.0f);

        #region 保护函数
        void Awake()
        {
            mLArea = TransTool.Find(mTrans, "LArea");
            mAreaBg = TransTool.Find(mTrans, "LArea/AreaBg");
            mCircle = TransTool.Find(mTrans, "LArea/Circle");

            mRArea = TransTool.Find(mTrans, "RArea");
        }

        private void OnEnable()
        {
            CustomAddEvent();
        }

        private void OnDisable()
        {
            CustomRemoveEvent();
        }

        private void CustomAddEvent()
        {
            Global.Instance.OnUpdate += OnUpate;
            UIEventListener.Get(mLArea).OnClick = OnCcCc;
            //TouchManager.Instance.AddTouchEvent(mLArea, OnCcCc);
            EventMgr.Instance.Add(EventKey.CreateUnit, OnCreateUnit);
        }

        private void CustomRemoveEvent()
        {
            Global.Instance.OnUpdate -= OnUpate;
            //TouchManager.Instance.RemoveTouchEvent(mLArea, OnCcCc);
            EventMgr.Instance.Remove(EventKey.CreateUnit, OnCreateUnit);
        }

        private void OnCcCc(GameObject go)
        {
        }

        private void OnCreateUnit(params object[] objs) 
        {
            if (UnitMgr.Instance.Player != null)
            {
                mPlayer = UnitMgr.Instance.Player;
                //uint id = mPlayer.CTemp.id;
                //RemoteControl.Instance.AddCommand(id, new MoveCommand(mPlayer), new AttackCommand(mPlayer));
            }
        }



        private void OnUpate()
        {
            if (!mLArea) return;
            if (!mAreaBg) return;
            if (!mCircle) return;
            Vector2 inputPos = TouchConst.TouchPosition();
            if (!mIsClickArea && TouchConst.TouchBegin())
            {
                if (inputPos.x >= 0 && inputPos.x < Offset.x * 2 && inputPos.y > 0 && inputPos.y < Offset.y * 2)
                {
                    mIsClickArea = true;
                }
            }
            if (!mIsClickArea) return;
            if (TouchConst.TouchIng())
            {
                KeyboardControl.IsRestrict = true;
                if (!mLArea.gameObject.activeSelf) mLArea.gameObject.SetActive(true);
                UpdateAreaBgPos(inputPos);
                UpdateCirclePos(inputPos);
                //UnitMgr.Instance.SetPlayerMove(true);
                if (mPlayer != null)
                    RemoteControl.Instance.ExecuteMove(mPlayer.CTemp.id);
            }
            else if(TouchConst.TouchEnd())
            {
                if(mPlayer != null)
                    RemoteControl.Instance.UndoMove(mPlayer.CTemp.id);
                //UnitMgr.Instance.SetPlayerMove(false);
                mIsFixedPos = false;
                mIsClickArea = false;
                if (mLArea.gameObject.activeSelf) mLArea.gameObject.SetActive(false);
                KeyboardControl.IsRestrict = false;
            }
        }

        #region 更新遥感位置
        /// <summary>
        /// 更新遥感定点位置
        /// </summary>
        /// <param name="pos"></param>
        private void UpdateAreaBgPos(Vector3 pos)
        {
            if (!mIsFixedPos)
            {
                mIsFixedPos = true;
                float dic = Vector3.Distance(Vector3.zero, pos);
                if (dic < DISTANCE)
                {
                    mAreaBg.localPosition = pos;
                }
                else
                {
                    float angle = GetAngle(pos);
                    mAreaBg.localPosition = GetPos(pos, angle, DISTANCE);
                }
            }
        }

        /// <summary>
        /// 更新遥感按钮位置
        /// </summary>
        private void UpdateCirclePos(Vector3 pos)
        {
            Vector3 originPos = mAreaBg.localPosition;
            Vector3 offsetPos = pos - originPos;
            float angle = GetAngle(offsetPos);
            float dic = Vector3.Distance(originPos, pos);
            if (dic < ACTION_DISTANCE)
            {
                mCircle.localPosition = pos;
            }
            else
            {
                mCircle.localPosition = GetPos(offsetPos, angle, ACTION_DISTANCE) + originPos;
            }
            UnitMgr.Instance.UpdatePlayerAngle(angle + 180);
        }

        private float GetAngle(Vector3 pos)
        {
            //两个向量的夹角
            float angle = Vector3.Angle(Vector3.up, pos);
            //计算向量的方向
            float dir = (Vector3.Dot(Vector3.forward, Vector3.Cross(Vector3.up, pos)) < 0 ? 1 : -1);
            angle *= dir;
            return angle;
        }

        /// <summary>
        /// 获取转换的坐标
        /// </summary>
        /// <param name="origin"> 原点 </param>
        /// <param name="pos"> 目标点 </param>
        /// <param name="dis"> 最大距离 </param>
        /// <returns></returns>
        private Vector3 GetPos(Vector3 pos, float angle, float dis)
        {
            //通过角度计算摇杆位置
            Quaternion quaternion = Quaternion.Euler(0, 0, -angle);
            return quaternion * new Vector3(0, dis, 0);
        }
        #endregion

        private void Clean()
        {
            mIsFixedPos = false;
            mIsClickArea = false;
            if (mLArea) mLArea.gameObject.SetActive(false);
        }

        void Destroy()
        {
            Clean();
            mLArea = null;
            mAreaBg = null;
            mCircle = null;
            mRArea = null;
        }

        #endregion
    }

}