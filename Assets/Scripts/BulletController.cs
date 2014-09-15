

using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    private Animator animator;
    // Use this for initialization
    void Start ()
    {
		//kkoklk,kmkkk.......,,,,,,,uuuuu
        animator = GetComponent<Animator> ();
    }

    public void OnAnimationEnd ()
    {
        Destroy (gameObject);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        animator.SetBool ("explode", true);

        if (collision.gameObject.tag != Tags.PILOT) 
        {
            rigidbody2D.collider2D.enabled = false;
        }
    }
}
