/**  
* 标    题：   UnitAnimInfoView.cs 
* 描    述：    
* 创建时间：   2018年08月25日 03:46 
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
    public partial class UnitListView : EditorWinBase<UnitListView>
    {
        #region 私有函数
        private void InitAnimType()
        {
            TypeRect = new Rect(0, 0, WinW / 4, WinH);
            AnimTypeInfos = new BtnGuidInfo[]
            {
                new BtnGuidInfo("出生状态", Color.yellow),
                new BtnGuidInfo("待机状态", Color.red),
                new BtnGuidInfo("技能状态", Color.red),
                new BtnGuidInfo("死亡状态", Color.green),
            };
        }

        private AType DrawAnim()
        {
            GUI.backgroundColor = Color.gray;
            EditorGUILayout.BeginScrollView(AnimPos, true, true, GUILayout.Width(TypeRect.width + 20), GUILayout.Height(TypeRect.height + 20));
            AType type = EditorUI.DrawSelectBtnGuid<AType>(ref AnimTypeInfos, TypeRect.width, BtnH);
            EditorGUILayout.EndScrollView();
            GUI.backgroundColor = Color.white;
            return type;
        }

        private void DrawAnims(AType type)
        {
            if (Player == null) return;
            Dictionary<string, UnitAnimInfo> anims = Player.SkillAnims;
            switch (type)
            {
                case AType.Start:
                    DrawAnimInfo(AType.Start, anims);
                    break;
                case AType.Await:
                    DrawAnimInfo(AType.Start, anims);
                    break;
                case AType.Skills:
                    DrawAnimInfo(AType.Skills, anims);
                    break;
                case AType.Dead:
                    DrawAnimInfo(AType.Dead, anims);
                    break;
            }
        }

        #endregion
    }
}