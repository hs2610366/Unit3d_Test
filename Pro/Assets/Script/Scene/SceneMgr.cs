/**  
* 标    题：   SceneMgr.cs 
* 描    述：    
* 创建时间：   2018年07月02日 02:55 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Divak.Script.Game;

public class SceneMgr : SceneBase
{
    public static readonly SceneMgr Instance = new SceneMgr();

#if UNITY_EDITOR
    /// <summary>
    /// 当前编辑器场景配置表
    /// </summary>
    public SceneTemp CurEditorTemp;
#endif

    public void EnterScene(UInt32 sceneid)
    {
        SceneTemp temp = SceneTempMgr.Instance.Find(sceneid);
        if(temp == null)
        {
            MessageBox.Log(string.Format("场景不存在 id:{0}",sceneid));
            return;
        }
        DestroyScene();
        UpdateTemp(temp);
    }
}
