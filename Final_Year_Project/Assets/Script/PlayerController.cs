﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayer;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    float halfXLength; //for ground overlap check x
    float halfYLength; //for ground overlap check y
    [SerializeField] float fallMultiplier; // for better fall down feeling
    [SerializeField] float minJumpTime; //check hold time for bigger jumps
    private float jumpTimeCounter;
    public int maxJumpCount;
    [SerializeField] int jumpCount;

    float temp;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d.freezeRotation = true;
        jumpTimeCounter = 0;
        jumpCount = maxJumpCount;
        halfXLength = GetComponent<BoxCollider2D>().size.x;
        halfYLength = GetComponent<BoxCollider2D>().size.y + GetComponent<BoxCollider2D>().edgeRadius;
        Debug.Log(halfXLength);
        Debug.Log(halfYLength);
    }

    void Update()
    {
        if(Input.GetKey("d") || Input.GetKey("right"))
        {
            Move(moveSpeed);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            Move(-moveSpeed);
        }
        else
        {
            Move(0);
        }

        if(Input.GetKey("space"))
        {
            if(IsGrounded() && jumpCount > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
                jumpCount--;
            }
        }
        //When it is jumping
        if(!IsGrounded())
        {
            jumpTimeCounter += Time.deltaTime;
        }
        // for jump span control
        if(!Input.GetKey("space"))
        {   
            //Instant fall when release jump
            if(rb2d.velocity.y > 0 && jumpTimeCounter >= minJumpTime)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);                                
            }
        }

        // Reset jump count prevent over bouncing and Time Counter  
        if(IsGrounded())
        {
            jumpTimeCounter = 0;
            if(!Input.GetKey("space"))
            {
                jumpCount = maxJumpCount;
            }
        }              
    }

    void FixedUpdate() 
    {
        //better fall feeling manipulator
        if(rb2d.velocity.y <= 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }    
    }

    void Move(float moveSpeed)
    {
        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
    }

    // Check if player touches ground
    bool IsGrounded()
    {
        bool isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - halfXLength, transform.position.y - halfYLength),
            new Vector2(transform.position.x + halfXLength, transform.position.y - (halfYLength + 0.01f)), groundLayer);
        return isGrounded;
    }
    //Debugging for Gorund touching check
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color (0, 1, 0, 0.5f);
        Gizmos.DrawCube (new Vector2 (transform.position.x, transform.position.y - halfYLength),
            new Vector2(halfXLength*2, 0.01f));
    }
}
