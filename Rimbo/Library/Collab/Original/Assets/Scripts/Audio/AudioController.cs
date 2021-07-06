using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Audiolist {

    public AudioClip[] audio;

}


public class AudioController : MonoBehaviour
{

    public Audiolist[] zonesEffects; // нумерация зон: 0.пустая, 1.риф, 2.город, 3.вулкан
    public AudioClip[] zonesBack;

    private AudioSource audio;

    private int indexOfCurrentZone = 0;
    private int indexOfNextZone = 0;


    private bool fade = false;
    private bool fadeDirection = true;


    public float fadeTime;
    private float testTime = 5;
    

    

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = 0;
        audio.Play();
        GoToZone(0);
    }

    void Update()
    {
        //        audio.PlayOneShot(emptyZoneEffects[3]);
        if (fade)
        {
            if (fadeDirection)
            {
                if(audio.volume < 1)
                {
                    audio.volume += Time.deltaTime / fadeTime;
                }
                else
                {
                    fade = false;
                    audio.volume = 1;
                    fadeDirection = false;
                }
            }
            else
            {
                if (audio.volume > 0)
                {
                    audio.volume -= Time.deltaTime / fadeTime;
                }
                else
                {
                    audio.volume = 0;
                    audio.clip = zonesBack[indexOfNextZone];
                    audio.Play();
                    fadeDirection = true;
                }

            }
        }

    }





    void GoToZone(int index)
    {
        fade = true;
        fadeDirection = false;
        indexOfNextZone = 0;
    }


}
