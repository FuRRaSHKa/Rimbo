using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeUnrealSheet : MonoBehaviour
{
    public float rangeOfInvisibility = 5;
    private GameObject player;
    private Renderer render;
    private float maxTimeOfCheck = 2f;
    private float currentTimeOfCheck = 2;
    private float minTimeOfCheck = 0.1f;

    void Start()
    {
        render = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(CheckRangeToPlayer());
    }

    IEnumerator CheckRangeToPlayer()
    {
        while (true)
        {
            float range = (transform.position - player.transform.position).magnitude;
            if (range >= 20)
            {
                yield return new WaitForSeconds(maxTimeOfCheck);
            }
            else if(range > rangeOfInvisibility)
            {
                if (!render.enabled)
                    render.enabled = true;
                currentTimeOfCheck = (maxTimeOfCheck * (range - rangeOfInvisibility) / 15) + minTimeOfCheck;
                yield return new WaitForSeconds(currentTimeOfCheck);
            }else if(range <= rangeOfInvisibility)
            {
                if(render.enabled)
                    render.enabled = false;
                yield return new WaitForSeconds(minTimeOfCheck);
            }
        }
    }
}
