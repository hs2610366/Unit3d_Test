/**  
* 标    题：   UnitModInfo.cs 
* 描    述：    
* 创建时间：   2019年03月14日 11:28 
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
    public class UnitInfo
    {
        #region 名字
        public string Name;
        #endregion

        #region 类型
        [NonSerialized]
        public UnitType Type;
        public int UType
        {
            get { return (int)Type; }
            set { Type = (UnitType)value; }
        }
        #endregion

        #region Collider
        public float CenterX;
        public float CenterY;
        public float CenterZ;
        public Vector3 Center
        {
            get { return new Vector3(CenterX, CenterY, CenterZ); }
            set
            {
                CenterX = value.x;
                CenterY = value.y;
                CenterZ = value.z;
            }
        }
        public float Height;
        #endregion

        public UnitInfo()
        {

        }

        public void UpdateController(CharacterController controller)
        {
            if (controller == null) return;
            Vector3 center = controller.center;
            CenterX = center.x;
            CenterY = center.y;
            CenterZ = center.z;
            Height = controller.height;
        }
    }
}