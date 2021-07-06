using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndMind : MonoBehaviour {

    public float maxHealth = 100;
    public float maxMind = 100;

    EnemyPool enemyPool;

    private void Start() {
        enemyPool = GetComponent<EnemyPool>();

    }

    private int HP = 100, MP = 100;


    public int GetHP() {
        return HP;
    }

    public int GetMP() {
        return MP;
    }


    public void MindHealthChange(int m) {

        MP += m;

        if (MP > 100) {

            MP = 100;

        } else if (MP <= 0) {

            MP = 0;

        }

        if (MP >= 33) {

            enemyPool.CheckEnemiesForPriority(1);

        }

        if (MP < maxHealth * 2 / 3) {

            enemyPool.CheckEnemiesForPriority(2);

        }

        if (MP < maxHealth / 3) {

            enemyPool.CheckEnemiesForPriority(1);

        }

    }


    public void HealthChange(int h) {

        HP += h;

        if (HP <= 0) {

            Death();

        } else if (HP > 100) {

            HP = 100;

        }

    }




    public void Death() {

        EventController.current.PlayerDeath();

    }

}
