﻿using UnityEngine;
using System.Collections;

public class LovelyCrowController : MonoBehaviour {

    Animator animator;
    float startFlytime;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.speed = 0.3f;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pilot")
        {
//            animator.SetTrigger("PlayerEnter");
            startFlytime = Time.time;
            StartCoroutine(flyAway());
        }
    }

    IEnumerator flyAway()
    {
        while(Time.time - startFlytime < 3)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 5);
            animator.speed += Time.deltaTime * 2;
            yield return 0;
        }

        Destroy(gameObject);
    }
}