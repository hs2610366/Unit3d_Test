/**  
* 标    题：   ScreenTmep.cs 
* 描    述：    
* 创建时间：   2017年12月19日 20:31 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game
{
    public class SceneTempMgr : BaseTempMgr<SceneTemp>
    {
        public static readonly SceneTempMgr Instance = new SceneTempMgr();
        public List<SceneTemp> Temps = new List<SceneTemp>();
        public Dictionary<UInt32, SceneTemp> Dic = new Dictionary<uint, SceneTemp>();

        protected override void CustomAnalysis(List<object> list)
        {
            for (int i = 0; i < list.Count; i ++)
            {
                SceneTemp temp = list[i] as SceneTemp;
                if (temp == null) continue;
                Temps.Add(temp);
                Dic.Add(temp.id, temp);
            }
            Temps.Sort((SceneTemp a, SceneTemp b) => { return a.id < b.id ? 1 : -1; });
        }

        public override SceneTemp Find(UInt32 id)
        {
            if (Dic.ContainsKey(id)) return Dic[id];
            MessageBox.Error(string.Format("ScreenTempMgr没有找到ID为[{0}]的数据", id));
            return null;
        }
        public override void Clear()
        {
            if (Temps != null) Temps.Clear();
            if (Dic != null) Dic.Clear();
        }
    }

    [Serializable]
    public class SceneTemp : Temp
    {
        [SerializeField]
        public UInt32 id { get; set; }
        [SerializeField]
        public String modle { get; set; }
        [SerializeField]
        public String name { get; set; }
        [SerializeField]
        public UInt32 camera_id { get; set; }
    }
}