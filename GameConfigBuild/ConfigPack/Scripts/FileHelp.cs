using System.IO;
using System.Text;

namespace SqlitePack.Scripts
{
    public class FileHelp
    {
        public static void CreateConfigFile(string msgName,string outFilePath)
        {
            string content = "message msg_config {\n\trepeated msg_data datas = 1;\n}\n";
            content = content.Replace("msg", msgName);
            using (FileStream fs = new FileStream(outFilePath, FileMode.Append, FileAccess.Write))
            {
                byte[] datas = Encoding.Default.GetBytes(content);
                fs.Write(datas, 0, datas.Length);
            }
        }
        private static string CSFile = @"
using System.Collections.Generic;
using System.Collections;
namespace CrazyData.Proto
{{
  
    [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=typeof(CLASSNAME).ToString())]
    public class CLASSNAME
    {{
        #PRM
    }}        
}}";
        
        public static void CreateClientFile1(string outPathDir,string FileName,byte[] buffs)
        {
       
              string codeFile =outPathDir+"/"+ FileName ;
              if (!Directory.Exists(outPathDir))
              {
                  Directory.CreateDirectory(outPathDir);
              }

              if (File.Exists(codeFile))
              {
                  File.Delete(codeFile);
              }

              File.WriteAllBytes(codeFile,buffs);
        }

        public static void CreateClientFile(string outPathDir,string protoFileName)
        {
            if (!Directory.Exists(outPathDir))
            {
                Directory.CreateDirectory(outPathDir);
            }

            string outFilePath = Path.Combine(outPathDir, protoFileName);
            using (FileStream fileStream = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                string preContent = "syntax = \"proto3\"; \noption optimize_for = LITE_RUNTIME;\n\n";
                byte[] datas = Encoding.Default.GetBytes(preContent);
                fileStream.Write(datas, 0, datas.Length);
            }
        }
        public static void CreateFile(string content,string outFilePath)
        {
            if (outFilePath .Length>0 && content.Length > 0)
            {
                using (FileStream fs = new FileStream(outFilePath, FileMode.Append, FileAccess.Write))
                {
                    byte[] datas = Encoding.Default.GetBytes(content);
                    fs.Write(datas, 0, datas.Length);
                }
            }
        }
    }
}