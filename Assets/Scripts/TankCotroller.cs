using UnityEngine;
using System.Collections;

public class TankCotroller : MonoBehaviour
{
    public float bulletSpeed = 3;
    public GameObject bullet;
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

    public void fire()
    {
        GameObject aBullet = (GameObject)Instantiate(bullet);
        aBullet.transform.position = transform.position + new Vector3(0, 0.2f, 0);
        aBullet.transform.rotation = transform.localRotation;

        aBullet.rigidbody2D.velocity = new Vector2(-Mathf.Sin(Mathf.PI * transform.eulerAngles.z / 180), Mathf.Cos(Mathf.PI * transform.eulerAngles.z / 180)) * bulletSpeed;
    }

    public void OnFireEnd()
    {
        animator.SetBool("onFire", false);
    }

    void OnBecameVisible() 
    {
        Debug.Log("tank visible");
        animator.SetBool("onFire", true);
    }
}
