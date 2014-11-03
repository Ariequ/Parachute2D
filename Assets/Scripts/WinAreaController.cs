using UnityEngine;
using System.Collections;

public class WinAreaController : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        GameObject.Find ("GameController").SendMessage ("EndGame", true);
    }
}
