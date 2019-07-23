/**  
* 标    题：   AnimInfoEditor.cs 
* 描    述：    
* 创建时间：   2019年05月23日 02:09 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Divak.Script.Editor;

namespace Divak.Script.Game
{
    [Serializable]
    public partial class AnimInfoEditor:AnimInfo
    {
        [NonSerialized]
        public UnitPlayer Target = null;
        /// <summary>
        /// 技能序列
        /// </summary>
        [NonSerialized]
        public int IndexID = 0;
        /// <summary>
        /// 选中状态
        /// </summary>
        [NonSerialized]
        public bool IsSelect = false;
        /// <summary>
        /// 移除
        /// </summary>
        [NonSerialized]
        public bool DelayRemove = false;

        #region 控件
        [NonSerialized]
        public GameObject CastDisArea = null;
        [NonSerialized]
        public LineRenderer CastDisLR = null;
        #endregion
        public void SetName(int index)
        {
            IndexID = index;
            Name =  string.Format("100{0}", IndexID);
        }

        public void DrawSelectGUI()
        {
            DrawSelectName();
        }

        /// <summary>
        /// 基础属性
        /// </summary>
        /// <param name="editor"></param>
        public void DrawPropertyGUI(RoleactionEditor editor)
        {
            GUILayout.BeginVertical();
            GUILayout.Space(3);
            GUILayout.Label("基础属性", UIStyles.CSAMS, GUILayout.Width(220));
            DrawName();
            DrawCastDistance();
            DrawAnimPlayType();
            GUILayout.Label("动作属性", UIStyles.CSAMS, GUILayout.Width(220));
            DrawBreak(editor.AnimList);
            GUILayout.Label("特效属性", UIStyles.CSAMS, GUILayout.Width(220));
            GUILayout.EndVertical();
        }

        /// <summary>
        /// 动作组
        /// </summary>
        public void DrawAnimGroup(RoleactionEditor editor)
        {
            GUILayout.BeginVertical();
            GUILayout.Space(3);
            DrawAnim(editor.AnimList);
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

            GUIStyle selectStyle = IsSelect ? UIStyles.FN1_18_White_UpperCenter : UIStyles.FN0_18_White_UpperCenter;

            bool select = GUILayout.Toggle(IsSelect, title, selectStyle, GUILayout.Width(180), GUILayout.Height(30));
            GUILayout.Space(2);
            //"-"按钮
            if (GUILayout.Button("", UIStyles.TSBCB, GUILayout.Width(30), GUILayout.Height(30)))
            {
                DelayRemove = true;
            }
            if (select == true && select != IsSelect)
            {
                IsSelect = select;
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(4);
        }

        /// <summary>
        /// 绘制动作名
        /// </summary>
        private void DrawName()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("技能名", UIStyles.Label_13_White, GUILayout.Width(60));
            GUILayout.Label(Name, UIStyles.LTF_14_White, GUILayout.Width(150));
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

        /// <summary>
        /// 动画组播放类型
        /// </summary>
        private void DrawAnimPlayType()
        {
            EditorUI.SetLabelWidth(65);
            AnimPlayType apt = (AnimPlayType)EditorGUILayout.EnumPopup("组播放类型：", APT, GUILayout.Width(215));
            if (apt != APT) APT = apt;
            EditorUI.SetLabelWidth(80);
        }

        /// <summary>
        /// 绘制中断选项
        /// </summary>
        private void DrawBreak(List<string> list)
        {
            int count = AnimGroup.Count;
            string[] names = list.ToArray();
            EditorUI.SetLabelWidth(200);
            bool ib = EditorGUILayout.Toggle("是否有其他动作中断：", IsBreak);
            if (ib != IsBreak) IsBreak = ib;
            int index = 0;
            EditorGUILayout.BeginHorizontal();
            if (!string.IsNullOrEmpty(EndAnim)) index = StrTool.IndexOfToStr(names, EndAnim);
            GUILayout.Label("停止动作：");
            int select = EditorGUILayout.Popup(string.Empty, index, names, GUILayout.Width(220));
            if (select != -1 && !names[select].Equals(EndAnim)) EndAnim = names[select];
            EditorGUILayout.EndHorizontal();
            EditorUI.SetLabelWidth(80);
        }

        /// <summary>
        /// 绘制动画下拉菜单
        /// </summary>
        private void DrawAnim(List<string> list)
        {
            int count = AnimGroup.Count;
            string[] names = list.ToArray();
            List<int> reduce = new List<int>();

            for (int i = 0; i < count; i++)
            {
                GUILayout.BeginHorizontal();
                int index = EditorGUILayout.Popup(string.Empty, StrTool.IndexOfToStr(names, AnimGroup[i]), names, GUILayout.Width(220));
                if (index != -1) AnimGroup[i] = names[index];
                if (GUILayout.Button("-", GUILayout.Width(24)))
                {
                    reduce.Add(i);
                }
                GUILayout.EndHorizontal();
            }
            if (reduce.Count > 0)
            {
                for (int i = 0; i < reduce.Count; i++)
                {
                    AnimGroup.RemoveAt(reduce[i]);
                }
            }
            if (GUILayout.Button("+", GUILayout.Width(250)))
            {
                AnimGroup.Add(list[0]);
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