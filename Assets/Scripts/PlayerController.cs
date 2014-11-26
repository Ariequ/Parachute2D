using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float downGravity = -40f;
    public float normalGravity = -40;
    public float ironMeshGravity = -10;
    private GameObject right;
    private GameObject left;
    private float moveX = 6.4f / 12;
    private float gravityScale = 0.5f;
    private float moveTime = 0.1f;
    private Animator animator;
    private float lastSpeedX;
    private float lastTouchTime = 0;
    private bool firstOperate;
    public bool showScreenEffect = true;
    private float startTime;
    private Vector2 m_gravity;
    
    enum Direction
    {
        LEFT = -1,
        RIGHT =1
    }

    void Start ()
    {
        animator = GetComponent<Animator> ();

        left = GameObject.Find ("left");
        right = GameObject.Find ("right");

        if (left != null)
        {
            left.SetActive (false);
        }

        if (right != null)
        {
            right.SetActive (false);
        }

        downGravity = normalGravity = -40f;
    }

    void Update ()
    {
        if (transform.tag == "Pilot")
        {
            CheckOperate ();
        }
        
        CheckPosition ();
        CheckAnimation ();
    }

    public void StartRecord ()
    {
        startTime = Time.time;
    }

    public Vector2 Gravity
    {
        get
        {
            return new Vector2 (0, downGravity);
        }
    }

    private void CheckOperate ()
    {
        if (Input.GetKeyDown (KeyCode.RightArrow))
        {
            Move (Direction.RIGHT);
            SoundManager.instance.PlayingSound ("Button", 1, Camera.main.transform.position);
        }
        else if (Input.GetKeyDown (KeyCode.LeftArrow))
        {
            Move (Direction.LEFT);
            SoundManager.instance.PlayingSound ("Button", 1, Camera.main.transform.position);
        } 
        
        if (Input.GetMouseButtonDown (0))
        {
            lastTouchTime = Time.time;
            
            if (Input.mousePosition.x < Screen.width / 2)
            { 
                Move (Direction.LEFT);
            }
            else
            {
                Move (Direction.RIGHT);
            }  

            SoundManager.instance.PlayingSound ("Button", 1, Camera.main.transform.position);
        }
    }

    private void CheckPosition ()
    {
        if (transform.position.x > 2.7f)
        {
            Vector3 temp = transform.position;
            temp.x = 2.7f;
            transform.position = temp;
        }
        else if (transform.position.x < - 2.7f)
        {
            Vector3 temp = transform.position;
            temp.x = -2.7f;
            transform.position = temp;
        }
    }

    private void CheckAnimation ()
    {
        if (Time.time - lastTouchTime <= moveTime * 2)
        {
            animator.SetFloat ("SpeedX", 1);
        }
        else
        {
            animator.SetFloat ("SpeedX", 0);
            downGravity = normalGravity;
        }
    }
    
    private void Move (Direction direction, bool needRecord = true)
    {
        lastTouchTime = Time.time;
//      Physics2D.gravity = gravityScale * Physics2D.gravity;
        downGravity = gravityScale * downGravity;
        iTween.MoveBy (gameObject, iTween.Hash ("x", moveX * (int)direction, "easeType", "easeOutExpo", "time", moveTime));
        transform.localScale = new Vector3 ((int)direction, 1, 1);

        onOperate (direction);

        if (needRecord)
        {
            CommandType type = direction == Direction.LEFT ? CommandType.MoveLeft : CommandType.MoveRight;
            RecoderManager.instance.AddCommand (new Command (Time.time - startTime, type));
        }
    }

    private void onOperate (Direction direction)
    {
        if (!firstOperate)
        {
            firstOperate = true;
            StartUIController controller = GameObject.Find ("StartUI").GetComponent<StartUIController> ();
            controller.HideGuide ();
        }

        if (showScreenEffect)
        {
            if (direction == Direction.RIGHT)
            {
                right.SetActive (true);
                StartCoroutine (hideright ());
            }
            else
            {
                left.SetActive (true);
                StartCoroutine (hideleft ());
            }
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
        if (collision.gameObject.tag == "Enemy" && transform.tag == "Pilot")
        {
            animator.SetBool ("die", true);

            GameObject go = GameObject.FindGameObjectWithTag ("Parachute");
            if (go != null)
            {
                go.SetActive (false);
            }

            iTween.ShakePosition (Camera.main.gameObject, iTween.Hash ("y", 0.3f, "time", 1.0f));

            SoundManager.instance.PlayingSound ("Die", 0.5f, Camera.main.transform.position);
        }
    }

    public void OnDieAniamtionEnd ()
    {
        Invoke ("sendEndGameMessage", 0.1f);
    }

    private void sendEndGameMessage ()
    {
        GameObject.Find ("GameController").SendMessage ("EndGame", false);
    }

    public void Do (Command command)
    {
        if (command.type == CommandType.MoveLeft)
        {
            Move (Direction.LEFT, false);
        }
        else
        {
            Move (Direction.RIGHT, false);
        }
    }

    void FixedUpdate ()
    {
        rigidbody2D.AddForce (Gravity * rigidbody2D.mass * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
}
