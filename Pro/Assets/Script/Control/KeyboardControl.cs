/**  
* 标    题：   KeyboardControl.cs 
* 描    述：    
* 创建时间：   2018年10月05日 03:20 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class KeyboardControl
    {
        public static void OnUpdate()
        {
            //方向
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) { Debug.Log("上"); }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) { Debug.Log("下"); }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) { Debug.Log("左"); }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) { Debug.Log("右"); }
            if (Input.GetKeyDown(KeyCode.Alpha1)) { Debug.Log("技能1"); }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { Debug.Log("技能2"); }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { Debug.Log("技能3"); }
            if (Input.GetKeyDown(KeyCode.Alpha4)) { Debug.Log("技能4"); }
            if (Input.GetKeyDown(KeyCode.Space)) { Debug.Log("您按下了空格键"); }
        }
	}
}