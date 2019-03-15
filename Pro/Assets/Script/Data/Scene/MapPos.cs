/**  
* 标    题：   MapPost.cs 
* 描    述：    
* 创建时间：   2018年07月12日 00:27 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Divak.Script.Game
{
    [Serializable]
    public class MapPos
    {
        [NonSerialized]
        public Vector3 Pos;
        public float H;
        public float V;
        public int ColorType;

        /// <summary>
        /// 权重
        /// </summary>
        public int Fn;
        /// <summary>
        /// 到父节点距离
        /// </summary>
        public int Gd;
        /// <summary>
        /// 到目标点距离
        /// </summary>
        public int Hd;

        public MapPos ParentNode;

        public MapPos()
        {

        }

        public void UpdatePos(Vector3 pos, float h, float v, int type)
        {
            pos = new Vector3(pos.x, 0, pos.z);
            H = h;
            V = v;
            ColorType = type;
        }


        public Color GetColor()
        {

            return MapInfo.Colors[ColorType];
        }

        public void UpdateDistance(int sH, int sV, int tH, int tV)
        {
            int h = (int)(Mathf.Abs(H - sH) * 10);
            int v = (int)(Mathf.Abs(V - sV) * 10);
            Gd = (int)Mathf.Sqrt(h * h + v * v);
            h = (int)(Mathf.Abs(H - tH) * 10);
            v = (int)(Mathf.Abs(V - tV) * 10);
            Hd = (int)Mathf.Sqrt(h * h + v * v);
            Fn = Hd + Gd;
        }



        public void UpdatePath()
        {
            if (ParentNode != null)
            {
                ParentNode.ColorType = 2;
                ParentNode.UpdatePath();
                MessageBox.Error("更新路径");
            }
        }
    }
}
