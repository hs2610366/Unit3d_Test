/**  
* 标    题：   PoolTool.cs 
* 描    述：    
* 创建时间：   2020年03月06日 16:30 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/
using System.IO;
using UnityEngine;

namespace Divak.Script.Game 
{

    public class ObjectPool : BasePool<Object>
    {

        public ObjectPool()
        {

        }

        public GameObject GetPrefab(string name)
        {
            return Get(name) as GameObject;
        }

        public Object GetObj(string name)
        {
            return Get(name) as Object;
        }

        public void AddPrefab(string name, GameObject go)
        {
            go.SetActive(false);
            Add(name, go);
        }

        public void AddObj(string name, Object go)
        {
            Add(name, go);
        }

        protected override Object CustomCreate(string name)
        {
            var obj = AssetsMgr.Instance.Load(name);
            return obj;
        }

        protected override void CustomReset(string name, ref Object t)
        {
            if(t is GameObject)
            {
                (t as GameObject).transform.parent = null;
                (t as GameObject).transform.position = Vector3.zero;
                (t as GameObject).transform.eulerAngles = Vector3.zero;
                (t as GameObject).transform.localScale = Vector3.one;
                (t as GameObject).name = Path.GetFileNameWithoutExtension(name);
            }
        }

        protected override void CustomClear(string name, Object t)
        {
            Object.Destroy(t);
            AssetsMgr.Instance.Destory(name);
        }
    }
}