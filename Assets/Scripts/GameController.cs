using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public int gizmosCount;
    public float gravityUpdateTime = 1f;
    public GameObject endGameUI;

    private float lastGravityUpdateTime;
    private GameObject parachute;
    private GameObject player;
    private  PlayerController parachuteController;
    private bool gameStart;


#if UNITY_IPHONE 
    private ADBannerView banner = null;
#endif
    private bool adLoaded = false;

    // Use this for initialization
    void Start ()
    {
        endGameUI.SetActive (false);
        Physics2D.gravity = new Vector2 (0, -10);
        gameStart = false;
        parachute = GameObject.FindGameObjectWithTag ("Parachute");

        player = GameObject.FindGameObjectWithTag ("Pilot");
        parachuteController = player.GetComponent<PlayerController> ();
        parachuteController.enabled = false;

        Physics2D.gravity = new Vector2 (0, 0);

#if UNITY_IPHONE 
        banner = new ADBannerView(ADBannerView.Type.Banner, ADBannerView.Layout.Bottom);
        ADBannerView.onBannerWasClicked += OnBannerClicked;
        ADBannerView.onBannerWasLoaded  += OnBannerLoaded;
        banner.visible = false;
#endif
    }

    void Update ()
    {
        if (gameStart && Time.time - lastGravityUpdateTime > gravityUpdateTime) {
            Physics2D.gravity += new Vector2 (0, -1);
            lastGravityUpdateTime = Time.time;
        }
    }
    
    public void StartGame (GameObject obj)
    {
//        if (uiController.label1.text == "TRY AGAIN") {
//            Application.LoadLevel (0);
//        }

        Physics2D.gravity = new Vector2 (0, -10);

        parachuteController.enabled = true;

        gameStart = true;

        obj.SetActive (false);
    }

    public void Replay()
    {
        Application.LoadLevel (0);
    }

    public void EndGame ()
    {
        endGameUI.SetActive (true);
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
        adLoaded = true;
        Debug.Log ("Loaded!\n");

    }

    void OnDestroy ()
    {
        Debug.Log ("On Destroy");
#if UNITY_IPHONE 
        banner.visible = false;
        banner = null;
#endif
    }
}
