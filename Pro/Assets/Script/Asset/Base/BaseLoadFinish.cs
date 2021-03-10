using System;
using Object = UnityEngine.Object;

namespace Divak.Script.Game
{
    public class BaseLoadFinish : ILoadFinish
    {
        protected string mName;
        protected Object mObj;
        protected Action<Object, string> mICallback;
        protected bool mLoadFinish = true;
        public bool LoadFinish { get { return mLoadFinish; } }
        protected bool mInstantiate = true;
        public bool Instantiate { get { return mInstantiate; } }

        public virtual void SetLoadFinish(bool value)
        {
            mLoadFinish = value;
        }

        public virtual void SetInstantiate(bool value)
        {
            mInstantiate = value;
        }

        public virtual void UpdateData(Object obj)
        {
            mObj = obj;
        }

        public virtual void AddName(string name)
        {
            mName = name;
        }

        public virtual void OnCallback()
        {
            if (mICallback != null) mICallback(mObj, string.IsNullOrEmpty(mName) ? mObj.name.Replace("(Clone)",string.Empty) : mName);
        }

        public virtual void AddCallbak<T, K>(Action<T, K> callback)
        {
            mICallback = callback as Action<Object, string>;
        }
    }
}
