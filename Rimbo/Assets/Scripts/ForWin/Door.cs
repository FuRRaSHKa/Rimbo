using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public GameObject[] doors;
    public GameObject crown;
    public GameObject glasse;
    public GameObject clocl;

    private void Start() {

        EventController.current.onWinCondition += OpenDoor;
        EventController.current.onGetArtifact += GotAnArtefact;

    }

    public void GotAnArtefact(int id) {

        if (id == 0) {

            crown.SetActive(false);


        }

        if (id == 1) {

            glasse.SetActive(false);


        }

        if (id == 2) {

            clocl.SetActive(false);

        
        }

    }

    public void OpenDoor() {

        doors[0].SetActive(false);
        doors[1].SetActive(true);

    }

}
