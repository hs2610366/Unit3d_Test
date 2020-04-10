using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Divak.Script.Game;

namespace Divak.Script.Editor
{
    public class SerializableTable
    {
        private static string Name = "TableInfo";
        public static void Start()
        {
            if (!Config.IsInt)
            {
                if (Config.Init())
                {
                    Analysis();
                }
            }
            else
            {
                Analysis();
            }
        }

        private static void Analysis()
        {
            string path = Application.dataPath + PathTool.AssetsEditorResource;
            Dictionary<string, Table> dic = Config.InputConfig<string, Table>(path, Name);
            if (dic == null)
            {
                Debug.LogError(string.Format("路径{0}没有找到{1}配置文件！！", path, Name));
                return;
            }
            InfoTool.GeneratingResourceFiles(dic);
        }
    }
}
