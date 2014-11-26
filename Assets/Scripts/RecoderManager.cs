using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecoderManager : MonoBehaviour
{
	public static RecoderManager instance;
	private List<CommandTap> commandTaps;
	private CommandTap currentCommandTap;
	public bool isPlayingRecord;
	public Vector3 originalPositon;
	
	// Use this for initialization
	void Start()
	{
		instance = this;
		commandTaps = new List<CommandTap>();
		DontDestroyOnLoad(gameObject);
	}

	public void StartNewRecoder()
	{
		isPlayingRecord = false;
		currentCommandTap = new CommandTap();
		commandTaps.Add(currentCommandTap);
	}

	public void AddCommand(Command command)
	{
		currentCommandTap.AddCommand(command);
	}

	public void PlayRecoder()
	{
		if(!isPlayingRecord)
		{
			isPlayingRecord = true;

            CameraFollow follow = Camera.main.GetComponent<CameraFollow>();
            follow.replayMode();

			for (int i = 0; i<commandTaps.Count; i++)
			{
				GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("Player")) as GameObject;
				player.name = "Player_" + i;
                player.tag = "Player";

				player.transform.position = originalPositon;

                GameObject pilot = GameObject.Find(player.name + "/Pilot");
                pilot.tag = "Player";

                PlayerController controller = pilot.GetComponent<PlayerController>();
				controller.showScreenEffect = false;

				SuperMeatBoyStyleRecorder recoder = player.AddComponent<SuperMeatBoyStyleRecorder>();

				recoder.m_tap = commandTaps[i];                			

				if (i == commandTaps.Count - 1)
				{					
					follow.followTarget = GameObject.Find(player.name + "/parachute").transform;
				}
			}
		}
	}
}