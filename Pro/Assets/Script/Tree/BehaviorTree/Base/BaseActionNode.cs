/**  
* 标    题：   BaseActionNode.cs 
* 描    述：    
* 创建时间：   2021年03月06日 16:35 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class BaseActionNode : BaseNode
    {
        #region 属性
        #endregion

        public BaseActionNode() : base()
        {
            mName = "ActionNode";
        }

        #region 公开函数

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