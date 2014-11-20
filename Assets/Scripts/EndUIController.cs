using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndUIController : MonoBehaviour
{
    public Text bestScore;
    public Text WinText;
	public Text score;
	public Image loseImage;

    void Awake()
    {
        WinText.text = "";
    }

    // Use this for initialization
    void Update ()
    {
        bestScore.text = "Best: " + PlayerPrefs.GetInt ("Best Score");
    }

    public void UpdateUI(bool isWin)
    {
		RectTransform rect = score.GetComponent<RectTransform>();
		rect.anchoredPosition = new Vector2(0, -181);
        if (isWin)
        {
            bestScore.gameObject.SetActive(false);
            WinText.text = "YOU WIN !";
			loseImage.enabled = false;
        }
        else
        {
            SoundManager.instance.PlayingSound("Lose", 1, Camera.main.transform.position);
        }
    }
//
//	void OnEnable()
//	{
//		RectTransform rect = score.GetComponent<RectTransform>();
//		rect.anchoredPosition = new Vector2(0, -181);
//	}
}
