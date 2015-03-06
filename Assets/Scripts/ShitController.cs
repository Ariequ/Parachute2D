using UnityEngine;
using System.Collections;

public class ShitController : MonoBehaviour
{
    private Animator animator;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator> ();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        animator.SetBool ("collider", true);
        GetComponent<Rigidbody2D>().GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().Sleep ();
    }

    public void OnExpodeOver ()
    {
        Destroy (gameObject);
    }
}
