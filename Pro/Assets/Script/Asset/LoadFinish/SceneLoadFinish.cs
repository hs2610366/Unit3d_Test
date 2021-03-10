using System;
using UnityEngine;

namespace Divak.Script.Game
{
    public class SceneLoadFinish : BaseLoadFinish
    {
        private Action<string, bool> mCallback;

        public override void AddCallbak<String, Boolean>(Action<String, Boolean> callback)
        {
            mCallback = callback as Action<System.String, System.Boolean>;
        }

        public override void OnCallback()
        {
            if (mObj == null) return;
            if (mCallback == null) return;
            mCallback(mName, mLoadFinish);
        }
    }
}
