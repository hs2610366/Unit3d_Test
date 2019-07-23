/**  
* 标    题：   UnitPlayer.cs 
* 描    述：    
* 创建时间：   2018年03月06日 01:58 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class UnitPlayer : Unit
    {
        #region 职业配置表
        private CareerTemp mCTemp;
        /// <summary>
        /// 职业配置表
        /// </summary>
        public CareerTemp CTemp { get { return mCTemp; } }
        #endregion

        public UnitPlayer():base()
        {

        }

        public void UpdatTemp(CareerTemp temp)
        {
            mCTemp = temp;
        }

        protected override void CustomUpdateModel()
        {
            CameraMgr.UpdatePlay(Trans);
        }
    }
}