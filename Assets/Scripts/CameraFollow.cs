using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public Transform followTarget;		// Reference to the player's transform.
	public float offsetY;
	private float startTime = 0;

	void Awake ()
	{
		// Setting up the reference.
		followTarget = GameObject.FindGameObjectWithTag ("Parachute").transform;
//		GameController.gameStart += OnGameStart;
		startTime = float.MaxValue;
       
        float designRatio = 1136f / 640;
        float currentRatio = (float)Screen.height / Screen.width;

        Debug.Log(designRatio);
        Debug.Log(currentRatio);

        Camera.main.orthographicSize =  currentRatio / designRatio * 1136f/200;
	}

	void Update ()
	{
		if (startTime < float.MaxValue) {
			Vector3 target = transform.position;
			target.y = Mathf.Lerp (followTarget.position.y, followTarget.position.y + offsetY, Time.time - startTime);
			transform.position = target;// new Vector3(transform.position.x, player.position.y + offsetY, transform.position.z);
		}
        else
        {
            transform.Translate(Vector3.down*Time.deltaTime * 3);
        }
	}

	public void OnGameStart ()
	{
		offsetY = -3.5f;
		startTime = Time.time;
	}

    public void replayMode()
    {
        offsetY = -3.5f;
    }

}