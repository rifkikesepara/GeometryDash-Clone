using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    private Rigidbody2D rb;
    private GameObject SpriteObject;

    public float groundCheckRadius;
    public float jumpSpeed;
    public float flyForce;
    public float rotationSpeed = 1.5f;
    public LayerMask GroundMask;
    public Transform groundChecker;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SpriteObject = GetComponentInChildren<SpriteRenderer>().gameObject;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 pos = groundChecker.position;
        pos.x = transform.position.x;
        pos.y = transform.position.y - 0.6f;
        groundChecker.position = pos;
        
        MoveForward();
    }

    void Update()
    {
        Invoke(LevelManager.Instance.gameMode.ToString(), 0);
    }

    private bool onGround()
    {
        return Physics2D.OverlapBox(
            groundChecker.position, Vector2.right * 1.1f + Vector2.up * groundCheckRadius, 0, GroundMask);
    }

    private void MoveForward()
    {
        Vector3 pos = gameObject.transform.position;
        if (!LevelManager.Instance.died)
        {
            pos.x += LevelManager.Instance.movementSpeed;
            gameObject.transform.position = pos;
        }
    }

    private void Jump()
    {
        if (onGround())
        {
            Vector3 rot = SpriteObject.transform.eulerAngles;
            rot.z = Mathf.Round(SpriteObject.transform.eulerAngles.z / 90) * 90;
            SpriteObject.transform.eulerAngles = rot;
            
            if (Input.GetKey(KeyCode.Space))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
        }
        else
        {
            SpriteObject.transform.Rotate(Vector3.back * rotationSpeed);
        }
    }

    private void Fly()
    {
        SpriteObject.transform.rotation = quaternion.Euler(Vector3.zero);
        rb.gravityScale = 5;

        if (Input.GetKey(KeyCode.Space))
            rb.AddForce(Vector2.up * flyForce, ForceMode2D.Force);
    }

    
}
