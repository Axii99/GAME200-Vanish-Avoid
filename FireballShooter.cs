using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballShooter : MonoBehaviour

{
    public float TimeInterval = 5.0f;
    private float NextShootTime;
    public Fireball fb;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(fb, this.transform.position, Quaternion.identity);
        NextShootTime = Time.time + TimeInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > NextShootTime) {
            Instantiate(fb, this.transform.position, Quaternion.identity);
            NextShootTime = Time.time + TimeInterval;
        }
    }
}
