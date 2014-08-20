using UnityEngine;
using System.Collections;

public class HelicopterController : MonoBehaviour {

    private Transform player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Parachute").transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y - player.position.y > 11.36/3)
        {
            Vector3 position = transform.position;
            position.y -= 15f;
            transform.position = position;
        }
      
	}
}
