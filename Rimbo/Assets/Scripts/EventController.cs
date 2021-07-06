using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class EventController : MonoBehaviour {

    public static EventController current;

    private void Awake() {

        if (current != null) {

            Destroy(this);
            return;

        }

        current = this;

    }

    public event Action<Vector2> onBoostSound;
    public void BoostSount(Vector2 position) {

        if (onBoostSound != null) {

            onBoostSound(position);

        }

    }

    public event Action onGrassEnter;
    public void GrassEnter() {

        if (onGrassEnter != null) {

            onGrassEnter();

        }

    }

    public event Action<int> onGetArtifact;
    public void GetArtefact(int id) {

        if (onGetArtifact != null) {

            onGetArtifact(id);

        }

    }

    public event Action onWinCondition;
    public void GotWinCondition() {

        if (onWinCondition != null) {

            onWinCondition();

        }

    }

    public event Action onPlayerDeath;
    public void PlayerDeath() {

        if (onPlayerDeath != null) {

            onPlayerDeath();

        }

    }

}
