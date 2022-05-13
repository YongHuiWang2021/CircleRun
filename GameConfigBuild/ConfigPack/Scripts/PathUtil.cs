using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlitePack.Scripts
{
    class PathUtil
    {
        public static void ClearFolerByType(string dirPath, string type, string name = "")
        {
            DirectoryInfo difo = new DirectoryInfo(dirPath);
            FileInfo[] files = difo.GetFiles();
            string[] fullFileName;
            foreach (var item in files)
            {
                Console.WriteLine(item.Name);
                fullFileName = item.Name.Split('.');
                if (fullFileName.Length > 0)
                {
                    string fileExt = fullFileName[fullFileName.Length - 1];
                    if (fileExt == type)
                    {
                        if (!string.IsNullOrEmpty(name))
                        {
                            if (name != item.Name)
                                continue;
                        }
                    }
                    string filePath = Path.Combine(dirPath , item.Name);
                    //File.Delete(filePath);
                }
            }
        }
    }
}
