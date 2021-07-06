using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AudioController : MonoBehaviour
{

    public AudioClip[] emptyZoneEffects; // нумерация зон: 0.пустая, 1.риф, 2.город, 3.вулкан
    public AudioClip[] zonesBack;

    private AudioSource audio;

    private int indexOfCurrentZone = 0;
    private int indexOfNextZone = 0;

    private float currentTime = 0;

    private bool fade = false;
    private bool fadeDirection = true;

    public int minRandomTime;
    public int maxRandomTime;
    public float fadeTime;
    public float maxVolume;
    

    

    void Start()
    {
        currentTime = Random.Range(minRandomTime, maxRandomTime);
        audio = GetComponent<AudioSource>();
        audio.volume = 0;
        audio.Play();
        GoToZone(0);
    }

    void Update()
    {
        if(indexOfCurrentZone == 0)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                audio.PlayOneShot(emptyZoneEffects[Random.Range(0, emptyZoneEffects.Length -1)], maxVolume);


                currentTime = Random.Range(minRandomTime, maxRandomTime);
            }
        }

        Fade();





        //        audio.PlayOneShot(emptyZoneEffects[3]);
        

    }

    void Fade()
    {
        if (fade)
        {
            if (fadeDirection)
            {
                if (audio.volume < maxVolume)
                {
                    audio.volume += Time.deltaTime / fadeTime * maxVolume;
                }
                else
                {
                    fade = false;
                    audio.volume = maxVolume;
                    indexOfCurrentZone = indexOfNextZone;
                    fadeDirection = false;
                }
            }
            else
            {
                if (audio.volume > 0)
                {
                    audio.volume -= Time.deltaTime / fadeTime * maxVolume;
                }
                else
                {
                    audio.clip = zonesBack[indexOfNextZone];
                    audio.Play();
                    fadeDirection = true;
                }

            }
        }
    }





    public void GoToZone(int index)
    {
        if(index!= indexOfCurrentZone)
        {
            if (index == 0)
                maxVolume = 1;
            else
                maxVolume = 0.4f;
            fade = true;
            fadeDirection = false;
            indexOfNextZone = index;
        }
    }


}
