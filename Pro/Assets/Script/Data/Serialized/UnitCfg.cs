/**  
* 标    题：   UnitCfg.cs 
* 描    述：    
* 创建时间：   2021年01月25日 15:55 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{

	public class UnitCfg
    {
        public readonly static UnitCfg Instance = new UnitCfg();
        private List<Unit> mUnits = new List<Unit>();
        public List<Unit> Units { get { return mUnits; } }

        #region 创建/删除
        public Unit Add()
        {
            var tag = GuidTool.GuidTo16String();
            var unit = new Unit(tag);
            mUnits.Add(unit);
            return unit;
        }

        public void Remove(Unit unit)
        {
            var index = FindIndex(unit.Tag); 
            mUnits.RemoveAt(index);
            unit.Dispose();
            unit = null;
        }
        #endregion

        #region 获取

        public int FindIndex(string tag)
        {
            return mUnits.FindIndex((o) => { return o.Tag == tag; });
        }

        public Unit FindUnit(string tag)
        {
            return mUnits.Find((o) => { return o.Tag == tag; });
        }

        #endregion

        #region 清除数据
        public void Clear()
        {
            while(mUnits.Count > 0)
            {
                var index = mUnits.Count - 1;
                var unit = mUnits[index];
                mUnits.RemoveAt(index);
                if(unit != null)
                {
                    unit.Dispose();
                }
                unit = null;
            }
            mUnits.Clear();
        }
        #endregion
    }
}