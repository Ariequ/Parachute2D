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
			for (int i = 0; i<commandTaps.Count; i++)
			{
				GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("Player")) as GameObject;
				player.name = "Player_" + i;

				player.transform.position = originalPositon;

				PlayerController controller = GameObject.Find(player.name + "/Pilot").GetComponent<PlayerController>();
				controller.showScreenEffect = false;

				SuperMeatBoyStyleRecorder recoder = player.AddComponent<SuperMeatBoyStyleRecorder>();

				Debug.Log("add SuperMeatBoyStyleRecorder");

				recoder.m_tap = commandTaps[i];

				Debug.Log("set m_tap");

				if (i == commandTaps.Count - 1)
				{
					CameraFollow follow = Camera.main.GetComponent<CameraFollow>();
					follow.followTarget = GameObject.Find(player.name + "/parachute").transform;
				}
			}
		}
	}
}