using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float groundCheckRadius;
    public float jumpSpeed;
    public float rotationSpeed = 1.5f;
    public LayerMask GroundMask;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (onGround())
        {
            Vector3 rot = transform.eulerAngles;
            rot.z = Mathf.Round(transform.eulerAngles.z / 90) * 90;
            transform.eulerAngles = rot;
            
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
        }
        else
        {
            transform.Rotate(Vector3.back * rotationSpeed);
        }
    }

    private bool onGround()
    {
        return Physics2D.OverlapCircle(
            new Vector2(transform.position.x, transform.position.y - 0.7f), groundCheckRadius,
            GroundMask);
    }

    
}
