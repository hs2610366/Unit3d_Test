/**  
* 标    题：   UnitAttr.cs 
* 描    述：    Unit 配置数据 参数设置更新
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
        #region 引用对象

        #region 模型配置表
        protected ModelTemp mModelTemp;
        /// <summary>
        /// 模型配置表
        /// </summary>
        public ModelTemp MTemp { get { return mModelTemp; } }
        #endregion

        #region 职业配置表
        protected CareerTemp mCareerTemp;
        /// <summary>
        /// 职业配置表
        /// </summary>
        public CareerTemp CTemp { get { return mCareerTemp; } }

        #region 技能配置

        #endregion

        #endregion

        #region 技能
        protected Dictionary<UInt32, SkillInfo> Skills = new Dictionary<uint, SkillInfo>();
        #endregion

        #region buff
        protected Dictionary<UInt32, BuffInfo> Buffs = new Dictionary<uint, BuffInfo>();
        #endregion

        #endregion

        #region 构造函数
        public UnitAttr() : base()
        {

        }

        public UnitAttr(string tag) : base(tag)
        {

        }
        #endregion

        #region 保护函数
        protected virtual void Instantiate()
        {

        }


        protected override void CustomUpdate()
        {

        }

        #endregion

        #region 公开函数
        public void UpdateCfgs(CareerTemp cTemp, ModelTemp mTemp)
        {
            mCareerTemp = cTemp;
            mModelTemp = mTemp;
            Instantiate();
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