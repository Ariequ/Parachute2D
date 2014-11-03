using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject right;
    public GameObject left;
    private float moveX = 6.4f / 12;
    private float gravityScale = 0.5f;
    private float moveTime = 0.1f;
    private Animator animator;
    private float lastSpeedX;
    private float lastTouchTime = 0;
    public GameController gameController;
    public Rect downControlRct = new Rect (0, 0, 100, 100);

    void Start ()
    {
        //test branch
        animator = GetComponent<Animator> ();
    }

    void Update ()
    {
        Vector3 localScale = transform.localScale;

        if (Input.GetKeyDown (KeyCode.RightArrow))
        {
            lastTouchTime = Time.time;
            localScale.x = 1;
            left.SetActive (true);
            StartCoroutine (hideleft ());
            Physics2D.gravity = gravityScale * Physics2D.gravity;
            iTween.MoveBy (gameObject, iTween.Hash ("x", moveX, "easeType", "easeOutExpo", "time", moveTime));
        }
        else
        if (Input.GetKeyDown (KeyCode.LeftArrow))
        {                     
            lastTouchTime = Time.time;            
            localScale.x = -1;
            left.SetActive (true);
            StartCoroutine (hideleft ());
            Physics2D.gravity = gravityScale * Physics2D.gravity;
            iTween.MoveBy (gameObject, iTween.Hash ("x", -moveX, "easeType", "easeOutExpo", "time", moveTime));

        } 
//            else if (Input.GetKey (KeyCode.DownArrow) && gameController.currentEnergy > 0)
//        {
//            lastTouchTime = Time.time;
//            Physics2D.gravity = new Vector2 (0, gameController.speedGravity);
//            gameController.AddEnergy (-gameController.energyConsumeSpeed * Time.deltaTime);
//        }

//        if (Input.GetMouseButton (0) && inRect (Input.mousePosition) && gameController.currentEnergy > 0)
//        {
//            lastTouchTime = Time.time;
//            Physics2D.gravity = new Vector2 (0, gameController.speedGravity);
//            gameController.AddEnergy (-gameController.energyConsumeSpeed * Time.deltaTime);
//        } else 
        if (Input.GetMouseButtonDown (0))
        {
            lastTouchTime = Time.time;

            if (Input.mousePosition.x < Screen.width / 2)
            { 
                localScale.x = -1;
                left.SetActive (true);
                StartCoroutine (hideleft ());
                Physics2D.gravity = gravityScale * Physics2D.gravity;
                iTween.MoveBy (gameObject, iTween.Hash ("x", -moveX, "easeType", "easeOutExpo", "time", moveTime));
            }
            else
            {
                iTween.MoveBy (gameObject, iTween.Hash ("x", moveX, "easeType", "easeOutExpo", "time", moveTime));
                localScale.x = 1;
                right.SetActive (true);
                StartCoroutine (hideright ());
                Physics2D.gravity = gravityScale * Physics2D.gravity;
            }      
        }

        transform.localScale = localScale;

        if (transform.position.x > 2.7f)
        {
            Vector3 temp = transform.position;
            temp.x = 2.7f;
            transform.position = temp;
        }
        else
        if (transform.position.x < - 2.7f)
        {
            Vector3 temp = transform.position;
            temp.x = -2.7f;
            transform.position = temp;
        }

        if (Time.time - lastTouchTime <= moveTime * 2)
        {
            animator.SetFloat ("SpeedX", 1);
        }
        else
        {
            animator.SetFloat ("SpeedX", 0);
            Physics2D.gravity = new Vector2 (0, gameController.downGravity);
        }
    }

    IEnumerator hideleft ()
    {
        yield return new WaitForSeconds (0.05f);
        left.SetActive (false);
    }

    IEnumerator hideright ()
    {
        yield return new WaitForSeconds (0.05f);
        right.SetActive (false);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            animator.SetBool ("die", true);

            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D> ();
            rigidbody.isKinematic = true;

            GameObject go = GameObject.FindGameObjectWithTag ("Parachute");
            if (go != null)
            {
                go.SetActive (false);
            }

            iTween.ShakePosition (Camera.main.gameObject, iTween.Hash ("y", 0.3f, "time", 1.0f));
        }
        else
        if (collision.gameObject.tag == "Energy")
        {
            gameController.AddEnergy (10);
            Destroy (collision.gameObject);
        }
    }

    public void OnDieAniamtionEnd ()
    {
        GameObject.Find ("GameController").SendMessage ("EndGame", false);
    }

    private bool inRect (Vector3 mousePosition)
    {
        return mousePosition.x < Screen.width / 2 + downControlRct.width &&
            mousePosition.x > Screen.width / 2 - downControlRct.width &&
            mousePosition.y < downControlRct.height;
    }

}
