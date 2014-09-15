using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float xSpeed = 5;
    public float ySpeed = 5;
    private Animator animator;
    private float lastSpeedX;
    public GameObject right;
    public GameObject left;
    private float lastTouchTime = 0;

    public GameController gameController;

    public Rect downControlRct = new Rect (0, 0, 100, 100);

    void Start()
    {
		//test branch
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 v = rigidbody2D.velocity;
        Vector3 localScale = transform.localScale;
        if (Input.GetKeyDown (KeyCode.RightArrow)) {
            Debug.Log ("key down");
            lastTouchTime = Time.time;
            v.x = xSpeed;
            rigidbody2D.velocity = v;
            localScale.x = 1;
            right.SetActive (true);
            StartCoroutine (hideright ());

            Physics2D.gravity = new Vector2 (0, gameController.normalGravity);
        } else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
            Debug.Log ("key down");
            v.x = -xSpeed;
            lastTouchTime = Time.time;
            rigidbody2D.velocity = v;
            localScale.x = -1;
            left.SetActive (true);
            StartCoroutine (hideleft ());
            Physics2D.gravity = new Vector2 (0, gameController.normalGravity);
        } else if (Input.GetKey(KeyCode.DownArrow) && gameController.currentEnergy > 0) {
            lastTouchTime = Time.time;
            Physics2D.gravity = new Vector2 (0, gameController.speedGravity);
            gameController.AddEnergy(-gameController.energyConsumeSpeed*Time.deltaTime);
        }

        if (Input.GetMouseButton (0) && inRect (Input.mousePosition) && gameController.currentEnergy > 0) {
            lastTouchTime = Time.time;
            Physics2D.gravity = new Vector2 (0, gameController.speedGravity);
            gameController.AddEnergy(-gameController.energyConsumeSpeed*Time.deltaTime);
        }

       else if (Input.GetMouseButtonDown(0))
        {
            lastTouchTime = Time.time;

            if (Input.mousePosition.x < Screen.width / 2)
            { 
                v.x = -xSpeed;

                rigidbody2D.velocity = v;
                localScale.x = -1;
                left.SetActive(true);
                StartCoroutine(hideleft());
                Physics2D.gravity = new Vector2(0, gameController.normalGravity);
            }
            else
            {
                v.x = xSpeed;
                rigidbody2D.velocity = v;
                localScale.x = 1;
                right.SetActive(true);
                StartCoroutine(hideright());
                Physics2D.gravity = new Vector2(0, gameController.normalGravity);
           }      
        }

        transform.localScale = localScale;

        if (Time.time - lastTouchTime <= 0.5)
        {
            animator.SetFloat("SpeedX", 1);
        } 
        else
        {
            animator.SetFloat("SpeedX", 0);
            Physics2D.gravity = new Vector2(0, gameController.downGravity);
        }

		Debug.Log (rigidbody2D.velocity.y);
    }

    IEnumerator hideleft()
    {
        yield return new WaitForSeconds(0.05f);
        left.SetActive(false);
    }

    IEnumerator hideright()
    {
        yield return new WaitForSeconds(0.05f);
        right.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            animator.SetBool ("die", true);

            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D> ();
            rigidbody.isKinematic = true;

            GameObject go = GameObject.FindGameObjectWithTag ("Parachute");
            if (go != null) {
                go.SetActive (false);
            }

            iTween.ShakePosition (Camera.main.gameObject, iTween.Hash ("y", 0.3f, "time", 1.0f));
        } 
        else if (collision.gameObject.tag == "Energy") {
            gameController.AddEnergy(10);
            Destroy(collision.gameObject);
        }
    }

    public void OnDieAniamtionEnd()
    {
        GameObject.Find("GameController").SendMessage("EndGame");
    }

    private bool inRect(Vector3 mousePosition)
    {
        return mousePosition.x < Screen.width / 2 + downControlRct.width &&
            mousePosition.x > Screen.width / 2 - downControlRct.width &&
            mousePosition.y < downControlRct.height;
    }

}
