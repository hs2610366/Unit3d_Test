/**  
* 标    题：   UIEventListener.cs 
* 描    述：    
* 创建时间：   2018年03月17日 02:24 
* 作    者：   by. by. T.Y.Divak 
* 详    细：    
*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace Divak.Script.Game
{
    public class UIEventListener : EventTrigger
    {
        public delegate void VoidDelegate(GameObject go);
        public VoidDelegate OnClick;

        public static UIEventListener Get(Selectable ui)
        {
            UIEventListener listener = ui.GetComponent<UIEventListener>();
            if (listener == null) listener = ui.gameObject.AddComponent<UIEventListener>();
            return listener;
        }
        public static UIEventListener Get(GameObject ui)
        {
            UIEventListener listener = ui.GetComponent<UIEventListener>();
            if (listener == null) listener = ui.AddComponent<UIEventListener>();
            return listener;
        }

        public static UIEventListener Get(Transform ui)
        {
            return Get(ui.gameObject);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (OnClick != null) OnClick(this.gameObject);
        }
    }
}