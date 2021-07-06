using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathLoad : MonoBehaviour {

    public int needToLoad;

    private void Start() {

        EventController.current.onPlayerDeath += LoadScene;

    }

    public void LoadScene() {

        SceneManager.LoadScene(needToLoad);

    }

}
