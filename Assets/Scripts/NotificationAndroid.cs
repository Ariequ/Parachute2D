using UnityEngine;
using System.Collections;

public class NotificationAndroid : MonoBehaviour
{

    // Use this for initialization
    void Start ()
    {
#if UNITY_ANDROID
        AndroidJavaObject nativeObj = new AndroidJavaObject("com.ariequ.alarm.AlarmReceiver");  

        if (nativeObj != null)
        {
            Debug.Log("get AlarmReceover");
        }

        nativeObj.CallStatic("startAlarm", new object[5]{"title","title", "msg", 10, 1});  
#endif
    }
}
