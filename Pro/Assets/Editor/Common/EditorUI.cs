/**  
* 标    题：   EditorUI.cs 
* 描    述：   脚本编辑器基类
* 创建时间：   2017年11月26日 02:58 
* 作    者：   by. T.Y.Divak
* 详    细：   创建脚本相关编辑器
*/
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Divak.Script.Editor
{
    public class BtnGuidInfo
    {
        public bool Value = false;
        public string Title = string.Empty;
        public Color UIColor = Color.gray;

        public BtnGuidInfo(string title)
        {
            Title = title;
        }

        public BtnGuidInfo(string title, Color color)
        {
            Title = title;
            UIColor = color;
        }
    }


    public class EditorUI
    {
        #region 属性
        private const float BtnSize = 14;
        private const float LabelSize = 150;
        #endregion

        #region 
        public static void SetLabelWidth(float width)
        {
            EditorGUIUtility.labelWidth = width;
        }
        #endregion

        #region UIElements
        /// <summary>
        /// 获取ui样式
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="uss">样式</param>
        /// <returns>获取状态</returns>
        public static bool GetStyleSheet(string path, out StyleSheet uss)
        {
            path = string.Format("UIElements/USS/{0}.uss", path);
            uss = EditorGUIUtility.Load(path) as StyleSheet;
            return uss != null;
        }

        /// <summary>
        /// 获取编辑器描述文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="vta">描述文件</param>
        /// <returns>获取状态</returns>
        public static bool GetVisualTreeAsset(string path, out VisualTreeAsset vta)
        {
            path = string.Format("UIElements/UXML/{0}.uxml", path);
            vta = EditorGUIUtility.Load(path) as VisualTreeAsset;
            return vta != null;
        }
        #endregion

        #region EditorPrefs
        public static string DrawTextField(string value, string lb = "",float labSize = 60, float texSize = 150)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(lb, UIStyles.Label_13_White, GUILayout.Width(labSize));
            string v = EditorGUILayout.TextField(string.Empty, value, UIStyles.LTF_14_White, GUILayout.Width(texSize));
            if (v != value) value = v;
            EditorGUILayout.EndHorizontal();
            return value;
        }
        public static int DrawIntField(int value, string lb = "", float labSize = 60, float texSize = 150, bool noStyles = true)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(lb, UIStyles.Label_13_White, GUILayout.Width(labSize));
            int v = EditorGUILayout.IntField(string.Empty, value, UIStyles.LTF_14_White, GUILayout.Width(texSize));
            if (v != value) value = v;
            EditorGUILayout.EndHorizontal();
            return value;
        }
        public static float DrawFloatField(float value, string lb = "", float labSize = 60, float texSize = 150)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(lb, UIStyles.Label_13_White, GUILayout.Width(labSize));
            float v = EditorGUILayout.FloatField(string.Empty, value, UIStyles.LTF_14_White, GUILayout.Width(texSize));
            if (v != value) value = v;
            EditorGUILayout.EndHorizontal();
            return value;
        }
        public static bool DrawBool(bool value, string lb = "", float labSize = 195, float texSize = 150)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(lb, UIStyles.Label_13_White, GUILayout.Width(labSize));
            bool v = EditorGUILayout.Toggle(string.Empty, value, GUILayout.Width(texSize));
            if (v != value) value = v;
            EditorGUILayout.EndHorizontal();
            return value;
        }


        #region 无样式 /// <summary>

        /// <summary>
        /// EditorPrefs输入文本
        /// </summary>
        public static string DrawPrefabsTextField(string value, string lb = "")
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(lb, GUILayout.Width(LabelSize));
            string v = EditorGUILayout.TextField(string.Empty, value);
            if (v != value) value = v;
            EditorGUILayout.EndHorizontal();
            return value;
        }

        /// EditorPrefs输入数字
        /// </summary>
        public static int DrawIntField(int value, string lb = "", float labW = 0, float indentation = 0.0f, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentation);
            if (labW == 0) labW = LabelSize;
            GUILayout.Label(lb, GUILayout.Width(labW));
            int v = EditorGUILayout.IntField(string.Empty, value, options);
            if (v != value) value = v;
            EditorGUILayout.EndHorizontal();
            return value;
        }

        /// EditorPrefs输入浮点数
        /// </summary>
        public static float DrawFloatField(float value, string lb = "", float labW = 0, float indentation = 0.0f, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentation);
            if (labW == 0) labW = LabelSize;
            GUILayout.Label(lb, GUILayout.Width(labW));
            float v = EditorGUILayout.FloatField(string.Empty, value, options);
            if (v != value) value = v;
            EditorGUILayout.EndHorizontal();
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string DrawPrefabTextFieldPath(string value, string lb)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(lb, GUILayout.Width(LabelSize));
            string v = EditorGUILayout.TextField(string.Empty, value);
            if (GUILayout.Button(EditorGUIUtility.IconContent("AvatarInspector/DotFrame"), GUILayout.Width(BtnSize), GUILayout.Height(BtnSize)))
            {
                string path = EditorUtility.OpenFolderPanel("请选择文件夹", string.Empty, string.Empty);
                if (!string.IsNullOrEmpty(path))
                {
                    v = path;
                }
            }
            if (v != value) value = v;
            EditorGUILayout.EndHorizontal();
            return value;
        }

        /// <summary>
        /// EditorPrefs
        /// </summary>
        public static string DrawPrefabsObjectField(string value, string lb, Type type, string suffix)
        {
            UnityEngine.Object obj = null;
            if (!string.IsNullOrEmpty(value))
            {
                Type t = Type.GetType(value);
                obj = AssetDatabase.LoadAssetAtPath(value, type);
            }
            obj = EditorGUILayout.ObjectField(lb, obj, type, false, GUILayout.Width(350));
            if (obj == null) return null;
            string name = Path.GetFileName(value);
            if (obj.name != name)
            {
                value = AssetDatabase.GetAssetPath(obj);
            }
            return value;
        }
        #endregion

        #endregion

        #region Enum
        public static Enum DrawEnum(Enum value, string lb = "", float labSize = 60, float uiSize = 150)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(lb, UIStyles.Label_13_White, GUILayout.Width(labSize));
            Enum v = EditorGUILayout.EnumPopup(string.Empty, value, UIStyles.DDB_White, GUILayout.Width(uiSize));
            if (v != value) value = v;
            EditorGUILayout.EndHorizontal();
            return value;
        }
        #endregion

        #region Toggle
        public static bool DrawToggle(bool value, string lb = "", float labSize = 60, float uiSize = 150)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(lb, UIStyles.Label_13_White, GUILayout.Width(labSize));
            bool v = EditorGUILayout.Toggle(string.Empty, value, UIStyles.BoldToggle_White, GUILayout.Width(uiSize));
            if (v != value) value = v;
            EditorGUILayout.EndHorizontal();
            return value;
        }

        public static bool DrawToggle(bool value, string lb = "", float labW = 0, float indentation = 0.0f, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(indentation);
            if (labW == 0) labW = LabelSize;
            GUILayout.Label(lb, GUILayout.Width(labW));
            bool v = EditorGUILayout.Toggle(string.Empty, value, options);
            if (v != value) value = v;
            EditorGUILayout.EndHorizontal();
            return value;
        }
        #endregion

        #region 收缩标题
        public static bool DrawHeader(string text) { return DrawHeader(text, text); }
        public static bool DrawHeader(string text, float w = 100f, float h = 20f, string style = "dragtab") { return DrawHeader(text, text, w, h, style); }
        public static bool DrawHeader(string text, string key) { return DrawHeader(text, key); }

        public static bool DrawHeader(string text, string key, float w = 100f, float h = 20f, string style = "dragtab")
        {
            bool state = EditorPrefs.GetBool(key, true);
            GUILayout.Space(3f);
            if (!state) GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);
            GUILayout.BeginHorizontal();
            GUI.changed = false;
            text = "<b><size=31>" + text + "</size></b>";
            if (state) text = "\u25BC " + text;
            else text = "\u25BA " + text;
            if (!GUILayout.Toggle(true, text, style, GUILayout.Width(w), GUILayout.MinHeight(h))) state = !state;
            if (GUI.changed) EditorPrefs.SetBool(key, state);
            GUILayout.Space(2f);
            GUILayout.EndHorizontal();
            GUI.backgroundColor = Color.white;
            GUILayout.Space(3f);
            return state;
        }
        #endregion

        #region 选择按钮
        public static bool DrawSelectBtn(string text) { return DrawSelectBtn(text, text); }
        public static bool DrawSelectBtn(string text, Color color, float w = 100f, float h = 20f) { return DrawSelectBtn(text, text, color, w, h); }
        public static bool DrawSelectBtn(string text, string key) { return DrawSelectBtn(text, key, Color.gray); }
        public static bool DrawSelectBtn(string text, string key, Color color, float w = 100f, float h = 20f)
        {
            bool state = EditorPrefs.GetBool(key, true);
            GUILayout.Space(3f);
            if (!state) GUI.backgroundColor = color;
            else GUI.backgroundColor = color * Color.gray;
            GUI.changed = false;
            if (!GUILayout.Toggle(true, text, "button", GUILayout.Width(w), GUILayout.Height(h))) state = !state;
            if (GUI.changed) EditorPrefs.SetBool(key, state);
            GUILayout.Space(2f);
            GUI.backgroundColor = Color.white;
            GUILayout.Space(3f);
            return state;
        }
        #endregion

        #region 选择按钮组
        public static T DrawSelectBtnGuid<T>(ref BtnGuidInfo[] infos, float w = 100f, float h = 20f)
        {
            if(infos != null )
            {
                Array list = Enum.GetValues(typeof(T));
                for (int i = 0; i < list.Length; i ++)
                {
                    GUILayout.Space(2f);
                    BtnGuidInfo info = infos[i];
                    if (!info.Value) GUI.backgroundColor = info.UIColor;
                    else GUI.backgroundColor = info.UIColor * Color.gray * Color.gray;
                    GUI.changed = false;
                    bool state = GUILayout.Toggle(info.Value, info.Title, "button", GUILayout.Width(w), GUILayout.Height(h));
                    if (state != info.Value)
                    {
                        for(int j = 0; j < infos.Length; j ++)
                        {
                            if (i != j) infos[j].Value = false;
                        }
                        infos[i].Value = state;
                        return (T)list.GetValue(i);
                    }
                    GUILayout.Space(2f);
                    GUI.backgroundColor = Color.white;
                }
                for(int i = 0; i < list.Length; i ++)
                {
                    BtnGuidInfo info = infos[i];
                    if(info != null && info.Value == true) return (T)list.GetValue(i); 
                }
            }
            infos[0].Value = true;
            return default(T);
        }
        #endregion

        #region BOX区域
        public static void DrawBox(string title, float w, float h, GUIStyle style = null)
        {
            if (style == null)
                style = GUIStyle.none;
            GUILayout.Box(new GUIContent(title), style, new[] { GUILayout.Width(w), GUILayout.Height(h) });
        }
        #endregion

        #region C#
        #endregion

        #region 关闭
        #endregion
    }
}
