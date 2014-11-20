using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CommandType
{
	MoveLeft,
	MoveRight
}

public class CommandTap
{	
	public List<Command> commandList = new List<Command>();

	public void AddCommand(Command command)
	{
		Debug.Log("Add command=====");
		commandList.Add(command);
	}
}

public class Command
{
	public float happenTime;
	public CommandType type;

	public Command(float happenTime, CommandType type)
	{
		this.happenTime = happenTime;
		this.type = type;
	}
}