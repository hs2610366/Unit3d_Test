/**  
* 标    题：   BehaviorTree.cs 
* 描    述：    
* 创建时间：   2021年03月06日 15:32 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class BehaviorTree
    {
        private INode mCurrentNode;
        private bool mIsPlay = false;

        public void Play(string name)
        {
            if(mIsPlay == true)
            {
                return;
            }
            mIsPlay = true;
            Global.Instance.StartCoroutine(Executing());
        }

        public void Stop()
        {
            if (mIsPlay == false) return;
            if (mCurrentNode == null) return;
            Global.Instance.StopCoroutine(Executing());
        }

        IEnumerator Executing()
        {
            while(true)
            {
                if (mIsPlay == false) break;
                var state = mCurrentNode.Executing();
                yield return new WaitForEndOfFrame();
                if (mIsPlay == false) break;
            }
        }

    }
}