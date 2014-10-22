using UnityEngine;

public class CloudController : MonoBehaviour
{
    private float scrollSpeed = 1.0f;
    public float tileSizeZ = 14;
    private bool gameStarted = false;
    private Vector3 startPosition;
    private float startTime;
    
    void Start ()
    {
        startPosition = transform.localPosition;
    }
    
    void Update ()
    {
        float newPosition = 0;
        if (gameStarted)
        {
            newPosition = Mathf.Repeat ((Time.time - startTime) * scrollSpeed, tileSizeZ);
        }

        transform.localPosition = startPosition + Vector3.up * newPosition + Vector3.left * Mathf.PingPong ((Time.time) / 10, 1);
    }

    public void StartGame ()
    {
        startTime = Time.time;
        gameStarted = true;
    }
}