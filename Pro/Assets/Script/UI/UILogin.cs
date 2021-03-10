/**  
* 标    题：   UILogin.cs 
* 描    述：    
* 创建时间：   2017年07月30日 04:12 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Divak.Script.Game 
{
	public class UILogin : UIBase
    {
        private InputField mAccounts;
        private InputField mPassword;
        private Button mEnter;
        private Button mQuit;

        private string mAccountsValue;
        private string mPasswordValue;

        public UILogin(GameObject go) : base(go)
        {
            mName = UIName.UILogin;
            mAccounts = ComTool.Find<InputField>(mGo, "Accounts", mName);
            mPassword = ComTool.Find<InputField>(mGo, "Password", mName);
            mEnter = ComTool.Find<Button>(mGo, "Enter", mName);
            mQuit = ComTool.Find<Button>(mGo, "Quit", mName);
            MessageBox.Warning("UILogin初始化");
        }

        #region 保护函数
        protected override void CustomOpen()
        {
            mAccountsValue = PlayerPrefs.GetString("Accounts");
            mPasswordValue = PlayerPrefs.GetString("Password");
            if (mAccounts != null)
                mAccounts.text = string.IsNullOrEmpty(mAccountsValue) ? string.Empty : mAccountsValue;
            if (mPassword != null)
                mPassword.text = string.IsNullOrEmpty(mPasswordValue) ? string.Empty : mPasswordValue;
        }
        #endregion

        #region 私有函数

        #endregion

        #region 公开函数
        protected override void CustomDispose()
        {
            mName = null;
            mAccounts = null;
            mPassword = null;
            mEnter = null;
            mQuit = null;
        }
        #endregion
    }
}