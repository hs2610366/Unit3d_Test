/**  
* 标    题：   BaseTemp.cs 
* 描    述：    
* 创建时间：   2017年12月19日 20:42 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class BaseTempMgr<T>
    {
        public BaseTempMgr()
        {
        }

        public void Init()
        {
            Clear();
            Analysis();
        }

        public virtual void Analysis()
        {
            string path = string.Format("{0}{1}", PathTool.DataPath, PathTool.Temp);
            string name = typeof(T).ToString();
            name = Path.GetExtension(name);
            name = name.Replace(".", string.Empty);
            List<object> list = Config.InputConfig<List<object>>(path, name, SuffixTool.TableInfo);
            if(list != null)
            {
                CustomAnalysis(list);
            }
        }

        protected virtual void CustomAnalysis(List<object> list)
        {

        }

        public virtual T Find(UInt32 id) { return default(T); }

        public virtual void Clear()
        {

        }

    }

    interface Temp { }
}