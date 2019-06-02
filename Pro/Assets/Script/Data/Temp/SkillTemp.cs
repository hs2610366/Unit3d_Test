/**  
* 标    题：   SkillTemp.cs 
* 描    述：    
* 创建时间：   2019年01月29日 04:02 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class SkillTempMgr : BaseTempMgr<SkillTemp>
    {
        public static readonly ModelTempMgr Instance = new ModelTempMgr();
        public List<SkillTemp> Temps = new List<SkillTemp>();
        public Dictionary<UInt32, SkillTemp> Dic = new Dictionary<uint, SkillTemp>();

        protected override void CustomAnalysis(List<object> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                SkillTemp temp = list[i] as SkillTemp;
                if (temp == null) continue;
                Temps.Add(temp);
                Dic.Add(temp.id, temp);
            }
            Temps.Sort((SkillTemp a, SkillTemp b) => { return a.id < b.id ? 1 : -1; });
        }

        public override SkillTemp Find(UInt32 id)
        {
            if (Dic.ContainsKey(id)) return Dic[id];
            MessageBox.Error(string.Format("SkillTempMgr没有找到ID为[{0}]的数据", id));
            return null;
        }
        public override void Clear()
        {
            if (Temps != null) Temps.Clear();
            if (Dic != null) Dic.Clear();
        }

    }

    [Serializable]
    public class SkillTemp : Temp
    {
        [SerializeField]
        public UInt32 id { get; set; }
        [SerializeField]
        public string name { get; set; }
        [SerializeField]
        public string model { get; set; }
    }
}