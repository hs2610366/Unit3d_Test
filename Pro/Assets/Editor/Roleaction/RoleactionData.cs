/**  
* 标    题：   RoleactionData.cs 
* 描    述：    
* 创建时间：   2019年03月02日 02:46 
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
	public partial class RoleactionEditor : EditorWinBase<RoleactionEditor>
    {

        #region 
        protected List<ModelTemp> Temps;
        protected UnitPlayer Player;
        #endregion

        #region 参数
        protected const int WinW = 800;
        protected const int WinH = 800;
        protected const int BtnW = 200;
        protected const int BtnH = 30;
        #endregion

        #region 私有函数
        private void InitData()
        {
            ContextRect = new Rect(100, 100, WinW, WinH);
            TempMgr.Init();
            Temps = ModelTempMgr.Instance.Temps;
        }
        #endregion
    }
}