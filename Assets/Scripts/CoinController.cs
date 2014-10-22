using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour
{
    Animator animator;

    // Use this for initialization
    void Start ()
    {
        animator = gameObject.GetComponent<Animator> ();
    }
    
    void OnTriggerEnter2D (Collider2D collision)
    {
        animator.SetBool ("MeetPilot", true);
    }

    public void OnAnimationEnd()
    {
        Destroy (gameObject);
    }
}
