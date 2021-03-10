/**  
* 标    题：   IEventData.cs 
* 描    述：    
* 创建时间：   2021年03月09日 14:55 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;

namespace Divak.Script.Game 
{
	public struct EventData
    {
        public string Key;
        public Action Execute;
    }
}