﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float xSpeed = 5;
    public float ySpeed = 5;
    private Animator animator;
    private float lastSpeedX;
    public GameObject right;
    public GameObject left;
    private float lastTouchTime = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector2 v = rigidbody2D.velocity;
//        v.y = ySpeed;
        if (Input.GetMouseButtonDown(0))
        {
            lastTouchTime = Time.time;
            Vector3 localScale = transform.localScale;

            if (Input.mousePosition.x < Screen.width / 2)
            { 
                v.x = -xSpeed;

                rigidbody2D.velocity = v;
                localScale.x = -1;
                left.SetActive(true);
                StartCoroutine(hideleft());
            }
            else
            {
                v.x = xSpeed;
                rigidbody2D.velocity = v;
                localScale.x = 1;
                right.SetActive(true);
                StartCoroutine(hideright());
            }

            transform.localScale = localScale;
        }

        if (Time.time - lastTouchTime <= 0.5)
        {
            animator.SetFloat("SpeedX", 1);
        } else
        {
            animator.SetFloat("SpeedX", 0);
        }
    }

    IEnumerator hideleft()
    {
        yield return new WaitForSeconds(0.05f);
        left.SetActive(false);
    }

    IEnumerator hideright()
    {
        yield return new WaitForSeconds(0.05f);
        right.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            animator.SetBool("die", true);

            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            rigidbody.isKinematic = true;

            GameObject go = GameObject.FindGameObjectWithTag("Parachute");
            if (go != null)
            {
                go.SetActive(false);
            }
        }
    }

    public void OnDieAniamtionEnd()
    {
        GameObject.Find("GameController").SendMessage("EndGame");
    }

}
