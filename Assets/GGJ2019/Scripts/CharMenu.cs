using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMenu : MonoBehaviour
{

    public Transform character;
    public Animator animator;

    public bool show
    {
        get { return animator.GetBool("Show"); }
        set { animator.SetBool("Show", value); }
    }

    void Update()
    {
        transform.position = character.position;
        if (Input.GetButtonDown("Jump"))
        {
            show = true;
            Character.instance.hat.SetActive(false);
            Character.instance.pole.SetActive(false);
            Character.instance.wing1.SetActive(false);
            Character.instance.wing2.SetActive(false);
            // Character.instance.bow.SetActive(false);
            foreach (Char c in GetComponentsInChildren<Char>())
            {
                c.child.gameObject.SetActive(true);
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            show = false;
        }
    }
}
