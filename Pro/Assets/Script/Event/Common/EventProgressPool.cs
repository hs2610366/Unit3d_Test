/**  
* 标    题：   EventProgressPool.cs 
* 描    述：    
* 创建时间：   2021年03月09日 15:19 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Divak.Script.Game 
{
	public class EventProgressPool<T> where T : class, new()
    {
        public readonly static T Instance = new T();
        #region 对象
        private ConcurrentQueue<EventData> mEvents = new ConcurrentQueue<EventData>();
        private ConcurrentQueue<EventProgress> mProgress = new ConcurrentQueue<EventProgress>();
        private List<EventProgress> mPool = new List<EventProgress>();
        #endregion

        public EventProgressPool()
        {
        }

        public K Get<K>() where K : EventProgress, new()
        {
            if (mPool.Count > 0) return mPool[0] as K;
            return new K();
        }

        public void Add(EventProgress progress)
        {
            progress.Reset();
            mPool.Add(progress);
        }

        public EventProgress GetProgress()
        {
            EventProgress progress = null;
            if (mProgress.Count > 0)
            {
                mProgress.TryDequeue(out progress);
            }
            return progress;
        }

        public void SetProgress(EventProgress data)
        {
            mProgress.Enqueue(data);
        }

        public EventData GetEventData()
        {
            EventData eData;
            if (mEvents.Count > 0)
            {
                mEvents.TryDequeue(out eData);
                return eData;
            }
            eData = new EventData();
            eData.Execute = InvokeProgress;
            return eData;
        }

        public void SetEventData(EventData data)
        {
            mEvents.Enqueue(data);
        }

        private void InvokeProgress()
        {
            if (mProgress.Count == 0)
            {
                return;
            }
            EventProgress progress = GetProgress();
            if (progress != null)
            {
                if (progress.Update != null)
                    progress.Update.Invoke(progress);
                Add(progress);
            }
        }

        public void Clear()
        {
            while (mEvents.Count > 0)
            {
                EventData eData;
                mEvents.TryDequeue(out eData);
            }
            while (mProgress.Count > 0)
            {
                EventProgress progress = null;
                mProgress.TryDequeue(out progress);
            }
            while (mPool.Count > 0)
            {
                int index = mPool.Count - 1;

                EventProgress progress = mPool[index];
                mPool.RemoveAt(index);
                progress = null;
            }
        }

    }
}