/**  
* 标    题：   MonoInstance.cs 
* 描    述：    
* 创建时间：   2021年03月09日 14:59 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class MonoInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region 属性

        private static T _sInstance;

        public static T Instance
        {
            get
            {
                if (_sInstance == null)
                {
                    _sInstance = FindObjectOfType<T>();
                    if (_sInstance == null)
                    {
                        _sInstance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                    }
                    DontDestroyOnLoad(_sInstance.gameObject);
                }
                return _sInstance;
            }
        }
        #endregion

        #region 公开函数
        #endregion
    }
}