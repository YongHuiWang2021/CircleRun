/*using System.IO;
using CocoPlayEditor;
using Plugins.Sctipts.Message;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace _Game._Shared.Editor.ProjectBuild
{
    public class CrazyPreProcessBuild: IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }
        
        public void OnPreprocessBuild(BuildReport report)
        {
            Debug.Log("CrazyPreProcessBuild . OnPreprocessBuild");
            ProjectBuildHelper.CopyAssetsBundle(true);
            ProjectBuildHelper.  MoveEditorRes(true);
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            {
                PlayerSettings.applicationIdentifier = AndroidKeystoreHelper.AndroidBundleID;
                AndroidKeystoreHelper.SetKeyStorePram();
                PlayerSettings.applicationIdentifier = AndroidKeystoreHelper.AndroidBundleID;
            }
            else if(EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
            {
                PlayerSettings.applicationIdentifier = "com.crazystyle.playrelaxrepeat";
            }
            ProjectBuildHelper.CopyTTP();
           
         //   AssetDatabase.Refresh();
        }
        
    }
}*/