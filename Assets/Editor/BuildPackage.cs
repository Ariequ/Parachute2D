using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class BuildPackage : MonoBehaviour
{
	[MenuItem("Tools/Build")]
	public static void Build ()
	{
		Debug.Log (Application.dataPath + "/build");
		BuildPipeline.BuildPlayer(GetBuildScenes(), Application.dataPath + "/../build", BuildTarget.iOS, BuildOptions.None); 
	}

	static string[] GetBuildScenes()
	{ 
		List<string> names = new List<string>(); 
		
		foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
		{ 
			if (e == null) 
				continue; 
			
			if (e.enabled)
			{
				Debug.Log("======= EditorBuildSettingsScene =========" + e.path);
				names.Add(e.path); 
			}
		} 
		return names.ToArray(); 
	}
}
