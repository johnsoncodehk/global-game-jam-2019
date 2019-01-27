using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swift : MonoBehaviour
{

    public List<CheckPoint> checkPoints;
    public Animator animator;

    CheckPoint nextPoint;
    float v;
    Vector2 v2;

    void Awake()
    {
        GotoNextCheckPoint();
    }
    void Update()
    {
        if (!nextPoint)
            return;

        if (checkPoints.Count == 0)
        {
            transform.position = Vector2.SmoothDamp(transform.position, nextPoint.transform.position, ref v2, 1f);

            animator.SetFloat("dx", 100);
        }
        else
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.SmoothDamp(pos.x, nextPoint.transform.position.x, ref v, 1f);
            transform.position = pos;

            animator.SetFloat("dx", Mathf.Abs(pos.x - nextPoint.transform.position.x));
        }

        if (Character.instance.transform.position.x > nextPoint.transform.position.x)
        {
            if (nextPoint.cation)
                nextPoint.cation.SetActive(true);
            nextPoint = null;
            GotoNextCheckPoint();
        }
    }
    void GotoNextCheckPoint()
    {
        if (checkPoints.Count > 0)
        {
            nextPoint = checkPoints[0];
            checkPoints.RemoveAt(0);
        }
    }
}
