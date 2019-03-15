/**  
* 标    题：   CheckAssetsDuplicationNname.cs 
* 描    述：   检测同名资源
* 创建时间：   2018年02月25日 02:27 
* 作    者：   by.  T.Y.Divak 
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
	public class CheckAssetsDuplicationNname
    {
        public static bool Check()
        {
            string path = Application.dataPath;
            List<string> assetsName = new List<string>();
            if (Directory.Exists(path))
            {
                DirectoryInfo direction = new DirectoryInfo(path);
                FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
                string RelativePath = string.Empty;
                int len = files.Length;
                for (int i = 0; i < len; i++)
                {
                    if (files[i].Name.EndsWith(SuffixTool.Meta)) continue;
                    if (assetsName.Contains(files[i].Name))
                    {
                        MessageBox.Error(string.Format("检测到同名资源：{0}", files[i].Name));
                        return true;
                    }
                    float offset = (i + 1) / (float)len;
                    assetsName.Add(files[i].Name);
                }
            }
            return false;
        }

        public static bool Check(string path)
        {
            string assetsPath = Application.dataPath;
            string p = string.Format("{0}/{1}", Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/')), path);
            p = p.Replace("/", "\\");
            if (Directory.Exists(assetsPath))
            {
                DirectoryInfo direction = new DirectoryInfo(assetsPath);
                FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
                string RelativePath = string.Empty;
                int len = files.Length;
                FileInfo info = null;
                for (int i = 0; i < len; i++)
                {
                    if (files[i].Name.EndsWith(SuffixTool.Meta)) continue;
                    if (path.Contains(files[i].Name) && !p.Contains(files[i].ToString()))
                    {
                        MessageBox.Error(string.Format("检测到同名资源：{0}", files[i]));
                        return true;
                    }
                }
            }
            return false;
        }
    }
}