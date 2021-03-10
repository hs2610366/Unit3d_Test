/**  
* 标    题：   Unit.cs 
* 描    述：    
* 创建时间：   2018年03月08日 21:53 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class Unit : UnitObj
    {
        #region 引用对象
        #region 模型配置表
        private ModelTemp mTemp;
        /// <summary>
        /// 模型配置表
        /// </summary>
        public ModelTemp MTemp { get { return mTemp; } }
        #endregion

        #region 控件
        #endregion
        #endregion

        #region 构造函数
        public Unit() : base()
        {

        }

        public Unit(string tag) : base(tag)
        {
            // mIdent = new Projection2();
        }
        #endregion

        #region 私有函数

        #endregion

        #region 保护函数
        #endregion

        #region 公开函数
        public override void UpdateOneself(GameObject go)
        {
            base.UpdateOneself(go);
        }
       
        public override void Dispose()
        {
            mTemp = null;
            base.Dispose();
        }
        #endregion
    }
}