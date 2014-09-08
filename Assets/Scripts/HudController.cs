using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudController : MonoBehaviour 
{
    public GameObject player;
	public int ScaleFactor = 7;

    private Text score;
    private Vector3 orinalPosition;
    private int currentScore;

	// Use this for initialization
	void Start () 
    {
        orinalPosition = player.transform.position;
        score = GetComponent<Text> ();
        currentScore = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (currentScore != (int)(orinalPosition.y - player.transform.position.y) / ScaleFactor)
        {
            currentScore = (int)(orinalPosition.y - player.transform.position.y) / ScaleFactor;
            score.text = "Score: " + currentScore;
        }
	}
}
