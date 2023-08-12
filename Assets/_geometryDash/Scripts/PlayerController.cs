using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    private Rigidbody2D rb;
    private GameObject SpriteObject;
    private ParticleSystem fireParticle;

    public float groundCheckRadius;
    public float jumpSpeed;
    public float rotationSpeed = 1.5f;
    public LayerMask GroundMask;
    public Transform groundChecker;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CameraController.Instance._startingPos = transform.position.x;
        
        SpriteObject = GetComponentInChildren<SpriteRenderer>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        fireParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (!LevelManager.Instance.died)
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
        Vector3 groundCheckerPos = groundChecker.position;
        groundCheckerPos.x = transform.position.x;
        groundCheckerPos.y = transform.position.y - 0.6f;
        groundChecker.position = groundCheckerPos;
        
        Vector3 pos = gameObject.transform.position;
        pos.x += LevelManager.Instance.movementSpeed * Time.deltaTime;
        gameObject.transform.position = pos;
    }

    private void Jump()
    {
        rb.gravityScale = 10;
        if (onGround())
        {
            if(!fireParticle.isPlaying)
                fireParticle.Play();
            
            Vector3 rot = SpriteObject.transform.eulerAngles;
            rot.z = Mathf.Round(SpriteObject.transform.eulerAngles.z / 90) * 90;
            SpriteObject.transform.eulerAngles = rot;

            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
        }
        else
        {
            if(fireParticle.isPlaying)
                fireParticle.Stop();
            SpriteObject.transform.Rotate(Vector3.back * rotationSpeed);
        }
    }

    private void Fly()
    {
    if(!fireParticle.isPlaying)
        fireParticle.Play();
        SpriteObject.transform.rotation = quaternion.Euler(0, 0, rb.velocity.y / 15);
        rb.gravityScale = 5;

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            rb.gravityScale = -5;
    }

    
}
