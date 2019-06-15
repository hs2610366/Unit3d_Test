/**  
* 标    题：   RoleactionData.cs 
* 描    述：    
* 创建时间：   2019年03月02日 02:46 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Divak.Script.Game;

namespace Divak.Script.Editor 
{
	public partial class RoleactionEditor : EditorWinBase<RoleactionEditor>
    {
        #region 
        protected Vector2 ListPos = Vector2.zero;
        protected Rect ListRect = new Rect(0, 60,228, 510);
        protected Rect BaseProRect = new Rect(230, 60, 228, 510);
        protected Rect AnimGroupRect = new Rect(460, 60, 260, 510);
        protected Rect BreakGroupRect = new Rect(460, 582, 260, 510);
        #endregion

        #region 
        protected List<ModelTemp> Temps;
        protected UnitPlayer Player;
        protected List<string> AnimNames = new List<string>();
        public List<string> AnimList { get { return AnimNames; } }
        protected List<AnimInfoEditor> AnimInfos = new List<AnimInfoEditor>();
        #endregion

        #region 参数
        protected const int WinW = 800;
        protected const int WinH = 800;
        protected const int BtnW = 200;
        protected const int BtnH = 30;
        #endregion

        protected AnimInfoEditor SelectAnimInfo = null;
        protected AnimInfoEditor RemoveAnimInfo = null;

        protected TextureData TexData = null;

        #region 私有函数
        private void InitData()
        {
            ContextRect = new Rect(100, 100, WinW, WinH);
            //TexData = new TextureData();
            //TexData.Load();
            TempMgr.Init();
            Temps = ModelTempMgr.Instance.Temps;

        }

        private void GetUnitAnim()
        {
            string path = string.Format("{0}/{1}/Animator/", UnitMgr.Instance.ModPath, Player.MTemp.model);
            path = Application.dataPath.Replace("Assets", path);
            string[] AnimPaths = PathTool.GetFiles(path);
            if (AnimPaths != null)
            {
                int length = AnimPaths.Length;
                for (int i = 0; i < length; i++)
                {
                    if (Path.GetExtension(AnimPaths[i]) == SuffixTool.Meta) continue;
                    AnimNames.Add(Path.GetFileNameWithoutExtension(AnimPaths[i]));
                }
            }
        }

        private void DrawSelectBtn(int index)
        {
            AnimInfoEditor info = AnimInfos[index];
            if (info == null) return;
            if(info.Target == null)
            {
                info.Target = Player;
            }
            info.IndexID = index + 1;
            info.DrawSelectGUI();

            if (info.IsSelect == true )
            {
                if(SelectAnimInfo != null && SelectAnimInfo.IndexID != info.IndexID)
                {
                    SelectAnimInfo.IsSelect = false;
                }
                SelectAnimInfo = info;
            }
            if (info.DelayRemove == true )
            {
                RemoveAnimInfo = info;
            }
        }

        private void ChangeSelect()
        {
            if(RemoveAnimInfo != null)
            {
                int index = RemoveAnimInfo.IndexID - 1;
                if (AnimInfos.Count > index)
                {
                    AnimInfos.RemoveAt(index);
                    RemoveAnimInfo.Dispose();
                }
                RemoveAnimInfo = null;
            }
        }

        private void ResetAnimInfo()
        {
            UnitMgr.Instance.TempID = 0;
            if (Player != null) Player.Dispose();
            Player = null;
            AnimNames.Clear();
        }
        #endregion

        #region 保护函数
        protected override void CustomDestroy()
        {
            ResetAnimInfo();
            if (Temps != null) Temps.Clear();
            Temps = null;
        }
        #endregion
    }
}