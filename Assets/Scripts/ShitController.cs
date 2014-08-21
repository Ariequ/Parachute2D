using UnityEngine;
using System.Collections;

public class ShitController : MonoBehaviour
{
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("shit OnCollisionEnter2D");
        animator.SetBool("collider",true);
       
        rigidbody2D.isKinematic = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("shit OnTriggerEnter2D");
        animator.SetBool("collider",true);
        rigidbody2D.isKinematic = true;
    }

    public void OnExpodeOver()
    {
        Destroy(gameObject);
    }
}
