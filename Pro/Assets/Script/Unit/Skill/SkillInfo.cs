/**  
* 标    题：   SkillInfo.cs 
* 描    述：    
* 创建时间：   2020年03月04日 11:20 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class SkillInfo
    {
        #region 技能配置表
        private SkillLevelTemp mLvTemp;
        public SkillLevelTemp LvTemp { get { return mLvTemp; } }
        private SkillGroupTemp mGroupTemp;
        public SkillGroupTemp GroupTemp { get { return mGroupTemp; } }
        #endregion

        #region 施放技能对象
        private Unit mPlayer;
        #endregion

        #region 参数
        #endregion

        #region 变量
        private UInt32 mSkillId;
        private UInt32 mSkillLvId;
        private UInt32 mSkillLv;
        #endregion

        public SkillInfo(Unit unit)
        {
            mPlayer = unit;
        }

        public SkillInfo(UInt32 lvid, Unit unit)
        {
            mPlayer = unit;
            mSkillLvId = lvid;
            mSkillId = lvid / 100;
            mSkillLv = lvid % 100;
            mGroupTemp = SkillGroupTempMgr.Instance.Find(mSkillId);
            UpdateLevelData();
        }

        #region 私有函数
        private void UpdateLevelData()
        {
            mLvTemp = SkillLevelTempMgr.Instance.Find(mSkillLvId);
        }
        #endregion

        #region 公有函数
        public void UpdateData()
        {
            Global.Instance.OnUpdate += Update;
            UpdateLevelData();
        }

        /// <summary>
        /// 技能升级
        /// </summary>
        public void UpLevel()
        {

        }

        public void Update()
        {

        }

        public void ResetSkillLevel()
        {

        }

        public void Reset()
        {
            Global.Instance.OnUpdate += Update;
            ResetSkillLevel();
        }

        public void Dispose()
        {
        }
        #endregion

    }
}