using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public Text label;
    public Text CountingDownLabel;
    public int leftSectonds;

    void Start()
    {
        CountingDownLabel.text = "";
    }

    public void UpdateUI (GameData gameData)
    {
        label.text = gameData.DogCount.ToString();  
    }

    IEnumerator ShowCountDown()
    {
        CountingDownLabel.text = leftSectonds.ToString();
        int waitFrame = 0;
        while(leftSectonds > -1)
        {
            waitFrame ++;
            if(waitFrame > 30)
            {
                CountingDownLabel.text = (leftSectonds--).ToString();
                waitFrame = 0;
            }
            yield return 1;
        }
        
        CountingDownLabel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
