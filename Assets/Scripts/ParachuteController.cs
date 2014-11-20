using UnityEngine;
using System.Collections;

public class ParachuteController : MonoBehaviour
{
    public GameObject screenEffect;
    public float gravityStep;

    private const string IDLE_TYPE = "idleType";
    private Animator animator;
    private int currentIdleType;
	private PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();  
        currentIdleType = 1;
		playerController = GameObject.Find(transform.parent.name + "/Pilot").GetComponent<PlayerController>();
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "Enemy")
        {
            currentIdleType = Mathf.Clamp(++currentIdleType, 1, 3);
            animator.SetInteger(IDLE_TYPE, currentIdleType);

			PlayerController playerController = GameObject.Find(transform.parent.name + "/Pilot").GetComponent<PlayerController>();
			playerController.normalGravity += -playerController.ironMeshGravity;
			playerController.downGravity += -playerController.ironMeshGravity;
        }
    }

    IEnumerator hideScreenEffect()
    {
        yield return new WaitForSeconds(0.1f);
        screenEffect.SetActive(false);
    }

    public void Reset()
    {
        currentIdleType = 1;
        animator.SetInteger(IDLE_TYPE, 1);
    }

	void FixedUpdate()
	{
        rigidbody2D.AddForce(-playerController.Gravity * rigidbody2D.mass * Time.fixedDeltaTime, ForceMode2D.Impulse);
	}

}
