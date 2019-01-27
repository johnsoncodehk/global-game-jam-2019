using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char : MonoBehaviour
{

    public Transform child;
    Transform prefab;

    void Awake()
    {
        child = transform.GetChild(0);
        prefab = Instantiate(child);
        prefab.gameObject.SetActive(false);
        prefab.localScale = Vector3.one;
        prefab.GetComponent<SpriteRenderer>().sortingOrder = 10;

        if (gameObject.name == "element_05")
        {
            prefab.gameObject.AddComponent<PolygonCollider2D>();
            var rig = prefab.gameObject.AddComponent<Rigidbody2D>();
            rig.fixedAngle = true;
            prefab.gameObject.layer = LayerMask.NameToLayer("RockHook");
        }
    }
    void OnMouseDown()
    {
        if (!child.gameObject.activeSelf)
            return;

        prefab.gameObject.SetActive(true);
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        prefab.position = pos;
    }
    void OnMouseUp()
    {
        if (!child.gameObject.activeSelf)
            return;

        prefab.gameObject.SetActive(false);
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        if (Vector3.Distance(pos, Character.instance.transform.position + new Vector3(0, 1, 0)) < 1)
        {
            if (gameObject.name == "element_02")
            {
                Character.instance.hat.SetActive(true);
            }
            else if (gameObject.name == "element_03")
            {
                Character.instance.pole.SetActive(true);
            }
            else if (gameObject.name == "element_04")
            {
                Character.instance.wing1.SetActive(true);
                child.gameObject.SetActive(false);
            }
            else if (gameObject.name == "element_04 (1)")
            {
                Character.instance.wing2.SetActive(true);
                child.gameObject.SetActive(false);
            }
        }
        else
        {
            if (gameObject.name == "element_02")
            {
                Character.instance.bow.SetActive(true);
                Character.instance.bow.transform.position = pos;
                Character.instance.bow.transform.localEulerAngles = new Vector3(0, 0, 167.913f);
            }
            else if (gameObject.name == "element_01")
            {
                Character.instance.rock.SetActive(true);
                Character.instance.rock.transform.position = pos;
            }
        }
    }
    void OnMouseDrag()
    {
        if (!child.gameObject.activeSelf)
            return;

        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        if (gameObject.name == "element_05")
        {
            prefab.GetComponent<Rigidbody2D>().MovePosition(pos);
        }
        else
        {
            prefab.position = pos;
        }
    }
}
