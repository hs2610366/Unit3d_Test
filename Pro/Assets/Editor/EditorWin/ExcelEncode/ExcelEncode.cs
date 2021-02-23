using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Divak.Script.Game;

namespace Divak.Script.Editor
{
    public class ExcelEncode : UIElementsEditorWin<ExcelEncode>
    {
        public const string mUssFile = "excel";
        /// <summary>
        /// 配置表路径名字
        /// </summary>
        private string[] mExcelNames;

        private StyleSheet mUss;

        private List<ExcelItem> mItems = new List<ExcelItem>();

        #region 初始化数据

        protected override void SetSize()
        {
            this.minSize = this.maxSize = Vector2.one * 450;
        }

        protected override void Init()
        {
            mUXML = "ExcelEncode";
            mUSS = "excel";
            base.Init();
        }
        protected override void CustomInit()
        {
            base.CustomInit();

            if (!Config.IsInt)
            {
                if (Config.Init())
                {
                    if (!Analysis()) return;
                }
                else
                {
                    mIsInit = false;
                    return;
                }
            }
            else
            {
                if (!Analysis()) return;
            }
            InitComplete();
            DrawUI();
            DecodeExcel(0);
        }

        private bool Analysis()
        {
            string path = Config.PathConfig[ConfigKey.TablePath];
            if (string.IsNullOrEmpty(path))
            {
                mIsInit = false;
                UpdateNotification("初始化失败，没有找到配置表路径");
                return false;
            }
            mExcelNames = PathTool.GetFiles(path);
            if (mExcelNames == null || mExcelNames.Length == 0)
            {
                mIsInit = false;
                UpdateNotification(string.Format("初始化失败，路径{0}没有获取到数据文件", path));
                return false;
            }
            if (!EditorUI.GetStyleSheet(mUssFile, out mUss))
            {
                mIsInit = false;
                UpdateNotification(string.Format("初始化失败，路径{0}没有获取到样式文件", mUssFile));
                return false;
            }
            return true;
        }

        protected override void CustomClose()
        {
            mItems.Clear();
            base.CustomClose();
        }
        #endregion

        #region 绘制UI
        private void DrawUI()
        {
            VisualElement visualRoot = mVisualTree.CloneTree().Q<ScrollView>("excelSV");
            mRoot.Add(visualRoot);

            if (mExcelNames.Length > 0)
            {
                ExcelItem.GetRoot();
                for (int i = 0; i < mExcelNames.Length; i++)
                {
                    var path = mExcelNames[i];
                    if (string.IsNullOrEmpty(path))
                    {
                        MessageBox.Error("Excel 路径为 null. " + i.ToString());
                        continue;
                    }
                    var item = ExcelItem.CreateItem(path);
                    item.AddStyle(mUss);
                    item.SetParent(visualRoot);
                    mItems.Add(item);
                }
            }
        }

        private void DecodeExcel(int index)
        {
            if (mItems.Count == 0 || mItems.Count <= index)
            {
                if(mItems.Count <= index)
                {
                    EditorUtility.DisplayDialog("完 成", "所有配置表已经解析完成并导出为二进制文件！", "确定");
                }
                return;
            }
            ExcelItem item = mItems[index];
            item.Decode(() => { 
                int i = index + 1;
                DecodeExcel(i);
            });
        }


        #endregion

        /**
        private void ShowWin()
        {
            // Each editor window contains a root VisualElement object

            // VisualElements objects can contain other VisualElement following a tree hierarchy.
            VisualElement label = new Label("Hello World! From C#");
            mRoot.Add(label);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            //var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/EditorWin/ExcelEncode/ExcelEncode.uss");
            VisualElement labelWithStyle = new Label("Hello World! With Style");
            labelWithStyle.styleSheets.Add(mCommonStyle);
            mRoot.Add(labelWithStyle);
        }
        */
    }
}