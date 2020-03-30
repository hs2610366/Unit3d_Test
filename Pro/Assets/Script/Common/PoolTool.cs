/**  
* 标    题：   PoolTool.cs 
* 描    述：    
* 创建时间：   2020年03月06日 16:30 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{

	public class PoolTool
    {
        public static readonly PoolTool Instance = new PoolTool();

        private Dictionary<string, Queue<UnityEngine.Object>> PrefabPools = new Dictionary<string, Queue<UnityEngine.Object>>();
        private Dictionary<string, Queue<System.Object>> Pools = new Dictionary<string, Queue<System.Object>>();
        //锁
        private readonly object PrefabQueueLocker = new object();
        private readonly object QueueLocker = new object();


        public PoolTool()
        {
            Global.Instance.OnUpdate += Update;
        }


        public void Update()
        {

        }

        /// <summary>
        /// 借用
        /// </summary>
        /// <param name="key">标识</param>
        /// <param name="path">参数</param>
        public T GetPrefab<T>(string key, string fileNameWithoutExtension, string suffix) where T : UnityEngine.Object 
        { 
            lock (PrefabQueueLocker)
            {
                if (!PrefabPools.ContainsKey(key))
                {
                    string path = fileNameWithoutExtension + suffix;
                    PrefabPools.Add(key, new Queue<UnityEngine.Object>());
                    return AssetsMgr.Instance.Load(path) as T;
                }
                else
                {
                    UnityEngine.Object t = PrefabPools[key].Dequeue();
                    return t as T ;
                }
            }
        }


        /// <summary>
        /// 归还
        /// </summary>
        /// <param name="key"></param>
        public void BackPrefab<T>(string key, T prefab) where T : UnityEngine.Object
        {
            lock (this.PrefabQueueLocker)
            {
                if (!PrefabPools.ContainsKey(key))
                {
                    MessageBox.Error(string.Format("Pools中未含有Key;{0}", key));
                    return;
                }
                if (PrefabPools[key].Count > 0)
                {
                    PrefabPools[key].Enqueue(prefab);
                }
                else
                {
                    MessageBox.Error(string.Format("Pools中未含有Key;{0}的Queue数量为0", key));
                }
            }
        }
    }
}