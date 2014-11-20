using UnityEngine;
using System.Collections;

public class TankCotroller : MonoBehaviour
{
    public float bulletSpeed = 3;
    public GameObject bullet;
    private Animator animator;
    private bool fired = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void fire()
    {
        if (!fired)
        {
            fired = true;
            GameObject aBullet = (GameObject)Instantiate (bullet);
            aBullet.transform.position = transform.position + new Vector3 (0, 0.2f, 0);
            aBullet.transform.rotation = transform.localRotation;

            aBullet.rigidbody2D.velocity = new Vector2 (-Mathf.Sin (Mathf.PI * transform.eulerAngles.z / 180), Mathf.Cos (Mathf.PI * transform.eulerAngles.z / 180)) * bulletSpeed;

            SoundManager.instance.PlayingSound("TankFire", 1f, transform.position);
        }
    }

    public void OnFireEnd()
    {
        animator.SetBool("onFire", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pilot")
        {     
              animator.SetBool ("onFire", true);
        }
    }

    public void Reset()
    {
        fired = false;
    }
}
