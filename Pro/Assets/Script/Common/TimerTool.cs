/**  
* 标    题：   TimerTool.cs 
* 描    述：    
* 创建时间：    
* 作    者：    
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class TimerTool
    {
        public static readonly TimerTool Instance = new TimerTool();

        private DateTime currentTime;

        public string GetTimeOfUIF8
        {
            private set { }
            get
            {
                string value = string.Empty;
                currentTime = DateTime.Now;
                value = currentTime.Year + "年" + Cover(currentTime.Month) + "月" + Cover(currentTime.Day) + "日 " + Cover(currentTime.Hour) + ":" + Cover(currentTime.Minute);
                return value;
            }
        }

        /// <summary>
        /// 补位
        /// </summary>
        /// <returns></returns>
        public string Cover(int value)
        {
            return value.ToString().PadLeft(2, '0');
        }
	}
}