/**  
* 标    题：   HttpDownLoad.cs 
* 描    述：    
* 创建时间：   2020年03月06日 17:50 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class HttpDownLoad
    {
        public static readonly HttpDownLoad Instance = new HttpDownLoad();

        private static object locker = new object();

        private int ThreadLimit = 5;
        private int ThreadCount = 0;

        private Queue<DownLoadItem> paths = new Queue<DownLoadItem>();

        public void Run()
        {
            if (paths.Count == 0) return;
            if (ThreadCount >= ThreadLimit) return;
            lock (locker)
            {
                DownLoadItem item = paths.Dequeue();
                Thread thread = new Thread(new ThreadStart(item.DownLoad));
                thread.Start();
                ThreadCount++;
            }
            if (paths.Count > 0)
            {
                Run();
            }
        }

        private void OnFinih()
        {
            ThreadCount--;
            if (paths.Count > 0)
            {
                Run();
            }
        }

        private void OnFail(string error)
        {
            ThreadCount--;
        }


        public void AddDownLoadPath(string path)
        {
            DownLoadItem item = new DownLoadItem(OnFinih, OnFail);
            item.UpdateData("http://localhost:81/", "C:/Users/Administrator/Downloads/下载目录", path);
            paths.Enqueue(item);
        }


        public void Dispose()
        {
        }
    }

}