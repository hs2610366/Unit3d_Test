﻿/**  
* 标    题：   RoleactionData.cs 
* 描    述：    
* 创建时间：   2019年03月02日 02:46 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Divak.Script.Game;

namespace Divak.Script.Editor 
{
	public partial class RoleactionEditor : EditorWinBase<RoleactionEditor>
    {
        #region 
        protected Vector2 StatePos = Vector2.zero;
        protected Vector2 ListPos = Vector2.zero;
        protected Rect StateRect = new Rect(0, 60, 228, 510);
        protected Rect ListRect = new Rect(230, 60,228, 510);
        protected Rect BaseProRect = new Rect(460, 60, 228, 510);
        protected Rect AnimGroupRect = new Rect(690, 60, 260, 510);
        protected Rect BreakGroupRect = new Rect(460, 582, 260, 510);
        #endregion

        #region 
        protected List<ModelTemp> Temps;
        protected UnitPlayer Player;
        public List<string> StatesList { get { return UnitStates; } }
        protected List<string> UnitStates = new List<string>();
        public List<string> AnimList { get { return AnimNames; } }
        protected List<string> AnimNames = new List<string>();
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

        private void GetUnitStates()
        {
            if(Player.Anims.Count > 0)
            {
                UnitStates.Clear();
                UnitStates.Add("nil");
                for (int i = 0; i < Player.Anims.Count; i ++)
                {
                    UnitStates.Add(Player.Anims[i].Name);
                }
            }
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
            AnimInfoEditor info = Player.Anims[index] as AnimInfoEditor;
            if (info == null) return;
            info.SetName(index + 1);
            if(info.Target == null)
            {
                info.Target = Player;
            }
            //info.IndexID = index + 1;
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
                if (Player.Anims.Count > index)
                {
                    Player.Anims.RemoveAt(index);
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

        /// <summary>
        /// 报错并导出状态
        /// </summary>
        /// <param name="obj"></param>
        private void SaveStateInfo(object obj = null)
        {
            if (Player == null)
            {
                MessageBox.Error("数据为null");
                return;
            }
            if (Player.UnitStates == null || Player.UnitStates.Count == 0) return;

            string path = string.Empty;
#if UNITY_EDITOR
            path = Application.dataPath + PathTool.AssetsEditorResource + PathTool.UnitState;
            Config.OutputConfig<UnitState, AnimInfo>(path, Player.MTemp.model, Player.UnitStates);
#endif
            path = PathTool.DataPath + PathTool.UnitState;
            Config.OutputConfig<UnitState, AnimInfo>(path, Player.MTemp.model, Player.UnitStates);
            AssetDatabase.Refresh();
            MessageBox.Log("角色动作状态配置导出完成。");
        }

        /// <summary>
        /// 报错并导出动作
        /// </summary>
        /// <param name="obj"></param>
        private void SaveAnimInfo(object obj = null)
        {
            if (Player == null)
            {
                MessageBox.Error("数据为null");
                return;
            }
            if (Player.Anims == null || Player.Anims.Count == 0) return;

            string path = string.Empty;
#if UNITY_EDITOR
            path = Application.dataPath + PathTool.AssetsEditorResource + PathTool.Anim;
            Config.OutputConfig<AnimInfo>(path, Player.MTemp.model, Player.Anims, SuffixTool.Animation);
#endif
            path = PathTool.DataPath + PathTool.Anim;
            List<AnimInfo> list = Reflection(Player.Anims);
            Config.OutputConfig<AnimInfo>(path, Player.MTemp.model, list, SuffixTool.Animation);
            AssetDatabase.Refresh();
            MessageBox.Log("角色动作配置导出完成。");
        }

        /// <summary>
        /// 保存并导出
        /// </summary>
        /// <returns></returns>
        private void SaveInfo(object obj = null)
        {
            SaveStateInfo();
            SaveAnimInfo();
        }
        #endregion

        #region 公开函数
        /// <summary>
        /// 子类型转父类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listObj"></param>
        /// <returns></returns>
        public List<AnimInfo> Reflection(List<AnimInfo> list)
        {
            List<AnimInfo> result = new List<AnimInfo>();
            for (int i = 0; i < list.Count; i++)
            {
                result.Add(Reflection(list[i]));
            }
            return result;
        }
        /// <summary>
        /// 脚本反射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public AnimInfo Reflection(AnimInfo obj)
        {
            Type t = obj.GetType();
            PropertyInfo[] list = t.GetProperties();
            AnimInfo info = new AnimInfo();
            Type ti = info.GetType();
            for (int i = 0; i < list.Length; i++)
            {
                PropertyInfo pt = list[i];
                if (pt == null) continue;
                pt.SetValue(info, pt.GetValue(obj, null), null);
            }
            return info;
        }

        public int GetUnitStatesIndex(int index)
        {
            UnitState state = Player.States[index];
            AnimInfo info = Player.UnitStates[state];
            if (info != null)
            {
                return UnitStates.FindIndex(s => { return s == info.Name; });
            }
            return 0;
        }

        public void ChangeUnitStates(int i, int tar)
        {
            string n = UnitStates[tar];
            AnimInfo info = Player.Anims.Find(s => { return s.Name == n; });
            if (info != null)
            {
                UnitState state = Player.States[i];
                Player.UnitStates[state] = info;
            }
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