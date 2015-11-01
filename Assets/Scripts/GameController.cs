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

    public GameUIController gameUIController;

    private GameData _gameData;

	// Use this for initialization
	void Start()
	{
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        _gameData = new GameData();

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
       
//        AdMob.requestInterstital( "ca-app-pub-1215085077559999/3564479460", "ca-app-pub-1215085077559999/5180813465" );
//        AdMob.init( "ca-app-pub-1215085077559999/3044727060", "ca-app-pub-1215085077559999/6187409461" );

        gameUIController.UpdateUI(gameData);
    }
    
    void Update()
	{
		if(Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

    public GameData gameData
    {
        get
        {
            return _gameData;
        }
    }
    
	public void StartGame(GameObject obj)
	{
		obj.SetActive(false);

		playerGameObject.SetActive(true);
		playerController.enabled = true;
		cloudController.SendMessage("StartGame");
		startUI.OnGameStart();
        gameUIController.gameObject.SetActive(true);
		cameraFollow.OnGameStart();
		playerController.StartRecord();

		RecoderManager.instance.StartNewRecoder();

//        if (GoogleAnalytics.instance)
//        {
//            string shareString = " Start Game";
//            
//            #if UNITY_IOS
//            shareString = "iOS " + shareString;
//            #elif UNITY_ANDROID
//            shareString = "Android" + shareString;
//            #endif
//            GoogleAnalytics.instance.LogScreen(shareString);          
//        }

//        AdMob.createBanner( AdMobBanner.SmartBanner, AdMobLocation.BottomCenter );
	}

	public void Replay()
	{
		Application.LoadLevel(0);
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
		endGameUI.GetComponent<EndUIController>().UpdateUI(isWin, gameData);

		Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();
		rigidbody.isKinematic = true;

		Rigidbody2D rigidbody1 = parachute.GetComponent<Rigidbody2D>();
		rigidbody1.isKinematic = true;

		parachute.SetActive(false);
		player.SetActive(false);

		Invoke("playRecoder", 2f);

		SoundManager.instance.stopBMG();
        
//        if (GoogleAnalytics.instance)
//        {
//            string shareString = " Score: " + startUI.CurrentScore;
//            
//            #if UNITY_IOS
//            shareString = "iOS " + shareString;
//            #elif UNITY_ANDROID
//            shareString = "Android" + shareString;
//            #endif
//            GoogleAnalytics.instance.LogScreen(shareString);
//
//            Debug.Log("Add to GoogleAnalyticss " + shareString);                   
//        }

        Invoke("ShowAdmob", 1f);
	}

	private void playRecoder()
	{
		RecoderManager.instance.PlayRecoder();
	}

    private void ShowAdmob()
    {
        FindObjectOfType<AD>().Show();
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
	
	void OnDestroy()
	{
		gameStart = null;
	}
}
