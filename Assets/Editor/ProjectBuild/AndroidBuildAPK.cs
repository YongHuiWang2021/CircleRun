using System;
using System.Collections.Generic;
using System.IO;
using Plugins.Package.LogWriterToLocal;
using UnityEditor;
using UnityEditor.Android;
using UnityEngine;

namespace _Game._Shared.Editor.ProjectBuild
{
    public class AndroidBuildAPK
    {
        //在这里找出你当前工程所有的场景文件，假设你只想把部分的scene文件打包 那么这里可以写你的条件判断 总之返回一个字符串数组。
        public  static string[] GetBuildScenes()
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

        [MenuItem("XD/BuidlPackage/AndroidGoogle")]
        public static void BuildForAndroidGoogle()
        {
            SetKeyStorePram();
            Build(true);
        }
       

        public static void Build(bool buildAppBundle = true)
        {
            EditorUserBuildSettings.buildAppBundle = buildAppBundle;
            if (EditorUserBuildSettings.activeBuildTarget!= BuildTarget.Android)
            {
                string FileName = "Android.txt";
                File.Create(Application.dataPath + "/" + FileName);
                EditorUserBuildSettings.SwitchActiveBuildTargetAsync(BuildPipeline.GetBuildTargetGroup(BuildTarget.Android),BuildTarget.Android);
            }
            else
            {
                BeginBuild();
            }
        }

        [MenuItem("XD/BuidlPackage/Android")]
        public static void BuildForAndroid()
        {
            SetKeyStorePram();
            Build(false);
        }
       

        public static void LoadClikZip()
        {
           
        }

        public   static void BeginBuild()
        {
            
            if (EditorUserBuildSettings.activeBuildTarget!= BuildTarget.Android)
                return;
     

            string APKName = "Circle_" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day +
                             "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            string path = Application.dataPath.Replace("Assets", "AndroidPackage") +"/" +APKName;
            if (EditorUserBuildSettings.buildAppBundle)
            { 
                path += ".aab";
            }  
            else
            {
                 path += ".apk";
            }

            BuildPipeline.BuildPlayer(GetBuildScenes(), path, BuildTarget.Android, BuildOptions.None);
        }

        public const string AndroidkeyaliasName = "XD";
        public const string AndroidBundleID  = "XD";
        public static void SetKeystore()
        {
            PlayerSettings.Android.keystoreName = "StoreKey/user.keystore";
            PlayerSettings.Android.keystorePass = "xd.com";
            PlayerSettings.Android.keyaliasName = AndroidkeyaliasName;//com.Rikodu.PlayRelaxRepeat
            PlayerSettings.Android.keyaliasPass = "xd.com";//GnAS4s77dyDdc3jS
        }
        [MenuItem("XD/BuidlPackage/SetKeyStorePram")]
      
        public static void SetKeyStorePram()
        {
         
            string keystorepath = Application.dataPath.Replace("Assets", "");
              Debug.Log( keystorepath.AddHTMLColor(Color.green));
            PlayerSettings.Android.useCustomKeystore = true;
                
            AndroidExternalToolsSettings.keystoresDedicatedLocation = keystorepath;
            SetKeystore();
            AssetDatabase.Refresh();
        }

    }
}