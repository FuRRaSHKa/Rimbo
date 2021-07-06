using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPool : MonoBehaviour {

    public int heightOfSpawn = 5;
    public int wigthOfSpawn = 5;
    public int ghostCount = 10;

    public float visibleRange = 15f;

    public GameObject ghostPrefab;
    private EnemyPool enemyPool;
    private GameObject[] allGhosts;

    void Start() {
       
        enemyPool = GetComponent<EnemyPool>();
        allGhosts = new GameObject[ghostCount];
       
        for (int i = 0; i != ghostCount; i++) {
            allGhosts[i] = Instantiate(ghostPrefab);
            allGhosts[i].SetActive(false);
        }


        EventController.current.onWinCondition += StartSpawnGhosts;

    }

    public void StartSpawnGhosts() {

        StartCoroutine(SpawnGhosts());

    }

    IEnumerator SpawnGhosts() {

        

        while (true) {
             
            foreach (GameObject ghost in allGhosts) {
            
                if ((ghost.transform.position - transform.position).sqrMagnitude >= visibleRange * visibleRange) {
                    ghost.SetActive(false);
                }

                if (!ghost.activeSelf) {
                    
                    ghost.transform.position = new Vector3(transform.position.x + Random.Range(-wigthOfSpawn, wigthOfSpawn), transform.position.y + Random.Range(-heightOfSpawn, heightOfSpawn), -1);
                    SomeUnrealSheet s = ghost.GetComponent<SomeUnrealSheet>();
                    ghost.SetActive(true);
                    s.SetZero();
                }

                yield return null;

            }

            yield return null;

        }
    
    }

}
