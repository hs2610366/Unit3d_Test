/**  
* 标    题：   ModelTemp.cs 
* 描    述：    
* 创建时间：   2018年03月05日 03:26 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class ModelTempMgr : BaseTempMgr<ModelTemp>
    {
        public static readonly ModelTempMgr Instance = new ModelTempMgr();
        public List<ModelTemp> Temps = new List<ModelTemp>();
        public Dictionary<UInt32, ModelTemp> Dic = new Dictionary<uint, ModelTemp>();

        protected override void CustomAnalysis(List<object> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                ModelTemp temp = list[i] as ModelTemp;
                if (temp == null) continue;
                Temps.Add(temp);
                Dic.Add(temp.id, temp);
            }
            Temps.Sort((ModelTemp a, ModelTemp b) => { return a.id < b.id ? 1 : -1; });
        }

        public override ModelTemp Find(UInt32 id)
        {
            if (Dic.ContainsKey(id)) return Dic[id];
            MessageBox.Error(string.Format("ModelTempMgr没有找到ID为[{0}]的数据", id));
            return null;
        }
        public override void Clear()
        {
            if (Temps != null) Temps.Clear();
            if (Dic != null) Dic.Clear();
        }
    }

    [Serializable]
    public class ModelTemp : Temp
    {
        [SerializeField]
        public UInt32 id { get; set; }
        [SerializeField]
        public string name { get; set; }
        [SerializeField]

        public string model { get; set; }
    }

}