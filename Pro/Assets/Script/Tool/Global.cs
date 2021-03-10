/**  
* 标    题：   Global.cs 
* 描    述：    
* 创建时间：   2017年07月24日 02:43 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class Global
    {
        public static readonly Global Instance = new Global();
        private MonoMgr mMono;
        /// <summary>
        /// MonoUpdate事件
        /// </summary>
        public Action OnUpdate;
        /// <summary>
        /// 初始化完成回调
        /// </summary>
        public event Fun GlobalInit;

        #region 初始化
        public void Init(MonoMgr mono)
        {
            this.mMono = mono;
            AssetsMgr.Instance.Init(OnLoadMABMComplete);
            Config.Init();
            mMono.OnUpdate += Update;
        }

        private void OnLoadMABMComplete()
        {
            TempMgr.Init();
            CameraMgr.Init();
            TouchConst.Init();
            UIMgr.Init();
            GlobalInit();
        }
        #endregion

        #region 协程
        /// <summary>
        /// StartCoroutine(IEnumerator function) 启动的协程调用 StopCoroutine(IEnumerator function) 并不能停止，
        /// 需要使用 StartCoroutine(string function, object value)启动 并调用StopCoroutine(string function)进行停止
        /// </summary>
        /// <param name="function"></param>
        public void StartCoroutine(IEnumerator function)
        {
            if (mMono == null)
            {
                MessageBox.Error("MonoMgr脚本不存在！");
                return;
            }
            mMono.StartCoroutine(function);
        }
        /// <summary>
        /// StartCoroutine(IEnumerator function) 启动的协程调用 StopCoroutine(IEnumerator function) 并不能停止，
        /// 需要使用 StartCoroutine(string function, object value)启动 并调用StopCoroutine(string function)进行停止
        /// </summary>
        /// <param name="function">协程方法名</param>
        /// <param name="value">参数</param>
        public void StartCoroutine(string function, object value)
        {
            if (mMono == null)
            {
                MessageBox.Error("MonoMgr脚本不存在！");
                return;
            }
            mMono.StartCoroutine(function, value);
        }

        public void StartCoroutine(string function)
        {
            if (mMono == null)
            {
                MessageBox.Error("MonoMgr脚本不存在！");
                return;
            }
            mMono.StartCoroutine(function);
        }

        /// <summary>
        /// StartCoroutine(IEnumerator function) 启动的协程调用 StopCoroutine(IEnumerator function) 并不能停止，
        /// 需要使用 StartCoroutine(string function, object value)启动 并调用StopCoroutine(string function)进行停止
        /// </summary>
        /// <param name="function"></param>
        public void StopCoroutine(IEnumerator function)
        {
            if (mMono == null)
            {
                MessageBox.Error("MonoMgr脚本不存在！");
                return;
            }
            mMono.StopCoroutine(function);
        }

        /// <summary>
        /// StartCoroutine(IEnumerator function) 启动的协程调用 StopCoroutine(IEnumerator function) 并不能停止，
        /// 需要使用 StartCoroutine(string function, object value)启动 并调用StopCoroutine(string function)进行停止
        /// </summary>
        /// <param name="function"></param>
        public void StopCoroutine(string function)
        {
            if (mMono == null)
            {
                MessageBox.Error("MonoMgr脚本不存在！");
                return;
            }
            mMono.StopCoroutine(function);
        }
        #endregion

        #region 更新
        public void Update()
        {
            if(OnUpdate != null)
                OnUpdate();
#if UNITY_EDITOR
            KeyboardControl.OnUpdate();
#endif
        }
        #endregion


    }
}