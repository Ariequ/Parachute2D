using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour
{
    public GameObject effect;
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
        initEffect(effect);
        audio.Play();
    }

    public void OnAnimationEnd()
    {
        Destroy (gameObject);
    }

    //Spawn effect method
    private void initEffect(GameObject prefab){
        GameObject go = (GameObject) Instantiate(prefab, transform.position, Quaternion.identity);
        go.transform.parent = transform;
        go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, go.transform.localPosition.z);    
    }
}
