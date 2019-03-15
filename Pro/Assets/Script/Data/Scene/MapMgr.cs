/**  
* 标    题：   MapMgr.cs 
* 描    述：    
* 创建时间：   2018年07月12日 00:08 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Divak.Script.Game;

namespace Divak.Script.Game
{
    public class MapMgr : MapNavMesh
    {
        public static readonly MapMgr Instance = new MapMgr();

#if UNITY_EDITOR
        private GameObject MapNavRoot = null;
        private float Dur = 60.0f;
        public float Timer = 0;

        public override void Init()
        {
            base.Init();
            MapNavRoot = new GameObject("MapNav");
            MapNavRoot.AddComponent<DrawMapNav>();
        }

        public void Reset()
        {
            Info.MapNav = new MapPos[MapH, MapV];
            UnityEditor.SceneView.RepaintAll();
        }

        public void Add(Vector3 pos)
        {
            if (Info.MapNav == null) return;
            int H = Mathf.FloorToInt(pos.x / Size) * Size;
            int V = Mathf.FloorToInt(pos.z / Size) * Size;
            if (H < 0 || H > MapH || V < 0 || V > MapV) return;
            MapPos mPos = new MapPos();
            mPos.UpdatePos(pos, H , V, MapInfo.ColorType);
            Info.MapNav[H, V] = mPos;
        }

        public void Remove(Vector3 pos)
        {
            if (Info.MapNav == null) return;
            float offset = Size / 2.0f;
            int H = Mathf.FloorToInt(pos.x / Size) * Size;
            int V = Mathf.FloorToInt(pos.z / Size) * Size;
            if (H < 0 || H > MapH || V < 0 || V > MapV) return;
            Info.MapNav[H, V] = null;
        }

        public void OnGUI()
        {
            if (MapH == 0 || MapV == 0) return;
            float t = Time.time;
            if (Timer == 0 || t - Timer >= Dur) Timer = t;
            Debug.DrawLine(Vector3.zero, Vector3.right * MapH, Color.white, Dur);
            Debug.DrawLine(Vector3.zero, Vector3.forward * MapV, Color.white, Dur);
            Debug.DrawLine(Vector3.right * MapH, new Vector3(MapH, 0, MapV), Color.white, Dur);
            Debug.DrawLine(Vector3.forward * MapV, new Vector3(MapH, 0, MapV), Color.white, Dur);
            for (int i = 0; i < MapH; i ++)
            {
                for(int j = 0; j < MapV; j ++)
                {
                    float offset = Size / 2.0f;
                    MapPos pos = MapNav[i, j];
                    if (pos == null) continue;
                    Vector3 origin = new Vector3(pos.H, 0, pos.V);
                    Vector3 pos1 = new Vector3(pos.H - offset, 0, pos.V - offset);
                    Vector3 pos2 = new Vector3(pos.H + offset, 0, pos.V - offset);
                    Vector3 pos3 = new Vector3(pos.H + offset, 0, pos.V + offset);
                    Vector3 pos4 = new Vector3(pos.H - offset, 0, pos.V + offset);
                    Debug.DrawLine(origin, pos1, pos.GetColor(), Dur);
                    Debug.DrawLine(origin, pos2, pos.GetColor(), Dur);
                    Debug.DrawLine(origin, pos3, pos.GetColor(), Dur);
                    Debug.DrawLine(origin, pos4, pos.GetColor(), Dur);
                    Debug.DrawLine(pos1, pos2, pos.GetColor(), Dur);
                    Debug.DrawLine(pos2, pos3, pos.GetColor(), Dur);
                    Debug.DrawLine(pos3, pos4, pos.GetColor(), Dur);
                    Debug.DrawLine(pos4, pos1, pos.GetColor(), Dur);
                }
            }
            UnityEditor.SceneView.RepaintAll();
        }
#endif
    }
}
