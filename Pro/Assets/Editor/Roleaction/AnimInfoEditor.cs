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


        public void DrawSelectGUI()
        {
            DrawName();
            DrawCastDistance();
        }

        #region 私有函数
        /// <summary>
        /// 绘制动画名字
        /// </summary>
        private void DrawName()
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

        /// <summary>
        /// 施放距离
        /// </summary>
        private void DrawCastDistance()
        {
            GUILayout.BeginHorizontal();

            string title = Name;
            if (string.IsNullOrEmpty(title)) title = string.Format("施放距离：", IndexID);
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
        #endregion

        protected override void Reset()
        {
            IndexID = 0;
            IsSelect = false;
            DelayRemove = false;
        }

    }
}