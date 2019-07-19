/**  
* 标    题：   Unit.cs 
* 描    述：    
* 创建时间：   2018年03月08日 21:53 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class Unit : UnitActive
    {
        #region 模型配置表
        private ModelTemp mTemp;
        /// <summary>
        /// 模型配置表
        /// </summary>
        public ModelTemp MTemp { get { return mTemp; } }
        #endregion

        #region 速度
        private int mSpeed = 5;
        public int Speed
        {
            get
            {
                return mSpeed;
            }
        }
        #endregion

        #region 控件
        /// <summary>
        /// 识别投影
        /// </summary>
        private Projection2 mIdent;
        #endregion

        public Unit():base()
        {
            mIdent = new Projection2();
        }

        #region MyRegion
        protected virtual void UpdateIdent(int ident)
        {
            if (mIdent == null) return;
            mIdent.SetIdent(ident);
        }
        #endregion

        #region 公有函数

        public override void UpdateModel(GameObject go)
        {
            base.UpdateModel(go);
            if (mIdent != null) mIdent.Add(Controller);
        }

        public void UpdateModelTemp(ModelTemp temp)
        {
            mTemp = temp;
            UpdateIdent(1);
        }
       
        public override void Dispose()
        {
            mTemp = null;
            if (mIdent != null) mIdent.Dispose();
            mIdent = null;
            base.Dispose();
        }
        #endregion

        #region 私有函数

        #endregion
    }
}