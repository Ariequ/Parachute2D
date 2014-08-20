using UnityEngine;
using System.Collections;

public class ParachuteController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision) 
    {
        Debug.Log("parachute OnCollisionEnter2D");
        GameObject collistionObject = collision.gameObject;
        if (collistionObject.tag == "Shit")
        {
            collistionObject.transform.parent = transform;

            collistionObject.GetComponent<Animator>().SetBool("collider",true);

            Rigidbody2D rigidbody = collistionObject.GetComponent<Rigidbody2D>();
            Destroy(rigidbody);

            Vector3 adjustPositon = collistionObject.transform.position;
            adjustPositon.y -= 0.2f;
           
            collistionObject.transform.position = adjustPositon;

            Physics2D.gravity += new Vector2(0, -5);
        }
    }
}
