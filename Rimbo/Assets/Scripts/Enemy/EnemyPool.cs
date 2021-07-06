using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour {
    public float visibleRange;


    private bool allEnemiesRemove = true;

    private EnemyController[] allEnemyController;
    private EnemyMoving[] allEnemyMooving;
    private Rigidbody2D[] rgbds;

    void Start() {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        allEnemyController = new EnemyController[enemies.Length];
        allEnemyMooving = new EnemyMoving[enemies.Length];
        rgbds = new Rigidbody2D[enemies.Length];

        for (int i = 0; i < enemies.Length; i++) {

            allEnemyController[i] = enemies[i].GetComponent<EnemyController>();
            allEnemyMooving[i] = enemies[i].GetComponent<EnemyMoving>();
            rgbds[i] = enemies[i].GetComponent<Rigidbody2D>();

        }

        StartCoroutine(CheckEnemiesForRange());

        EventController.current.onWinCondition += RemoveAllEnemies;

    }

    IEnumerator CheckEnemiesForRange() {

        yield return new WaitForSeconds(1f);

        while (true) {
            for (int i = 0; i < allEnemyMooving.Length; i++) {

                if ((allEnemyMooving[i].transform.position - transform.position).sqrMagnitude >= visibleRange * visibleRange) {

                    allEnemyMooving[i].enabled = false;
                    allEnemyController[i].enabled = false;
                    rgbds[i].velocity = Vector2.zero;

                } else {

                    allEnemyMooving[i].enabled = true;
                    allEnemyController[i].enabled = true;

                }
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(3);
        }
    }

    public void CheckEnemiesForPriority(int threshold) {

        if (allEnemiesRemove) {

            for (int i = 0; i < allEnemyController.Length; i++) {

                if (allEnemyController[i].GetComponent<EnemyPriority>().priority < threshold) {
                    if (allEnemyController[i].enabled) {
                        allEnemyController[i].enabled = false;
                        allEnemyMooving[i].enabled = false;
                        allEnemyController[i].transform.GetChild(0).gameObject.SetActive(false);
                        allEnemyController[i].GetComponent<EnemyAttack>().enabled = false;
                    }
                } else {
                    if (!allEnemyController[i].enabled) {
                        allEnemyController[i].GetComponent<EnemyAttack>().enabled = true;
                        allEnemyController[i].enabled = true;
                        allEnemyMooving[i].enabled = true;
                        allEnemyController[i].transform.GetChild(0).gameObject.SetActive(true);
                    }
                }

            }

        }

    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, visibleRange);
    }

    public void RemoveAllEnemies() {

        allEnemiesRemove = false;

        foreach (EnemyMoving enemeies in allEnemyMooving) {
            enemeies.gameObject.SetActive(false);
        }

    }
}
