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
	public class SkillLevelTempMgr : BaseTempMgr<SkillLevelTemp>
    {
        public static readonly SkillLevelTempMgr Instance = new SkillLevelTempMgr();
        public List<SkillLevelTemp> Temps = new List<SkillLevelTemp>();
        public Dictionary<UInt32, SkillLevelTemp> Dic = new Dictionary<uint, SkillLevelTemp>();

        protected override void CustomAnalysis(List<object> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                SkillLevelTemp temp = list[i] as SkillLevelTemp;
                if (temp == null) continue;
                Temps.Add(temp);
                Dic.Add(temp.id, temp);
            }
            Temps.Sort((SkillLevelTemp a, SkillLevelTemp b) => { return a.id < b.id ? 1 : -1; });
        }

        public override SkillLevelTemp Find(UInt32 id)
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
    public class SkillLevelTemp : Temp
    {
        [SerializeField]
        public UInt32 id { get; set; }
        [SerializeField]
        public String name { get; set; }
        [SerializeField]
        public UInt32 level { get; set; }
        [SerializeField]
        public UInt32 castDistance { get; set; }
        [SerializeField]
        public Byte targetType { get; set; }
        [SerializeField]
        public UInt32 cd { get; set; }
        [SerializeField]
        public Int32 triggerType { get; set; }
        [SerializeField]
        public Int64 triggerParam { get; set; }
        [SerializeField]
        public Int64 triggerLimit { get; set; }
        [SerializeField]
        public Int32 effType { get; set; }
        [SerializeField]
        public Int32 effRange { get; set; }
        [SerializeField]
        public Int32 effLimit { get; set; }
        [SerializeField]
        public UInt32 startEffId { get; set; }
        [SerializeField]
        public UInt32 transitionEffId { get; set; }
        [SerializeField]
        public UInt32 endEffId { get; set; }
        [SerializeField]
        public UInt32 priority { get; set; }
        [SerializeField]
        public string animClip { get; set; }
        [SerializeField]
        public UInt32 sound { get; set; }
    }
}