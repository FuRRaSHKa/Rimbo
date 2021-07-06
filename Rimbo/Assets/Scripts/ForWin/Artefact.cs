using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artefact : MonoBehaviour {
    public int id = 1;




    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.transform.tag == "Player") {

            EventController.current.GetArtefact(id);
            gameObject.SetActive(false);

        }

    }

}
