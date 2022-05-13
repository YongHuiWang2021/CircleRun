using System;
using NPOI.SS.Formula.Functions;

namespace SqlitePack.Scripts
{
    public class CrazyLog
    {
        public static void WriteLine(string str,ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss")+":"+str);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}