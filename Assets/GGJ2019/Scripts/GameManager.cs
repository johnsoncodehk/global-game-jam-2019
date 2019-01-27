using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Animator homeAnimator, home2Animator;
    public Character character, character2;
    public Swift swift;

    bool nextHumanGo = false;

    IEnumerator Start()
    {
        character2.ai = true;

        character.ai = true;
        swift.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);
        homeAnimator.Play("Open Door");
        yield return new WaitForSeconds(0.5f);
        character.aiHorizontal = 1;
        yield return new WaitForSeconds(2.6f);
        character.aiHorizontal = 0;
        homeAnimator.Play("Close Door");
        yield return new WaitForSeconds(2.5f);

        swift.gameObject.SetActive(true);
        character.ai = false;
    }
    void Update()
    {
        if (!nextHumanGo)
        {
            if (Character.instance.transform.position.x > 125)
            {
                nextHumanGo = true;
                StartCoroutine(NextHumanGo());
            }
        }
    }
    IEnumerator NextHumanGo()
    {
        yield return new WaitForSeconds(1);
        home2Animator.Play("Open Door");
        yield return new WaitForSeconds(0.5f);
        character2.aiHorizontal = 1;
        yield return new WaitForSeconds(2.6f);
        home2Animator.Play("Close Door");
    }
}
