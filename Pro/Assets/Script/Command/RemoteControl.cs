/**  
* 标    题：   RemoteControl.cs 
* 描    述：   命令模式控制器
* 创建时间：   2019年07月18日 16:03 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class RemoteControl
    {
        public static readonly RemoteControl Instance = new RemoteControl();

        private Dictionary<uint, ICommand> MoveDic = new Dictionary<uint, ICommand>();
        private Dictionary<uint, ICommand> AttackDic = new Dictionary<uint, ICommand>();

        public void AddCommand(uint id, MoveCommand move, AttackCommand attack)
        {
            if(!MoveDic.ContainsKey(id)) MoveDic.Add(id, move);
            if(!AttackDic.ContainsKey(id)) AttackDic.Add(id, attack);
        }

        public void RemoveCommand(uint id)
        {
            if (MoveDic.ContainsKey(id))
            {
                MoveCommand move = MoveDic[id] as MoveCommand;
                move.Dispose();
                move = null;
                MoveDic.Remove(id);
            }
            if (AttackDic.ContainsKey(id))
            {
                AttackCommand att = MoveDic[id] as AttackCommand;
                att.Dispose();
                att = null;
                AttackDic.Remove(id);
            }
        }
    }
}