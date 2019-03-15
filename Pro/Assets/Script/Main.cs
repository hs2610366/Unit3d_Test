/**  
* 标    题：   Main.cs 
* 描    述：   启动脚本
* 创建时间：   2018年03月03日 15:32 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game
{
    public class Main : MonoBehaviour
    {

        private GameType mGameType;
        void Awake()
        {
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height / 2), "Single"))
            {
                StarGame(false);
            }
            else if (GUI.Button(new Rect(0, Screen.height / 2, Screen.width, Screen.height / 2), "NetWork"))
            {
                StarGame(true);
            }
        }

        private void StarGame(bool isNetWork)
        {
            Global.Instance.GlobalInit += OnGlobalInit;
            if (!isNetWork)
            {
                mGameType = GameType.Single;
            }
            else
            {
                mGameType = GameType.NetWork;
            }
            Global.Instance.Init(gameObject.AddComponent<MonoMgr>());

        }

        private void OnGlobalInit()
        {
            Global.Instance.GlobalInit -= OnGlobalInit;
            if (mGameType == GameType.Single)
            {
                Single.Instance.Init();
            }
            else
            {
#if UNITY_EDITOR
#else
                    AssetsHotUpdateMgr.Instance.StartUpdate(() =>
                    {
                        UIMgr.Open(UIName.UILogin);
                    });
#endif
            }
            this.enabled = false;
        }
    }
}