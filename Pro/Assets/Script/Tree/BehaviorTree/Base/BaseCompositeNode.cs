/**  
* 标    题：   BaseCompositeNode.cs 
* 描    述：    
* 创建时间：   2018年08月07日 01:59 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class BaseCompositeNode : IBTCompositeNode
    {
        private string mNodeName = "CompositeNode";
        public string NodeName { get { return mNodeName; } set { mNodeName = value; } }

        private IBTNode mParent;
        public IBTNode Parent { get { return mParent; } set { mParent = value; } }

        private BTStatus mStatus = BTStatus.Running;
        public BTStatus Status { get { return mStatus; } }

        private List<IBTNode> mNodeList = new List<IBTNode>();
        public List<IBTNode> NodeList { get { return mNodeList; } }

        private List<IBTNode> mConditionList = new List<IBTNode>();
        public List<IBTNode> ConditionList { get { return mConditionList; } }

        public void AddNode(IBTNode node) { }
        public void RemoveNode(IBTNode node) { }
        public bool HasNode(IBTNode node) { return false; }
        public void AddCondition(IBTNode node) { }
        public void RemoveCondition(IBTNode node) { }
        public bool HasCondition(IBTNode node) { return false; }

        public bool Enter(object input) { return false; }
        public bool Leave(object input) { return false; }
        public bool Tick(object input, object output) { return false; }

        public IBTNode Clone() { return default(IBTNode); }

    }
}