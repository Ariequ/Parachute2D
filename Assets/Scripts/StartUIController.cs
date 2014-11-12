using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartUIController : MonoBehaviour
{
	public GameObject player;
	private float ScaleFactor = 4.81f;
	private Text score;
	private GameObject title1;
    private GameObject title2;
	private Vector3 orinalPosition;
	private int currentScore;
    private bool gameStarted;

	// Use this for initialization
	void Start ()
	{
		Debug.Log ("start");
		orinalPosition = player.transform.position;
		currentScore = 0;
        gameStarted = false;
		score = GameObject.Find ("Score").GetComponent<Text> ();
		title1 = GameObject.Find ("Title1");
		title2 = GameObject.Find ("Title2");

		score.text = "";

//		GameController.gameStart += OnGameStart;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (currentScore != (int)((orinalPosition.y - player.transform.position.y) / ScaleFactor) && gameStarted) {
			currentScore = (int)((orinalPosition.y - player.transform.position.y) / ScaleFactor);
			score.text = "" + currentScore;

			if (currentScore > PlayerPrefs.GetInt ("Best Score")) {
				PlayerPrefs.SetInt ("Best Score", currentScore);
			}
		}
	}

	public void OnGameStart ()
	{
		Debug.Log ("OnGameStart");
        gameStarted = true;

		if (title1 != null) {
            title1.SetActive(false);
		}

		if (title2 != null) {
            title2.SetActive(false);
		}

		score.text = "0";

//		if (score != null) {
//			score.enabled = true;
//		}
	}
}
