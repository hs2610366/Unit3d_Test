/**  
* 标    题：   UnitState.cs 
* 描    述：    
* 创建时间：   2018年03月17日 22:57 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
    public enum UnitStatus
    {
        Idea,
        Walk,
        Run,
    }

	public class UnitState : UnitActive
    {
        private UnitStatus mStatus = UnitStatus.Idea;
        public UnitStatus Status
        {
            get { return mStatus; }
            set
            {
                mStatus = value;
                UpdateAnim();
            }
        }

        #region 私有函数
        private void UpdateAnim()
        {
        }
        #endregion
    }
}