using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Divak.Script.Game
{
    public class CameraTempMgr : BaseTempMgr<CameraTemp>
    {
        public static readonly CameraTempMgr Instance = new CameraTempMgr();
        public List<CameraTemp> Temps = new List<CameraTemp>();
        private Dictionary<UInt32, CameraTemp> Dic = new Dictionary<uint, CameraTemp>();

        protected override void CustomAnalysis(List<object> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                CameraTemp temp = list[i] as CameraTemp;
                if (temp == null) continue;
                Temps.Add(temp);
                Dic.Add(temp.id, temp);
            }
            Temps.Sort((CameraTemp a, CameraTemp b) => { return a.id < b.id ? 1 : -1; });
        }

        public CameraTemp Find(UInt32 id)
        {
            if (Dic.ContainsKey(id)) return Dic[id];
            MessageBox.Error(string.Format("ModelTempMgr没有找到ID为[{0}]的数据", id));
            return null;
        }

        public void Clear()
        {
            if (Temps != null) Temps.Clear();
            if (Dic != null) Dic.Clear();
        }
    }

    [Serializable]
    public class CameraTemp
    {
        public UInt32 id { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float eulerX { get; set; }
        public float eulerY { get; set; }
        public float eulerZ { get; set; }
        public float fov { get; set; }
    }
}
