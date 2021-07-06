using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pills : MonoBehaviour {

    public int healedMind = 10;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player") {

            collision.GetComponent<HealthAndMind>().MindHealthChange(healedMind);

            gameObject.SetActive(false);
        
        }

    }


}
