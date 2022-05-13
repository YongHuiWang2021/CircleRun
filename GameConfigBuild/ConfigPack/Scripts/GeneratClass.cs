using System;
using System.Diagnostics;
using System.IO;
using SqlitePack.Scripts;

namespace ExcelToProtobuf
{
    // .proto文件转class
    public class GeneratClass
    {
        public static bool CallProtoc(string path)
        {
        
            string dir = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);
            CrazyLog.WriteLine(dir +" ::" + fileName, ConsoleColor.DarkMagenta);
            System.Diagnostics.Process.Start(dir,dir +"/"+fileName);
            /*Process proc = new Process();
         
            proc.StartInfo.WorkingDirectory = dir;
            proc.StartInfo.FileName =fileName;
            proc.StartInfo.Arguments = dir +"/"+fileName;
            proc.Start();
            proc.WaitForExit();*/
            return true;
        }
    }
}
