/**  
* 标    题：   SequenceNode.cs 
* 描    述：   次序节点
* 
*              当执行本类型Node时，它将从begin到end迭代执行自己的Child Node：
*              如遇到一个Child Node执行后返回False，那停止迭代，
*              本Node向自己的Parent Node也返回False；否则所有Child Node都返回True，
*              那本Node向自己的Parent Node返回True。
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

	}
}