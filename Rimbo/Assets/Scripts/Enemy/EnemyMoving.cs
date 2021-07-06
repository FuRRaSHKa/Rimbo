using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMoving : MonoBehaviour {

    public float waitingTime = 2f;
    public float speed;
    public float angularSpeed;
    public float minDistance = 1f;
    public float pathfindingRate = .5f;

    public Transform[] waypoints;

    private Rigidbody2D rgbd2D;
    private Rigidbody2D player;

    private Vector2 direction;
    private Vector2 currentRotation;
    private Vector2 sightPosition;
   
    private Transform target;

    private bool goToSight = false;
    private bool isHunting = false;
    private int waypointIndex = 0;


    //All for pathfinding
    private Seeker seeker;
    private Path path;
    private int currentPathPoint = 0;
    private bool isReachedEnd = false;
    private bool isWaiting = false;

    private void Start() {

        seeker = GetComponent<Seeker>();

        StartCoroutine(Pathfindng());

        target = waypoints[waypointIndex];
    }

    void onPathComplete(Path path) {

        if (!path.error) {
        
            this.path = path;
        
        }

    }

    public void SetProperties(Rigidbody2D rgbd2D, Rigidbody2D player) {

        this.rgbd2D = rgbd2D;
        this.player = player;

    }

    public void Update() {

        currentRotation = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.PI / 180), Mathf.Sin(transform.eulerAngles.z * Mathf.PI / 180)).normalized;

        if (isWaiting)
            return;


        PathfindingCheck();

        if (goToSight && isReachedEnd) {

            StartCoroutine(Wait(waitingTime));
            
            return;

        }

        if (!isHunting && isReachedEnd) {

            waypointIndex = ++waypointIndex % waypoints.Length;
            currentPathPoint = 0;
            target = waypoints[waypointIndex];
        }

    }

    private void PathfindingCheck() {



        if (path == null)
            return;

        if (currentPathPoint >= path.vectorPath.Count) {

            isReachedEnd = true;
            
            return;

        } else {

            isReachedEnd = false;

        }

        
        direction = ((Vector2)path.vectorPath[currentPathPoint] - rgbd2D.position).normalized;

        if (Vector2.Distance(transform.position, path.vectorPath[currentPathPoint]) < minDistance) {

            currentPathPoint++;

        }


    }

    private void FixedUpdate() {

        if (isWaiting)
            return;

        PhysicsRotating();

        rgbd2D.AddForce(direction * speed / 15, ForceMode2D.Impulse);

        if (rgbd2D.velocity.sqrMagnitude > speed * speed) {

            rgbd2D.velocity = rgbd2D.velocity.normalized * speed;

        }

    }

    //Rotation by physics
    private void PhysicsRotating() {

        if (rgbd2D.velocity != Vector2.zero) {

            float angle = Vector2.SignedAngle(currentRotation, target.position - transform.position);

            if (angle > 0) {

                if (angle < angularSpeed * Time.fixedDeltaTime) {
                    rgbd2D.rotation += angle;
                    return;
                }

                rgbd2D.rotation += angularSpeed * Time.fixedDeltaTime;


            } else if (angle < 0) {

                if (angle > -angularSpeed * Time.fixedDeltaTime) {
                    rgbd2D.rotation += angle;
                    return;
                }

                rgbd2D.rotation += -angularSpeed * Time.fixedDeltaTime;

            }

        }

    }

    public void StopHunting() {

        isHunting = false;

    }

    public void StartHunting() {

        isHunting = true;

        isWaiting = false;

        target = player.transform;

        seeker.StartPath(rgbd2D.position, target.position, onPathComplete);

    }

    public void GoToSight(Vector2 sightPosition) {

        goToSight = true;

        isWaiting = false;

        this.sightPosition = sightPosition;

    }


    IEnumerator Pathfindng() {

        while (true) {

            yield return new WaitForSeconds(pathfindingRate);

            seeker.StartPath(rgbd2D.position, target.position, onPathComplete);


        }

    }

    IEnumerator Wait(float waitingTime) {

        isWaiting = true;

        yield return new WaitForSeconds(waitingTime);

        if(isWaiting)
            target = waypoints[waypointIndex];

    }

}
