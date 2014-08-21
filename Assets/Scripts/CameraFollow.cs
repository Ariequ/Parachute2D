using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	private Transform player;		// Reference to the player's transform.
    public float offsetY;

	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Parachute").transform;
	}

	void Update ()
	{
        transform.position = new Vector3(transform.position.x, player.position.y + offsetY, transform.position.z);
	}

}