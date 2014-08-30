using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudController : MonoBehaviour 
{
    public GameObject player;
    private Text score;
    private Vector3 orinalPosition;
	// Use this for initialization
	void Start () 
    {
        orinalPosition = player.transform.position;
        score = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () 
    {
        score.text = "Score: " +  (int)(orinalPosition.y - player.transform.position.y) / 2;
	}
}
