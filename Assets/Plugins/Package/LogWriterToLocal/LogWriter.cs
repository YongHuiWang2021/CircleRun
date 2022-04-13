using UnityEngine;
using System;
using System.IO;
using System.Text;
using Scripts.Common.Factory;

///
/// Debug输出写入本地硬件方法
///

public class LoggerWriter : SingletonMonoBehaviour<LoggerWriter>

{

    public void InitWriter( string logName )
    {
        mLogPath = GetLogPath(logName);
        CreateLogFile( );
    }


    private string GetLogPath( string logName )

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
    private byte mSaveCount = 0;
    private void LateUpdate()
    {
        if (mSaveCount++ > 10)
        {
           SaveLog();
            mSaveCount = 0;
        }
    }
    private void OnApplicationPause(bool pauseStatus)
    {
        SaveLog();
    }

    private void OnApplicationQuit()
    {
        SaveLog();
       Cancellation();
    }
    ///
    /// 根据路径创建日记文件，并注册文件写入的函数。
    ///

    private void CreateLogFile( )
    {
        string path = mLogPath;
      
        int index = path.LastIndexOf("/");
        string directory = path.Substring(0, index);
        Debug.Log(directory);
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
           
        }
        catch (System.Exception ex)
        {

            Debug.LogError(string.Format("can't Create file = {0},\n{1}" , path , ex));

        }
        /// 注册事件，当Debug调用时，就会调用：
        Application.logMessageReceived += OnLogCallBack;
        OutputSystemInfo();
    }

    private string mLogPath = "";
    public  void Cancellation()
    {
        Application.logMessageReceived -= OnLogCallBack;
    }

    private StringBuilder mLog = new StringBuilder();
    private void OnLogCallBack( string condition , string stackTrace , LogType type )
    {
        
        string logStr = string.Empty;
        switch (type)
        {
            case LogType.Log:
            {
                logStr = string.Format("{0}:{1}：{2}\n" , type,DateTime.Now.ToString("HH:m:s tt zzz") , condition);
            }
                break;
            case LogType.Assert:
            case LogType.Warning:
            case LogType.Exception:
            case LogType.Error:
            {
                logStr = string.IsNullOrEmpty(stackTrace) ? string.Format("{0}：{1}\n{2}\n" , type , condition , Environment.StackTrace) : string.Format("{0}：{1}\n{2}" , type , condition , stackTrace);
            }
                break;
        }

        mLog.AppendLine(logStr);
        
        
        
        
        

    }

    public void SaveLog()
    {
        if(mLog.Length <=0)
            return;
        
        if (File.Exists(mLogPath))
        {
            var filestream = File.Open(mLogPath , FileMode.Append);
            using (StreamWriter sw = new StreamWriter(filestream))
            {
                sw.WriteLine(mLog.ToString());
            }
            filestream.Close();
        }
        else
        {
            Debug.LogError(string.Format("not Exists File = {0} ！" , mLogPath));
        }

        mLog.Clear();
    }

    ///
    /// 输出系统/硬件等一些信息
    ///
    private void OutputSystemInfo()
    {

        string str2 = string.Format("日志记录开始时间: {0}, 版本: {1}." , DateTime.Now.ToString() , Application.unityVersion);
        string systemInfo = SystemInfo.operatingSystem + " "
        + SystemInfo.processorType + " " + SystemInfo.processorCount + " "
        + "存储容量:" + SystemInfo.systemMemorySize + " "
        + "图形设备: " + SystemInfo.graphicsDeviceName + " 供应商: " + SystemInfo.graphicsDeviceVendor
        + " 存储容量: " + SystemInfo.graphicsMemorySize + " " + SystemInfo.graphicsDeviceVersion;
        Debug.Log(string.Format("{0}\n{1}" , str2 , systemInfo));

    }

   



}
