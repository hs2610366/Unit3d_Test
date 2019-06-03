/**  
* 标    题：   EditorWinBase.cs 
* 描    述：   脚本编辑器基类
* 创建时间：   2017年11月19日 02:58 
* 作    者：   by. T.Y.Divak
* 详    细：   创建脚本相关编辑器
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Divak.Script.Editor;

public class EditorWinBase<T> : EditorWindow
{
    /// <summary>
    /// 是否初始化
    /// </summary>        
    protected bool IsInit = false;
    protected string Title = "EditorWinBase";
    protected Rect ContextRect = new Rect(0, 0, 10, 10);

    public static void ShowWin(string title = null)
    {
        Type t = typeof(T);
        EditorWindow win = EditorWindow.GetWindow(t, false, string.IsNullOrEmpty(title) ? t.Name : title, true);
        if (win != null && win is EditorWinBase<T>)
        {
            (win as EditorWinBase<T>).Init();
        }
    }

    #region 私有函数

    /// <summary>
    /// ui
    /// </summary>
    private void OnGUI()
    {
        if (IsInit) return;
        CustomGUI();
    }

    /// <summary>
    /// 无效
    /// </summary>
    private void OnDrawGizmos()
    {
        if (IsInit) return;
        CustomDrawGizmos();
    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {

    }/// <summary>
     /// 当窗口获得焦点时调用一次
     /// </summary>
    private void OnFocus()
    {
    }

    /// <summary>
    /// 当窗口丢失焦点时调用一次
    /// </summary>
    private void OnLostFocus()
    {
    }

    /// <summary>
    /// 当Hierarchy视图中的任何对象发生改变时调用一次
    /// </summary>
    private void OnHierarchyChange()
    {
    }

    /// <summary>
    /// 当Project视图中的资源发生改变时调用一次
    /// </summary>
    private void OnProjectChange()
    {
    }

    /// <summary>
    /// 窗口面板的更新 
    /// 这里开启窗口的重绘，不然窗口信息不会刷新
    /// </summary>
    private void OnInspectorUpdate()
    {
        this.Repaint();
    }

    /// <summary>
    /// 当窗口出去开启状态，并且在Hierarchy视图中选择某游戏对象时调用
    /// 有可能是多选，这里开启一个循环打印选中游戏对象的名称
    /// </summary>
    private void OnSelectionChange()
    {
        foreach (Transform t in Selection.transforms)
        {
            Debug.Log("OnSelectionChange" + t.name);
        }
    }


    private void OnDisable()
    {
        CustomClose();
    }

    /// <summary>
    /// 当销毁
    /// </summary>
    private void OnDestroy()
    {
        CloseNotification();
        CustomDestroy();
    }
    #endregion

    #region 保护函数
    /// <summary>
    /// 初始化
    /// </summary>
    protected virtual void Init()
    {
        this.titleContent = new GUIContent(Title);
        this.position = ContextRect;
        CustomInit();
    }
    /// <summary>
    /// 更新消息
    /// </summary>
    protected virtual void UpdateNotification(string msg)
    {
        if (string.IsNullOrEmpty(msg)) return;
        ShowNotification(new GUIContent(msg));
    }

    /// <summary>
    /// 关闭消息
    /// </summary>
    protected virtual void CloseNotification()
    {
        RemoveNotification();
    }

    /// <summary>
    /// 自定义初始化
    /// </summary>
    protected virtual void CustomInit()
    {
        IsInit = true;
        UpdateNotification("正在初始化数据...");
    }

    protected virtual void InitComplete()
    {
        UpdateNotification("初始化数据完成");
        IsInit = false;
    }

    /// <summary>
    /// 自定义UI
    /// </summary>
    protected virtual void CustomGUI()
    {
        RightMenu();
    }

    protected virtual void CustomDrawGizmos()
    {

    }

    /// <summary>
    /// 自定义关闭
    /// </summary>
    protected virtual void CustomClose()
    {

    }

    /// <summary>
    /// 自定义销毁
    /// </summary>
    protected virtual void CustomDestroy()
    {

    }

    /// <summary>
    /// 右键菜单
    /// </summary>
    protected virtual void RightMenu()
    {
        Event evt = Event.current;
        if (evt.type == EventType.ContextClick)
        {
            // Vector2 mousePos = Event.current.mousePosition;
            CustomRightMenu();
        }
        //evt.Use();
    }

    /// <summary>
    /// 自定义右键菜单
    /// </summary>
    protected virtual void CustomRightMenu()
    {

    }
    #endregion
}
