/**  
* 标    题：   INode.cs 
* 描    述：   行为树动作节点执行的先决条件
* 创建时间：   2021年03月06日 15:36 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/
namespace Divak.Script.Game 
{
	public interface IConditionNode
    {
        bool Allow();
        void Dispose();
    }
}