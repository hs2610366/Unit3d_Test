/**  
* 标    题：   UIBase.cs 
* 描    述：   UI的基类
* 创建时间：   2017年07月25日 02:12 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase
{
    protected GameObject mGo;
    protected Transform mTrans;
    protected string mName;

    public UIBase(GameObject go)
    {
        this.mGo = go;
        this.mTrans = go.transform;
        CustomInit();
        CustomAddEvent();
    }

    #region 保护函数
    /// <summary>
    /// 自定义初始化
    /// </summary>
    protected virtual void CustomInit() { }
    /// <summary>
    /// 自定义添加函数
    /// </summary>
    protected virtual void CustomAddEvent() { }
    /// <summary>
    /// 自定义移除函数
    /// </summary>
    protected virtual void CustomRemoveEvent() { }
    /// <summary>
    /// 自定义打开
    /// </summary>
    protected virtual void CustomOpen() { }
    /// <summary>
    /// 自定义关闭
    /// </summary>
    protected virtual void CustomClose() { }
    /// <summary>
    /// 清理
    /// </summary>
    protected virtual void Clean() { }
    /// <summary>
    /// 自定义销毁
    /// </summary>
    protected virtual void CustomDispose() { }

    #endregion

    #region 私有函数

    #endregion

    #region 公开函数
    /// <summary>
    /// UI对象
    /// </summary>
    public GameObject GO { get { return mGo; } }

    /// <summary>
    /// 打开
    /// </summary>
    public void Open()
    {
        if (mGo != null) mGo.SetActive(true);
        CustomOpen();
    }

    /// <summary>
    /// 关闭
    /// </summary>
    public void Close()
    {
        Clean();
        CustomClose();
        if (mGo != null) mGo.SetActive(false);
    }

    /// <summary>
    /// 释放
    /// </summary>
    public void Dispose()
    {
        CustomRemoveEvent();
        CustomDispose();
        if (mGo != null)
        {
            GameObject.Destroy(mGo);
            mGo = null;
        }
    }
    #endregion
}
