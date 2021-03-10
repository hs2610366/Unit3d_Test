/**  
* 标    题：   UILoading.cs 
* 描    述：   进度
* 创建时间：   2017年07月25日 02:28 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Divak.Script.Game
{
    public class UILoading
    {
        private static GameObject mGO;
        private static Slider mSlider;
        private static Text mText;

        public void Init()
        {
            if (UIMgr.UITrans != null)
            {
                Transform trans = TransTool.Find(UIMgr.UITrans, UIName.UILoading);
                if (trans != null) mGO = trans.gameObject;
            }
            if(mGO == null)
            {
                AssetsMgr.Instance.LoadPrefab(UIName.UILoading, SuffixTool.Prefab, OnLoadFinish);
            }
        }

        public void OnLoadFinish(GameObject obj, string name)
        {
            if (obj == null)
            {
                MessageBox.Error(string.Format("获取{0}失败！！", UIName.UILoading));
                return;
            }
            var go = obj as GameObject;
            mGO = go;
            GetUI();

        }

        public static void GetUI()
        {
            if (mGO == null) return;
            mSlider = ComTool.Find<Slider>(mGO, "Slider", UIName.UILoading);
            mText = ComTool.Find<Text>(mGO, "Text", UIName.UILoading);
        }

        #region 保护函数
        /// <summary>
        /// 清理数据
        /// </summary>
        public static void Clean()
        {
            if (mSlider != null) mSlider.value = 0;
            if (mText != null)
            {
                mText.text = string.Empty;
            }
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 设置进度描述
        /// </summary>
        /// <param name="text"> 描述文本 </param>
        private static void LoadingText(string text)
        {
            if (mText == null) return;
            mText.text = text;
        }
        #endregion

        #region 公开函数
        /// <summary>
        /// 设置进度
        /// </summary>
        /// <param name="value"> 进度百分百 </param>
        /// <param name="text"> 进度描述 </param>
        public static void Progress(float value, string text)
        {
            Progress(value);
            LoadingText(text);
        }

        /// <summary>
        /// 设置进度条
        /// </summary>
        /// <param name="value"> 进度条百分百 </param>
        public static void Progress(float value)
        {
            if (mSlider == null) return;
            mSlider.value = value;
        }

        public static void Dispose()
        {
            mSlider = null;
            mText = null;
            if (mGO != null) GameObject.Destroy(mGO);
        }
        #endregion
    }
}
