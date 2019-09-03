using System;
using System.Collections.Generic;
using System.Text;

namespace SVNTool
{
    class ConsoleTool
    {

        public static ChangeType Select()
        {
            Console.Write("#### 版本递增类型 1/2/3 版本/次级版本/修订版本 ####\n");
            while (true)
            {
                string cmd = Console.ReadLine();
                if (cmd == "1")
                {
                    Console.Write("---- 版本递增 ----\n\n");
                    return ChangeType.MAJOR;
                }
                else if (cmd == "2")
                {
                    Console.Write("---- 次级版本递增 ----\n\n");
                    return ChangeType.MINOR;
                }
                else if (cmd == "3")
                {
                    Console.Write("---- 修订版本递增 ----\n\n");
                    return ChangeType.PATCH;
                }
            }
        }

        public static bool Create()
        {
            Console.Write("#### 未找到版本文件，是否重新生成？Y/N ####\n");
            while (true)
            {
                string cmd = Console.ReadLine();
                if (cmd == "Y" || cmd == "y")
                {
                    Console.Write("---- YES ----\n\n");
                    return true;
                }
                else if (cmd == "N" || cmd == "n")
                {
                    Console.Write("---- NO ----\n\n");
                    return false;
                }
            }
        }

        public static void Close()
        {
            Console.ReadLine(); //等待用户按一个回车
            return; //可选，按下回车后关闭
        }
    }
}
