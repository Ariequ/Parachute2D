using UnityEngine;
using System.Collections;

public class ParachuteController : MonoBehaviour
{
    public GameObject screenEffect;
    public float gravityStep;

    private const string IDLE_TYPE = "idleType";
    private Animator animator;
    private int currentIdleType;

    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
        animator = GetComponent<Animator>();  
        currentIdleType = 1;
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "Enemy")
        {
            currentIdleType = Mathf.Clamp(++currentIdleType, 1, 3);
            animator.SetInteger(IDLE_TYPE, currentIdleType);
            gameController.normalGravity += -gameController.ironMeshGravity;
            gameController.downGravity += -gameController.ironMeshGravity;
//            Physics2D.gravity += new Vector2(0, -5);
//			iTween.ShakePosition(Camera.main.gameObject,iTween.Hash("y",0.1f,"time",0.1f));

//            screenEffect.SetActive(true);
//            StartCoroutine(hideScreenEffect());
        }
    }

    IEnumerator hideScreenEffect()
    {
        yield return new WaitForSeconds(0.1f);
        screenEffect.SetActive(false);
    }
}
