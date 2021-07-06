using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;



public class PlayerController : MonoBehaviour {

    public float soundRate = 1f;
    public float maxMovmentSpeed = 0.5f;
    public float rotatingSpeed = 2f; // Скорость поворота в градусах
    public float lightRadius = 1f;
    public float boostTime = 2f; // Ускорение в секундах
    public float minDistance = 1f;

    private float currentSpeed;
    private float currentRotatingSpeed;
    private float currentBoost = 0; // нынешний заряд ускорения

    private bool isBoosting = false;

    private Vector2 movingDirection = Vector2.zero;
    private Vector2 rotateDirection = Vector2.zero;
    private Rigidbody2D rgbd2d;

    private Transform GFX;

    private Vector2 target;


    // Start is called before the first frame update
    void Start() {

        currentSpeed = maxMovmentSpeed;

        currentRotatingSpeed = rotatingSpeed;

        GFX = gameObject.transform.GetChild(0);

        rgbd2d = GetComponent<Rigidbody2D>();

        target = transform.position;

    }


    private void OnDrawGizmos() {

        Gizmos.DrawLine(transform.position, (Vector3)movingDirection + transform.position);

    }


    private void Update() {

        if (Input.GetAxisRaw("Fire1") == 1) {

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }

        rotateDirection = (target - rgbd2d.position).normalized;

    }


    private void FixedUpdate() {

        Movement();

        Boost();

    }

    private void Movement() {



        float angle;

        movingDirection = new Vector2(Mathf.Cos(rgbd2d.rotation), Mathf.Sin(rgbd2d.rotation));

        if ((target - rgbd2d.position).sqrMagnitude < minDistance * minDistance) {

            rgbd2d.velocity = Vector2.zero;

            return;

        }

        rgbd2d.velocity = movingDirection * currentSpeed;

        angle = Vector2.SignedAngle(movingDirection, rotateDirection);

        if (Mathf.Abs(angle) <= currentRotatingSpeed) {

            rgbd2d.rotation += angle * Time.fixedDeltaTime;

        } else if (angle < 0) {

            rgbd2d.rotation -= currentRotatingSpeed * Time.fixedDeltaTime;

        } else if (angle > 0) {

            rgbd2d.rotation += currentRotatingSpeed * Time.fixedDeltaTime;

        }

        GFX.eulerAngles = new Vector3(0, 0, rgbd2d.rotation / Mathf.PI * 180);

    }




    private void Boost() {

        if (Input.GetAxisRaw("Fire3") == 1 && currentBoost >= 0) {

            currentBoost -= Time.fixedDeltaTime;
            currentSpeed = maxMovmentSpeed * 2;
            currentRotatingSpeed = rotatingSpeed * 2;

        } else {

            if (currentBoost <= boostTime) {

                currentBoost += Time.fixedDeltaTime;

            }

            currentSpeed = maxMovmentSpeed;
            currentRotatingSpeed = rotatingSpeed;

        }

    }



    private IEnumerator Boosting() {

        while (true) {

            yield return new WaitForSeconds(soundRate);

            if (isBoosting) {

                EventController.current.BoostSount(rgbd2d.position);

            }

        }

    }

}
