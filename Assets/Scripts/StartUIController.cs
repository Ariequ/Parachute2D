using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartUIController : MonoBehaviour 
{
    public GameObject player;
	private float ScaleFactor = 4.81f;

    public Text score;
    private Vector3 orinalPosition;
    private int currentScore;

	// Use this for initialization
	void Start () 
    {
        orinalPosition = player.transform.position;
        currentScore = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (currentScore != (int)((orinalPosition.y - player.transform.position.y) / ScaleFactor))
        {
            currentScore = (int)((orinalPosition.y - player.transform.position.y) / ScaleFactor);
            score.text = "" + currentScore;

            if (currentScore > PlayerPrefs.GetInt("Best Score"))
            {
                PlayerPrefs.SetInt("Best Score", currentScore);
            }
        }
	}
}
