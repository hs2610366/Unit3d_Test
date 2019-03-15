/**  
* 标    题：   SceneBase.cs 
* 描    述：    
* 创建时间：   2018年07月02日 03:00 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Divak.Script.Game;
using UnityEngine.SceneManagement;

public class SceneBase
{
    protected SceneTemp temp;

    protected void UpdateTemp(SceneTemp t)
    {
        temp = t;
        LoadScene(temp.modle);
    }

    private void LoadScene(string name)
    {
        if(string.IsNullOrEmpty(name))
        {
            MessageBox.Error(string.Format("场景{0}资源为空", temp.id));
            return;
        }
        AssetsMgr.Instance.LoadSceneComplete += LoadSceneComplete;
        AssetsMgr.Instance.LoadScene(name);
    }


    private void LoadSceneComplete(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
        EnterSceneComplete();
    }

    private void EnterSceneComplete()
    {

    }

    public void DestroyScene()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}
