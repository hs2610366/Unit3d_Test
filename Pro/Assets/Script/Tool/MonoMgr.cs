/**  
* 标    题：   Mono.cs 
* 描    述：    
* 创建时间：   2017年07月29日 01:10 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class MonoMgr : MonoBehaviour
    {
        public Action OnUpdate;

        void Awake()
        {
            DontDestroyOnLoad(this);
        }

		// Use this for initialization
		void Start () {
				
		}
		
		// Update is called once per frame
		void Update ()
        {
			if(OnUpdate != null)
            {
                OnUpdate();
            }
		}

        void OnGUI()
        {

            Single.Instance.OnGUI();
        }

    }
}