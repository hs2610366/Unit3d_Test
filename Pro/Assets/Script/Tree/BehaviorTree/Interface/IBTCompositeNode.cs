/**  
* 标    题：   IBTCompositeNode.cs 
* 描    述：    
* 创建时间：   2018年08月07日 01:47 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public interface IBTCompositeNode : IBTNode
    {
        void AddNode(IBTNode node);
        void RemoveNode(IBTNode node);
        bool HasNode(IBTNode node);

        void AddCondition(IBTNode node);
        void RemoveCondition(IBTNode node);
        bool HasCondition(IBTNode node);

        List<IBTNode> NodeList { get; }
        List<IBTNode> ConditionList { get; }

    }
}