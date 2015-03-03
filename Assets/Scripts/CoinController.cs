using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour
{
    public GameObject effect;
    Animator animator;
    GameData _gameData;
    GameUIController _gameUIController;
    GameController gameController;

    // Use this for initialization
    void Start ()
    {
        animator = gameObject.GetComponent<Animator> ();
        animator.speed = Random.value;

        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public GameData gameData
    {
        get
        {
            if(_gameData == null)
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
    
    void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Pilot")
        {
            animator.speed = 1;
            animator.SetBool ("MeetPilot", true);
            initEffect (effect);
            SoundManager.instance.PlayingSound ("GetCoin", 1f, transform.position);
            gameData.DogCount += 1;
            gameUIController.UpdateUI(gameData);
        }
    }

    public void OnAnimationEnd ()
    {
        Destroy (gameObject);
    }

    //Spawn effect method
    private void initEffect (GameObject prefab)
    {
        GameObject go = (GameObject)Instantiate (prefab, transform.position, Quaternion.identity);
        go.transform.parent = transform;
        go.transform.localPosition = new Vector3 (go.transform.localPosition.x, go.transform.localPosition.y, go.transform.localPosition.z);    
    }
}
