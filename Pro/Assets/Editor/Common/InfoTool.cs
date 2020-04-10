/**  
* 标    题：   InfoTool.cs 
* 描    述：    
* 创建时间：   2018年01月19日 20:35 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Divak.Script.Game;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace Divak.Script.Editor
{

    public class InfoTool
    {
        public static string BuildCSharpNamespace = "Divak.Script.Game.";

        #region PathConfig
        private static string PathConfigName = "EditorInfo";
        public static string BuildCSharpDll = "/Assembly-CSharp.dll";

        #endregion

        #region 生成配置文件
        public static void GeneratingResourceFiles(Dictionary<string, Table> dic)
        {
            if (dic == null) return;
            EditorUtility.DisplayCancelableProgressBar("生成配置文件", "开始生成",0);
            int len = dic.Values.Count;
            int index = 0;
            foreach (Table table in dic.Values)
            {
                if (!table.IsOutput) continue;
                EditorUtility.DisplayCancelableProgressBar("生成配置文件", string.Format("正在读取Excel配置表:{0}", table.Path), index / len);
                IWorkbook workbook = ExcelMgr.ReadExcel(table.Path);
                if (workbook == null) continue;
                Dictionary<string, TableInfo>.ValueCollection values = table.Dic.Values;
                foreach (TableInfo ti in values)
                {
                    if (!ti.IsOutput) continue;
                    EditorUtility.DisplayCancelableProgressBar("生成配置文件", string.Format("正在解析Excel配置表:{0}分页:{0}", Path.GetDirectoryName(table.Path), ti.Sheet), index / len);
                    Generating(table.Path, workbook, ti);
                }
                EditorUtility.DisplayCancelableProgressBar("生成配置文件", string.Format("Excel配置表{0}cfg文件生成完成, 关闭{0}Excel表", Path.GetDirectoryName(table.Path), table.Path), index / len);
                ExcelMgr.CloseExcel(workbook);
                index++;
            }
            EditorUtility.DisplayCancelableProgressBar("生成配置文件", "Excel配置表数据生成完毕！！", 1);
            EditorUtility.ClearProgressBar();
        }

        private static void Generating(string path, IWorkbook iwb, TableInfo ti)
        {
            ISheet sheet = iwb.GetSheet(ti.Sheet);
            if (sheet == null) return;
            List<string> names = ExcelMgr.GetSheetTitle(iwb, ti.Sheet);
            if (names == null || names.Count == 0) return;
            int len = sheet.LastRowNum;
            if (len < 1)
            {
                MessageBox.Error(string.Format("{0}没有数据！！", path));
                return;
            }
            List<object> outputList = new List<object>();
            for (int i = 1; i <= len; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;
                Dictionary<string, ParamInfo> param = ti.Params;
                if (param == null) continue;
                object obj = InstantiationScript(ti.ScriptPath);
                if (obj == null) continue;
                Type t = obj.GetType();
                foreach (KeyValuePair<string, ParamInfo> pd in param)
                {
                    if (!pd.Value.IsOutput) continue;
                    if (pd.Value.Index == 0) continue;
                    string proName = ti.PropertyNames[pd.Value.Index];
                    Type proType = ti.PropertyDic[proName];
                    if (proType == null) continue;
                    string value = row.GetCell(pd.Value.CellIndex).ToString();
                    PropertyInfo pro = t.GetProperty(proName);
                    if (pro != null)
                    {
                        pro.SetValue(obj, TypeConversion(value, proType), null);
                    }
                }
                outputList.Add(obj);
            }
            if (outputList.Count == 0)
            {
                Debug.LogError("GetByteByDic Fail!!, dic is null");
                return;
            }
            string outputPath = string.Format("{0}{1}", PathTool.DataPath, PathTool.Temp);
            if(string.IsNullOrEmpty(outputPath))
            {
                Debug.LogError("Get Assets Path Fail!!, InfoTool.PathConfig[EditorPrefsKey.AssetsPath] is null");
                return;
            }
            Config.OutputConfig<object>(outputPath, ti.OutputName, outputList,SuffixTool.TableInfo.ToLower());
        }
        #endregion

        #region 实例化一个脚本
        public static object InstantiationScript(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path);
            if (string.IsNullOrEmpty(name)) return null;
            name = string.Format("{0}{1}", BuildCSharpNamespace, name);
            string dllPath = Config.PathConfig[ConfigKey.CSharpDllPath];
            if (string.IsNullOrEmpty(dllPath)) return null;
            dllPath = string.Format("{0}/{1}", dllPath, BuildCSharpDll);
            Assembly assembly = Assembly.LoadFile(dllPath); // 加载程序集（EXE 或 DLL） 
            return assembly.CreateInstance(name);
        }
        #endregion

        #region 类型转换
        private static object TypeConversion(string value, Type type)
        {
            if(type == typeof(System.Byte))
            {
                return Convert.ToByte(value);
            }
            else if (type == typeof(System.Int16))
            {
                return Convert.ToInt16(value);
            }
            else if (type == typeof(System.UInt16))
            {
                return Convert.ToUInt16(value);
            }
            else if (type == typeof(System.Int32))
            {
                return Convert.ToInt32(value);
            }
            else if (type == typeof(System.UInt32))
            {
                return Convert.ToUInt32(value);
            }
            else if (type == typeof(System.Int64))
            {
                return Convert.ToInt64(value);
            }
            else if (type == typeof(System.UInt64))
            {
                return Convert.ToUInt64(value);
            }
            else if (type == typeof(System.String))
            {
                return Convert.ToString(value);
            }
            else if (type == typeof(System.Single))
            {
                return Convert.ToSingle(value);
            }
            else if (type == typeof(System.Double))
            {
                return Convert.ToDouble(value);
            }
            return (object)value;
        }
        #endregion

        #region 获取屏幕尺寸
        public static Rect GetViewRect()
        {
            int SW = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int SH = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            return new Rect(SW / 2, SH / 2, SW, SH);
        }
        #endregion
    }
}