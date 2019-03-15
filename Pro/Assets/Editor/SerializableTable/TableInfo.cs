/**  
* 标    题：   TableInfo.cs 
* 描    述：    
* 创建时间：   2017年12月15日 17:36 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.CompilerServices.DynamicAttribute;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Divak.Script.Game;

namespace Divak.Script.Editor
{
    [Serializable]
    public class Table
    {
        public string Path = string.Empty;
        public bool IsOutput = false;
        public Dictionary<string, TableInfo> Dic = new Dictionary<string, TableInfo>();
    }

    [Serializable]
    public class ParamInfo
    {
        public bool IsOutput = false;
        public int CellIndex = -1;
        public int Index = 0;
    }


    [Serializable]
    public class TableInfo
    {
        public string OutputName { get; set; }
        public bool IsOutput = false;
        public string Sheet { get; set; }
        //public string Name { get; set; }
        public string[] ScriptObjParams { get; set; }
        private string ScriptPathValue { get; set; }
        /// <summary>
        /// 下拉列表索引
        /// </summary>
        public string[] PropertyNames;
        /// <summary>
        /// 属性name,type
        /// </summary>
        public Dictionary<string, Type> PropertyDic = new Dictionary<string,Type>();

        public string ScriptPath
        {
            get { return ScriptPathValue; }
            set
            {
                ScriptPathValue = value;
                UpdateScriptObj(value);
            }
        }
        /// <summary>
        /// 表字段标题,属性
        /// </summary>
        public Dictionary<string, ParamInfo> Params = new Dictionary<string, ParamInfo>();

        public TableInfo()
        {
            if(!string.IsNullOrEmpty(ScriptPathValue))
            {
                UpdateScriptObj(ScriptPathValue);
            }
        }
        
        #region Params
        public void UpdateParams(List<string> list)
        {
            if (list == null || list.Count == 0)
            {
                Params.Clear();
            }
            else
            {
                AddParams(list);
                RemoveParams(list);
            }
        }

        private void AddParams(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                string paramName = list[i];
                if (!Params.ContainsKey(paramName))
                {
                    ParamInfo info = new ParamInfo();
                    info.CellIndex = i;
                    Params.Add(paramName, info);
                }
                Params[paramName].IsOutput = true;
            }
        }

        private void RemoveParams(List<string> list)
        {
            List<string> remove = new List<string>();
            foreach (KeyValuePair<string, ParamInfo> dic in Params)
            {
                if (!list.Contains(dic.Key)) remove.Add(dic.Key);
            }
            if(remove.Count > 0)
            {
                for(int i = 0; i < remove.Count; i ++)
                {
                    string name = remove[i];
                    Params.Remove(name);
                }
            }
        }
        #endregion

        #region ScriptObject
        private void UpdateScriptObj(string path)
        {
            PropertyDic.Clear();
            PropertyNames = null;
            if (!string.IsNullOrEmpty(path))
            {
                OutputName = Path.GetFileNameWithoutExtension(path);
                /**
                string name = Path.GetFileNameWithoutExtension(path);
                if (string.IsNullOrEmpty(name)) return;
                name = string.Format("{0}{1}", PathEditorTool.BuildCSharpNamespace, name);
                string dllPath = InfoTool.PathConfig[EditorPrefsKey.CSharpDllPath];
                if (string.IsNullOrEmpty(dllPath)) return;
                dllPath = string.Format("{0}/{1}", dllPath, PathEditorTool.BuildCSharpDll);
                Assembly assembly = Assembly.LoadFile(dllPath); // 加载程序集（EXE 或 DLL） 
                object obj = assembly.CreateInstance(name);
                **/
                object obj = InfoTool.InstantiationScript(path);
                if (obj != null)
                {
                    Type t = obj.GetType();
                    List<string> names = new List<string>();
                    names.Add("None");
                    foreach(PropertyInfo pi in t.GetProperties())
                    {
                        names.Add(pi.Name);
                        PropertyDic.Add(pi.Name, pi.PropertyType);
                    }
                    if (names.Count > 0) PropertyNames = names.ToArray();
                }
            }
        }
        #endregion

    }
}
