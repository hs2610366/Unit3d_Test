/**  
* 标    题：   INode.cs 
* 描    述：    
* 创建时间：   2018年08月07日 01:39 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public interface IBTNode
    {
        string NodeName { get; set; }
        IBTNode Parent { get; set; }
        BTStatus Status { get; }
        bool Enter(object input);
        bool Leave(object input);
        bool Tick(object input, object output);
        IBTNode Clone();
    }
}