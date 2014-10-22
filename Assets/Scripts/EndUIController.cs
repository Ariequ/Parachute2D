using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndUIController : MonoBehaviour
{
    public Text bestScore;

    // Use this for initialization
    void Update ()
    {
        bestScore.text = "Best: " + PlayerPrefs.GetInt ("Best Score");
    }
}
