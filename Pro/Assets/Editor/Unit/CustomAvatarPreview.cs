/**  
* 标    题：   NewBehaviourScript.cs 
* 描    述：    
* 创建时间：   2020年12月17日 17:06 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Internal;

namespace Divak.Script.Editor 
{
    public class CustomAvatarPreview : EditorWindow
    {

        public static void ShowWin(string title = null)
        {
            EditorWindow win = EditorWindow.GetWindow(typeof(CustomAvatarPreview), false, "CustomAvatarPreview", true);
            if (win != null && win is CustomAvatarPreview)
            {
                (win as CustomAvatarPreview).Init();
            }
        }

        public void Init()
        {
            //EditorUtility.CreateGameObjectWithHideFlags()
            //PreviewRenderUtility
            ObjectPreview preview = new ObjectPreview();
            preview.DrawPreview(new Rect(0, 0, 100, 100));
            
        }

        public void OnGUI()
        {
            AssetPreview.GetAssetPreview(null);
        }
    }
}