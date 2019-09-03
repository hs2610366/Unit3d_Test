/**  
* 标    题：   SkillMgr.cs 
* 描    述：    
* 创建时间：   2019年01月29日 03:09 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class SkillMgr
    {
        public static readonly SkillMgr Instance = new SkillMgr();
        private  List<SkillBase> Temps = new List<SkillBase>();

    }
}