/**  
* 标    题：   TextureData.cs 
* 描    述：    
* 创建时间：   2019年06月04日 01:07 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Divak.Script.Editor 
{
	public class TextureData
    {
        public Texture bg1 = null;

        public void Load()
        {
            bg1 = EditorGUIUtility.Load("Tex/bg1.png") as Texture;
        }

	}
}