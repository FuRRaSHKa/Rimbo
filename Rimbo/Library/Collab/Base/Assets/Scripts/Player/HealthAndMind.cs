using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndMind : MonoBehaviour {

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

        if(MP < 66 && MP + m >= 66)
        {
            enemyPool.CheckEnemiesForPriority(3);
        }else if((MP < 33 && MP + m >= 33) ||( MP >= 66 && MP + m < 66))
        {
            enemyPool.CheckEnemiesForPriority(2);
        }else if(MP>= 33 && MP + m < 33)
        {
            enemyPool.CheckEnemiesForPriority(1);
        }


        MP += m;
        if (MP > 100) {
            MP = 100;
        } else if (MP <= 0) {
            MP = 0;
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

    }

}
