/**  
* 标    题：   Single.cs 
* 描    述：    
* 创建时间：   2018年03月03日 16:23 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Divak.Script.Game 
{
	public class Single
    {
        public static readonly Single Instance = new Single();
        UnitPlayer player;
        public void Init()
        {
            //UIMgr.Open("UIControl");
            player =  UnitMgr.Instance.CreatePlayer(101,"希尔瓦纳斯", Vector3.zero);
            //return;
            //SceneMgr.Instance.EnterScene(10001);
        }


        private void LoadSceneComplete(string name)
        {
        }

        public void OnGUI()
        {
            if(GUI.Button(new Rect(0,0, 200,200),"1"))
            {
                player.Execute("Idea1");
            }
            if (GUI.Button(new Rect(300, 0, 200, 200), "2"))
            {
                player.Execute("Idea2");
            }
            if (GUI.Button(new Rect(600, 0, 200, 200), "run"))
            {
                player.Execute("Run");
            }
        }
    }
}