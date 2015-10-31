using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class PlayerController : MonoBehaviour
{
    public GameObject ContinueUI;
    public float downGravity = -40f;
    public float normalGravity = -40;
    public float ironMeshGravity = -10;

    private GameObject right;
    private GameObject left;
    private float moveX = 6.4f / 6;
    private float gravityScale = 0.5f;
    private float moveTime = 0.15f;
    private Animator animator;
    private float lastSpeedX;
    private float lastTouchTime = 0;
    private bool firstOperate;
    public bool showScreenEffect = false;
    private float startTime;
    private Vector2 m_gravity;
    private Collision2D collision;
    private bool showingContinueUI;
    GameData _gameData;
    GameController gameController;
    GameUIController _gameUIController;

    private Rigidbody2D rigidbody;

    private bool usingAD;

    private int coinReviveCount;

    enum Direction
    {
        LEFT = -1,
        RIGHT = 1
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        left = GameObject.Find("left");
        right = GameObject.Find("right");

        if (left != null)
        {
            left.SetActive(false);
        }

        if (right != null)
        {
            right.SetActive(false);
        }

        downGravity = normalGravity = -40f;

        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.tag == "Pilot" && !showingContinueUI)
        {
            CheckOperate();
        }
        
        CheckPosition();
        CheckAnimation();
    }

    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                _gameData = gameController.gameData;
            }
            
            return _gameData;
        }
    }

    public GameUIController gameUIController
    {
        get
        {
            if (_gameUIController == null)
            {
                _gameUIController = gameController.gameUIController;
            }
            
            return _gameUIController;
        }
    }

    public void StartRecord()
    {
        startTime = Time.time;
    }

    public Vector2 Gravity
    {
        get
        {
            return new Vector2(0, downGravity);
        }
    }

    private void CheckOperate()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Direction.RIGHT);
            SoundManager.instance.PlayingSound("Button", 1, Camera.main.transform.position);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Direction.LEFT);
            SoundManager.instance.PlayingSound("Button", 1, Camera.main.transform.position);
        } 
        
        if (Input.GetMouseButtonDown(0))
        {
            lastTouchTime = Time.time;
            
            if (Input.mousePosition.x < Screen.width / 2)
            { 
                Move(Direction.LEFT);
            }
            else
            {
                Move(Direction.RIGHT);
            }  

            SoundManager.instance.PlayingSound("Button", 1, Camera.main.transform.position);
        }
    }

    private void CheckPosition()
    {
        if (transform.position.x > 2.7f)
        {
            Vector3 temp = transform.position;
            temp.x = 2.7f;
            transform.position = temp;
        }
        else if (transform.position.x < -2.7f)
        {
            Vector3 temp = transform.position;
            temp.x = -2.7f;
            transform.position = temp;
        }
    }

    private void CheckAnimation()
    {
        if (Time.time - lastTouchTime <= moveTime * 2)
        {
            animator.SetFloat("SpeedX", 1);
        }
        else
        {
            animator.SetFloat("SpeedX", 0);
            downGravity = normalGravity;
        }
    }

    private void Move(Direction direction, bool needRecord = true)
    {
        lastTouchTime = Time.time;
//      Physics2D.gravity = gravityScale * Physics2D.gravity;
        downGravity = gravityScale * downGravity;
        iTween.MoveBy(gameObject, iTween.Hash("x", moveX * (int)direction, "easeType", "easeOutExpo", "time", moveTime));
        transform.localScale = new Vector3((int)direction, 1, 1);

        onOperate(direction);

        if (needRecord)
        {
            CommandType type = direction == Direction.LEFT ? CommandType.MoveLeft : CommandType.MoveRight;
            RecoderManager.instance.AddCommand(new Command(Time.time - startTime, type));
        }
    }

    private void onOperate(Direction direction)
    {
        if (!firstOperate)
        {
            firstOperate = true;

            GameObject startUI = GameObject.Find("StartUI");

            if (startUI != null)
            {
                startUI.GetComponent<StartUIController>().HideGuide();
            }
        }

        if (showScreenEffect)
        {
            if (direction == Direction.RIGHT)
            {
                right.SetActive(true);
                StartCoroutine(hideright());
            }
            else
            {
                left.SetActive(true);
                StartCoroutine(hideleft());
            }
        }
    }

    IEnumerator hideleft()
    {
        left.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(0.1f);
        left.SetActive(false);
    }

    IEnumerator hideright()
    {
        right.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(0.1f);
        right.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && transform.tag == "Pilot")
        {
            this.collision = collision;
            ShowContinueUI();
        }
    }

    private void ShowContinueUI()
    {
        Time.timeScale = 0;
        ContinueUI.SetActive(true);
        showingContinueUI = true;

        int cost = 100*(++coinReviveCount);

        
        if (gameData.DogCount >= cost)
        {
            usingAD = false;
            ContinueUI.GetComponent<ContinueUIController>().UpdateUI(true,cost);
        }
        else
        {
            usingAD = true;
            ContinueUI.GetComponent<ContinueUIController>().UpdateUI(false,0);
        }
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                Revive();
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

    public void OnConfirm()
    {
        if (usingAD)
        {
            ShowRewardedAd();
        }
        else
        {
            gameData.DogCount -= 100 * coinReviveCount;
            Revive();
        }
    }

    private void Revive()
    {
        gameUIController.UpdateUI(gameData);
        showingContinueUI = false;
        collision.gameObject.SetActive(false);
        ContinueUI.SetActive(false);

        _gameUIController.leftSectonds = 3;
        _gameUIController.CountingDownLabel.gameObject.SetActive(true);
        _gameUIController.StartCoroutine("ShowCountDown");
    }

    public void OnCancel()
    {
        showingContinueUI = false;

        Time.timeScale = 1;

        ContinueUI.SetActive(false);

        animator.SetBool("die", true);
        
        GameObject go = GameObject.FindGameObjectWithTag("Parachute");
        if (go != null)
        {
            go.SetActive(false);
        }
        
        iTween.ShakePosition(Camera.main.gameObject, iTween.Hash("y", 0.3f, "time", 1.0f));
        
        SoundManager.instance.PlayingSound("Die", 0.5f, Camera.main.transform.position);
    }

    public void OnDieAniamtionEnd()
    {
        Invoke("sendEndGameMessage", 0.1f);
    }

    private void sendEndGameMessage()
    {
        GameObject.Find("GameController").SendMessage("EndGame", false);
    }

    public void Do(Command command)
    {
        if (command.type == CommandType.MoveLeft)
        {
            Move(Direction.LEFT, false);
        }
        else
        {
            Move(Direction.RIGHT, false);
        }
    }

    void FixedUpdate()
    {
        rigidbody.AddForce(Gravity * GetComponent<Rigidbody2D>().mass * Time.fixedDeltaTime * 10, ForceMode2D.Impulse);
    }
}
