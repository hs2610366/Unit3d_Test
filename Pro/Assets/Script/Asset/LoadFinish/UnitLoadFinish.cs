using System;
using UnityEngine;

namespace Divak.Script.Game
{
    public class UnitLoadFinish : BaseLoadFinish
    {
        private Unit mUnit = null;
        private Action<GameObject, Unit> mCallback;

        public void AddUnit(Unit unit)
        {
            mUnit = unit;
        }

        public override void AddCallbak<GameObject, Unit>(Action<GameObject, Unit> callback)
        {
            mCallback = callback as Action<UnityEngine.GameObject, Divak.Script.Game.Unit>;
        }

        public override void OnCallback()
        {
            if (mObj == null) return;
            if (mCallback == null) return;
            mCallback(mObj as GameObject, mUnit);
        }
    }
}
