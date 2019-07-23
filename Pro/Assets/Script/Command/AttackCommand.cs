/**  
* 标    题：   NewBehaviourScript.cs 
* 描    述：    
* 创建时间：   2019年07月18日 16:01 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class AttackCommand : ICommand
    {
        private Unit mUnit;

        public AttackCommand(Unit unit)
        {
            mUnit = unit;
        }

        public void Execute()
        {
            if (mUnit == null) return;
            mUnit.Fight();
        }

        public void UndoExecute()
        {
            if (mUnit == null) return;
            mUnit.UndoFight();
        }
        public void Dispose()
        {
            mUnit = null;
        }

    }
}