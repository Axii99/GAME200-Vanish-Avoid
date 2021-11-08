using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackEnemy : MonoBehaviour
{

    public bool isRespawnable = false;
    public float RespawnTime = 5.0f;
    private bool isDead = false;
    private Vector3 StartPosition;
    //Movement
    public float MoveSpeed = 15;
    public Transform[] PatrolPoints;
    private int currentPatrolPoint = 0;

    //Attack
    [SerializeField]
    private GameObject Target;
    public float AttackRange = 5.0f;
    public LayerMask TargetMask;
    public int DamageAmount = -10;
    public float AttackRate = 1.0f;
    private float NextTimeToAttack = 0.0f;

    // Start is called before the first frame update
    void Start() {
        GoToNextPoint();
        StartPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (isDead) {
            return;
        }
        //Target = null;
        SearchTarget();

        if (Target) {
            //Debug.Log(Target.name);
            Track();

        }
        else {
            Move();
        }

        if (PatrolPoints.Length> 0 && Vector2.Distance(transform.position, PatrolPoints[currentPatrolPoint].position) < 0.05) {
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
        //PlayerMovement pm = inRangeEnemy.GetComponent<PlayerMovement>();
        if (inRangeEnemy && !inRangeEnemy.GetComponent<PlayerMovement>().isvanish) {
          
                Target = inRangeEnemy.gameObject;
             
        }
        else {
            Target = null;
            //Debug.Log("Searching");
        }

    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(this.transform.position, AttackRange);
    }

    void Track() {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * MoveSpeed / 10);

    }

    private void OnTriggerStay2D(Collider2D hitInfo) {
        if (isDead) {
            return;
        }
        if (hitInfo.gameObject.tag == "Player" && !hitInfo.gameObject.GetComponent<PlayerMovement>().isvanish) {
            if (NextTimeToAttack < Time.time) {
                hitInfo.gameObject.GetComponent<PlayerCharacter>().AddEnergy(DamageAmount);
                hitInfo.gameObject.GetComponent<PlayerMovement>().isDamagedinside = false;
                hitInfo.gameObject.GetComponent<PlayerMovement>().isDamaged = true;
                hitInfo.gameObject.GetComponent<PlayerMovement>().timeSpentInvincible = 0f;
                NextTimeToAttack = Time.time + 1.0f / AttackRate;
            }
           
        }

    }

    public IEnumerator Kill() {
        Debug.Log("Kill");
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
