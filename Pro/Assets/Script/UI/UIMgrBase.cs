/**  
* 标    题：   UIMgrBase.cs 
* 描    述：    
* 创建时间：   2017年07月31日 04:05 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game

{
    public class UIMgrBase
    {

        #region 实例化ui类
        protected static UIBase Instantiation(string uiName, GameObject go)
        {
            switch(uiName)
            {
                case UIName.UILogin: return new UILogin(go);
                case UIName.UIControl: return new UIControl(go);
            }
            return null;
        }
        #endregion
    }
}