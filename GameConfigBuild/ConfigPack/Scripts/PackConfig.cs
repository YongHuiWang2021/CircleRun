using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using DataType;
using Game.Common.Utils;
using GamePlay.GameDataLocal.DataType;

namespace SqlitePack.Scripts
{
    public class CfgData1
         {
             public   Dictionary<int, string> idxMapping = new Dictionary<int, string>();
             public Dictionary<string, string> Type = new Dictionary<string, string>();
             public Dictionary<int, List<string>> Data = new Dictionary<int, List<string>>();
             public string SheetName;
         }
    class PackConfig
    {
        
      

        class CfgData
        {
            /// <summary>
            /// 无效的
            /// </summary>
            public bool invalid { get; set; }
            public string[] cl_List;
            public string[] type_list;
            public string[] name_list;
            public string[] msg_list;
            public bool[] arr_list;
            public Dictionary<int, int> idxMapping;
            public List<string[]> data_list;
            public HashSet<int> valididx;
            public Dictionary<string, int> fields;
            public string SheetName;
        }
        public static int PackConfigDir(string srcDir, string BinPath)
        {
            //读取配置
            DirectoryInfo di = new DirectoryInfo(srcDir);
            string filePath = di.FullName;
            var infs = di.GetFiles();
            string[] fullFileName;
            FileStream fs = null;
            IWorkbook workbook = null;
            foreach (var file in infs)
            {
                fullFileName = file.Name.Split('.');
                if (fullFileName.Length <= 0)
                {
                    continue;
                }
                string fileExt = fullFileName[fullFileName.Length - 1];
                if (fileExt == "xls" ||
                    fileExt == "xlsx" ||
                    fileExt == "xlsm"
                    )
                {
                    string fileName = file.Name;
                    CrazyLog.WriteLine("start pack :" + file.Name + " " + "time :" + DateTime.Now.ToString());
                 
                    fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);

                    if (fileName.IndexOf(".xlsx") > 0 || fileName.IndexOf(".xlsm") > 0) //2007版本及其以后
                    {
                        workbook = new XSSFWorkbook(fs);
                    }
                    else if (fileName.IndexOf(".xls") > 0)//2003版本
                    {
                        workbook = new HSSFWorkbook(fs);
                    }
                    int vSheetCount = workbook.NumberOfSheets;
                    if (vSheetCount == 0)
                    {
                        Console.WriteLine("can not read :" + fileName);
                        continue;
                    }
                    int curTableIdx = 0;
                    while (curTableIdx < vSheetCount)
                    {
                        ISheet workSheet = workbook.GetSheetAt(curTableIdx);
                        string tname = workSheet.SheetName;

                       
                        
                        ++curTableIdx;
              
                        //读取数据 基础数据转换
                        var cfgData = ParseXls(workSheet);

                      // /Users/mac/Desktop/Project/Calm/calm/GameConfigPackage/MiniGameData/ConfigPack/bin/OutData/

                        string savePath = Config.CONF_BIN .Replace("bin/","");
                        /*string NewPath =
                            (savePath).Replace("GameConfigBuild/MiniGameData/ConfigPack/OutData", "Assets");*/
                        CrazyLog.WriteLine("Path： "  + savePath);
                        FileHelp.CreateClientFile1(savePath,workSheet.SheetName+".json",System.Text.Encoding.UTF8.GetBytes(cfgData));
                    }
                }

            }

            return 0;
        }

        private static string FormatValue(string typeName,string value)
        {
            StringBuilder content = new StringBuilder();
            if (!value.IsNullOrEmpty())
            {
                for(int i=0 ;i < value.Length;++i)
                {
                    char item = value[i];
                  
                    if (item.ToString() == Temp)
                    {
                        content.Append('`');
                    }
                    else if(item.ToString() == "\r")
                    {
                        content.Append("\\r");
                    }
                    else if(item.ToString() == "\n")
                    {
                        content.Append("\\n");
                    }
                    else
                    {
                        content.Append(item);
                    }
                    
                }
            }

            switch (typeName)
            {
                case "int":
                case "double":
                    return " " + content.ToString() + "";
                case "string":
                  
                    return Temp + content.ToString() + Temp;
                default:
                    return value;
            }
        }
        static Dictionary<int, List<string>> InitProps1(ISheet worksheet )
        {
           
            Dictionary<int, List<string>> data = new Dictionary<int, List<string>>();
            data[1] = new List<string>();
            IRow row1 = worksheet.GetRow(1);
            var columns1 = row1.LastCellNum;
            for (int m = 0; m < columns1; ++m)
            {
                ICell cell = row1.GetCell(m);
                if(cell == null || cell.ToString().IsNullOrEmpty())
                    continue;
                CrazyLog.WriteLine("cell ： "  + cell.ToString());
                data[1].Add(cell.ToString());
            }
            data[0] = new List<string>();
            row1 = worksheet.GetRow(0);
            columns1 = row1.LastCellNum;
            for (int m = 0; m < columns1; ++m)
            {
                ICell cell = row1.GetCell(m);
                if(cell == null || cell.ToString().IsNullOrEmpty())
                    continue;
                CrazyLog.WriteLine("cell ： "  + cell.ToString());
                data[0].Add(cell.ToString());
            }
            int RowNum = worksheet.LastRowNum;
            for (int i = 2; i <= RowNum; ++i)
            {
              
                IRow row = worksheet.GetRow(i);
                if(row == null)
                    continue;
             
                var columns = row.LastCellNum;
                List<string> dList = new List<string>();
                for (int j= 0; j < columns; ++j)
                {
                    ICell cell = row.GetCell(j);
                    if(cell == null || cell.ToString().IsNullOrEmpty())
                        continue;
                    CrazyLog.WriteLine("cell ： "  + cell.ToString());
                    dList.Add(cell.ToString());
                }
                if(dList.Count >0)
                    data[i] = dList;
            }
            return data;
            
        }

        static bool cellPlayType(string vType)
        {
            bool ret = false; 
           
            if (vType == "TRUE" || vType == "true")
                ret = true;
            CrazyLog.WriteLine("cellPlayType : "+ vType +" ::" + ret,ConsoleColor.Blue); 
            return ret;
        }

        private static object ConvertToObject(string typeName, string value)
        {
            switch (typeName)
            {
                case "int":
                    return int.Parse(value);
                case "double":
                    return double.Parse(value);
                case "string":
                    return value;
                case "bool":
                case "BOOL":
                    return bool.Parse(value);
                default:
                    return value;
            }
        }

        private readonly static string Temp = @"""";
        static string ParseXls(ISheet sheet)
        {

            var data = InitProps1(sheet);
            CrazyLog.WriteLine("data.count : " + data.Count, ConsoleColor.Green);
            List<CrazyProtobufDataBase> ret = new List<CrazyProtobufDataBase>();
            List<string> KEY = data[1];
            List<string> type = data[0];
            StringBuilder josnBuild = new StringBuilder();
            josnBuild.Append("[\n");
            int dix = 0;
            foreach (var item in data)
            {
                if (item.Key == 1 || item.Key == 0)
                {
                  ++  dix;
                    continue;
                }

                Dictionary<string, object> jsonData = new Dictionary<string, object>();
                string jsonChildContent = "{\n";
                int max = item.Value.Count;
                for (int i = 0; i < max; ++i)
                {
                    string d = item.Value[i];
                    if( i >= KEY.Count )
                        continue;
                    string key = KEY[i];
                    string va = "";
                    jsonData[key] = ConvertToObject(type[i],d);
                }
              if (dix < data.Count - 1)
              {
                  josnBuild.AppendLine(JsonUtils.ToJsonStr(jsonData)+",");
              }
              else
              {
                  josnBuild.AppendLine(JsonUtils.ToJsonStr(jsonData));
              }
             
                ++dix;
            }
            josnBuild.Append("]\n");
       
            return josnBuild.ToString();
        }

    }
}
