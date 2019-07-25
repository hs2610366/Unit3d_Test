/**  
* 标    题：   EnumTool.cs 
* 描    述：    
* 创建时间：   2017年12月15日 12:26 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Divak.Script.Game
{
    #region 变量类型
    public enum GameType
    {
        Single,
        NetWork
    }

    public enum TypeEnum
    {
        Double,
        UInt64,
        Int64,
        UInt32,
        Int32,
        UInt16,
        Int16,
        Byte,
        String,
        ByteArray,
    }
    #endregion

    #region 角色类型
    public enum UnitType
    {
        Player,
        Team,
        Enemy
    }
    #endregion

    #region 动画切换类型
    public enum UnitAnimInfoType
    {
        INT,
        FLOAT,
        BOOL,
        TRIGGER
    }
    #endregion

    #region 动作类型
    public enum AnimPlayType
    {
        //连续的 顺序 一次触发一个动作
        Continuity,
        //随机的 随机 一次触发一个动作
        Random,
        //一组的 顺序 一次触发所有动作
        Group,
    }
    #endregion

    #region 动作类型
    public enum UnitAnimState
    {
        None,
        Play,
        CossFade,
    }
    #endregion

    #region 角色状态
    [SerializeField]
    public enum UnitState
    {
        None,
        Idea,
        Walk,
        Run,
        Fight,
        Skill_1,
        Skill_2,
        Skill_3,
    }
    #endregion

    public class EnumTool
    {

        #region 获取枚举名
        public static string Description(Enum en)
        {


            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }
        #endregion
    }
}