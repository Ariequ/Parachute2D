using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public Text label;

    public void UpdateUI (GameData gameData)
    {
        label.text = gameData.DogCount.ToString();  
    }
}
