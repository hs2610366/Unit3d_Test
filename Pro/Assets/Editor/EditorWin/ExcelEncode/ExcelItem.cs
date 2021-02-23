/**  
* 标    题：   ExcelItem.cs 
* 描    述：    
* 创建时间：   2020年12月08日 11:23 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using NPOI.SS.UserModel;
using Divak.Script.Game;

namespace Divak.Script.Editor 
{
	public class ExcelItem : CustomUIElement
    {
        private const string ExcelItemUXML = "ExcelItem";

        private string mPath = string.Empty;
        private string mExcelName = string.Empty;
        public string ExcelName { get { return mExcelName; } }

        private IWorkbook mWorkbook;

        private StyleSheet mItemStyle = null;
        private Label mNameLab = null;
        private ProgressBar mProgressBar = null;
        private Label mTip = null;

        public ExcelItem(string path, VisualElement root)
        {
            mPath = path;
            mRoot = root;
        }

        #region 公开函数
        public void Init()
        {
            mExcelName = Path.GetFileNameWithoutExtension(mPath);
            if (string.IsNullOrEmpty(mExcelName)) mExcelName = string.Format("未获取excel名（{0}）", mPath);
            mNameLab = mRoot.Q<Label>("name");
            SetLabelVal(mExcelName);
            mProgressBar = mRoot.Q<ProgressBar>("ProgressBar");
            mProgressBar.value = 0.01f;
            mTip = mRoot.Q<Label>("tip");
            mTip.text = "读取中..";
        }

        public void SetLabelVal(string val)
        {
            if (mNameLab == null) return;
            mNameLab.text = val;
        }

        public void Decode(Action finish)
        {
            EditorCoroutineRunner.StartEditorCoroutine(Read(mPath, SetTipVal, finish));
        }
        #endregion

        #region 私有函数
        private void SetTipVal(string val, Color color)
        {
            if (val == null) return;
            mTip.text = val;
        }

        private IEnumerator Read(string path, Action<string, Color> setTipVal, Action finish)
        {
            if(mWorkbook == null) mWorkbook = ExcelMgr.ReadExcel(mPath);
            yield return new WaitForEndOfFrame();
            if (mWorkbook == null)
            {
                setTipVal(string.Format("Excel路径<{0}>没有找到指定配置表！", mPath), Color.red);
            }
            else
            {
                mProgressBar.value = 0.01f;
                setTipVal("读取完成", Color.green);
                yield return new WaitForEndOfFrame();
                List<object> outputList = new List<object>();
                string cfgName = null;
                string[] keys = null;
                int count = mWorkbook.NumberOfSheets;
                var pgs = 0.9f / count;
                for (var page = 0; page < count; page++)
                {
                    ISheet sheet = mWorkbook.GetSheetAt(page);
                    if (sheet != null)
                    {
                        if(keys == null)
                        {
                            IRow row = ExcelMgr.GetSheetRow(sheet, 1);
                            keys = new string[row.LastCellNum];
                            if (row != null && row.LastCellNum > 1)
                            {
                                for (int c = 0; c < row.LastCellNum; c++)
                                {
                                    var str = row.GetCell(c).ToString();
                                    if (string.IsNullOrEmpty(str))
                                    {
                                        yield return new WaitForEndOfFrame();
                                        goto ROW_CELL_IS_NULL;
                                    }
                                    keys[c] = str;
                                }
                            }
                            else
                            {
                                yield return new WaitForEndOfFrame();
                                goto DECODE_EXCEL_FAIL;
                            }
                        }
                        var rowLen = sheet.LastRowNum + 1;
                        mProgressBar.value = (pgs * page) - (rowLen * pgs) * 2;
                        if (rowLen <= 2) continue;
                        for (var r = 2; r < rowLen; r++)
                        {
                            IRow row = ExcelMgr.GetSheetRow(sheet, r);
                            if (row != null && row.LastCellNum > 1)
                            {
                                object cfg = null;
                                for (int c = 0; c < row.LastCellNum; c ++)
                                {
                                    var key = keys[c];
                                    if (c == 0)
                                    {
                                        if (!string.IsNullOrEmpty(key))
                                        {
                                            cfg = InfoTool.InstantiationScript(key);
                                            if (cfg == null)
                                            {
                                                goto GET_ASSEMBLY_FAIL;
                                            }
                                            cfgName = key;
                                            yield return new WaitForEndOfFrame();
                                        }
                                        else
                                        {
                                            yield return new WaitForEndOfFrame();
                                            goto GET_ASSEMBLY_FAIL;
                                        }
                                    }
                                    else
                                    {
                                        var cell = row.GetCell(c);
                                        if (cell == null) continue;
                                        WriteCfg(keys[c], row.GetCell(c).ToString(), ref cfg);
                                    }
                                }
                                if (cfg == null) continue;
                                outputList.Add(cfg);
                            }
                            mProgressBar.value = (pgs * page) - (rowLen * pgs) * r;
                            yield return new WaitForEndOfFrame();
                        }
                    }
                }
                mProgressBar.value = 0.9f;
                setTipVal("解码完成", Color.green);
                yield return new WaitForEndOfFrame();
                string outputPath = string.Format("{0}{1}", PathTool.DataPath, PathTool.Temp);
                Config.OutputConfig<object>(outputPath, cfgName, outputList, SuffixTool.TableInfo.ToLower());
                mProgressBar.value = 1.0f;
            }
            goto FINISH;
            ROW_CELL_IS_NULL: MessageBox.Error("配置表字段key为null"); goto RETURN;
            GET_ASSEMBLY_FAIL: MessageBox.Error("程序集没有识别到Assembly对应的脚本。"); goto RETURN;
            DECODE_EXCEL_FAIL: MessageBox.Error("解析excel失败"); goto RETURN;
            RETURN: Clear();
            FINISH:
            setTipVal("导出完成", Color.green);
            Clear();
            finish();
        }

        private void WriteCfg(string key, string val, ref object cfg)
        {
            if (cfg == null) return;
            Type t = cfg.GetType();
            PropertyInfo pro = t.GetProperty(key);
            if (pro != null)
            {
                pro.SetValue(cfg, InfoTool.TypeConversion(val, pro.PropertyType), null);
            }
        }

        private void Clear()
        {
            if (mWorkbook != null) mWorkbook.Close();
            mWorkbook = null;
        }
        #endregion

        #region 静态接口参数

        private static VisualTreeAsset mItemTreeAsset = null;

        public static void GetRoot()
        {
            EditorUI.GetVisualTreeAsset(ExcelItemUXML, out mItemTreeAsset);
        }

        public static ExcelItem CreateItem(string path)
        {
            if (mItemTreeAsset == null)
            {
                MessageBox.Error("ExcelItem描述文件null");
                return null;
            }
            var root = mItemTreeAsset.CloneTree();
            var item = new ExcelItem(path, root);
            item.Init();

            return item;
        }
        #endregion
    }
}