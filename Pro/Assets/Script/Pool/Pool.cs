/**  
* 标    题：   Pool.cs 
* 描    述：    
* 创建时间：   2021年03月02日 15:30 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections.Generic;

namespace Divak.Script.Game 
{
	public class Pool<T> where T : new()
    {
        public readonly Pool<T> Instance = new Pool<T>();
        public Queue<T> mQueue = new Queue<T>();

        private readonly object locker = new object();

        public T Get()
        {
            lock (locker)
            {
                if (mQueue.Count > 0)
                {
                    return mQueue.Dequeue();
                }
                return new T();
            }
        }

        public void Add(T obj)
        {
            lock(locker)
            {
                mQueue.Enqueue(obj);
            }
        }

        public void Clear()
        {
            lock(locker)
            {
                while (mQueue.Count > 0)
                {
                    mQueue.Dequeue();
                }
                mQueue.Clear();
            }
        }
	}
}