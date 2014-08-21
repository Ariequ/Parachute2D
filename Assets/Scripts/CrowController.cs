using UnityEngine;
using System.Collections;

public class CrowController : MonoBehaviour
{
    public GameObject shit;
    public float speed = 20;
    public float shitFactior = 0.01f;
    public int direction;

    private float startTime;
    private float journeyLength;
    private Vector3 target;
    private Vector3 start;

    // Use this for initialization
    void Start()
    {
        enabled = false;
        start = transform.position;
        target = new Vector3(transform.position.x + direction * 6, transform.position.y, transform.position.z);

        journeyLength = Vector3.Distance(start, target);
    }
    
    // Update is called once per frame
    void Update()
    {
//        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, 0);
//        newPosition.x = startX + Mathf.Lerp(-4, 4, (Mathf.Sin(Time.time / 100 * speed) + 1) / 2);
//
//        if (newPosition.x > transform.position.x && transform.localScale.x != -1)
//        {
//            transform.localScale = new Vector3(-1, 1, 1);
//        } 
//        else if (newPosition.x < transform.position.x && transform.localScale.x != 1)
//        {
//            transform.localScale = new Vector3(1, 1, 1);
//        }

//        transform.position = newPosition;

        float distCovered = (Time.time - startTime) * speed / 100;
        float fracJourney = distCovered / journeyLength;

        Debug.Log(fracJourney);

        transform.position = Vector3.Lerp(start, target, fracJourney);

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
        startTime = Time.time;
    }
}
