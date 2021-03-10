/**  
* 标    题：   Mahtex.cs 
* 描    述：    
* 创建时间：   2019年12月11日 12:09 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
    public class Mathex
    {
        /// <summary>
        /// 获取旋转角度
        /// </summary>
        /// <param name="birth"> 原点 </param>
        /// <param name="target"> 目标点 </param>
        /// <param name="upwards"> 定义上方向 </param>
        /// <returns> 原点朝向目标点旋转角度</returns>
        public static Vector3 GetAngles(Vector3 from, Vector3 to, Vector3 upwards)
        {
            upwards = upwards != null ? upwards : Vector3.up;
            Vector3 forwardDir = to - from;
            if (forwardDir == Vector3.zero) return Vector3.zero;
            Quaternion lookAtRot = Quaternion.LookRotation(forwardDir, upwards);
            return lookAtRot.eulerAngles;
        }

        public static Vector3 GetAngles(Vector3 from, Vector3 to)
        {
            return GetAngles(from, to, Vector3.up);
        }

        /// <summary>
        /// 获取水平旋转角度（y轴相同） 
        /// </summary>
        /// <param name="from">原点</param>
        /// <param name="to">目标点</param>
        /// <param name="upwards">定义上方向</param>
        /// <returns>原点水平朝向目标点旋转角度</returns>
        public static Vector3 GetAnglesNoY(Vector3 from, Vector3 to, Vector3 upwards)
        {
            from.y = 0;
            to.y = 0;
            return GetAngles(from, to, upwards);
        }

        /// <summary>
        /// 获取水平方向(默认 upwards 为 up)
        /// </summary>
        /// <param name="from">原点</param>
        /// <param name="to">目标点</param>
        /// <returns>原点水平朝向目标点旋转角度</returns>
        public static Vector3 GetAnglesNoY(Vector3 from, Vector3 to)
        {
            return GetAnglesNoY(from, to, Vector3.up);
        }

        /// <summary>
        /// 角度取模
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static float GetModAngles(float angle)
        {
            angle = angle % 360;
            angle *= 360;
            return angle;
        }

        /// <summary>
        /// 范围角度 -180~180
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void GetRangeAngles(ref Vector3 from, ref Vector3 to)
        {
            float value = from.y - to.y;
            if (value > 180)
                to.y = to.y + 360;
            else if (value < -180)
                from.y = from.y + 360;
        }

        /// <summary>
        /// 获取方向
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static Vector3 GetDir(Vector3 from, Vector3 to)
        {
            return (from - to).normalized;
        }
        /// <summary>
        /// 获取水平方向
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static Vector3 GetDirNoY(Vector3 from, Vector3 to)
        {
            from.y = 0;
            to.y = 0;
            return GetDir(from, to);
        }

        /// <summary>
        /// 通过方向距离 算目标点
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="dis"></param>
        /// <returns></returns>
        public static Vector3 GetDirDisPos(Vector3 from, Vector3 to, float dis)
        {
            Vector3 dir = GetDir(from, to);
            return from - dir.normalized * dis;
        }

        //<summary>
        //线性贝塞尔
        //</summary>
        //<param name = "from" > 始点 </ param >
        //< param name="to">终点</param>
        //<param name = "t" > 时间百分比 </ param >
        public static Vector3 GetLinearPoint(Vector3 from, Vector3 to, float t)
        {
            t = Mathf.Clamp01(t);
            return from + ((to - from) * t);
        }

        // <summary>
        // 二阶贝塞尔
        // </summary>
        // <param name = "from" > 始点 </ param >
        // < param name="p">中间点</param>
        // <param name = "to" > 终点 </ param >
        // < param name="t">时间百分比</param>
        // <returns></returns>
        public static Vector3 GetQuadraticCurvePoint(Vector3 from, Vector3 p, Vector3 to, float t)
        {
            t = Mathf.Clamp01(t);
            Vector3 part1 = from * Mathf.Pow(1 - t, 2);
            Vector3 part2 = p * 2 * (1 - t) * t;
            Vector3 part3 = to * Mathf.Pow(t, 2);
            return part1 + part2 + part3;
        }

        //相对抛物线中点 
        // <param name = "from" > 始点 </ param >
        // < param name="to">终点</param>
        // <returns>中间点</returns>
        public static Vector3 GetParaCurvePos(Vector3 from, Vector3 to)
        {
            Vector3 forward = to - from;
            //小于1不作曲线
            if (forward.sqrMagnitude < 1)
                return from;
            Vector3 pos = forward / 2 + from;
            pos.y = pos.y * 5;
            return pos;
        }
    }
}