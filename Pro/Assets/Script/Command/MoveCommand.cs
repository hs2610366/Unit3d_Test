

using System;
/**  
* 标    题：   MoveCommand.cs 
* 描    述：    
* 创建时间：   2019年07月18日 11:43 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class MoveCommand : ICommand
    {
        private Unit mUnit;
        public MoveCommand(Unit unit)
        {
            mUnit = unit;
        }

        public void Execute(UnitState state)
        {
            if (mUnit == null) return;
            mUnit.Move();
        }

        public void UndoExecute(UnitState state)
        {
            if (mUnit == null) return;
            mUnit.UndoMove();
        }

        public void Dispose()
        {
            mUnit = null;
        }
    }
}