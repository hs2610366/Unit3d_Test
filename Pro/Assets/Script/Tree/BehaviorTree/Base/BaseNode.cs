/**  
* 标    题：   IBTBaseNode.cs 
* 描    述：    
* 创建时间：   2018年08月07日 02:05 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class BaseNode : IBTNode
    {
        private string mNodeName = "CompositeNode";
        public string NodeName { get { return mNodeName; } set { mNodeName = value; } }

        private IBTNode mParent;
        public IBTNode Parent { get { return mParent; } set { mParent = value; } }

        private BTStatus mStatus = BTStatus.Running;
        public BTStatus Status { get { return mStatus; } }

        public bool Enter(object input) { return false; }
        public bool Leave(object input) { return false; }
        public bool Tick(object input, object output) { return false; }

        public IBTNode Clone() { return default(IBTNode); }
    }
}