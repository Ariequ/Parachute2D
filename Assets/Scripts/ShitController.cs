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
       
        Vector3 adjustPositon = transform.position;
        adjustPositon.y -= 0.2f;
        
        transform.position = adjustPositon;
        transform.parent = collision.gameObject.transform;

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        Destroy(rigidbody);
    }
}
