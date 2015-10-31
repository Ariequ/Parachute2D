using UnityEngine;
using System.Collections;

public class TankCotroller : MonoBehaviour
{
    public float bulletSpeed = 30;
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

            aBullet.GetComponent<Rigidbody2D>().velocity = new Vector2 (-Mathf.Sin (Mathf.PI * transform.eulerAngles.z / 180), Mathf.Cos (Mathf.PI * transform.eulerAngles.z / 180)) * bulletSpeed;

//            aBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2 (-Mathf.Sin (Mathf.PI * transform.eulerAngles.z / 180), Mathf.Cos (Mathf.PI * transform.eulerAngles.z / 180)) * bulletSpeed * 10, ForceMode2D.Impulse);

            Debug.Log(aBullet.GetComponent<Rigidbody2D>().velocity);

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
