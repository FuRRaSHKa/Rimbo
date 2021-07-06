using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine;

public class SomeUnrealSheet : MonoBehaviour {
    public float maxRange = 10f;
    public float rangeOfInvisibility = 5;

    private GameObject player;

    private float intensity;

    private SpriteRenderer spriteRenderer;
    private UnityEngine.Experimental.Rendering.Universal.Light2D light2D;

    private void Awake() {
        light2D = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");



        intensity = light2D.intensity;
    }

    void Start() {

        

        

    }

    public void SetZero() {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.b, spriteRenderer.color.g, 0);
        light2D.intensity = 0;
    }

    private void Update() {

        float range = (transform.position - player.transform.position).magnitude;

        if (range < maxRange) {

            if (range > rangeOfInvisibility) {

                if (!spriteRenderer.enabled)
                    spriteRenderer.enabled = true;

                float cf = (range - rangeOfInvisibility) / (maxRange - rangeOfInvisibility);
                float currentIntensity = ((range - rangeOfInvisibility) / (maxRange - rangeOfInvisibility)) * intensity;

                light2D.intensity = currentIntensity;

                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.b, spriteRenderer.color.g, cf);

            } else {

                light2D.intensity = 0;

                spriteRenderer.enabled = false;

            }

        } else {

            if (spriteRenderer.color.a < 1) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.b, spriteRenderer.color.g, 1);
            }

        }

    }
}