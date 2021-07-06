using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ceneLoad : MonoBehaviour
{
    public void LoadSCENEBLYAT(int needToLoad) {

        SceneManager.LoadScene(needToLoad);    
    
    }

}
