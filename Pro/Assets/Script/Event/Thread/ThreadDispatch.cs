/**  
* 标    题：   ThreadDispatch.cs 
* 描    述：    
* 创建时间：   2021年03月09日 14:54 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class ThreadDispatch : MonoInstance<ThreadDispatch>
    {
        #region 参数
        public static float MAX_EXECUTE_TIME { get; set; } = int.MaxValue; //= 1f / 30f;
        #endregion

        #region 对象

        private Queue<EventData> _actions;
        #endregion

        #region 私有函数
        private void Awake()
        {
            _actions = new Queue<EventData>(16);
        }

        private void LateUpdate()
        {
            if (_actions.Count > 0)
            {
                float time = Time.realtimeSinceStartup;
                while (_actions.Count > 0)
                {
                    if (_actions.Count <= 0 || Time.realtimeSinceStartup - time > MAX_EXECUTE_TIME)
                        break;
                    _actions.Dequeue().Execute?.Invoke();
                }
            }
        }
        #endregion

        #region 公开函数
        public static void PushEvent(EventData eventData)
        {
            Instance._actions.Enqueue(eventData);
        }
        #endregion

    }


}