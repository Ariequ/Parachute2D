// C# example:
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using UnityEditor.iOS.Xcode;
using System.IO;

public class Postprocessor
{
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target == BuildTarget.iOS) 
        {
            _AddDeviceCapabilities(pathToBuiltProject);
        }
    }

    static void _AddDeviceCapabilities(string pathToBuiltProject)
    {
        string infoPlistPath = Path.Combine (pathToBuiltProject, "./Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromString (File.ReadAllText(infoPlistPath));

        PlistElementDict rootDict = plist.root;
        PlistElementArray deviceCapabilityArray = rootDict.CreateArray("UIRequiredDeviceCapabilities");
        deviceCapabilityArray.AddString("armv7");
        deviceCapabilityArray.AddString("gamekit"); 

        rootDict.SetBoolean("UIRequiresFullScreen", true);

        File.WriteAllText(infoPlistPath,plist.WriteToString());
    }
}