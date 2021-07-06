using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForSound : MonoBehaviour
{
    public int indexOfZone;
    AudioController audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioController>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            audio.GoToZone(indexOfZone);
        }
    }
}
