/**  
* 标    题：   CustomUIElement.cs 
* 描    述：    
* 创建时间：   2020年12月17日 19:57 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace Divak.Script.Editor 
{
	public abstract class CustomUIElement
    {
        protected VisualTreeAsset mTreeAsset = null;
        protected VisualElement mRoot = null;

        protected string mUXML = null;

        public void InitRoot()
        {
            EditorUI.GetVisualTreeAsset(mUXML, out mTreeAsset);
            if (mTreeAsset == null)
            {
                MessageBox.Error("ExcelItem描述文件null");
                return;
            }
            mRoot = mTreeAsset.CloneTree();
        }

        public void AddStyle(StyleSheet style)
        {
            if (style == null) return;
            if (mRoot == null) return;
            mRoot.styleSheets.Add(style);
        }

        public void SetParent(VisualElement parent)
        {
            if (mRoot == null) return;
            if (parent == null)
            {
                MessageBox.Error("传入的VisualElement父节点为空");
                return;
            }
            parent.Add(mRoot);
        }

    }
}