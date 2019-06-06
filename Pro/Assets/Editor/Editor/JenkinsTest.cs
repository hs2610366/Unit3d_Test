using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class JenkinsTest
{

    // Use this for initialization
    static void JenkinsTestStart()
    {
        string path = "E:/MMO/test.apk";
        bool isDebug = false;
        string[] strs = System.Environment.GetCommandLineArgs();
        if (strs != null)
        {
            int len = strs.Length;
            foreach (string k in strs)
            {
                if (k == "IsDebug")
                {
                    isDebug = true;
                }
            }
        }
        //EditorUserBuildSettings.development = isDebug;
        EditorUserBuildSettings.allowDebugging = isDebug;
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            Debug.LogError("===========>>>1 " + scene.path);
        }
        Debug.LogError("===========>>>2 activeBuildTarget " + EditorUserBuildSettings.activeBuildTarget.ToString());
        var str = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path, BuildTarget.Android, BuildOptions.None);
        //BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
        Debug.LogFormat("===========>>>3:{0}", str);

        if (File.Exists(path))
        {
            Debug.LogFormat("===========>>>exist:{0}", path);
        }
        else
        {
            Debug.LogFormat("===========>>>not exist:{0}", path);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
