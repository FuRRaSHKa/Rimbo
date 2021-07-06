using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthPlayer : MonoBehaviour {

    [HideInInspector]
    public bool isInGrass = false;


    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Grass") {

            EventController.current.GrassEnter();

            isInGrass = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.tag == "Grass") {

            isInGrass = false;
            
        }

    }

}
