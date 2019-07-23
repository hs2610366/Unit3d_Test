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
        public static bool IsRestrict = false;
        public static void OnUpdate()
        {
            /**
            if (IsRestrict==true) return;
            bool isMove = false;
            //方向
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                UnitMgr.Instance.UpdatePlayerAngle(180);
                Debug.Log("上");
                isMove = true;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                UnitMgr.Instance.UpdatePlayerAngle(0);
                Debug.Log("下");
                isMove = true;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                UnitMgr.Instance.UpdatePlayerAngle(90);
                Debug.Log("左");
                isMove = true;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                UnitMgr.Instance.UpdatePlayerAngle(270);
                Debug.Log("右");
                isMove = true;
            }
            
            if (UnitMgr.Instance.Player != null)
            {
                if (isMove)
                    RemoteControl.Instance.ExecuteMove(UnitMgr.Instance.Player.CTemp.id);
                else
                    RemoteControl.Instance.UndoMove(UnitMgr.Instance.Player.CTemp.id);
            }
            */
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                RemoteControl.Instance.ExecuteAttack(UnitMgr.Instance.Player.CTemp.id);
                Debug.Log("平a");
            }
            if (Input.GetKeyDown(KeyCode.Alpha1)) { Debug.Log("技能1"); }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { Debug.Log("技能2"); }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { Debug.Log("技能3"); }
            if (Input.GetKeyDown(KeyCode.Alpha4)) { Debug.Log("技能4"); }
            if (Input.GetKeyDown(KeyCode.Space)) { Debug.Log("您按下了空格键"); }
        }
	}
}