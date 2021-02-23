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

namespace Divak.Script.Game 
{

	public class BasePool<T>
    {
        public readonly BasePool<T> Instance = new BasePool<T>();

        protected Dictionary<string, Queue<T>> mPool = new Dictionary<string, Queue<T>>();

        private readonly object locker = new object();

        protected T Get(string name)
        {
            lock (locker)
            {
                if (!mPool.ContainsKey(name)) mPool.Add(name, new Queue<T>());
                var queue = mPool[name];
                if (queue.Count == 0)
                {
                    return CustomCreate(name);
                }
                return queue.Dequeue();
            }
        }

        protected void Add(string name, T t)
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
        


        protected virtual T CustomCreate(string name)
        {
            return default(T);
        }

        protected virtual void CustomReset(string name, ref T t)
        {

        }

        protected virtual void CustomClear(string name, T t)
        {

        }
    }
}