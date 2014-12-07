using UnityEngine;
using System.Collections;
using System;

public class Notification : MonoBehaviour
{
    void Update ()
    {
        if (NotificationServices.localNotificationCount > 0)
        {
            Debug.Log (NotificationServices.localNotifications [0].alertBody);
            NotificationServices.ClearLocalNotifications ();
        }
    }

    void OnApplicationPause (bool pauseStatus)
    {
        Debug.Log("game psuse status:" + pauseStatus);
        var notif = new LocalNotification ();
        notif.fireDate = DateTime.Now.AddSeconds (1800);
        notif.alertBody = "Why not try again!";
        NotificationServices.ScheduleLocalNotification (notif);
    }
}
