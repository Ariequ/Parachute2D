using UnityEngine;
using System.Collections;
using System;

public class NotificationIOS : MonoBehaviour
{
    void Update ()
    {
        #if UNITY_IOS
        if (NotificationServices.localNotificationCount > 0)
        {
            Debug.Log (NotificationServices.localNotifications [0].alertBody);
            NotificationServices.ClearLocalNotifications ();
        }
#endif
    }

    void OnApplicationPause (bool pauseStatus)
    {
        #if UNITY_IOS

        if (pauseStatus)
        {
            var notif = new LocalNotification ();
            notif.fireDate = DateTime.Now.AddSeconds (1800);
            notif.alertBody = "Why not try again!";
            NotificationServices.ScheduleLocalNotification (notif);
        }
#endif
    }
}
