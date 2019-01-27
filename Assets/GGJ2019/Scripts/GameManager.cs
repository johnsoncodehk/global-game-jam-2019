using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Animator homeAnimator;
    public Character character;
    public Swift swift;

    IEnumerator Start()
    {
        character.ai = true;
        swift.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);
        homeAnimator.Play("Open Door");
        yield return new WaitForSeconds(0.5f);
        character.aiHorizontal = 1;
        yield return new WaitForSeconds(2);
        character.aiHorizontal = 0;
        homeAnimator.Play("Close Door");
        yield return new WaitForSeconds(2.5f);

        swift.gameObject.SetActive(true);
        character.ai = false;
    }
}
