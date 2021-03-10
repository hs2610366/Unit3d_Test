using System;
using Object = UnityEngine.Object;

namespace Divak.Script.Game
{
    interface ILoadFinish
    {
        void UpdateData(Object obj);
        void AddName(string name);
        void AddCallbak<T, K>(Action<T, K> callback);
        void OnCallback();
    }
}
