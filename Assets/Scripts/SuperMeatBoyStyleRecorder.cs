using UnityEngine;
using System.Collections;

public class SuperMeatBoyStyleRecorder : MonoBehaviour
{
	public CommandTap m_tap;
	float startTime;
	Command currentCommand;
	int currentCommanIndex;
	PlayerController controller;

	void Start()
	{
		startTime = Time.time;
		currentCommanIndex = 0;

		if (m_tap.commandList.Count > 0)
		{
			currentCommand = m_tap.commandList [currentCommanIndex];
		}

		controller = GameObject.Find(gameObject.name + "/Pilot").GetComponent<PlayerController>();
	}

	void Update()
	{
		if(currentCommand != null && Time.time - startTime >= currentCommand.happenTime)
		{
			controller.Do(currentCommand);
			currentCommanIndex++;

			if(currentCommanIndex < m_tap.commandList.Count)
			{
				currentCommand = m_tap.commandList [currentCommanIndex];
			}
			else
			{
				currentCommand = null;
				this.enabled = false;
			}
		}
	}
}
