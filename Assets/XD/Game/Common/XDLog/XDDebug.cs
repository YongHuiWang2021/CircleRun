using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Scripts.Common.Factory;
using UnityEngine;

namespace XD.XDLog
{
    public class XDDebug
    {
        static string LogPath {
            get
            {
             
                return mLogPath;
            }
        }
    
        public static void Init()
        {
            XDDebugListener.Instance.InitData(() => { });
            mLogPath = GetLogPath(DateTime.Now.Ticks.ToString());
            CreateLogFile();
        }
    
    
        private static string GetLogPath( string logName )
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                {
                   return string.Format("{0}/{1}.txt" , Application.persistentDataPath , logName);
                }
                case RuntimePlatform.IPhonePlayer:
                {
                    return string.Format("{0}/{1}.txt" , Application.persistentDataPath , logName);
                }
                case RuntimePlatform.WindowsPlayer:
                {
                    return string.Format("{0}/../{1}.txt" , Application.dataPath , logName);
                }
                case RuntimePlatform.WindowsEditor:
                {
                    return string.Format("{0}/../{1}.txt" , Application.dataPath , logName);
                }
                case RuntimePlatform.OSXEditor:
                {
                    return string.Format("{0}/../{1}.txt" , Application.dataPath , logName);
                }
            }
    
            return "";
    
        }
    
        ///
        /// 根据路径创建日记文件，并注册文件写入的函数。
        ///
    
        private static void CreateLogFile( )
        {
            string path = LogPath;
            int index = path.LastIndexOf("/");
            string directory = path.Substring(0, index);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
    
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            try
            {
                FileStream fs = File.Create(path);
                fs.Close();
                if (File.Exists(path))
                {
                    Debug.Log(string.Format("Create file = {0}" , path));
                }
              
                OutputSystemInfo();
            }
            catch (System.Exception ex)
            {
    
                Debug.LogError(string.Format("can't Create file = {0},\n{1}" , path , ex));
    
            }
    
        }
    
        private static string mLogPath = "";
       
        private static StringBuilder mLog = new StringBuilder("LOG:\n");
       
    
        public static void SaveLog()
        {
         
            if(mLog.Length <=0)
                return;
         
            if (File.Exists(LogPath))
            {
                var filestream = File.Open(LogPath , FileMode.Append);
                using (StreamWriter sw = new StreamWriter(filestream))
                {
                    sw.WriteLine(mLog.ToString());
                }
                filestream.Close();
            }
            else
            {
                Debug.LogError(string.Format("not Exists File = {0} ！" , LogPath));
            }
            mLog.Clear();
        }
    
        ///
        /// 输出系统/硬件等一些信息
        ///
        private static void OutputSystemInfo()
        {
    
            string str2 = string.Format("日志记录开始时间: {0}, 版本: {1}." , DateTime.Now.ToString() , Application.unityVersion);
            string systemInfo = SystemInfo.operatingSystem + " "
            + SystemInfo.processorType + " " + SystemInfo.processorCount + " "
            + "存储容量:" + SystemInfo.systemMemorySize + " "
            + "图形设备: " + SystemInfo.graphicsDeviceName + " 供应商: " + SystemInfo.graphicsDeviceVendor
            + " 存储容量: " + SystemInfo.graphicsMemorySize + " " + SystemInfo.graphicsDeviceVersion;
            Debug.Log(string.Format("{0}\n{1}" , str2 , systemInfo));
        }
    
        public static StringBuilder LogData
        {
            get { return mLog; }
        }
    
        public static void Log(string msg)
        {
            Debug.Log(msg);
            mLog.AppendLine(msg);
            if (mLog.MaxCapacity <= mLog.Length)
            {
                SaveLog();
                mLog.Clear();
            }
        }
    }
}


