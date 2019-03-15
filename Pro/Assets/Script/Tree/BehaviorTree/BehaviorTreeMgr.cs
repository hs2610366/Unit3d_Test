/**  
* 标    题：   BehaviorTreeMgr.cs 
* 描    述：   行为树管理
* 创建时间：   2018年08月07日 01:10 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
    /// <summary>
    /// 行为树状态
    /// </summary>
    public enum BTStatus
    {
        Success,
        Failure,
        Running
    }

    public class BehaviorTreeMgr
    {
        public static readonly BehaviorTreeMgr Instance = new BehaviorTreeMgr();

    }
}