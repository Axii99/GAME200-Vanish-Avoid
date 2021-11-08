using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    public float speed = 1.0f;
    public float jumpForce = 2.0f;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        if (Input.GetKeyDown(KeyCode.E)) {
            GetComponent<Connect>().SetStartPosition(transform.position);
            GetComponent<Connect>().SetEndPosition(transform.position + transform.right*100);
            StartCoroutine(GetComponent<Connect>().ShootRay());
            
        }
    }

    private void Move() {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }


    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}

