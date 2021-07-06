using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tided : MonoBehaviour
{

    public int needToLoad;

    private void Start() {

        StartCoroutine(ssss());

    }

    IEnumerator ssss() {

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(needToLoad);

    }

}
