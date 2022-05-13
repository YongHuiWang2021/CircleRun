using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlitePack.Scripts
{
    class Config
    {
        public static string CWD = Directory.GetCurrentDirectory();
        public const string ProtoFileEndName = ".proto";
        public const string DatafileEndName = ".datas";
#if Release

#else
     //   public static string ProtobufexePath = "";
        public static string ROOT_PATH = CWD + "/";
       public static string CONFIG_PATH = Path.Combine(ROOT_PATH, "Config");
       public static string CONF_BIN= Path.Combine(ROOT_PATH, @"OutData");
       public static string CONF_Proto= Path.Combine(ROOT_PATH, @"OutProto");
       /*  public static string TMPL_PATH = Path.Combine(CWD, @"Temp/");
        public static string CONF_TMPL= Path.Combine(TMPL_PATH, @"");
        public static string CONF_BIN= Path.Combine(TMPL_PATH, @"Bin");
        public static string RES_PATH = Path.Combine(ROOT_PATH,@"Res");
        public static string RES_EDITOR_PATH = Path.Combine(RES_EDITOR_PATH,@"editor");
        public static string RES_GAME_CONF_FILE_PATH = Path.Combine();*/
#endif
        public static string SQLLITE_NAME = "ConfigData.bytes";


    }
}
