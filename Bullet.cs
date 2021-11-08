using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 15;
    public float LifeTime = 1.5f;
    public int damageamount = -10;

    public bool isTracking = false;
    public float RotateSpeed = 50.0f;
    public float TrackTime = 3.0f;
    
    public Rigidbody2D rb;

    private float StopTrackTime = 0.0f;
    private GameObject TrackTarget;

    // Start is called before the first frame update
    void Start() {
        rb.velocity = transform.right * Speed;
        StopTrackTime = Time.time + TrackTime;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        
        if (hitInfo.gameObject.tag == "Player" && !hitInfo.gameObject.GetComponent<PlayerMovement>().isvanish) {
            //Debug.Log("Hit!");
            Destroy(gameObject);
            hitInfo.gameObject.GetComponent<PlayerCharacter>().AddEnergy(damageamount);
            hitInfo.gameObject.GetComponent<PlayerMovement>().isDamagedinside = false;
            hitInfo.gameObject.GetComponent<PlayerMovement>().isDamaged = true;
            hitInfo.gameObject.GetComponent<PlayerMovement>().timeSpentInvincible = 0f;
        }
        
    }

    private void Update() {
        Destroy(gameObject, LifeTime);

        if ( isTracking && Time.time < StopTrackTime) {
            Track();
        }

    }

    public void SetTrackTarget(GameObject target) {
        //Debug.Log("TargetSet");
        TrackTarget = target;
    }

    void Track() {
        if (TrackTarget.GetComponent<PlayerMovement>().isvanish)
        {
            return;
        }
        Vector3 Diff = TrackTarget.transform.position - transform.position;
        Diff.Normalize();
        transform.right = Vector2.Lerp(transform.right, Diff, RotateSpeed / 100.0f);
        rb.velocity = transform.right * Speed;
        
    }
}
