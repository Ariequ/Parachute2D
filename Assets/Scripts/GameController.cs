using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int gizmosCount;
    public float gravityUpdateTime = 1f;
    public GameObject endGameUI;

    private float lastGravityUpdateTime;
    private GameObject parachute;
    private GameObject player;
    private  PlayerController parachuteController;

    public float downGravity = -500f;
    public float normalGravity = -5;
    public float ironMeshGravity = 5;

    public float totalEnergy = 100;
    public float currentEnergy = 0;
    public float energyConsumeSpeed = 1;

    public Image energyImage;

    public CloudController cloudController;


#if UNITY_IPHONE 
    private ADBannerView banner = null;
	private bool adLoaded = false;
#endif
   

    // Use this for initialization
    void Start ()
    {
//        UpdateEnergyImage ();
        endGameUI.SetActive (false);
        parachute = GameObject.FindGameObjectWithTag ("Parachute");

        player = GameObject.FindGameObjectWithTag ("Pilot");
        parachuteController = player.GetComponent<PlayerController> ();
        parachuteController.enabled = false;

        Physics2D.gravity = new Vector2 (0, 0);

#if UNITY_IPHONE 
        banner = new ADBannerView(ADBannerView.Type.Banner, ADBannerView.Layout.Top);
        ADBannerView.onBannerWasClicked += OnBannerClicked;
        ADBannerView.onBannerWasLoaded  += OnBannerLoaded;
        banner.visible = false;
#endif
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

        Physics2D.gravity = new Vector2 (0, downGravity);

        parachuteController.enabled = true;

        obj.SetActive (false);

        cloudController.SendMessage ("StartGame");
    }

    public void Replay()
    {
        Application.LoadLevel (0);
    }

    public void EndGame (bool isWin)
    {
        endGameUI.SetActive (true);
        endGameUI.GetComponent<EndUIController>().UpdateUI (isWin);
        parachute.SetActive (false);
        player.SetActive (false);


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

        for (int i = 0; i < gizmosCount; i++) {
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

    public void AddEnergy(float amount)
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
        banner.visible = false;
        banner = null;
#endif
    }
}
