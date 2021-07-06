using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour {

    public int neeedofArtefact = 3;

    private int currentArtefact = 0;

    private void Start() {

        EventController.current.onGetArtifact += GetArtefact;

    }

    public void GetArtefact(int id) {

        currentArtefact += 1;

        if (currentArtefact >= neeedofArtefact) {

            EventController.current.GotWinCondition();

        }
    }

}
