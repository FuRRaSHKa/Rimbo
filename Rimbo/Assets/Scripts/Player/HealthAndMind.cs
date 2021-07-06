using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndMind : MonoBehaviour {

    public float maxHealth = 100;
    public float maxMindHealth = 100;

    public float mindDecreasing = 5;

    private float currentHP, currentMP;

    EnemyPool enemyPool;

    private void Start() {

        enemyPool = GetComponent<EnemyPool>();
        StartCoroutine(MindBlowing());

        currentHP = maxHealth;
        currentMP = maxMindHealth;

        MindHealthChange(0);

    }

    private IEnumerator MindBlowing() {

        while (true) {

            yield return new WaitForSeconds(1);

            MindHealthChange(-mindDecreasing);

        }
    }





    public float GetHP() {
        return currentHP;
    }

    public float GetMP() {
        return currentMP;
    }


    public void MindHealthChange(float m) {

        currentMP += m;

        if (currentMP > 100) {

            currentMP = 100;

        } else if (currentMP <= 0) {

            currentMP = 0;

        }


        BarsScript.current.SetMindPoint(currentMP / maxMindHealth);

        if (currentMP < maxMindHealth / 3) {
            enemyPool.CheckEnemiesForPriority(1);
            return;
        }

        if (currentMP < maxMindHealth * 2 / 3) {
            enemyPool.CheckEnemiesForPriority(2);
            return;
        }


        enemyPool.CheckEnemiesForPriority(3);
        return;


    }


    public void HealthChange(int h) {

        currentHP += h;

        if (currentHP <= 0) {

            Death();

        } else if (currentHP > 100) {

            currentHP = 100;

        }

        BarsScript.current.SetHealthPoint(currentHP / maxHealth);

    }




    public void Death() {

        EventController.current.PlayerDeath();

    }

}
