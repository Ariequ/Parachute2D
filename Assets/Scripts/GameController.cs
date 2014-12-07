using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public int gizmosCount;
	public float gravityUpdateTime = 1f;
	public GameObject endGameUI;
    public GameObject startGameUI;
	public GameObject playerGameObject;
	private float lastGravityUpdateTime;
	private GameObject parachute;
	private GameObject player;
	private  PlayerController playerController;
	private ParachuteController parachuteController;
	public CloudController cloudController;
	public GameObject startButton;
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

#if UNITY_ANDROID
    private AdMobPlugin plugin = null;
#endif

   

	// Use this for initialization
	void Start()
	{
        startGameUI.SetActive(true);
		endGameUI.SetActive(false);
		parachute = GameObject.FindGameObjectWithTag("Parachute");
		parachuteController = parachute.GetComponent<ParachuteController>();
		player = GameObject.FindGameObjectWithTag("Pilot");
		playerController = player.GetComponent<PlayerController>();
		playerController.enabled = false;

		Physics2D.gravity = new Vector2(0, 0);

		originPlayerPosition = player.transform.position;
		originParachutePosition = parachute.transform.position;

		trankControllers = level.GetComponentsInChildren<TankCotroller>();

		playerGameObject.SetActive(false);

		SoundManager.instance.startBGM();

#if UNITY_IPHONE 
        banner = new ADBannerView(ADBannerView.Type.Banner, ADBannerView.Layout.Bottom);
        ADBannerView.onBannerWasClicked += OnBannerClicked;
        ADBannerView.onBannerWasLoaded  += OnBannerLoaded;
        banner.visible = false;
		#endif

#if UNITY_ANDROID
        plugin = this.GetComponent<AdMobPlugin>();   
#endif
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
    
	public void StartGame(GameObject obj)
	{
		obj.SetActive(false);

		playerGameObject.SetActive(true);
		playerController.enabled = true;
		cloudController.SendMessage("StartGame");
		startUI.OnGameStart();
		cameraFollow.OnGameStart();
		playerController.StartRecord();

		RecoderManager.instance.StartNewRecoder();
	}

	public void Replay()
	{
		Application.LoadLevel(1);
	}

	public void recordPlayFinish()
	{
		startButton.SetActive(true);

		Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();
		rigidbody.isKinematic = false;

		Rigidbody2D rigidbody1 = parachute.GetComponent<Rigidbody2D>();
		rigidbody1.isKinematic = false;

		parachuteController.Reset();
		cloudController.Reset();

		parachute.transform.position = originParachutePosition;
		player.transform.position = originPlayerPosition;

		parachute.transform.rotation = Quaternion.identity;
		player.transform.rotation = Quaternion.identity;

		for (int i = 0; i < trankControllers.Length; i ++)
		{
			trankControllers [i].Reset();
		}
	}

	public void EndGame(bool isWin)
	{
        startGameUI.SetActive(false);
		endGameUI.SetActive(true);
		endGameUI.GetComponent<EndUIController>().UpdateUI(isWin);

		Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();
		rigidbody.isKinematic = true;

		Rigidbody2D rigidbody1 = parachute.GetComponent<Rigidbody2D>();
		rigidbody1.isKinematic = true;

		parachute.SetActive(false);
		player.SetActive(false);

		Invoke("playRecoder", 2f);

		SoundManager.instance.stopBMG();

#if UNITY_IPHONE 
        if (adLoaded)
        {
            banner.visible = true;
        }
#endif
#if UNITY_ANDROID
        this.plugin.Load();
#endif
	}

	private void playRecoder()
	{
		RecoderManager.instance.PlayRecoder();
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		float gap = 6.4f / (gizmosCount - 1);

		for (int i = 0; i < gizmosCount; i++)
		{
			Gizmos.DrawLine(new Vector3(-3.2f + gap * i, 1000, 0), new Vector3(-3.2f + gap * i, -1000, 0));
		}
	}

	void OnBannerClicked()
	{
		Debug.Log("Clicked!\n");
	}
    
	void OnBannerLoaded()
	{
		#if UNITY_IPHONE 
        adLoaded = true;
#endif
		Debug.Log("Loaded!\n");

	}
	
	void OnDestroy()
	{
#if UNITY_IPHONE 
//        banner.visible = false;
        banner = null;
#endif

		gameStart = null;
	}
}
