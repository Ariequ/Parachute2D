using UnityEngine;
using System.Collections;

public class ParachuteController : MonoBehaviour
{
    private const string IDLE_TYPE = "idleType";
    private Animator animator;
    private int currentIdleType;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();  
        currentIdleType = 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("parachute OnCollisionEnter2D");
        GameObject collistionObject = collision.gameObject;
        if (collistionObject.tag == "Shit")
        {
            collistionObject.transform.parent = transform;

            collistionObject.GetComponent<Animator>().SetBool("collider", true);

            Rigidbody2D rigidbody = collistionObject.GetComponent<Rigidbody2D>();
            Destroy(rigidbody);

            Vector3 adjustPositon = collistionObject.transform.position;
            adjustPositon.y -= 0.2f;
           
            collistionObject.transform.position = adjustPositon;

            Physics2D.gravity += new Vector2(0, -5);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("parachute OnTriggerEnter2D");

        if (other.tag == "Shit")
        {
            currentIdleType = Mathf.Clamp(++currentIdleType, 1, 3);
            Debug.Log(currentIdleType);
            animator.SetInteger(IDLE_TYPE, currentIdleType);
            Physics2D.gravity += new Vector2(0, -5);
        }
    }
}
