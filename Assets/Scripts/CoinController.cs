using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour
{
    Animator animator;

    // Use this for initialization
    void Start ()
    {
        animator = gameObject.GetComponent<Animator> ();
        animator.speed = Random.value;
    }
    
    void OnTriggerEnter2D (Collider2D collision)
    {
        animator.speed = 1;
        animator.SetBool ("MeetPilot", true);
    }

    public void OnAnimationEnd()
    {
        Destroy (gameObject);
    }
}
