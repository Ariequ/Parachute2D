using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartUIController : MonoBehaviour
{
	public GameObject player;
    public GameObject guide;
	private float ScaleFactor = 4.81f;
	private Text score;
	private GameObject logo;
	private Vector3 orinalPosition;
	private int currentScore;
    private bool gameStarted;
    private GameController gameController;

	// Use this for initialization
	void Start ()
	{
		orinalPosition = player.transform.position;
		currentScore = 0;
        gameStarted = false;
		score = GameObject.Find ("Score").GetComponent<Text> ();
        logo = GameObject.Find ("logo");

		score.text = "";

        guide.SetActive(false);

        gameController = GameObject.Find("GameController").GetComponent<GameController>();
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
        gameStarted = true;

        logo.SetActive(false);

		score.text = "0";

        guide.SetActive(true);

//		if (score != null) {
//			score.enabled = true;
//		}
	}

    public void HideGuide()
    {
        guide.SetActive(false);
    }

    public void onStartButtonPressed(GameObject obj)
    {
        gameController.StartGame(obj);
        audio.Play();
    }

    public void onReplayButtonPressed()
    {
        audio.Play();

        Invoke("replayGame", 0.05f);
    }

    private void replayGame()
    {
        gameController.Replay();
    }

    public int CurrentScore
    {
        get
        {
            return currentScore;
        }
    }
}
