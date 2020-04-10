/**  
* 标    题：   EditorWinBase.cs 
* 描    述：   脚本编辑器基类
* 创建时间：   2017年11月19日 02:58 
* 作    者：   by. T.Y.Divak
* 详    细：   创建脚本相关编辑器
*/
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using Divak.Script.Game;

namespace Divak.Script.Editor
{
    public class TableEditor : EditorWinBase<TableEditor>
    {
        private string Name = "TableInfo";
        private string Output = "OutputEditorInfo";
        private string OutputAndSave = "OutputInfoAndSaveTemp";
        #region GUI
        /// <summary>
        /// 滚动区宽
        /// </summary>
        private int SVWidth = 200;
        /// <summary>
        /// 滚动区高
        /// </summary>
        private int SVHeight = 800;
        /// <summary>
        /// 配置区宽
        /// </summary>
        private int CSVWidth = 1700;
        /// <summary>
        /// 属性按钮宽度
        /// </summary>
        private int PWidth = 150;
        /// <summary>
        /// 选中的配置表索引
        /// </summary>
        private int SelectTableIndex = -1;
        /// <summary>
        /// 选中的配置表分页索引
        /// </summary>
        private int SelectSheetIndex = -1;
        /// <summary>
        /// 滚动区拖动条位置坐标
        /// </summary>
        private Vector2 SVPos = Vector2.one;
        /// <summary>
        /// 配置区拖动条位置坐标
        /// </summary>
        private Vector2 CSVPos = Vector2.one;
        /// <summary>
        /// 配置表状态
        /// </summary>
        private bool[] ToggleList;
        /// <summary>
        /// 配置表分页名
        /// </summary>
        private List<string> SheetsName;
        /// <summary>
        /// 配置表分页状态
        /// </summary>
        private bool[] ToggleSheetList;
        /// <summary>
        /// 配置表路径名字
        /// </summary>
        private string[] PathNames;
        /// <summary>
        /// 配置表分页数量
        /// </summary>
        private int SheetNum;
        /// <summary>
        /// 配置表数据
        /// </summary>
        private IWorkbook Workbook;
        /// <summary>
        /// 配置表分页数据字段名
        /// </summary>
        private List<string> ParamNames;
        /// <summary>
        /// 选中的配置表Key
        /// </summary>
        private string SelectKey;
        /// <summary>
        /// 选中的配置表分页名
        /// </summary>
        private string SelectSheetName;

        private Dictionary<string, Table> ConfigDic = new Dictionary<string, Table>();
        //private Dictionary<string, Dictionary<string, TableInfo>> Dic = new Dictionary<string, Dictionary<string, TableInfo>>();
        #endregion

        #region 保护函数

        protected override void Init()
        {
            Title = "配置表数据关联生成";
            ContextRect = new Rect(SVWidth, 0, CSVWidth, SVHeight);
            base.Init();
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

        protected override void CustomGUI()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Width(SVWidth));
            SVPos = EditorGUILayout.BeginScrollView(SVPos, false, true, GUILayout.Height(SVHeight), GUILayout.Width(SVWidth));
            GUILayout.BeginVertical();
            DrawScrollView();
            GUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            DrawSheetTitle();
            GUILayout.EndHorizontal();
            CSVPos = EditorGUILayout.BeginScrollView(CSVPos, true, true, GUILayout.Height(SVHeight), GUILayout.Width(CSVWidth));
            DrawConfigure();
            EditorGUILayout.EndScrollView();
            GUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            base.CustomGUI();
        }

        protected override void CustomDestroy()
        {
            ClearToggleIndex();
            if (Workbook != null) Workbook.Close();
            Workbook = null;
        }

        protected override void CustomRightMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("导出EditorInfo"), false, RightMenuCallback, Output);
            menu.AddItem(new GUIContent("导出EditorInfo并生成数据文件"), false, RightMenuCallback, OutputAndSave);
            menu.ShowAsContext();
        }
        #endregion

        #region 私有函数

        #region 解析配置表
        /// <summary>
        /// 解析配置表
        /// </summary>
        private void Analysis()
        {
            string path = Config.PathConfig[ConfigKey.TablePath];
            if (string.IsNullOrEmpty(path))
            {
                UpdateNotification("初始化失败，没有找到配置表路径");
                return;
            }
            PathNames = PathTool.GetFiles(path);
            if (PathNames == null)
            {
                IsInit = false;
                UpdateNotification(string.Format("初始化失败，路径{0}没有获取到数据文件", path));
                return;
            }
            ToggleList = new bool[PathNames.Length];
            InputConfig();
            UpdateConfig();
            InitComplete();
        }
        #endregion

        #region 读取excel
        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="path"></param>
        private void ReadExcel(int index)
        {
            if (PathNames == null || PathNames.Length <= index) return;
            SelectKey = PathNames[index];
            Workbook = ExcelMgr.ReadExcel(SelectKey);
            if (Workbook == null)
            {
                if (EditorUtility.DisplayDialog("错  误", string.Format("Excel路径<{0}>没有找到指定配置表！", SelectKey), "确定"))
                    return;
                return;
            }
            SheetNum = Workbook.NumberOfSheets;
            //sheetsName = new string[mSheetNum];
            ToggleSheetList = new bool[SheetNum];
            SheetsName = ExcelMgr.GetSheetsName(Workbook);
//             for (int i = 0; i < mSheetNum; i++)
//             {
//                 sheetsName[i] = mWorkbook.GetSheetName(i);
//             }

            // 
            //             ISheet sheet = workbook.GetSheetAt(0);  //获取第一个工作表  
            //             IRow row;// = sheet.GetRow(0);            //新建当前工作表行数据  
            //             for (int i = 0; i < sheet.LastRowNum; i++)  //对工作表每一行  
            //             {
            //                 row = sheet.GetRow(i);   //row读入第i行数据  
            //                 if (row != null)
            //                 {
            //                     for (int j = 0; j < row.LastCellNum; j++)  //对工作表每一列  
            //                     {
            //                         string cellValue = row.GetCell(j).ToString(); //获取i行j列数据  
            //                         //Console.WriteLine(cellValue);
            //                     }
            //                 }
            //             }
            //Console.ReadLine();
            ExcelMgr.CloseExcel(Workbook);
        }

        /// <summary>
        /// 更新配置数据
        /// </summary>
        private void UpdateTableInfo(int index)
        {
            if (SheetsName == null || SheetsName.Count == 0) return;
            if (Workbook == null) return;
            SelectSheetName = SheetsName[index];
            ParamNames = ExcelMgr.GetSheetTitle(Workbook, SelectSheetName);
            if(ConfigDic.ContainsKey(SelectKey))
            {
                if (!ConfigDic[SelectKey].Dic.ContainsKey(SelectSheetName))
                {
                    TableInfo table = new TableInfo();
                    table.Sheet = SelectSheetName;
                    table.UpdateParams(ParamNames);
                    ConfigDic[SelectKey].Dic.Add(SelectSheetName, table);
                }
            }
        }
        #endregion

        #region 拖动区
        /// <summary>
        /// 绘制拖动栏
        /// </summary>
        private void DrawScrollView()
        {
            if (PathNames != null && PathNames.Length != 0)
            {
                for (int i = 0; i < PathNames.Length; i++)
                {
                    string path = PathNames[i];
                    if (string.IsNullOrEmpty(path)) continue;
                    if (!ConfigDic.ContainsKey(path)) continue;
                    Table table = ConfigDic[path];
                    if (table == null)
                    {
                        UpdateNotification(string.Format("没有找到路径[{0}]的配置表数据！！", path));
                        return;
                    }
                    string name = Path.GetFileName(path);
                    EditorGUILayout.BeginHorizontal();
                    if (ToggleList[i] == true) GUI.color = Color.green;
                    ToggleList[i] = GUILayout.Toggle(ToggleList[i], name, "button", GUILayout.Width(SVWidth - 40));
                    if (ToggleList[i] == true) GUI.color = Color.white;
                    table.IsOutput = GUILayout.Toggle(table.IsOutput, "", "toggle", GUILayout.Width(20));
                    EditorGUILayout.EndVertical();
                    if (ToggleList[i] == true)
                    {
                        if (SelectTableIndex != i)
                            SelectToggle(i);
                    }
                    else
                    {
                        if (SelectTableIndex == i)
                            ToggleList[i] = true;
                    }
                }
            }
        }

        /// <summary>
        /// 拖动栏选择
        /// </summary>
        private void SelectToggle(int index)
        {
            if (SelectTableIndex == index)
            {
                return;
            }
            if (SelectTableIndex != -1)
            {
                if (SelectTableIndex == index)
                    ToggleList[index] = true;
                else
                    ToggleList[SelectTableIndex] = false;
            }
            ClearToggleIndex();
            SelectTableIndex = index;
            ReadExcel(SelectTableIndex);
        }

        /// <summary>
        /// 清除数据
        /// </summary>
        private void ClearToggleIndex()
        {
            if (SheetsName != null) SheetsName.Clear();
            SheetsName = null;
            ToggleSheetList = null;
            SheetNum = 0;
            SelectSheetIndex = -1;
            SelectKey = string.Empty;
            SelectSheetName = string.Empty;
            CSVPos = Vector2.zero;
        }

        #endregion

        #region 分页区
        /// <summary>
        /// 绘制Sheet标题
        /// </summary>
        private void DrawSheetTitle()
        {
            if (SheetsName != null)
            {
                for (int i = 0; i < SheetsName.Count; i++)
                {
                    string name = Path.GetFileName(SheetsName[i]);
                    bool toggle = ToggleSheetList[i];
                    if (toggle == true) GUI.color = Color.green;
                    toggle = GUILayout.Toggle(toggle, name, "button", GUILayout.Width(SVWidth - 20));
                    ToggleSheetList[i] = toggle;
                    if (ToggleSheetList[i] == true) GUI.color = Color.white;
                    if (ToggleSheetList[i] == true)
                    {
                        if (SelectSheetIndex != i)
                            SelectSheetToggle(i);
                    }
                    else
                    {
                        if (SelectSheetIndex == i)
                            ToggleSheetList[i] = true;
                    }
                }
            }
        }

        /// <summary>
        /// 分页标签选择
        /// </summary>
        private void SelectSheetToggle(int index)
        {
            if (SelectSheetIndex == index)
            {
                return;
            }
            if (SelectSheetIndex != -1)
            {
                if (SelectSheetIndex == index)
                    ToggleSheetList[index] = true;
                else
                    ToggleSheetList[SelectSheetIndex] = false;
            }
            SelectSheetIndex = index;
            UpdateTableInfo(SelectSheetIndex);
        }
        #endregion

        #region 功能区
        private void DrawConfigure()
        {
            if (SelectSheetIndex == -1) return;
            if (ConfigDic == null || !ConfigDic.ContainsKey(SelectKey) || !ConfigDic[SelectKey].Dic.ContainsKey(SelectSheetName)) return;
            bool isOutput = ConfigDic[SelectKey].Dic[SelectSheetName].IsOutput;
            isOutput = GUILayout.Toggle(isOutput, "是否导出：");
            ConfigDic[SelectKey].Dic[SelectSheetName].IsOutput = isOutput;

            string scriptName = ConfigDic[SelectKey].Dic[SelectSheetName].ScriptPath;
            scriptName = EditorUI.DrawPrefabsObjectField(scriptName, "关联脚本", typeof(MonoScript), SuffixTool.CS);
            if(ConfigDic[SelectKey].Dic[SelectSheetName].ScriptPath != scriptName)
            {
                ConfigDic[SelectKey].Dic[SelectSheetName].ScriptPath = scriptName;
            }
            if (ParamNames != null  && ParamNames.Count > 0)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                GUILayout.Label("Client Output:");
                for (int i = 0; i < ParamNames.Count; i ++)
                {
                    DrawConfigureInfo(ParamNames[i]);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawConfigureInfo(string param)
        {
            if (ConfigDic == null || !ConfigDic.ContainsKey(SelectKey) || !ConfigDic[SelectKey].Dic.ContainsKey(SelectSheetName)) return;
            EditorGUILayout.BeginHorizontal();
            //GUILayout.Button(param, GUILayout.Width(PWidth));
            bool toggle = ConfigDic[SelectKey].Dic[SelectSheetName].Params[param].IsOutput;
            if (toggle == true) GUI.color = Color.green;
            toggle = GUILayout.Toggle(toggle, param, "button", GUILayout.Width(PWidth));
            ConfigDic[SelectKey].Dic[SelectSheetName].Params[param].IsOutput = toggle;
            if (ConfigDic[SelectKey].Dic[SelectSheetName].Params[param].IsOutput == true) GUI.color = Color.white;
            GUILayout.Space(20);
            string[] names = ConfigDic[SelectKey].Dic[SelectSheetName].PropertyNames;
            if (names != null && names.Length != 0)
            {
                int index = ConfigDic[SelectKey].Dic[SelectSheetName].Params[param].Index;
                index = EditorGUILayout.Popup(index, names, GUILayout.Width(PWidth / 2));
                if(index != ConfigDic[SelectKey].Dic[SelectSheetName].Params[param].Index)
                {
                    ConfigDic[SelectKey].Dic[SelectSheetName].Params[param].Index = index;
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        #endregion

        #region 右键菜单回调
        private void RightMenuCallback(object obj)
        {
            string key = obj.ToString();
            if(key == Output)
            {
                OutputInfo();
            }
            else if(key == OutputAndSave)
            {
                OutputInfoAndSaveTemp();
            }
        }

        private void InputConfig()
        {
            string path = Application.dataPath + PathTool.AssetsEditorResource;
            Dictionary<string, Table> dic = Config.InputConfig<string, Table>(path, Name);
            if (dic == null)
                UpdateNotification(string.Format("路径：{0}下没有读取到{1}配置数据", path, Name));
            else
                ConfigDic = dic;
        }

        private void UpdateConfig()
        {
            if (ConfigDic != null)
            {
                for (int i = 0; i < PathNames.Length; i++)
                {
                    string path = PathNames[i];
                    if (string.IsNullOrEmpty(path)) continue;
                    if(!ConfigDic.ContainsKey(path))
                    {
                        ConfigDic.Add(path, new Table());
                        ConfigDic[path].Path = path;
                    }
                }
            }
        }

        private void OutputInfo()
        {
            if (string.IsNullOrEmpty(SelectKey)) return;
            if (ConfigDic == null || ConfigDic.Count == 0) return;
            string path = Application.dataPath + PathTool.AssetsEditorResource;
            Config.OutputConfig<string,Table>(path, Name, ConfigDic);
            AssetDatabase.Refresh();
        }

        private void OutputInfoAndSaveTemp()
        {
            OutputInfo();
            InfoTool.GeneratingResourceFiles(ConfigDic);

        }
        #endregion
        #endregion
    }
}
