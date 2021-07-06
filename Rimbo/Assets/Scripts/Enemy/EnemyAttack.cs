using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float currentMeeleAttackRefresh = 0;
    public float meeleAttackRange = 0.5f;
    public float meeleAttackRefresh = 1f;//кд в секундах

    private Rigidbody2D player;
    private Rigidbody2D rgbd2D;
    EnemyController controller;

    // Start is called before the first frame update
    void Start() {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        rgbd2D = GetComponent<Rigidbody2D>();
        controller = GetComponent<EnemyController>();

    }

    // Update is called once per frame
    void Update() {

        MeeleAttack();

    }



    private void MeeleAttack() {

        if (currentMeeleAttackRefresh <= 0) {

            if ((player.position - rgbd2D.position).sqrMagnitude <= meeleAttackRange * meeleAttackRange) {

                    player.GetComponent<HealthAndMind>().HealthChange(-50);
                    currentMeeleAttackRefresh = meeleAttackRefresh;
                    Debug.Log(false);

            } 
        
        } else {

            currentMeeleAttackRefresh -= Time.deltaTime;

        }

    }

}