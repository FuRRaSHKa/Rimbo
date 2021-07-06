using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// The RequireComponent attribute automatically adds required components as dependencies
// EnemyController requires the GameObject to have a EnemyMoving component
[RequireComponent(typeof(EnemyMoving))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyController : MonoBehaviour {

    public float maxHearRange = 15f;
    public float viewAngle = 60;
    public float checkRate = 0.5f;
    public float maxRange = 20f;
    public LayerMask obstacleLayer;

    public bool isHunting = false;

    private Rigidbody2D rgbd2D;
    private Rigidbody2D player;
    private EnemyMoving enemyMoving;



    private void Start() {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        enemyMoving = GetComponent<EnemyMoving>();

        rgbd2D = GetComponent<Rigidbody2D>();

        enemyMoving.SetProperties(rgbd2D, player);

        StartCoroutine(Checking());

        EventController.current.onBoostSound += GetSound;

        EventController.current.onGrassEnter += PlayersGrass;

    }

    public void GetSound(Vector2 position) {

        Vector2 soundDirection = position - rgbd2D.position;

        float magnitude = soundDirection.magnitude;

        if (magnitude > maxHearRange)
            return;


        RaycastHit2D raycastHit2D = Physics2D.Raycast(rgbd2D.position, soundDirection.normalized, magnitude, obstacleLayer);

        if (!raycastHit2D) {
            enemyMoving.GoToSight(position);
        }

    }


    public bool RaycastCheck() {

        Vector2 direction = player.position - rgbd2D.position;

        float magnitude = direction.magnitude;

        if (magnitude < maxRange) {

            Vector2 rotVector = new Vector2(Mathf.Sin(transform.eulerAngles.z * Mathf.PI / 180), Mathf.Cos(transform.eulerAngles.z * Mathf.PI / 180));

            if (Mathf.Abs(Vector2.Angle(rotVector, direction.normalized)) < viewAngle / 2) {

                RaycastHit2D raycastHit2D = Physics2D.Raycast(rgbd2D.position, direction.normalized, magnitude, obstacleLayer);


                if (!raycastHit2D)
                    return true;

            }



        }


        return false;

    }

    public void PlayersGrass() {

        if (!RaycastCheck() && isHunting) {

            enemyMoving.StopHunting();

            isHunting = false;

        }

    }

    IEnumerator Checking() {

        StealthPlayer stealthPlayer = player.GetComponent<StealthPlayer>();

        while (true) {

            yield return new WaitForSeconds(checkRate);

            if (isHunting) {

                if ((player.position - rgbd2D.position).sqrMagnitude > maxRange * maxRange) {

                    isHunting = false;
                    enemyMoving.StopHunting();

                }

                continue;

            }

            if (!stealthPlayer.isInGrass) {

                if (RaycastCheck()) {

                    enemyMoving.StartHunting();
                    isHunting = true;

                }

            }

        }

    }

}
