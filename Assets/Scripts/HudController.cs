using UnityEngine;
using System.Collections;

public class HudController : MonoBehaviour 
{
    public GameObject player;
    public UILabel scoreLabel;

    private Vector3 orinalPosition;
	// Use this for initialization
	void Start () 
    {
        orinalPosition = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        scoreLabel.text = "Score: " + (int)(orinalPosition - player.transform.position).y;
	}
}
