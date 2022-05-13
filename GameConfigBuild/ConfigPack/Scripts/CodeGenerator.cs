using NPOI.SS.UserModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlitePack.Scripts
{
    public class CodeGenerator
    {
        public static ClearConfigData MainClearData = null;
        public static ClearConfigData HotClearData = null;

        static int ReadColumnData(string type)
        {
            if (type.StartsWith("int"))
            {
                if (type.Contains("["))
                {
                    return 11;
                }
                else
                {
                    return 1;
                }
            }
            else if (type.StartsWith("string") || type.StartsWith("String"))
            {
                if (type.Contains("["))
                {
                    return 12;
                }
                else
                {
                    return 2;
                }
            }
            else if (type.StartsWith("float"))
            {
                if (type.Contains("["))
                {
                    return 13;
                }
                else
                {
                    return 3;
                }
            }
            else if (type.StartsWith("bool")|| type.StartsWith("boolean")|| type.StartsWith("Boolean"))
            {
                if (type.Contains("["))
                {
                    return 14;
                }
                else
                {
                    return 4;
                }
            }
            else if (type.StartsWith("short"))
            {
                if (type.Contains("["))
                {
                    return 15;
                }
                else
                {
                    return 5;
                }
            }
            else if (type.StartsWith("byte"))
            {
                if (type.Contains("["))
                {
                    return 16;
                }
                else
                {
                    return 6;
                }
            }
            else if (type.StartsWith("long"))
            {
                if (type.Contains("["))
                {
                    return 17;
                }
                else
                {
                    return 7;
                }
            }
            else if (type.StartsWith("double"))
            {
                if (type.Contains("["))
                {
                    return 18;
                }
                else
                {
                    return 8;
                }
            }
            return 0;
        }
        static string confTemp1;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="genFile"></param>
        public static void GenFile(SheetMsg msg, string genFile)
        {
            if (System.IO.File.Exists(genFile))
            {
                File.Delete(genFile);
            }


            if (confTemp1 == null)
            {
            //    confTemp1 = File.ReadAllText();
            }



        }

        public static SheetMsg ParseXls(ISheet sheet)
        {
            SheetMsg msg = new SheetMsg();
            var row_0 = sheet.GetRow(0);
            var columns = row_0.LastCellNum;
            int col_cnt = 0;
            IRow row_3 = sheet.GetRow(2);
            for (int i = 0; i < columns; ++i)
            {
                ICell iCell_3 = row_3.GetCell(i);
                if (iCell_3 == null)
                    continue;
                string tempname = iCell_3.ToString();
                if (string.IsNullOrEmpty(tempname))
                    continue;

                if (row_0.GetCell(i) != null)
                {
                    string cl_flag = row_0.GetCell(i).ToString();
                    if (cl_flag.Contains("c"))
                    {
                        ++col_cnt;
                    }
                }

            }


            List<string> indexTypes = new List<string>();
            List<string> indicies = new List<string>();
            List<string> indiciesUpper = new List<string>();
            msg.AttributeTypes = new string[col_cnt];
            msg.AttributeNames = new string[col_cnt];
            msg.AttributeValues = new int[col_cnt];
            msg.AttributeComments = new string[col_cnt];
            msg.SheetColumns = col_cnt;
            col_cnt = 0;
            for (int i = 0; i < columns; ++i)
            {
                var row_4 = row_0.GetCell(i);
                if (row_4 != null)
                {
                    string cl_flag = row_4.ToString().ToLower();
                    if (cl_flag.Contains("c"))
                    {
                        string tmp = sheet.GetRow(1).GetCell(i).ToString();
                        msg.AttributeTypes[col_cnt] = tmp.Trim();
                        tmp = sheet.GetRow(2).GetCell(i).ToString();
                        msg.AttributeNames[col_cnt] = tmp.Trim();
                        if (cl_flag.Contains('k'))
                        {
                            string index = msg.AttributeNames[col_cnt];
                            string upper = index[0].ToString().ToUpper() + index.Substring(1);
                            indicies.Add(index);
                            indiciesUpper.Add(upper);
                            indexTypes.Add(msg.AttributeTypes[col_cnt]);
                        }
                        tmp = sheet.GetRow(3).GetCell(i).ToString();
                        if (tmp.Contains('\n'))
                        {
                            tmp = tmp.Replace('\n', ' ');
                        }
                        msg.AttributeComments[col_cnt] = tmp.Trim();
                        msg.AttributeValues[col_cnt] = ReadColumnData(msg.AttributeTypes[col_cnt]);
                        col_cnt++;
                    }
                }
            }
            msg.Indicies = indicies.ToArray();
            msg.IndexTypes = indexTypes.ToArray();
            msg.IndiciesUpper = indiciesUpper.ToArray();
            msg.IndiciesCount = indicies.Count;
            return msg;
        }
    }
    public class ClearConfigData
    {
        public string FileName = "";
        public List<string> ClassNameList = new List<string>();
    }
    public class ClassListMsg
    {
        public List<string> cls_name_lsit;
    }
    public class SheetMsg
    {
        public string[] AttributeTypes { get; set; }
        public string[] AttributeNames { get; set; }
        public string[] AttributeNamesUpper { get; set; }
        public string TableName { get; set; }
        public int SheetColumns { get; set; }
        public string ExcelName { get; set; }
        public int [] AttributeValues { get; set; }
        public string[] AttributeComments { get; set; }
        public string [] IndexTypes { get; set; }
        public string [] Indicies { get; set; }
        public string [] IndiciesUpper { get; set; }
        public int IndiciesCount { get; set; }
    }
}
