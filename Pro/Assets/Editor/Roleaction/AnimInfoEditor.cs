/**  
* 标    题：   AnimInfoEditor.cs 
* 描    述：    
* 创建时间：   2019年05月23日 02:09 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Divak.Script.Game;

namespace Divak.Script.Editor 
{
	public class AnimInfoEditor : AnimInfo
    {
        public UnitPlayer Target = null;
        /// <summary>
        /// 技能序列
        /// </summary>
        public int IndexID = 0;
        /// <summary>
        /// 选中状态
        /// </summary>
        public bool IsSelect = false;
        /// <summary>
        /// 移除
        /// </summary>
        public bool DelayRemove = false;

        #region 控件
        public GameObject CastDisArea = null;
        public LineRenderer CastDisLR = null;
        #endregion


        public void DrawSelectGUI()
        {
            DrawSelectName();
        }

        public void DrawPropertyGUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Space(3);
            DrawName();
            DrawCastDistance();
            GUILayout.EndVertical();
        }

        public void OnUpdate()
        {
            DrawDistanceArea();
        }

        #region 私有函数
        /// <summary>
        /// 绘制动画名字
        /// </summary>
        private void DrawSelectName()
        {
            GUILayout.BeginHorizontal();
            string title = Name;
            if (string.IsNullOrEmpty(title)) title = string.Format("技能{0}", IndexID);
            bool select = GUILayout.Toggle(IsSelect, title, "button", GUILayout.Width(180), GUILayout.Height(18));
            if (GUILayout.Button("-", GUILayout.Width(18), GUILayout.Height(18)))
            {
                DelayRemove = true;
            }
            if (select == true && select != IsSelect)
            {
                IsSelect = select;
            }
            GUILayout.EndHorizontal();
        }

        private void DrawName()
        {
            string title = Name;
            if (string.IsNullOrEmpty(title)) title = string.Format("技能{0}", IndexID);
            EditorGUILayout.BeginHorizontal();
            string v = EditorUI.DrawTextField(title, "技能名：");
            if (v != title) Name = v;
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 施放距离
        /// </summary>
        private void DrawCastDistance()
        {
            EditorGUILayout.BeginHorizontal();
            float v = EditorUI.DrawFloatField(Distance, "攻击距离：");
            if (v != Distance)
            {
                Distance = v;
                UpdateLR();
            }
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 释放距离范围
        /// </summary>
        private void DrawDistanceArea()
        {
            if (Distance == 0) return;
            //SceneView.lastActiveSceneView.Repaint();
            Vector3 center = Vector3.zero;
            //Vector3 last = Vector3.zero;
            for(int i = 0; i < 360; i ++)
            {
                float x = center.x + Distance * Mathf.Cos(i * 3.14f / 180);
                float z = center.z + Distance * Mathf.Sin(i * 3.14f / 180);
                if(CastDisLR != null)
                {
                    CastDisLR.SetPosition(i,new Vector3(x, 0, z));
                }
                //Debug.DrawLine(last, new Vector3(x, 0, z), Color.red, 0.0f);
                //last.x = x;
                //last.z = z;
            }
        }
        #endregion

        private void UpdateLR()
        {
            if (Distance == 0)
            {
                if (CastDisArea != null)
                {
                    CastDisArea.transform.parent = null;
                    GameObject.DestroyImmediate(CastDisArea);
                    CastDisArea = null;
                    CastDisLR = null;
                }
            }
            else if(Distance > 0)
            {
                if(CastDisArea == null)
                {
                    CastDisArea = new GameObject("CastDistanceArea");
                    CastDisLR = CastDisArea.AddComponent<LineRenderer>();
                    CastDisLR.positionCount = 360;
                    CastDisLR.startWidth = CastDisLR.endWidth = 0.2f;
                    CastDisLR.material = new Material(Shader.Find("Standard"));
                    CastDisLR.material.color = new Color(0, 0.8f, 1, 1);
                    CastDisArea.transform.parent = Target.Trans;
                }
            }

        }

        protected override void Reset()
        {
            Target = null;
            IndexID = 0;
            IsSelect = false;
            DelayRemove = false;
            if(CastDisArea != null) GameObject.Destroy(CastDisArea);
            CastDisArea = null;
            CastDisLR = null;
        }

    }
}