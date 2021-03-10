/**  
* 标    题：   PoolTool.cs 
* 描    述：    
* 创建时间：   2020年03月06日 16:30 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Divak.Script.Game 
{

    public class ObjectPool : BasePool<ObjectPool>
    {


        public ObjectPool()
        {
        }

        public GameObject GetPrefab(string name)
        {
            return Get<GameObject>(name);
        }

        public void AddPrefab(string name, GameObject go)
        {
            go.SetActive(false);
            Add(name, go);
        }

        protected override GameObject CustomCreate<GameObject>(string name)
        {
            // var obj = AssetsMgr.Instance.LoadPrefab(name);
            // return obj;
            return null;
        }

        protected override void CustomReset<GameObject>(string name, ref GameObject t)
        {
            if(t is UnityEngine.GameObject)
            {
                (t as UnityEngine.GameObject).transform.parent = null;
                (t as UnityEngine.GameObject).transform.position = Vector3.zero;
                (t as UnityEngine.GameObject).transform.eulerAngles = Vector3.zero;
                (t as UnityEngine.GameObject).transform.localScale = Vector3.one;
                (t as UnityEngine.GameObject).name = Path.GetFileNameWithoutExtension(name);
            }
        }

        protected override void CustomClear<GameObject>(string name, GameObject t)
        {
            Object.Destroy(t);
            //AssetsMgr.Instance.UnloadPrefab(t as UnityEngine.GameObject);
        }
    }
}