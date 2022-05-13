using SqlitePack.Scripts;
using System;
using System.Collections.Generic;
using System.IO;

namespace SqlitePack
{
    class Program
    {
        static void Main(string[] args)
        {
            CrazyLog.WriteLine("Start Build PackData");
            CrazyLog.WriteLine("ROOT_PATH :"+Config.ROOT_PATH);
            CrazyLog.WriteLine("CONFIG_PATH :"+Config.CONFIG_PATH);
            /*Config.ROOT_PATH = "/Users/mac/Desktop/Project/Calm/calm/GameConfigPackage/MiniGameData/ConfigPack/";
            Config.CONFIG_PATH =
                "/Users/mac/Desktop/Project/Calm/calm/GameConfigPackage/MiniGameData/ConfigPack/Config";*/
            if (!Directory.Exists( Config.CONFIG_PATH))
            {
                Directory.CreateDirectory( Config.CONFIG_PATH);
            
            }
            var allConfigs = Directory.GetFiles(Config.CONFIG_PATH);
            foreach (var item in allConfigs)
            {
                CrazyLog.WriteLine(item,ConsoleColor.Red);
           
            }  
            PackConfig.PackConfigDir(Config.CONFIG_PATH,Config.CONF_BIN);
         
            Console.WriteLine("over ");
        }
    }
}
