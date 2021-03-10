/**  
* 标    题：   BaseControlNode.cs 
* 描    述：    
* 创建时间：   2021年03月06日 16:37 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class BaseCompositeNode : BaseNode
    {
        #region 属性
        /// <summary>
        /// 子节点队列
        /// </summary>
        public List<INode> mChildren = new List<INode>();
        #endregion

        public BaseCompositeNode() : base()
        {
            mName = "CompositeNode";
        }

        #region 保护函数
        #endregion


        #region 公开函数
        /// <summary>
        /// 设置子节点
        /// </summary>
        /// <param name="node"></param>
        public virtual void AddChild(INode node)
        {
            mChildren.Add(node);
        }

        public override IEnumerator Executing()
        {
            yield return new WaitUntil(AllowCondition);
            if (!Completed()) yield break;
            yield return new WaitUntil(CustomExecuting);
            mComplete = Completed();
        }

        public override bool CustomExecuting()
        {
            return true;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public override void Reset()
        {
            if (mChildren.Count > 0)
            {
                foreach (var node in mChildren)
                {
                    node.Reset();
                }
            }
            base.Reset();
        }

        /// <summary>
        /// 释放
        /// </summary>
        public override void Dispose()
        {
            while (mChildren.Count > 0)
            {
                var node = mChildren[0];
                mChildren.RemoveAt(0);
                node.Dispose();
                node = null;
            }
            mChildren.Clear();
            base.Dispose();
        }
        #endregion
    }
}