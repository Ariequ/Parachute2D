using UnityEngine;
using System.Collections;
using System;

public class NotificationIOS : MonoBehaviour
{
    void Update ()
    {
        #if UNITY_IOS
        if (UnityEngine.iOS.NotificationServices.localNotificationCount > 0)
        {
            Debug.Log (UnityEngine.iOS.NotificationServices.localNotifications [0].alertBody);
            UnityEngine.iOS.NotificationServices.ClearLocalNotifications ();
        }
#endif
    }

    void OnApplicationPause (bool pauseStatus)
    {
        #if UNITY_IOS

        if (pauseStatus)
        {
            UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification ();
            notif.fireDate = DateTime.Now.AddSeconds (1800);
            notif.alertBody = "Why not try again!";
            UnityEngine.iOS.NotificationServices.ScheduleLocalNotification (notif);
        }
#endif
    }
}
