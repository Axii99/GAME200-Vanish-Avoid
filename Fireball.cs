using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int damageamount = -10;
    private Rigidbody2D rb;
    public float Force = 600.0f;
    public float LifeTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up*Force);
        Destroy(this.gameObject, LifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    private void OnTriggerEnter2D(Collider2D hitInfo) {

        if (hitInfo.gameObject.tag == "Player") {
            //Debug.Log("Hit!");
            //Destroy(gameObject);
            if (hitInfo.gameObject.GetComponent<PlayerMovement>().isvanish) {
                hitInfo.gameObject.GetComponent<PlayerMovement>().isDamagedinside = true;
                hitInfo.gameObject.GetComponent<PlayerMovement>().isDamaged = false;
                hitInfo.gameObject.GetComponent<PlayerMovement>().onAppear();
            }
            else {
                hitInfo.gameObject.GetComponent<PlayerMovement>().isDamagedinside = false;
                hitInfo.gameObject.GetComponent<PlayerMovement>().isDamaged = true;
                hitInfo.gameObject.GetComponent<PlayerMovement>().timeSpentInvincible = 0f;
                hitInfo.gameObject.GetComponent<PlayerCharacter>().AddEnergy(damageamount);
            }

        }

    }
}
