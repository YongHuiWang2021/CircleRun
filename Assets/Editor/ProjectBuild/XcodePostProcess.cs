
using UnityEngine;
 
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
//using  UnityEditor.iOS.Xcode;
using System.Xml;
#endif
using System.IO;
using _Game._Shared.Editor.ProjectBuild;
using UnityEngine.TestTools;

public static class XCodePostProcess
{
	[MenuItem("XD/BuidlPackage/IOS")]
	public static void BuildForIOS()
	{
		if (EditorUserBuildSettings.activeBuildTarget!= BuildTarget.iOS)
		{
			
			string FileName = "IOS.txt";
			File.Create(Application.dataPath + "/" + FileName);
			bool ret =  EditorUserBuildSettings.SwitchActiveBuildTargetAsync(BuildPipeline.GetBuildTargetGroup(BuildTarget.iOS),BuildTarget.iOS);
               
			
		}
		else
		{
			BeginBuild();
		}
	
	}

	public static void BeginBuild()
	{
	
		if (EditorUserBuildSettings.activeBuildTarget!= BuildTarget.iOS)
			return;
	//	ProjectBuildHelper. MoveEditorRes(true);
		
        
		//  BuildPipeline.BuildPlayer(GetBuildScenes(), path, BuildTarget.iOS, BuildOptions.None);
		XCodePostProcess.BuildIOS(	AndroidBuildAPK.GetBuildScenes());
	}

#if UNITY_EDITOR
 
	public static string projectName
	{
		get
		{
			foreach(string arg in System.Environment.GetCommandLineArgs()) {
				if(arg.StartsWith("project"))
				{
					return arg.Split("-"[0])[1];
				}
			}
			return "test";
		}
	}

	public static void BuildIOS(string[] scenes)
	{
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, "USE_SHARE");
		string path = Application.dataPath.Replace("LineAndCircle/Assets", "IOSProject");
		Debug.Log("IOS Project Path :"+path);
		//参数1 需要打包的所有场景
		//参数2 需要打包的名子， 这里取到的就是 shell传进来的字符串 91
		//参数3 打包平台
		BuildPipeline.BuildPlayer(scenes, path, BuildTarget.iOS, BuildOptions.None);
	}

	/*
	[PostProcessBuild(700)]
	public static void OnPostProcessBeginBuild(
		BuildTarget target, string pathToBuiltProject)
	{
	  
		if (target != BuildTarget.iOS)
		{
			return;
		}
			
		var projPath = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
		var proj = new PBXProject();
		proj.ReadFromFile(projPath);
		var targetGUID = proj.TargetGuidByName("Unity-iPhone");
		proj.SetBuildProperty(targetGUID, "ENABLE_BITCODE", "NO");
		proj.AddBuildProperty(targetGUID, "OTHER_LDFLAGS", "-ObjC");
		proj.AddFrameworkToProject(targetGUID, "libresolv.9.tbd", false);
		proj.AddFrameworkToProject(targetGUID, "WebKit.framework", false);
	        
		proj.WriteToFile(projPath); 
	}*/

  
#endif
}