/**  
* 标    题：   SuffixTool.cs 
* 描    述：   后缀工具
* 创建时间：   2017年04月13日 01:07 
* 作    者：   by. T.Y.Divak 
* 详    细：   方便添加字符串后缀
*/
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Divak.Script.Game 
{
	public class SuffixTool
    {
        public const string AssetName = ".demo";

        /// <summary>
        /// 无
        /// </summary>
        public const string None = "none";

        /// <summary>
        /// 场景资源后缀名
        /// </summary>
        public const string Scene = ".unity";

        /// <summary>
        /// Shader后缀名
        /// </summary>
        public const string Shader = ".shader";

        /// <summary>
        /// 图片png格式
        /// </summary>
        public const string PNG = ".png";

        /// <summary>
        /// 图片jpg格式
        /// </summary>
        public const string JPG = ".jpg";

        /// <summary>
        /// 图片tga格式
        /// </summary>
        public const string TGA = ".tga";

        /// <summary>
        /// 图片dds格式
        /// </summary>
        public const string DDS = ".dds";

        /// <summary>
        /// 图片psd格式
        /// </summary>
        public const string PSD = ".psd";

        /// <summary>
        /// 材质后缀名
        /// </summary>
        public const string Material = ".mat";

        /// <summary>
        /// 物理材质
        /// </summary>
        public const string PhysicMat = ".physicmaterial";

        /// <summary>
        /// 动画片段后缀名
        /// </summary>
        public const string Animation = ".anim";

        /// <summary>
        /// 动画控制器后缀名
        /// </summary>
        public const string Animator = ".controller";

        /// <summary>
        /// 动画肌肉遮罩
        /// </summary>
        public const string AvatarMask = ".mask";

        /// <summary>
        /// 字体设置后缀
        /// </summary>
        public const string Font = ".fontsetting";

        /// <summary>
        /// OTF字体
        /// </summary>
        public const string OTF = ".otf";

        /// <summary>
        /// TTF字体
        /// </summary>
        public const string TTF = ".ttf";

        /// <summary>
        /// GUI皮肤后缀
        /// </summary>
        public const string GUISkin = ".skin";

        /// <summary>
        /// 3DMax导出的模型后缀名
        /// </summary>
        public const string FBX = ".fbx";

        /// <summary>
        /// 玛雅导出的模型后缀名
        /// </summary>
        public const string MB = ".mb";

        /// <summary>
        /// 预制件后缀名
        /// </summary>
        public const string Prefab = ".prefab";

        /// <summary>
        /// 音乐WAV格式后缀名
        /// </summary>
        public const string WAV = ".wav";

        /// <summary>
        /// MP3格式后缀名
        /// </summary>
        public const string MP3 = ".mp3";

        /// <summary>
        /// Ogg格式后缀名
        /// </summary>
        public const string OGG = ".ogg";

        /// <summary>
        /// 光照贴图后缀名
        /// </summary>
        public const string EXR = ".exr";

        /// <summary>
        /// 通用资源后缀名
        /// </summary>
        public const string Asset = ".asset";

        /// <summary>
        /// AB后缀名
        /// </summary>
        public const string AB = ".dat";

        /// <summary>
        /// CSharp文件后缀名
        /// </summary>
        public const string CS = ".cs";

        /// <summary>
        /// Lua脚本文件后缀
        /// </summary>
        public const string LUA = ".lua";

        /// <summary>
        /// JavaScript文件后缀名
        /// </summary>
        public const string JS = ".js";

        /// <summary>
        /// 压缩文件后缀
        /// </summary>
        public const string ZIP = ".zip";

        /// <summary>
        /// 元数据文件后缀名
        /// </summary>
        public const string Meta = ".meta";

        /// <summary>
        /// 清单文件后缀
        /// </summary>
        public const string MANIFEST = ".manifest";
        /// <summary>
        /// 配置表文件
        /// </summary>
        public const string TableInfo = ".Temp";
        /// <summary>
        /// 
        /// </summary>
        public const string Config = ".config";
        /// <summary>
        /// 路径文件
        /// </summary>
        public const string Nav = ".Nav";

        #region 检测是否是资源
        public static bool IsAsset(string path)
        {
            string suffix = Path.GetExtension(path);
            switch (suffix)
            {
                case Scene:
                case Shader:
                case PNG:
                case JPG:
                case TGA:
                case PSD:
                case Material:
                case PhysicMat:
                case Animation:
                case Animator:
                case AvatarMask:
                case Font:
                case OTF:
                case TTF:
                case GUISkin:
                case FBX:
                case MB:
                case Prefab:
                case WAV:
                case MP3:
                case OGG:
                case EXR:
                case Asset:
                case AB:
                case CS:
                case LUA:
                case JS:
                    return true;
            }
            return false;
        }
        #endregion
    }
}