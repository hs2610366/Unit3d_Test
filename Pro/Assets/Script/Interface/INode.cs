/**  
* 标    题：   INode.cs 
* 描    述：   行为树节点
* 创建时间：   2021年03月06日 15:36 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/
using System.Collections;
namespace Divak.Script.Game 
{
	public interface INode
    {
        IEnumerator Executing();
        bool CustomExecuting();
        bool Completed();
        void Reset();
        void Dispose();
    }
}