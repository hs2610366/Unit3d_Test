/**  
* 标    题：   NavmeshWin.cs 
* 描    述：    
* 创建时间：   2020年04月02日 01:39 
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
    public class NavmeshWin : SceneArrayBase<NavmeshWin>
    {
        #region 保护函数
        protected override void Init()
        {
            Title = "场景列表";
            ContextRect = new Rect(300, 300, 300, 800);
            base.Init();
            TempMgr.Init();
            sList = SceneTempMgr.Instance.Temps;
            InitComplete();
        }

        protected override void CustomDestroy()
        {
        }

        #endregion

        #region 刷新GUI
        protected override void CustomGUI()
        {
            //OnSetScriptCreatorGUI();
            base.CustomGUI();
        }
        #endregion

        #region 私有函数
        protected override void OpenUnitView(object obj = null)
        {
            SceneNavMeshPath.ShowWin();
            AssetDatabase.Refresh();
            this.Close();
        }
        #endregion
    }
}