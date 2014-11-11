using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int gizmosCount;
    public float gravityUpdateTime = 1f;
    public GameObject endGameUI;
//    public GameObject player;

    private float lastGravityUpdateTime;
    private GameObject parachute;
    private GameObject player;
    private  PlayerController playerController;
    private ParachuteController parachuteController;

    public float downGravity = -100f;
    public float normalGravity = -5;
    public float ironMeshGravity = 5;
    public float totalEnergy = 100;
    public float currentEnergy = 0;
    public float energyConsumeSpeed = 1;
    public Image energyImage;
    public CloudController cloudController;
  
//    public Recorder recorder1;
//    public Recorder recorder2;
    public GameObject startButton;
    public Text scoreText;

    private Vector3 originPlayerPosition;
    private Vector3 originParachutePosition;

    private TankCotroller[] trankControllers;

    public GameObject level;

	public delegate void OnGameStart();

	public static event OnGameStart gameStart;

    public StartUIController startUI;
    public CameraFollow cameraFollow;

#if UNITY_IPHONE 
    private ADBannerView banner = null;
    private bool adLoaded = false;
#endif
   

    // Use this for initialization
    void Start ()
    {
//        UpdateEnergyImage ();
        endGameUI.SetActive (false);

//        player = GameObject.Find ("Player");
        parachute = GameObject.FindGameObjectWithTag ("Parachute");
        parachuteController = parachute.GetComponent<ParachuteController> ();
        player = GameObject.FindGameObjectWithTag ("Pilot");
        playerController = player.GetComponent<PlayerController> ();
        playerController.enabled = false;

        Physics2D.gravity = new Vector2 (0, 0);

        originPlayerPosition = player.transform.position;
        originParachutePosition = parachute.transform.position;

        trankControllers = level.GetComponentsInChildren<TankCotroller> ();


//#if UNITY_IPHONE 
//        banner = new ADBannerView(ADBannerView.Type.Banner, ADBannerView.Layout.Top);
//        ADBannerView.onBannerWasClicked += OnBannerClicked;
//        ADBannerView.onBannerWasLoaded  += OnBannerLoaded;
//        banner.visible = false;
//#endif
    }

//    void Update ()
//    {
//        if (gameStart && Time.time - lastGravityUpdateTime > gravityUpdateTime) {
////            Physics2D.gravity += new Vector2 (0, -1);
//            lastGravityUpdateTime = Time.time;
//        }
//    }
    
    public void StartGame (GameObject obj)
    {
//        if (uiController.label1.text == "TRY AGAIN") {
//            Application.LoadLevel (0);
//        }

		Debug.Log("=============");

        Debug.Log("StartGame call time: " + Time.time);

        downGravity = -40f;
        Physics2D.gravity = new Vector2 (0, downGravity);

        playerController.enabled = true;

        obj.SetActive (false);

        cloudController.SendMessage ("StartGame");

//        recorder1.startRecord ();
//        recorder2.startRecord ();

//		gameStart();

        Debug.Log("call OnGameStart");
        startUI.OnGameStart();
        cameraFollow.OnGameStart();

        Debug.Log("StartGame end time: " + Time.time);
    }

    public void Replay ()
    {
        Application.LoadLevel (0);

//        #if UNITY_IPHONE 
//            banner.visible = false;
//        #endif
//
//        RectTransform rect = scoreText.GetComponent<RectTransform> ();
//        rect.anchoredPosition = new Vector2 (0, -53);
//
//        parachute.SetActive (true);
//        player.SetActive (true);
//        endGameUI.SetActive (false);
//        Physics2D.gravity = new Vector2 (0, 0);
//        playerController.enabled = false;
//
//        recorder2.backPlayRecord ();
//        recorder1.backPlayRecord ();
    }

    public void recordPlayFinish ()
    {
        startButton.SetActive (true);

        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D> ();
        rigidbody.isKinematic = false;

        Rigidbody2D rigidbody1 = parachute.GetComponent<Rigidbody2D> ();
        rigidbody1.isKinematic = false;

        parachuteController.Reset ();
        cloudController.Reset ();

        parachute.transform.position = originParachutePosition;
        player.transform.position = originPlayerPosition;

        parachute.transform.rotation = Quaternion.identity;
        player.transform.rotation = Quaternion.identity;

        for (int i = 0; i < trankControllers.Length; i ++)
        {
            trankControllers[i].Reset();
        }
    }

    public void EndGame (bool isWin)
    {
        endGameUI.SetActive (true);
        endGameUI.GetComponent<EndUIController> ().UpdateUI (isWin);

        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D> ();
        rigidbody.isKinematic = true;

        Rigidbody2D rigidbody1 = parachute.GetComponent<Rigidbody2D> ();
        rigidbody1.isKinematic = true;

        parachute.SetActive (false);
        player.SetActive (false);
//        player.SetActive (false);



//        recorder1.endRecord ();
//        recorder2.endRecord ();

#if UNITY_IPHONE 
        if (adLoaded)
        {
            banner.visible = true;
        }
#endif
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.yellow;
        float gap = 6.4f / (gizmosCount - 1);

        for (int i = 0; i < gizmosCount; i++)
        {
            Gizmos.DrawLine (new Vector3 (-3.2f + gap * i, 1000, 0), new Vector3 (-3.2f + gap * i, -1000, 0));
        }
    }

    void OnBannerClicked ()
    {
        Debug.Log ("Clicked!\n");
    }
    
    void OnBannerLoaded ()
    {
        #if UNITY_IPHONE 
        adLoaded = true;
#endif
        Debug.Log ("Loaded!\n");

    }

    public void AddEnergy (float amount)
    {
        currentEnergy += amount;
//        UpdateEnergyImage ();
    }

//    public void UpdateEnergyImage()
//    {
//        energyImage.fillAmount = currentEnergy / totalEnergy;
//    }

    void OnDestroy ()
    {
#if UNITY_IPHONE 
//        banner.visible = false;
        banner = null;
#endif

		gameStart = null;
    }
}
