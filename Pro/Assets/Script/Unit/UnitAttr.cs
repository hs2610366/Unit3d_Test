/**  
* 标    题：   UnitAttr.cs 
* 描    述：    
* 创建时间：   2020年03月03日 20:53 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class UnitAttr : UnitBase
    {
        #region 模型配置表
        private ModelTemp mModelTemp;
        /// <summary>
        /// 模型配置表
        /// </summary>
        public ModelTemp MTemp { get { return mModelTemp; } }
        #endregion

        #region 职业配置表
        private CareerTemp mCareerTemp;
        /// <summary>
        /// 职业配置表
        /// </summary>
        public CareerTemp CTemp { get { return mCareerTemp; } }

        #region 技能配置
        private SkillLevelTemp mSkillLvTemp;
        /// <summary>
        /// 技能等级配置表
        /// </summary>
        public SkillLevelTemp SkillLvTemp { get { return mSkillLvTemp; } }

        /// <summary>
        /// 技能组配置表
        /// </summary>
        private SkillGroupTemp mSkillGroupTemp;
        public SkillGroupTemp SGTemp { get { return mSkillGroupTemp; } }
        #endregion

        #endregion


        #region 技能
        Dictionary<UInt32, SkillInfo> Skills = new Dictionary<uint, SkillInfo>();
        #endregion


        #region buff
        Dictionary<UInt32, BuffInfo> Buffs = new Dictionary<uint, BuffInfo>();
        #endregion

        public UnitAttr() : base()
        {
           
        }


        #region 保护函数
        protected override void CustomUpdate()
        {

        }

        #endregion

        #region 公有函数

        public void UpdateCfgs(CareerTemp cTemp, ModelTemp mTemp)
        {
            mCareerTemp = cTemp;
            mModelTemp = mTemp;
        }

        public override void Reset()
        {
            mCareerTemp = null;
            mModelTemp = null;
            base.Reset();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
        #endregion
    }
}