using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recorder : MonoBehaviour
{

    public Transform recorderTarget;
    private float recorderGap;
    private Stack<Vector3> positonStack = new Stack<Vector3> ();
    // Use this for initialization

    private GameObject player;

    GameController gameController;

    void Start ()
    {
        recorderGap = Time.deltaTime * 10;
        recorderTarget = transform;
        gameController = GameObject.Find ("GameController").GetComponent<GameController>();
    }

    public void startRecord ()
    {
        InvokeRepeating ("record", 0, recorderGap);
    }

    private void record ()
    {
        if (positonStack.Count == 0 || positonStack.Peek () != recorderTarget.position)
        {
            positonStack.Push (recorderTarget.position);
        }
    }

    public void endRecord ()
    {
        CancelInvoke ("record");
    }

    public void backPlayRecord ()
    {
        StartCoroutine ("moveBack", positonStack.Pop ());     
    }

    IEnumerator moveBack (Vector3 position)
    {

        yield return new WaitForSeconds (Time.deltaTime);
        iTween.MoveTo (recorderTarget.gameObject, iTween.Hash ("position", position, "time", Time.deltaTime));
        if (positonStack.Count > 0)
        {
            StartCoroutine ("moveBack", positonStack.Pop ());
        }
        else
        {
            gameController.SendMessage("recordPlayFinish");
        }
    }
}
