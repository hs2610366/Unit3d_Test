using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game
{
    public class ObjLoadFinish : BaseLoadFinish
    {
        private Action<GameObject, string> mCallback;

        public override void AddCallbak<GameObject, String>(Action<GameObject, String> callback)
        {
            mCallback = callback as Action<UnityEngine.GameObject, System.String>;
        }

        public override void OnCallback()
        {
            if (mObj == null) return;
            if (mCallback == null) return;
            mCallback(mObj as GameObject, mName);
        }
    }
}
