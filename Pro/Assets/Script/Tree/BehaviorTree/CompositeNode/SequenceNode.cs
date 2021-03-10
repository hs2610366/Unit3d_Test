/**  
* 标    题：   SequenceNode.cs 
* 描    述：   次序节点
* 
*              当执行本类型Node时，它将从begin到end迭代执行自己的Child Node：
*              如遇到一个Child Node执行后返回False，则执行下一节点，
*              如遇到一个Child Node执行后返回True,那本Node向自己的Parent Node返回True。
* 
* 创建时间：   2018年08月07日 01:18 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class SequenceNode : BaseCompositeNode
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
            var index = 0;
            while (mChildren.Count > 0)
            {
                var node = mChildren[index];
                if (node == null) continue;
                yield return new WaitUntil(node.CustomExecuting);
                state = node.Completed();
                if (state == false) index++;
                else
                {
                    break;
                }
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