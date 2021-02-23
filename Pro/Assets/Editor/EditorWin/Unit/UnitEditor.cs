/**  
* 标    题：   Unit.cs 
* 描    述：    
* 创建时间：   2021年01月23日 14:35 
* 作    者：   by. T.Y.Divak 
* 详    细：    
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Divak.Script.Game;


namespace Divak.Script.Editor 
{
    public class UnitEditor : UIElementsEditorWin<UnitEditor>
    {

        #region 变量
        private enum FILE_MENU
        {
            [Description("创建新的")]
            Create = 0,
            [Description("读取全部")]
            Load_All,
            [Description("读取 ...")]
            Load_AS,
            [Description("保存全部")]
            Save_All,
            [Description("保存 ...")]
            Save_AS,
        }

        private string mUnitSVItemUXML = string.Empty;
        private VisualTreeAsset mUnitSVItemTreeAsset = null;

        private Unit mSelectUnit;
        #endregion

        #region 控件
        private ScrollView mUnitSV;
        private List<VisualElement> mUnitSVItems = new List<VisualElement>();
        #endregion

        #region 初始化数据

        protected override void SetSize()
        {
            this.minSize = this.maxSize = new Vector2(1600, 800);
        }

        protected override void Init()
        {
            mUXML = "UnitEditor";
            mUnitSVItemUXML = "UnitSVItem";
            mUSS = "unit";
            base.Init();
        }

        protected override void CustomInit()
        {
            base.CustomInit();
            SetWinCfg();
            DrawUI();
            InitComplete();
        }

        #endregion

        #region 绘制UI
        private void DrawUI()
        {
            DrawFileMenu();
            DrawPropertyView();
        }

        private void DrawFileMenu()
        {
            VisualElement tree = mVisualTree.CloneTree();
            ToolbarMenu menu = tree.Q<ToolbarMenu>("menu");
            if (menu == null)
            {
                MessageBox.Error("未获取到ToolbarMenu");
                return;
            }
            Type type = typeof(FILE_MENU);
            foreach (var temp in Enum.GetValues(type))
            {
                var field = type.GetField(temp.ToString());
                if (field == null)
                {
                    menu.menu.InsertAction((int)temp, "null", FileMenuAction);
                    continue;
                }
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                menu.menu.InsertAction((int)temp, attribute.Description, FileMenuAction, MenuStatusAction, temp);
            }
            menu.styleSheets.Add(mCommonStyle);
            mRoot.Add(menu);
        }

        private void DrawPropertyView()
        {
            VisualElement view = mVisualTree.CloneTree().Q<IMGUIContainer>("view");
            view.styleSheets.Add(mCommonStyle);
            mRoot.Add(view);
            mUnitSV = view.Q<ScrollView>("unit_sv");
            VisualElement propertyView = view.Q("property_view");
            ApplyModifiedProperties();
        }

        private void ApplyModifiedProperties()
        {
            #region unitList数据
            if (mUnitSV != null)
            {
                var units = UnitCfg.Instance.Units;
                mUnitSV.style.display = units.Count > 0 ? DisplayStyle.Flex : DisplayStyle.None;
                if (units.Count > mUnitSVItems.Count)
                {
                    for (int i = mUnitSVItems.Count; i < units.Count; i++)
                    {
                        if (mUnitSVItemTreeAsset == null) EditorUI.GetVisualTreeAsset(mUnitSVItemUXML, out mUnitSVItemTreeAsset);
                        if (mUnitSVItemTreeAsset != null)
                        {
                            var item = mUnitSVItemTreeAsset.CloneTree();
                            item.styleSheets.Add(mCommonStyle);
                            mUnitSV.Add(item);
                            item.RegisterCallback<MouseUpEvent, Unit>(OnClickUnitSVItem, units[i]);
                            mUnitSVItems.Add(item);
                        }
                    }
                }
                else if (units.Count < mUnitSVItems.Count)
                {
                    for (int i = units.Count; i < mUnitSVItems.Count; i++)
                    {
                        mUnitSVItems[i].style.display = DisplayStyle.None;
                    }
                }
                for (int i = 0; i < units.Count; i++)
                {
                    var ve = mUnitSVItems[i];
                    var lab = ve.Q<Label>("name");
                    if (lab == null)
                    {
                        MessageBox.Error("控件获取失败！");
                        continue;
                    }
                    lab.text = units[i].Tag;
                    ve.style.display = DisplayStyle.Flex;
                }
            }
            #endregion

        }
        #endregion

        #region 事件
        private void FileMenuAction(DropdownMenuAction action)
        {
            switch ((FILE_MENU)action.userData)
            {
                case FILE_MENU.Create:
                    UnitCfg.Instance.Add();
                    break;
                case FILE_MENU.Load_All:
                    break;
                case FILE_MENU.Load_AS:
                    break;
                case FILE_MENU.Save_All:
                    break;
                case FILE_MENU.Save_AS:
                    break;
            }
            ApplyModifiedProperties();
        }

        private DropdownMenuAction.Status MenuStatusAction(DropdownMenuAction arg)
        {
            return DropdownMenuAction.Status.Normal;
        }

        private void OnClickUnitSVItem(MouseUpEvent evt, Unit userArgs)
        {
            if (mSelectUnit != null )
            {
                if (mSelectUnit.Tag == userArgs.Tag)
                    return;
                var selectIndex = UnitCfg.Instance.FindIndex(mSelectUnit.Tag);
                var selectItem = mUnitSVItems[selectIndex];
                selectItem.style.backgroundColor = Color.clear;
            }
            mSelectUnit = userArgs;
            var curIndex = UnitCfg.Instance.FindIndex(mSelectUnit.Tag);
            var item = mUnitSVItems[curIndex];
            item.style.backgroundColor = Color.gray;
        }
        #endregion

        #region 保护函数
        /// <summary>
        /// 自定义关闭
        /// </summary>
        protected override void CustomClose()
        {
            mSelectUnit = null;
            mUnitSVItems.Clear();
            UnitCfg.Instance.Clear();
        }
        #endregion
    }
}