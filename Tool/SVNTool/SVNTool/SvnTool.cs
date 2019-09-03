using System;
using System.Collections.ObjectModel;
using SharpSvn;

namespace SVNTool
{
    class SvnTool
    {

        // 更新SVN
        public static void UpdateSvn(string path)
        {
            SvnClient client = new SvnClient();
            client.Update(path);
        }

        // 清理SVN
        public static void CleanSvn(string path)
        {
            SvnClient client = new SvnClient();
            client.CleanUp(path);
        }

        //提交SVN
        public static void CommitSvn(string path)
        {
            SvnClient client = new SvnClient();
            SvnCommitArgs comArgs = new SvnCommitArgs();
            comArgs.LogMessage = "这里输入你的提交信息";
            client.Commit(path);
        }

        //处理有问题的文件
        public static void QuestionFile(string path)
        {
            SvnClient client = new SvnClient();
            SvnStatusArgs sa = new SvnStatusArgs();
            Collection<SvnStatusEventArgs> status;
            client.GetStatus(path, sa, out status);
            foreach (var item in status)
            {
                string fPath = item.FullPath;
                if (item.LocalContentStatus != item.RemoteContentStatus)
                {
                    //被修改了的文件
                }
                if (!item.Versioned)
                {
                    //新增文件
                    client.Add(fPath);
                }
                else if (item.Conflicted)
                {
                    //将冲突的文件用工作文件解决冲突
                    client.Resolve(fPath, SvnAccept.Working);
                }
                else if (item.IsRemoteUpdated)
                {
                    //更新来自远程的新文件
                    client.Update(fPath);
                }
                else if (item.LocalContentStatus == SvnStatus.Missing)
                {
                    //删除丢失的文件
                    client.Delete(fPath);
                }
            }
        }

        //获取SVN上最新150条的提交日志信息
        public static void GetSvnLog(string path, out Collection<SvnLogEventArgs> status)
        {
            SvnClient client = new SvnClient();
            SvnLogArgs logArgs = new SvnLogArgs();
            logArgs.RetrieveAllProperties = false; //不检索所有属性
            client.GetLog(path, logArgs, out status);
        }
    }
}
