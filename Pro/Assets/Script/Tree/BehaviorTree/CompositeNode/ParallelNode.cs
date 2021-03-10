/**  
* 标    题：   ParallelNode.cs 
* 描    述：   并行节点
* 
*              当执行本类型Node时，它将从begin到end迭代执行自己的Child Node：
*              如遇到一个Child Node执行后返回False，那停止迭代，
*              本Node向自己的Parent Node也返回False；否则所有Child Node都返回True，
*              那本Node向自己的Parent Node返回True。
*              
* 创建时间：   2018年08月07日 01:24 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class ParallelNode : BaseCompositeNode
    {
        #region 属性

        #endregion

        #region 私有函数
        #endregion

        #region 保护函数
        #endregion

        #region 公开函数

        public override IEnumerator Executing()
        {
            yield return new WaitUntil(AllowCondition);
            if (!Completed()) yield break;
            var state = false;
            for (int i = 0; i < mChildren.Count; i++)
            {
                var node = mChildren[i];
                if (node == null) continue;
                yield return new WaitUntil(node.CustomExecuting);
                state = node.Completed();
                if (state == false) break;
            }
            mComplete = state;
        }
        public override bool CustomExecuting()
        {
            return true;
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
        #endregion
    }
}