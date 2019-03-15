/**  
* 标    题：   MapNavMgr.cs 
* 描    述：    
* 创建时间：   2018年12月04日 01:56 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game
{

    public class MapNavMesh : NavMeshBase
    {

        private Vector3 mStartPos = Vector3.zero;
        private MapPos[,] mOpenNode;
        private MapPos[,] mCloseNode;

        public override void Init()
        {
            base.Init();
            mOpenNode = new MapPos[MapH, MapV];
            mCloseNode = new MapPos[MapH, MapV];
        }

        public void StartNavMesh(Vector3 targetPos)
        {
            int sH = Mathf.FloorToInt(mStartPos.x / Size) * Size;
            int sV = Mathf.FloorToInt(mStartPos.z / Size) * Size;
            int tH = Mathf.FloorToInt(targetPos.x / Size) * Size;
            int tV = Mathf.FloorToInt(targetPos.z / Size) * Size;
            RecursionOpenNodeList(sH, sV, tH, tV);
        }

        private void RecursionOpenNodeList(int sH, int sV, int tH, int tV)
        {
            int H = -1;
            int V = -1;
            int Fn = -1;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        mCloseNode[sH , sV] = Info.MapNav[sH, sV];
                        mCloseNode[sH , sV].ColorType = 4;
                        continue;
                    }
                    int h = sH + i;
                    int v = sV + j;
                    if (h < 0 || v < 0) continue;
                    if (Info.MapNav[h, v] == null) continue;
                    if (mCloseNode[h, v] != null) continue;
                    mOpenNode[h, v] = Info.MapNav[h, v];
                    mOpenNode[h, v].ParentNode = Info.MapNav[sH, sV];
                    if (h == tH && v == tV)
                    {
                        NavMeshComplete(h, v);
                        MessageBox.Error("寻路完成");
                        return;
                    }
                    mOpenNode[h, v].UpdateDistance(sH, sV, tH, tV);
                    mOpenNode[h, v].ColorType = 3;
                    int fn = mOpenNode[h, v].Fn;
                    if (Fn < 0 || Fn > fn)
                    {
                        H = h;
                        V = v;
                        Fn = fn;
                    }
                }
            }
            if (H != -1 && V != -1 && Fn != -1)
            {
                mOpenNode[H, V] = null;
                RecursionOpenNodeList(H, V, tH, tV);
                return;
            }
            NavMesh(tH, tV);
        }

        private void NavMesh(int tH, int tV)
        {
            int H = -1;
            int V = -1;
            int Fn = -1;
            for (int i = 0; i < MapH; i ++)
            {
                for(int j = 0; j < MapV; j ++)
                {
                    if (Info.MapNav[i, j] == null) continue;
                    if (mCloseNode[i, j] != null) continue;
                    if (mOpenNode[i, j] == null) continue;
                    int fn = mOpenNode[i, j].Fn;
                    if (Fn < 0 || Fn > fn)
                    {
                        H = i;
                        V = j;
                        Fn = fn;
                    }
                }
            }
            if (H != -1 && V != -1 && Fn != -1)
            {
                mCloseNode[H, V] = mOpenNode[H, V];
                mOpenNode[H, V] = null;
                mCloseNode[H, V].ColorType = 4;
                RecursionOpenNodeList(H, V, tH, tV);
                return;
            }
            NavMeshFail();
            MessageBox.Error("没有路径可以到达");
        }

        private void NavMeshComplete(int tH, int tV)
        {
            Info.MapNav[tH, tV].ColorType = 2;
            Info.MapNav[tH, tV].UpdatePath();
        }

        private void NavMeshFail()
        {

        }
    }
}