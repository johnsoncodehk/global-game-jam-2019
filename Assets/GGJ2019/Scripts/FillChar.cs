using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillChar : MonoBehaviour
{

    public Image image;

    void Update()
    {
        float left = 137.261f;
        float right = 139.129f;
        float crtPos = Character.instance.transform.position.x;
        float fill = (crtPos - left) / (right - left);
        image.fillAmount = fill;
    }
}
