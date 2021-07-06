using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeUnrealSheet : MonoBehaviour {
    public float maxRange = 10f;
    public float rangeOfInvisibility = 5;

    private GameObject player;

    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update() {

        float range = (transform.position - player.transform.position).magnitude;

        if (range < maxRange) {

            if (range > rangeOfInvisibility) {

                if (!spriteRenderer.enabled)
                    spriteRenderer.enabled = true;

                float cf = (range - rangeOfInvisibility) / (maxRange - rangeOfInvisibility);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.b, spriteRenderer.color.g, cf);
            } else {

                spriteRenderer.enabled = false;

            }

        } else {

            if (spriteRenderer.color.a < 1) {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.b, spriteRenderer.color.g, 1);
            }

        }

    }
}