/**  
* 标    题：   BaseCommand.cs 
* 描    述：    
* 创建时间：   2019年07月18日 11:33 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public interface ICommand
    {

        void Execute(UnitState state);

        void UndoExecute(UnitState state);

        void Dispose();
	}
}