using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    public bool isRespawnable = false;
    public float RespawnTime = 5.0f;
    private bool isDead = false;
    private Vector3 StartPosition;
    //Movement
    public float MoveSpeed = 1;
    public Transform[] PatrolPoints;
    private int currentPatrolPoint = 0;

    //Attack
    private GameObject Target;
    private float NextTimeToFire = 0.0f;
    

    public float AttackRate = 1.0f;
    public float AttackRange = 5.0f;
    public LayerMask TargetMask;
    public GameObject BulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GoToNextPoint();
        StartPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead) {
            return;
        }
        SearchTarget();

        if (Target) {
            //Debug.Log(Target.name);
            if (BulletPrefab && Time.time > NextTimeToFire) {
                Shoot();
            }


        }
        else {
            Move();
        }
        
        if (Vector2.Distance(transform.position, PatrolPoints[currentPatrolPoint].position) < 0.05) {
            GoToNextPoint();
        }
    }


    void GoToNextPoint() {
        if (PatrolPoints.Length == 0) {
            return;
        }

        currentPatrolPoint = (currentPatrolPoint + 1) % PatrolPoints.Length;
    }

    private void Move() {
        if (PatrolPoints.Length == 0) {
            return;
        }
        if (PatrolPoints[currentPatrolPoint].position.x < transform.position.x) {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else {
            this.GetComponent<SpriteRenderer>().flipX = false;
        } 
        transform.position = Vector2.MoveTowards(transform.position, PatrolPoints[currentPatrolPoint].position, Time.deltaTime * MoveSpeed / 10);
    }

    private void SearchTarget() {
        Collider2D inRangeEnemy = Physics2D.OverlapCircle(this.transform.position, AttackRange, TargetMask);
        if (inRangeEnemy) {
            PlayerMovement pm = inRangeEnemy.GetComponent<PlayerMovement>();
            if (pm && !pm.isvanish) {
                Target = inRangeEnemy.gameObject;
                //Debug.Log(Target.name);
            }


        }
        else {
            Target = null;
            //Debug.Log("Searching");
        }
        
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(this.transform.position, AttackRange);
    }

    void Shoot() {
        Transform FirePoint = transform.Find("FirePoint");


        //Calculate Rotation
        Vector2 Diff = Target.transform.position - transform.position;
        Diff.Normalize();
        float rotZ = Mathf.Atan2(Diff.y, Diff.x) * Mathf.Rad2Deg; //get the angle of difference vector
         

        GameObject clone =  Instantiate(BulletPrefab, FirePoint.position, Quaternion.Euler(0f, 0f, rotZ)) as GameObject; //create bullet
        clone.transform.parent = FirePoint;

        //Set TrackTarget
        clone.GetComponent<Bullet>().SetTrackTarget(Target);

        NextTimeToFire = Time.time + 1.0f / AttackRate; //Set next shoot time
    }

    public IEnumerator Kill() {
        if (!isRespawnable) {
            Destroy(this.gameObject);
            yield return 0;
        }
        else {
            isDead = true;
            this.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(RespawnTime);
            isDead = false;
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.transform.position = StartPosition;
        }

    }

}
