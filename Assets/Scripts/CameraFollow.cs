﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	private Transform player;		// Reference to the player's transform.
	public float offsetY;
	private float startTime = 0;

	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag ("Parachute").transform;
//		GameController.gameStart += OnGameStart;
		startTime = float.MaxValue;
	}

	void Update ()
	{
		if (startTime < float.MaxValue) {
			Vector3 target = transform.position;
			target.y = Mathf.Lerp (player.position.y, player.position.y + offsetY, Time.time - startTime);
			transform.position = target;// new Vector3(transform.position.x, player.position.y + offsetY, transform.position.z);
		}
        else
        {
            transform.Translate(Vector3.down*Time.deltaTime * 3);
        }
	}

	public void OnGameStart ()
	{
		offsetY = -3.5f;
		startTime = Time.time;
	}

}