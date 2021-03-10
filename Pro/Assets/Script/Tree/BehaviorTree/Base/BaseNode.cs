
/**  
* 标    题：   IBTBaseNode.cs 
* 描    述：    
* 创建时间：   2018年08月07日 02:05 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/
using System;
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using Object = UnityEngine.Object;
using UnityEngine.UIElements;
#endif

namespace Divak.Script.Game 
{
	public class BaseNode : INode
    {
        #region 属性
        protected string mName = "Node";
        public string Name { get { return mName; } set { mName = value; } }
        /// <summary>
        /// 完成状态
        /// </summary>
        protected bool mComplete = true;
        /// <summary>
        /// 父节点
        /// </summary>
        protected INode mParent;
        public INode Parent { get { return mParent; } set { mParent = value; } }
        /// <summary>
        /// 执行的先决条件
        /// </summary>
        protected IConditionNode mCondition;
        #endregion

        public BaseNode()
        {
        }

        #region 私有函数
        #endregion

        #region 公开函数
        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="node"></param>
        public virtual void AddParent(INode node)
        {
            mParent = node;
        }
        /// <summary>
        /// 设置前提条件
        /// </summary>
        /// <param name="precondition"></param>
        public virtual void AddPrecondition(IConditionNode condition)
        {
            mCondition = condition;
        }   
        /// <summary>
        /// 前提条件判断
        /// </summary>
        /// <returns></returns>
        public virtual bool AllowCondition()
        {
            if(mCondition != null)
            {
                var state = mCondition.Allow();
                if (!state) mComplete = false;
                return state;
            }
            return true;
        }
        /// <summary>
        /// 执行节点
        /// </summary>
        public virtual IEnumerator Executing()
        {
            yield break;
        }

        public virtual bool CustomExecuting()
        {
            return true;
        }
        /// <summary>
        /// 执行完成
        /// </summary>
        /// <returns></returns>
        public virtual bool Completed() { return mComplete; }

        /// <summary>
        /// 重置数据
        /// </summary>
        public virtual void Reset()
        {
            mComplete = false;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public virtual void Dispose()
        {
            Reset();
        }

        #endregion

#if UNITY_EDITOR

        public string MD5Code = string.Empty;

        protected string mUxml = "BehaviorTreeNode";
        public Vector2 position = new Vector2(20, 20);
        public Button VE;

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
            VE = treeAsset.CloneTree().Q<Button>("node");
            VE.style.backgroundImage = new StyleBackground();
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