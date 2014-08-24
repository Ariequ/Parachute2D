using UnityEngine;
using System.Collections;

public class ParachuteController : MonoBehaviour
{
    public GameObject screenEffect;

    private const string IDLE_TYPE = "idleType";
    private Animator animator;
    private int currentIdleType;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();  
        currentIdleType = 1;
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "Enemy")
        {
            Debug.Log(other.name);
    
            currentIdleType = Mathf.Clamp(++currentIdleType, 1, 3);
            Debug.Log(currentIdleType);
            animator.SetInteger(IDLE_TYPE, currentIdleType);
            Physics2D.gravity += new Vector2(0, -5);
			iTween.ShakePosition(Camera.main.gameObject,iTween.Hash("y",0.1f,"time",0.1f));

            screenEffect.SetActive(true);
            StartCoroutine(hideScreenEffect());
        }
    }

    IEnumerator hideScreenEffect()
    {
        yield return new WaitForSeconds(0.1f);
        screenEffect.SetActive(false);
    }
}
