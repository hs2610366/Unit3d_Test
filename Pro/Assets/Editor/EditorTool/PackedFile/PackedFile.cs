/**  
* 标    题：   PackedFile.cs 
* 描    述：   打包资源文件
* 创建时间：   2017年07月22日 01:43 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Divak.Script.Game;

namespace Divak.Script.Editor 
{
	public class PackedFile
    {
        public static void SetPackedFile()
        {
            string outputPath = Application.dataPath;
            outputPath = outputPath.Replace(string.Format("{0}/", Config.ProjectName), "");
            outputPath = outputPath + string.Format("/{0}/", EditorUserBuildSettings.activeBuildTarget.ToString());
            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);
            //lz4壓縮
           BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
        }
	}
}