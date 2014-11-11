using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndUIController : MonoBehaviour
{
    public Text bestScore;
    public Text WinText;
	public Text score;

    // Use this for initialization
    void Update ()
    {
        bestScore.text = "Best: " + PlayerPrefs.GetInt ("Best Score");
    }

    public void UpdateUI(bool isWin)
    {
        if (isWin)
        {
            bestScore.gameObject.SetActive(false);
            WinText.gameObject.SetActive(true);
        }
    }

	void OnEnable()
	{
		RectTransform rect = score.GetComponent<RectTransform>();
		rect.anchoredPosition = new Vector2(0, -181);
	}
}
