using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharBall : MonoBehaviour
{

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, Character.instance.transform.position) < 3)
        {
            enabled = false;
            animator.Play("Hide");
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}
