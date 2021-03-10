
/**  
* 标    题：   ConditionNode.cs 
* 描    述：   条件 叶节点
*              满足条件
* 创建时间：   2018年08月07日 01:09 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

namespace Divak.Script.Game
{
    public class BaseConditionNode : IConditionNode
    {
        #region 属性
        protected string mName = "Precondition";
        public string Name { get { return mName; } set { mName = value; } }
        #endregion

        #region 构造函数
        public BaseConditionNode()
        {

        }
        #endregion

        #region 公开函数

        public bool Allow()
        {
            return true;
        }

        public void Dispose()
        {
        }
        #endregion
    }
}