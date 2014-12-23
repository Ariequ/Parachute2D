using UnityEngine;
using System.Collections;

public class NotificationCenter : MonoBehaviour {

	// Use this for initialization
	void Start () {
#if UNITY_ANDROID
        AndroidJavaObject nativeObj = new AndroidJavaObject("AlarmReceiver");  
        nativeObj.CallStatic("startAlarm", new object[5]{"title","title", "msg", 10, 1});  
#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
