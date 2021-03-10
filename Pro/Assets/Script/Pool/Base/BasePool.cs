/**  
* 标    题：   BasePool.cs 
* 描    述：    
* 创建时间：   2021年01月25日 19:28 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Divak.Script.Game 
{

	public class BasePool<C> where C : class,new()
    {
        protected static C mInstance;
        public static C Instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = new C();
                }
                return mInstance;
            }
        }

        protected Dictionary<string, Queue<Object>> mPool = new Dictionary<string, Queue<Object>>();

        private readonly object locker = new object();

        protected T Get<T>(string name) where T : Object
        {
            lock (locker)
            {
                if (!mPool.ContainsKey(name)) mPool.Add(name, new Queue<Object>());
                var queue = mPool[name];
                if (queue.Count == 0)
                {
                    return CustomCreate<T>(name);
                }
                var obj = queue.Dequeue();
                return (T)obj;
            }
        }

        protected void Add<T>(string name, T t) where T : Object
        {
            lock(locker)
            {
                if (!mPool.ContainsKey(name)) return;
                CustomReset(name, ref t);
                mPool[name].Enqueue(t);
            }
        }

        public void Clear()
        {
            var dem = mPool.GetEnumerator();
            while (dem.MoveNext())
            {
                var queue = dem.Current.Value;
                var qem = queue.GetEnumerator();
                while (qem.MoveNext())
                {
                    var t = qem.Current;
                    CustomClear(dem.Current.Key, t);
                }
                queue.Clear();
            }
        }
        


        protected virtual T CustomCreate<T>(string name) where T : Object
        {
            return default(T);
        }

        protected virtual void CustomReset<T>(string name, ref T t) where T : Object
        {

        }

        protected virtual void CustomClear<T>(string name, T t) where T : Object
        {

        }
    }
}