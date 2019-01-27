using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowWater : MonoBehaviour
{

    public AudioSource audio;

    float lowPos, topPos;
    float lastRockPos;

    void Awake()
    {
        lowPos = transform.position.y;
        topPos = lowPos + 1;
    }
    void Start()
    {
        lastRockPos = Character.instance.rock.transform.position.y;
    }
    void Update()
    {
        float nowRockPos = Character.instance.rock.transform.position.y;
        if (lastRockPos > -0.6f && nowRockPos <= -0.6f)
            audio.Play();
        lastRockPos = nowRockPos;

        float y = Mathf.Lerp(lowPos, topPos, -Character.instance.rock.transform.position.y - 0.6f);
        Vector3 pos = transform.position;
        pos.y = y;
        transform.position = pos;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        print(other);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        print(other);
    }
}
