using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.XCodeEditor;
using System.Xml;
#endif
using System.IO;

public static class XCodePostProcess
{
    #if UNITY_EDITOR
    [PostProcessBuild (100)]
    public static void OnPostProcessBuild (BuildTarget target, string pathToBuiltProject)
    {
        if (target != BuildTarget.iPhone) {
            Debug.LogWarning ("Target is not iPhone. XCodePostProcess will not run");
            return;
        }

        //得到xcode工程的路径
        string path = Path.GetFullPath (pathToBuiltProject);

        // Create a new project object from build target
        XCProject project = new XCProject (pathToBuiltProject);

        // Find and run through all projmods files to patch the project.
        // Please pay attention that ALL projmods files in your project folder will be excuted!
        //在这里面把frameworks添加在你的xcode工程里面
        string[] files = Directory.GetFiles (Application.dataPath, "*.projmods", SearchOption.AllDirectories);
        foreach (string file in files) {
            project.ApplyMod (file);
        }

        project.AddLibrarySearchPaths("$(SRCROOT)/Libraries");

        // 编辑plist 文件
        EditorPlist(path);

        //编辑代码文件
        EditorCode(path);

        // Finally save the xcode project
        project.Save ();

    }

    private static void EditorPlist(string filePath)
    {
     
        XCPlist list =new XCPlist(filePath);

        string PlistAdd = @"  
            <key>CFBundleURLTypes</key>
            <array>
            <dict>
            <key>CFBundleURLSchemes</key>
            <array>
            <string>wx6dc874911aca6ad5</string>
            </array>
            </dict>
            </array>";
        
        //在plist里面增加一行
        list.AddKey(PlistAdd);
        //保存
        list.Save();

    }

    private static void EditorCode(string filePath)
    {
		//读取UnityAppController.mm文件
        XClass UnityAppController = new XClass(filePath + "/Classes/UnityAppController.mm");

        //在指定代码后面增加一行代码
        UnityAppController.WriteBelow("#include \"PluginBase/AppDelegateListener.h\"","#import \"UMSocial.h\"");

        //在指定代码后面增加一行
        UnityAppController.WriteBelow("AppController_SendNotificationWithArg(kUnityOnOpenURL, notifData);","return [UMSocialSnsService handleOpenURL:url];");


    }

    #endif
}
