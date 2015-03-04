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
    public GameObject flarePrefab;
    public Transform flareParent;
    public GameObject NewHighScoreText;

    void Awake()
    {
        WinText.text = "";
    }

    public void UpdateUI(bool isWin, GameData gameData)
    {
        score.text = gameData.Score.ToString();
        bestScore.text = gameData.HighScore.ToString();
        if (isWin)
        {
            best.SetActive(false);
            currentScore.SetActive(false);
            WinText.text = "YOU WIN !";
			loseImage.enabled = false;
            AddFlare();
            SoundManager.instance.PlayingSound("Win", 1, Camera.main.transform.position);
        }
        else
        {
            SoundManager.instance.PlayingSound ("Lose", 1, Camera.main.transform.position);
        }

        if(gameData.isHighScore)
        {
            NewHighScoreText.SetActive(true);
            AddFlare();
        }
        else
        {
            NewHighScoreText.SetActive(false);
        }
    }

    public void AddFlare()
    {
        StartCoroutine(Fire());
    }
    
    IEnumerator Fire()
    {
        while (true)
        {
            int count = Random.Range(5, 20);
            for (int i = 0; i < count; i++)
            {
                GameObject flare = Instantiate(flarePrefab) as GameObject;
                flare.transform.SetParent(flareParent);
                flare.transform.localScale = Vector3.one * Random.value / 2;
                flare.transform.localPosition = new Vector3((Random.value - 0.5f) * 640, (Random.value - 0.5f) * 1136, 0);
                yield return new WaitForSeconds(Random.value / 10);
            }           
        }
    }
}
