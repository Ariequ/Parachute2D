using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.LoadLevel(1);
	}
}
