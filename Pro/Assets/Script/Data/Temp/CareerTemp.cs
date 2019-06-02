/**  
* 标    题：   CareerTemp.cs 
* 描    述：    
* 创建时间：   2018年03月06日 00:57 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class CareerTempMgr : BaseTempMgr<CareerTemp>
    {
        public static readonly CareerTempMgr Instance = new CareerTempMgr();
        public List<CareerTemp> Temps = new List<CareerTemp>();
        public Dictionary<UInt32, CareerTemp> Dic = new Dictionary<uint, CareerTemp>();

        protected override void CustomAnalysis(List<object> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                CareerTemp temp = list[i] as CareerTemp;
                if (temp == null) continue;
                Temps.Add(temp);
                Dic.Add(temp.id, temp);
            }
            Temps.Sort((CareerTemp a, CareerTemp b) => { return a.id < b.id ? 1 : -1; });
        }

        public override CareerTemp Find(UInt32 id)
        {
            if (Dic.ContainsKey(id)) return Dic[id];
            MessageBox.Error(string.Format("CareerTempMgr没有找到ID为[{0}]的数据", id));
            return null;
        }
        public override void Clear()
        {
            if (Temps != null) Temps.Clear();
            if (Dic != null) Dic.Clear();
        }
    }

    [Serializable]
    public class CareerTemp : Temp
    {
        [SerializeField]
        public UInt32 id { get; set; }
        [SerializeField]
        public Byte type { get; set; }
        [SerializeField]
        public UInt32 modId { get; set; }
        [SerializeField]
        public string name  { get; set; }
    }
}