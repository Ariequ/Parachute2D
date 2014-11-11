using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndUIController : MonoBehaviour
{
    public Text bestScore;
    public Text WinText;
	public Text score;
	public Image loseImage;

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
            WinText.gameObject.SetActive(true);
			loseImage.enabled = false;
        }
    }
//
//	void OnEnable()
//	{
//		RectTransform rect = score.GetComponent<RectTransform>();
//		rect.anchoredPosition = new Vector2(0, -181);
//	}
}
