/**  
* 标    题：   Guid.cs 
* 描    述：    
* 创建时间：   2021年01月25日 17:48 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;

namespace Divak.Script.Game 
{
	public class GuidTool
    {/// <summary>
     /// 由连字符分隔的32位数字
     /// </summary>
     /// <returns></returns>
        private static string GetGuid()
        {
            System.Guid guid = new Guid();
            guid = Guid.NewGuid();
            return guid.ToString();
        }
        /// <summary>  
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>  
        /// <param name=\"guid\"></param>  
        /// <returns></returns>  
        public static string GuidTo16String()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long GuidToLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}