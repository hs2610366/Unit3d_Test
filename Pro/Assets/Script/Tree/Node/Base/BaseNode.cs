/**  
* 标    题：   IBTBaseNode.cs 
* 描    述：    
* 创建时间：   2018年08月07日 02:05 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEngine.UIElements;
#endif

namespace Divak.Script.Game 
{
	public class BaseNode : IBTNode
    {
        private string mNodeName = "CompositeNode";
        public string NodeName { get { return mNodeName; } set { mNodeName = value; } }

        private IBTNode mParent;
        public IBTNode Parent { get { return mParent; } set { mParent = value; } }

        private BTStatus mStatus = BTStatus.Running;
        public BTStatus Status { get { return mStatus; } }

        public bool Enter(object input) { return false; }
        public bool Leave(object input) { return false; }
        public bool Tick(object input, object output) { return false; }

        public IBTNode Clone() { return default(IBTNode); }

#if UNITY_EDITOR

        public string MD5Code = string.Empty;

        protected string mUxml = "TreeNode";
        public Vector2 position = new Vector2(20, 20);
        public VisualElement VE;

        public virtual void Create()
        {
            GetVisualTreeAsset();
        }

        protected virtual void GetVisualTreeAsset()
        {
            if (string.IsNullOrEmpty(mUxml)) return;
            var path = string.Format("Assets/Editor Default Resources/UIElements/UXML/{0}.uxml", mUxml);
            VisualTreeAsset treeAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            if (treeAsset == null) return;
            VE = treeAsset.CloneTree().Q("node");
            if (VE == null) return;
            VE.transform.position = position;
            CreateMD5();
            Object.DestroyImmediate(treeAsset, false);
        }

        protected void CreateMD5()
        {
            MD5Code = MD5Tool.CreateMD5Hash(Time.deltaTime.ToString());
        }
#endif
    }
}