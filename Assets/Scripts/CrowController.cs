using UnityEngine;
using System.Collections;

public class CrowController : MonoBehaviour
{
    public GameObject shit;
    public float speed = 10;
    public float shitFactior = 0.01f;

    private float startX;

    // Use this for initialization
    void Start()
    {
        enabled = false;
        startX = transform.position.x;
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, 0);
        newPosition.x = startX + Mathf.Lerp(-4, 4, (Mathf.Sin(Time.time / 100 * speed) + 1) / 2);

        if (newPosition.x > transform.position.x && transform.localScale.x != -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } 
        else if (newPosition.x < transform.position.x && transform.localScale.x != 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        transform.position = newPosition;

        if (Random.value < shitFactior)
        {
            fire();
        }

        if (transform.position.y - Camera.main.transform.position.y > Screen.height / 100)
        {
            Destroy(gameObject);
        }
    
    }

    public void fire()
    {
        GameObject ashit = (GameObject)Instantiate(shit);

        ashit.transform.position = transform.position + new Vector3(0, -0.3f, 0);
    }

    void OnBecameVisible()
    {
        Debug.Log("OnBecameVisible");
        enabled = true;
    }
}
