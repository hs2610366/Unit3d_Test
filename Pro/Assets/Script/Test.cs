/**  
* 标    题：   Test.cs 
* 描    述：    
* 创建时间：   2018年11月12日 01:09 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class Test : MonoBehaviour
    {

        public float Dis;
        public float H;
        public float V;

		// Use this for initialization
		void Start () {

            System.Net.ServicePointManager.Expect100Continue = false;
            HttpDownLoad.Instance.AddDownLoadPath("/assets/dizuo_Battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/gongjitisheng_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/hp1_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/hp2_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/hp3_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/hp4_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/jianbian_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/jiesuan1_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/jiesuan2_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/jiesuanhei_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/pause_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/shibingicon_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/shijiao_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/shiqi1_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/shiqi1bg_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/shiqi2_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/shiqi2bg_battle.png");
            HttpDownLoad.Instance.AddDownLoadPath("/assets/template.tps");
            HttpDownLoad.Instance.Run();
		}
		
		// Update is called once per frame
		void Update ()
        {
            CameraControl cc = CameraMgr.CC;

            if (cc == null) return;
            Dis = cc.mDistance;
            H = cc.mHorizontalAngle;
            V = cc.mVerticalAngle;
		}

	}
}