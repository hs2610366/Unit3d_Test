/**  
* 标    题：   UnitAnimGrodView.cs 
* 描    述：    
* 创建时间：   2018年08月25日 03:52 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Divak.Script.Game;

namespace Divak.Script.Editor
{
    public partial class UnitListView : EditorWinBase<UnitListView>
    {
        #region 私有函数

        private void DrawAnimInfo(AType type, Dictionary<string, UnitAnimInfo> anims)
        {
            string key = EnumTool.Description(type);
            if (!anims.ContainsKey(key)) anims.Add(key, new UnitAnimInfo(key));
            UnitAnimInfo info = anims[key];
            EditorGUILayout.BeginVertical();
            GUILayout.Toggle(true, string.Format("{0} 数据", info.Title), "dragtab");
            int Count = info.List.Count;
            string[] names = AnimNames.ToArray();
            List<int> reduce = new List<int>();
            for (int i = 0; i < Count; i++)
            {
                GUILayout.BeginHorizontal();
                string title = "    ";
                if (i == 0) title = "动作组:";
                GUILayout.Label(title, GUILayout.Width(80));
                GUILayout.Button(info.List[i], "button");
                if(GUILayout.Button("-", GUILayout.Width(24)))
                {
                    reduce.Add(i);
                }
                int index = EditorGUILayout.Popup(title, StrTool.IndexOfToStr(names, info.List[i]), names);
                if(index != -1) info.List[i] = names[index];
                GUILayout.EndHorizontal();
            }
            if(reduce.Count > 0)
            {
                for(int i = 0; i < reduce.Count; i ++)
                {
                    info.List.RemoveAt(reduce[i]);
                }
            }
            if (GUILayout.Button("+"))
            {
                info.List.Add(string.Empty);
            }
            EditorGUILayout.EndVertical();
        }

        /**
        private void DrawAnimInfo(AType type, Dictionary<string, UnitAnimInfo> dic)
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Toggle(true, string.Format("{0}数据", info.Title), "dragtab");
            int Count = info.List.Count;
            for (int i = 0; i < Count; i++)
            {
                EditorGUILayout.Popup("动作组:", 1, AnimNames.ToArray());
            }
            if (GUILayout.Button("+"))
            {
                info.List.Add(new AnimInfo(string.Format("动作{0}", Count)));
            }
            if (GUI.changed == true)
            {
            }
            EditorGUILayout.EndVertical();
        }
        */

        private void DrawAnimPopup()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();
        }
        #endregion
    }
}