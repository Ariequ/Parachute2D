using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public UIController uiController;
    public float[] gizmosX;
    public float gravityUpdateTime = 1f;
    private float lastGravityUpdateTime;
    private GameObject parachute;
    private GameObject player;
    private  PlayerController parachuteController;
    private bool gameStart;
    private ADBannerView banner = null;
    private bool adLoaded = false;

    // Use this for initialization
    void Start()
    {
        Physics2D.gravity = new Vector2(0, -10);
        gameStart = false;
        parachute = GameObject.FindGameObjectWithTag("Parachute");

        player = GameObject.FindGameObjectWithTag("Pilot");
        parachuteController = player.GetComponent<PlayerController>();
        parachuteController.enabled = false;

        Physics2D.gravity = new Vector2(0, 0);

        banner = new ADBannerView(ADBannerView.Type.Banner, ADBannerView.Layout.Top);
        ADBannerView.onBannerWasClicked += OnBannerClicked;
        ADBannerView.onBannerWasLoaded  += OnBannerLoaded;
    }

    void Update()
    {
        if (gameStart && Time.time - lastGravityUpdateTime > gravityUpdateTime)
        {
            Physics2D.gravity += new Vector2(0, -1);
            lastGravityUpdateTime = Time.time;
        }
    }
    
    public void StartGame()
    {
        if (uiController.label1.text == "TRY AGAIN")
        {
            Application.LoadLevel(0);
        }


        Physics2D.gravity = new Vector2(0, -10);

//        parachute.rigidbody2D.isKinematic = false;
//        player.rigidbody2D.isKinematic = false;
        parachuteController.enabled = true;

        uiController.gameObject.SetActive(false);

        gameStart = true;
    }

    public void EndGame()
    {
        parachute.SetActive(false);
        player.SetActive(false);
        uiController.gameObject.SetActive(true);
        uiController.label1.text = "TRY AGAIN";

        if (adLoaded)
        {
            banner.visible = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        float gap = 6.4f / (gizmosX.Length - 1);

        for (int i = 0; i < gizmosX.Length; i++)
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
        adLoaded = true;
        Debug.Log("Loaded!\n");

    }
}
