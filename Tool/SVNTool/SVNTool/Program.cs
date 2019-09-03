using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using SharpSvn;
using SharpSvn.Implementation;

namespace SVNTool
{
    public enum ChangeType
    {
        MAJOR,
        MINOR,
        PATCH
    }

    class Program
    {
        private static string path = "H:/Github";
        private static string versionPath = "H:/Github/trunk/Assets/Version";
        private static string deletePath = string.Empty;

        private static string[] ChangePaths = {"Resources"};
        private static string[] ScreeningSuffixs = { ".meta"};

        private static long Major = 0;
        private static long Minor = 0;
        private static long Patch = 0;

        private static List<string> Changes = new List<string>();

        static void Main(string[] args)
        {
            Console.Write("==========================\n" +
                          "##########################\n" +
                          "#### 版本资源配置生成 ####\n" +
                          "##########################\n" +
                          "==========================\n");
            Collection<SvnLogEventArgs> logs = null;
            Console.Write("#### 读取SVNlog数据 ####\n\n");
            SvnTool.GetSvnLog(path, out logs);
            if(logs != null)
            {
                long revision = GetRecord();
                if(revision == -1)
                {
                    ConsoleTool.Close();
                    return;
                }
                GetRevision(logs, revision);
                if(revision != 0)
                {
                    ChangeType select = ConsoleTool.Select();
                    if (select == ChangeType.MAJOR) Major += 1;
                    else if(select == ChangeType.MINOR) Minor += 1;
                }
                Output();
            }
            ConsoleTool.Close();
        }


        private static long GetRecord()
        {
            string[] fileNames = Directory.GetFiles(versionPath);
            if (fileNames == null || fileNames.Length !=1)
            {
                bool create = ConsoleTool.Create();
                if(create)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            deletePath = fileNames[0];
            string version = Path.GetFileNameWithoutExtension(fileNames[0]);
            string[] values = version.Split('.');
            Major = Convert.ToInt64(values[0]);
            Minor = Convert.ToInt64(values[1]);
            return Convert.ToInt64(values[2]);
        }

        private static void GetRevision(Collection<SvnLogEventArgs> logs, long revision)
        {
            Console.Write("#### 检测SVNlog数据 ####\n\n");
            int count = logs.Count;
            for(int i = 0; i < count; i ++)
            {
                SvnLogEventArgs args = logs[i];
                if (revision != 0 && args.Revision == revision)
                {
                    if(i == 0)
                    {
                        Console.Write("#### SVNRevision Num未发生改变 ####\n\n");
                        ConsoleTool.Close();
                        return;
                    }
                    break;
                }
                if (i == 0)
                {
                    Patch = args.Revision;
                }
                SvnChangeItemCollection itemCollection = args.ChangedPaths;
                foreach(SvnChangeItem item in itemCollection)
                {
                    string path = item.Path;
                    if(CheckChangePath(path) && CheckSuffix(path))
                    {
                        if (Changes.Contains(path) == true) continue;
                        Changes.Add(Path.GetFileName(path));
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            Console.Write(string.Format("#### 最新Revision Num : {0} ####\n\n", Patch));
            foreach(string path in Changes)
            {
                Console.Write(string.Format("====>更新文件：{0}\n", path));
            }
            Console.Write("########################################\n");
        }

        private static bool CheckChangePath(string path)
        {
            if(ChangePaths.Length > 0)
            {
                foreach (string key in ChangePaths)
                {
                    if (!path.Contains(key)) return false;
                }
            }
            return true;
        }

        private static bool CheckSuffix(string path)
        {
            if(ScreeningSuffixs.Length > 0)
            {
                foreach (string key in ScreeningSuffixs)
                {
                    if (key.Contains(Path.GetExtension(path))) return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private static void Output()
        {
            if(Changes.Count == 0)
            {
                ConsoleTool.Close();
                return;
            }
            string strs = string.Empty;
            foreach(string file in Changes)
            {
                strs += file + "\n";
            }
            string path = string.Format("{0}/{1}.{2}.{3}.version", versionPath, Major, Minor, Patch);
            UTF8Encoding encoding = new UTF8Encoding(true);
            File.WriteAllText(path, strs, encoding);
            if (!string.IsNullOrEmpty(deletePath)) File.Delete(deletePath);
        }
    }
}
