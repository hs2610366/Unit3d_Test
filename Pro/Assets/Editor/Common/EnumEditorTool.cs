/**  
* 标    题：   EnumEditorTool.cs 
* 描    述：   编辑器枚举工具
* 创建时间：   2.17-03-03 
* 作    者：   T.Y.Divak
* 详    细：    
*/


using Divak.Script.Game;
using System.IO;

#region 编辑器类型
public enum EditorType
{
    SetScriptCreator,
}
#endregion

#region 脚本类型
/// <summary>
/// 脚本类型
/// </summary>
public enum ScriptType
{
    None,
    C_Sharp,
    Javascript,
}
#endregion

#region 路径类型
/// <summary>
/// 选择路径类型
/// </summary>
public enum SelectPathType
{
    None,
    Editor,
    Script,
    Scene
}
#endregion

#region 资源类型
public enum AssetType
{
    None,
    Folder,
    Script,
    Scene,
    Material,
    Shader,
    PNG,
    JPG,
    TGA,
    DDS,
    PSD,
    PhysicMat,
    Animation,
    Animator,
    AvatarMask,
    Font,
    OTF,
    TTF,
    GUISkin,
    MB,
    FBX,
    Prefab,
    WAV,
    MP3,
    OGG,
    EXR,
    Asset,
    AB,
    CS,
    LUA,
    JS,
    ZIP,
    META,
    MANIFEST,
    DEMO
}
#endregion

public class EnumEditorTool
{
    #region 脚本分类
    /// <summary>
    /// 獲得資源類型
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static AssetType GetAssetType(string filePath)
    {
        string path = filePath.Replace(".meta", "");
        string fileExt = Path.GetExtension(path);
        fileExt = fileExt.ToLower();

        if (fileExt == SuffixTool.Scene)
            return AssetType.Scene;
        else if (fileExt == SuffixTool.Material)
            return AssetType.Material;
        else if (fileExt == SuffixTool.Shader)
            return AssetType.Shader;
        else if (fileExt == SuffixTool.PNG)
            return AssetType.PNG;
        else if (fileExt == SuffixTool.JPG)
            return AssetType.JPG;
        else if (fileExt == SuffixTool.TGA)
            return AssetType.TGA;
        else if (fileExt == SuffixTool.PSD)
            return AssetType.PSD;
        else if (fileExt == SuffixTool.DDS)
            return AssetType.DDS;
        else if (fileExt == SuffixTool.PhysicMat)
            return AssetType.PhysicMat;
        else if (fileExt == SuffixTool.Animation)
            return AssetType.Animation;
        else if (fileExt == SuffixTool.Animator)
            return AssetType.Animator;
        else if (fileExt == SuffixTool.AvatarMask)
            return AssetType.AvatarMask;
        else if (fileExt == SuffixTool.Font)
            return AssetType.Font;
        else if (fileExt == SuffixTool.OTF)
            return AssetType.OTF;
        else if (fileExt == SuffixTool.TTF)
            return AssetType.TTF;
        else if (fileExt == SuffixTool.GUISkin)
            return AssetType.GUISkin;
        else if (fileExt == SuffixTool.MB)
            return AssetType.MB;
        else if (fileExt == SuffixTool.FBX)
            return AssetType.FBX;
        else if (fileExt == SuffixTool.Prefab)
            return AssetType.Prefab;
        else if (fileExt == SuffixTool.WAV)
            return AssetType.WAV;
        else if (fileExt == SuffixTool.MP3)
            return AssetType.MP3;
        else if (fileExt == SuffixTool.OGG)
            return AssetType.OGG;
        else if (fileExt == SuffixTool.EXR)
            return AssetType.EXR;
        else if (fileExt == SuffixTool.Asset)
            return AssetType.Asset;
        else if (fileExt == SuffixTool.AB)
            return AssetType.AB;
        else if (fileExt == SuffixTool.CS)
            return AssetType.CS;
        else if (fileExt == SuffixTool.LUA)
            return AssetType.LUA;
        else if (fileExt == SuffixTool.JS)
            return AssetType.JS;
        else if (fileExt == SuffixTool.ZIP)
            return AssetType.ZIP;
        else if (fileExt == SuffixTool.Meta)
            return AssetType.META;
        else if (fileExt == SuffixTool.MANIFEST)
            return AssetType.MANIFEST;
        return AssetType.Folder;
    }

    /// <summary>
    /// 是否需要设置assetBundleName
    /// </summary>
    public static bool IsAllowSetAssetBundle(AssetType type)
    {
        switch (type)
        {
            case AssetType.Scene:
            case AssetType.Prefab:
            case AssetType.Material:
            case AssetType.Shader:
            case AssetType.PNG:
            case AssetType.JPG:
            case AssetType.TGA:
            case AssetType.DDS:
            case AssetType.PSD:
            case AssetType.Animation:
            case AssetType.Animator:
            case AssetType.AvatarMask:
            case AssetType.Font:
            case AssetType.MP3:
            case AssetType.OGG:
                return true;
        }
        return false;
    }

    /// <summary>
    /// 获得资源后缀名
    /// </summary>
    /// <returns></returns>
    public static string GetAssetSuffix(AssetType type)
    {
        switch (type)
        {
            case AssetType.Scene:
                return SuffixTool.Scene;
            case AssetType.Prefab:
                return SuffixTool.Prefab;
            case AssetType.Material:
                return SuffixTool.Material;
            case AssetType.Shader:
                return SuffixTool.Shader;
            case AssetType.PNG:
                return SuffixTool.PNG;
            case AssetType.JPG:
                return SuffixTool.JPG;
            case AssetType.TGA:
                return SuffixTool.TGA;
            case AssetType.PSD:
                return SuffixTool.PSD;
            case AssetType.Animation:
                return SuffixTool.Animation;
            case AssetType.Animator:
                return SuffixTool.Animator;
            case AssetType.AvatarMask:
                return SuffixTool.AvatarMask;
            case AssetType.Font:
                return SuffixTool.Font;
            case AssetType.MP3:
                return SuffixTool.MP3;
            case AssetType.OGG:
                return SuffixTool.OGG;
        }
        return SuffixTool.None;
    }
    #endregion
}