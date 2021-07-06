using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DedEndulsa : MonoBehaviour {

    public int SceneToload;


    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player")
            SceneManager.LoadScene(SceneToload);

    }


}
