using System;
using System.Collections.Generic;
using System.IO;
using Plugins.Scripts.XDGlobal;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using XD.XDLog;

public class ProjectBuildHelper : Editor
{
    
    //在这里找出你当前工程所有的场景文件，假设你只想把部分的scene文件打包 那么这里可以写你的条件判断 总之返回一个字符串数组。
    static string[] GetBuildScenes()
    {
        List<string> names = new List<string>();
 
        foreach(EditorBuildSettingsScene e in EditorBuildSettings.scenes)
        {
            if(e==null)
                continue;
            if(e.enabled)
                names.Add(e.path);
        }
        return names.ToArray();
    }
 
    //得到项目的名称
    public static string projectName
    {
        get
        { 
            //在这里分析shell传入的参数， 还记得上面我们说的哪个 project-$1 这个参数吗？
            //这里遍历所有参数，找到 project开头的参数， 然后把-符号 后面的字符串返回，
            //这个字符串就是 91 了。。
            foreach(string arg in System.Environment.GetCommandLineArgs()) {
                if(arg.StartsWith("project"))
                {
                    return arg.Split("-"[0])[1];
                }
            }
            return "test";
        }
    }
    //shell脚本直接调用这个静态方法
    static void BuildForIPhone()
    { 
        //打包之前先设置一下 预定义标签， 我建议大家最好 做一些  标签。 这样在代码中可以灵活的开启 或者关闭 一些代码。
        //因为 这里我是承接 上一篇文章， 我就以sharesdk做例子 ，这样方便大家学习 ，
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, "USE_SHARE");
        //这里就是构建xcode工程的核心方法了， 
        //参数1 需要打包的所有场景
        //参数2 需要打包的名子， 这里取到的就是 shell传进来的字符串 91
        //参数3 打包平台
        BuildPipeline.BuildPlayer(GetBuildScenes(), projectName, BuildTarget.iOS, BuildOptions.None);
    }

    public static void CopyTTP()
    {
        string ClickDataPath = "";
        string gooleServiesPath = "";
        
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
        {
            ClickDataPath = Application.dataPath.Replace("Assets", "playrelaxrepeat_final_google_D1");
            gooleServiesPath =   Application.dataPath.Replace("Assets", 
                "AndroidTTPGooleServices");      
        }
        else if(EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
        {
            ClickDataPath = Application.dataPath.Replace("Assets", "com.crazystyle.playrelaxrepeat_iOS_D1");
            gooleServiesPath =   Application.dataPath.Replace("Assets", 
                "IOSTTPGooleServices");
        }
        
        string StreamingAssetsPath = Application.streamingAssetsPath + "/ttp/configurations/";
        DirectoryInfo dirInf = new DirectoryInfo(ClickDataPath);
        FileInfo[] fileInfs = dirInf.GetFiles("*.json", SearchOption.AllDirectories);
        foreach (var item in fileInfs)
        {
            string fileName = item.Name;
            string filePath = StreamingAssetsPath + "/" + fileName ;
          //  Debug.Log(filePath);
           // Debug.Log(item.FullName.AddHTMLColor(Color.red));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
              //  Debug.Log("fileName".AddHTMLColor(Color.green));
            }
            File.Copy(item.FullName,filePath);
        }

        string gooleServie = gooleServiesPath + "/google-services.xml";
        string gooleServieMoveTo =  Application.dataPath+"/Plugins/Android/AndroidConfigurations/res/values/google-services.xml";
        if (File.Exists(gooleServieMoveTo))
        {
            File.Delete(gooleServieMoveTo);
        }
        File.Copy(gooleServie,gooleServieMoveTo);
    }
  
  


  public static void CopyAssetsBundle(bool inOrOut)
    {
        string ClickDataPath = "";
        string bundleSavepath = "";
        
        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
        {
            ClickDataPath = Application.dataPath.Replace("Assets", "AssetsBundle/Android");
        }
        else if(EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
        {
            ClickDataPath = Application.dataPath.Replace("Assets", "AssetsBundle/IOS");
        }
        bundleSavepath =  Application.streamingAssetsPath + "/"+MLStringDefine.MiniGameDataBundleFolder ;
        DirectoryInfo dirInf = null;
        if (inOrOut)
        {  
            dirInf= new DirectoryInfo(ClickDataPath);
        }
        else
        {
            dirInf= new DirectoryInfo(bundleSavepath);  
        }

        FileInfo[] fileInfs = dirInf.GetFiles("*"+ MLStringDefine.AssetsBundleEndName, SearchOption.AllDirectories);
        foreach (var item in fileInfs)
        {
            string fileName = item.Name;
        
            if (inOrOut)
            { 
                string filePath = bundleSavepath + "/" + fileName ;
                Debug.Log(filePath);
                Debug.Log(item.FullName.AddHTMLColor(Color.red));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                   
                }
                File.Copy(item.FullName,filePath);
            }
            else
            {
                string filePath = ClickDataPath + "/" + fileName ;
                Debug.Log(filePath);
                Debug.Log(item.FullName.AddHTMLColor(Color.red));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                 
                }
                File.Copy(item.FullName,filePath);
            }

           
        }
    }

    [PostProcessBuild(1)] 
    
    public static void AfterBuild(BuildTarget target, string pathToBuiltProject)
    {
        Debug.Log("Build Success  输出平台: " + target + "  输出路径: " + pathToBuiltProject);
 
        //打开文件或文件夹
        //    System.Diagnostics.Process.Start(pathToBuiltProject);
 
          int index = pathToBuiltProject.LastIndexOf("/");
 
        // Debug.Log("导出包体的目录 :"+pathToBuiltProject.Substring(0, index));
        MoveEditorRes(false);
        System.Diagnostics.Process.Start(pathToBuiltProject.Substring(0, index));
    }
    
    
    
    public  static void MoveEditorRes(bool IsOut)
    {
        return;
        if (IsOut)
        {
            Debug.Log("BeginMoveEditorRes Out");
            string path = Application.dataPath + "/EditorResources";
            string MovePath = path.Replace("Assets/", "aaa/");

            string partPath = Application.dataPath;
            partPath = partPath.Replace("Assets", "aaa/");
            if (!Directory.Exists(partPath))
            {
                Directory.CreateDirectory(partPath);
            }

            if (Directory.Exists(MovePath))
            {
                Directory.Delete(MovePath,true);
            }
            Debug.Log(path);
            Debug.Log(MovePath);
            FileUtil.MoveFileOrDirectory(path,MovePath);
      
    
        }
        else
        {
            Debug.Log("BeginMoveEditorRes in");
            string path = Application.dataPath + "/EditorResources";
            string MovePath = path.Replace("Assets/", "aaa/");
            if (Directory.Exists(path))
            {
                Directory.Delete(path,true);
            }
            Directory.Move(MovePath,path);
          
        }
          
      //  AssetDatabase.Refresh();
    }
    
    
    
    
    
}