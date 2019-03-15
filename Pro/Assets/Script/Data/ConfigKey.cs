/**  
* 标    题：   EditorPrefsKey.cs 
* 描    述：   EditorPrefs标识Key
* 创建时间：   2017年11月19日 02:58 
* 作    者：   by. T.Y.Divak
* 详    细：   
*/

namespace Divak.Script.Game
{
    public class ConfigKey
    {
        /// <summary>
        /// 创建者
        /// </summary>
        public const string CreateName = "#CREATOR#";
        /// <summary>
        /// 安装路径
        /// </summary>
        public const string InstallPath = "#InstallPath#";
        /// <summary>
        /// 资源路径
        /// </summary>
        public const string AssetsPath = "#AssetsPath#";
        /// <summary>
        /// 配置表路径
        /// </summary>
        public const string TablePath = "#TablePath#";
        /// <summary>
        /// 工程dll路径
        /// </summary>
        public const string CSharpDllPath = "#BuildDllPath#";
    }
}
