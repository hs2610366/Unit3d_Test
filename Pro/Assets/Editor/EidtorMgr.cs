/**  
* 标    题：   EditorWinBase.cs 
* 描    述：   脚本编辑器基类
* 创建时间：   2017年11月19日 02:58 
* 作    者：   by. T.Y.Divak
* 详    细：   创建脚本相关编辑器
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Divak.Script.Editor;

namespace Divak.Script.Editor
{

    public class EidtorMgr
    {
        #region 数据配置
        [MenuItem("DivakTool/设置/路径引用", false)]
        public static void InfoEditorWin()
        {
            PathReferencesWin.ShowWin();
        }
        #endregion

        #region 配置表脚本关联
        [MenuItem("DivakTool/配置表/配置表脚本关联", false)]
        static void TabelEditorTool()
        {
            TableEditor.ShowWin();
        }
        [MenuItem("DivakTool/配置表/生成配置文件", false)]
        [MenuItem("Assets/DivakTool/生成配置文件", false, 98)]
        public static void ExcelEncodeTool()
        {
            ExcelEncode.ShowWin();
        }
        #endregion

        #region 设置AssetBundleName Tool
        /// <summary>
        /// 设置设置AssetBundleName Tool
        /// </summary>
        [MenuItem("DivakTool/資源/设置资源名", false)]
        [MenuItem("Assets/DivakTool/设置资源名", false, 99)]
        public static void SetAssetsBundleNameTool()
        {
            SetAssetsBundleName.SetAssetBundleName();
        }
        #endregion

        #region 检测同名资源 Tool
        /// <summary>
        /// 设置AssetBundleName Tool
        /// </summary>
        [MenuItem("DivakTool/資源/检测同名资源", false)]
        public static void CheckAssetsDuplicationNnameTool()
        {
            CheckAssetsDuplicationNname.Check();
        }
        #endregion

        #region 导出AssetBundleName Tool
        /// <summary>
        /// 导出AssetBundleName
        /// </summary>
        [MenuItem("DivakTool/資源/导出AssetBundleName", false)]
        public static void OutputAssetsBundle()
        {
            PackedFile.SetPackedFile();
        }
        #endregion

        #region 角色
        [MenuItem("DivakTool/角色/角色编辑", false)]
        [MenuItem("Assets/角色/角色编辑", false, 98)]
        public static void UnitEditorTool()
        {
            UnitEditor.ShowWin();
        }
        #endregion

        #region 动作
        /// <summary>
        /// 导出AssetBundleName
        /// </summary>
        [MenuItem("DivakTool/模型/动作", false)]
        public static void ModelAnim()
        {
            UnitListView.ShowWin();
        }
        #endregion

        #region 动作编辑器
        /// <summary>
        /// 技能编辑器
        /// </summary>
        [MenuItem("DivakTool/模型/动作编辑器", false)]
        public static void Roleaction()
        {
            RoleactionEditor.ShowWin();
        }
        #endregion

        #region 导场景相关
        /// <summary>
        /// 导出AssetBundleName
        /// </summary>
        [MenuItem("DivakTool/场景/寻路路径", false)]
        public static void NavMeshPath()
        {
            NavmeshWin.ShowWin();
            //MapNavMeshPath.ShowWindow();
        }
        #endregion

        #region 树
        /// <summary>
        /// 行为树
        /// </summary>
        [MenuItem("DivakTool/树/行为树", false)]
        public static void BehaviorTree()
        {
            BehaviorTreeWin.ShowWin();
        }

        [MenuItem("DivakTool/树/流程树", false)]
        [MenuItem("Assets/DivakTool/流程树", false, 100)]
        public static void ProcessTreeWin()
        {
            ProcessTree.ShowWin();
        }
        #endregion

        #region 编辑器相关
        /// <summary>
        /// 关闭进度条
        /// </summary>
        [MenuItem("DivakTool/编辑器/关闭进度", false)]
        public static void ClearProgressBar()
        {
            EditorUtility.ClearProgressBar();
        }
        #endregion

        #region 测试
        /// <summary>
        /// 关闭进度条
        /// </summary>
        [MenuItem("DivakTool/测试/创建角色", false)]
        public static void CreateUnit()
        {
            if(Divak.Script.Game.Config.Init())
            {
                Divak.Script.Game.TempMgr.Init();
                Divak.Script.Game.UnitPlayer player = Divak.Script.Game.UnitMgr.Instance.CreatePlayer(101, string.Empty, Vector3.zero);
                if(player == null)
                {
                    MessageBox.Error("没有加载成功");
                    return;
                }
                Selection.activeGameObject = player.Oneself;
                SceneView view = SceneView.lastActiveSceneView;
                view.pivot = Vector3.zero;
                view.size = 20f;
                view.orthographic = true;
                view.MoveToView(player.Trans);
            }
        }
        #endregion

    }
}
