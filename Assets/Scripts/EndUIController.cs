using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndUIController : MonoBehaviour
{
    public GameObject best;
    public GameObject currentScore;
    public Text bestScore;
    public Text WinText;
	public Text score;
	public Image loseImage;
    public StartUIController startUIController;

    void Awake()
    {
        WinText.text = "";
    }

    // Use this for initialization
    void Update ()
    {
        bestScore.text = PlayerPrefs.GetInt ("Best Score").ToString();
    }

    public void UpdateUI(bool isWin)
    {
        score.text = startUIController.CurrentScore.ToString();

        if (isWin)
        {
            best.SetActive(false);
            currentScore.SetActive(false);
            WinText.text = "YOU WIN !";
			loseImage.enabled = false;
      
            SoundManager.instance.PlayingSound("Win", 1, Camera.main.transform.position);
        }
        else
        {
            SoundManager.instance.PlayingSound ("Lose", 1, Camera.main.transform.position);
        }

        Debug.Log("Add to GoogleAnalyticss " + startUIController.CurrentScore);
        if (GoogleAnalytics.instance)
        {
            GoogleAnalytics.instance.LogScreen("Score: " + startUIController.CurrentScore);
        }
    }
//
//	void OnEnable()
//	{
//		RectTransform rect = score.GetComponent<RectTransform>();
//		rect.anchoredPosition = new Vector2(0, -181);
//	}
}
