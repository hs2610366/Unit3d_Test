/**  
* 标    题：   SkillBase.cs 
* 描    述：    
* 创建时间：   2019年01月29日 03:11 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game
{
    [ExecuteInEditMode]
    public class SkillBase : MonoBehaviour
    {
        /// <summary>
        /// 起源
        /// </summary>
        private Vector3 Origin = Vector3.zero;
        /// <summary>
        /// 终端
        /// </summary>
        private Vector3 Terminal = Vector3.zero;

        /// <summary>
        /// 技能配置
        /// </summary>
        private SkillTemp Temp = null;

        #region 私有的

        private void OnAwake()
        {

        }

        private void OnEnable()
        {
            Origin = transform.position;
        }

        private void OnStart()
        {

        }
        #endregion


        #region 公开的
        public virtual void UpdateData(SkillTemp temp)
        {
            Temp = temp;
        }
        #endregion

    }
}