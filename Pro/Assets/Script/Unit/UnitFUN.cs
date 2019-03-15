/**  
* 标    题：   UnitInfo.cs 
* 描    述：    
* 创建时间：   2018年03月20日 00:57 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game
{
    public partial class UnitMgr
    {

        #region 更新Player数据
        /// <summary>
        /// 设置主角角度
        /// </summary>
        /// <param name="angle"></param>
        public void UpdatePlayerAngle(float angle)
        {
            if (mPlayer == null) return;
            mPlayer.Angle = angle;
        }

        /// <summary>
        /// 设置主角移动
        /// </summary>
        /// <param name="value"></param>
        public void SetPlayerMove(bool value)
        {
            if (mPlayer == null) return;
            mPlayer.IsMove = value;
        }
        #endregion
    }
}