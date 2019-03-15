/**  
* 标    题：   CameraScale.cs 
* 描    述：   自适应UI摄像机
* 创建时间：   2017年07月22日 03:29 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class CameraScale : MonoBehaviour
    {
        void Start()
        {
            int ManualWidth = 960;
            int ManualHeight = 640;
            int manualHeight;
            if (System.Convert.ToSingle(Screen.height) / Screen.width > System.Convert.ToSingle(ManualHeight) / ManualWidth)
                manualHeight = Mathf.RoundToInt(System.Convert.ToSingle(ManualWidth) / Screen.width * Screen.height);
            else
                manualHeight = ManualHeight;
            Camera camera = GetComponent<Camera>();
            float scale = System.Convert.ToSingle(manualHeight / 640f);
            camera.fieldOfView *= scale;
        }

    }
}