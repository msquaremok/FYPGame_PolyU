using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript_2 : MonoBehaviour {

    /// <summary>
    /// This script controls "EnemyType_2"
    /// </summary>

    public float VerticalSpeed; // variables you can change in the inspector
    public float HorizontalSpeed;
    public bool canMove;

    private bool facingRight;

    private Rigidbody2D rb; // refrences
    private SpriteRenderer sr;

    public Transform leftBound, rightBound; // the boundaries the enemy flies between
    public Transform topBound, botBound;

    private float startWaitToFlipTimer = 0.4f; // a boundary timer to eliminate frame bugs
    private float waitToFlipTimer;

    void Start() // on startup
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        SetStartingDir();
        waitToFlipTimer = startWaitToFlipTimer;
    }

    void Update()
    {
        if (canMove == true) // checks if enemy can move
        {
            MoveVertical();
            MoveHorizontal();
        }
        FlipOnEdges();
    }

    void FlipOnEdges() // flips the enemys texture and redirects velocities when a boundary is reached
    {
        if(waitToFlipTimer <= 0)
        {
            if (sr.flipX == true && transform.position.x > rightBound.position.x) // right boundary
            {
                sr.flipX = false;
                VerticalSpeed = -VerticalSpeed;
                waitToFlipTimer = startWaitToFlipTimer;
            }
            if (sr.flipX == false && transform.position.x < leftBound.position.x) // left boundary
            {
                sr.flipX = true;
                VerticalSpeed = -VerticalSpeed;
                waitToFlipTimer = startWaitToFlipTimer;
            }
            if (transform.position.y < topBound.position.y) // top boundary
            {
                HorizontalSpeed = -HorizontalSpeed;
                waitToFlipTimer = startWaitToFlipTimer;
            }
            if (transform.position.y > botBound.position.y) // bottom boundary
            {
                HorizontalSpeed = -HorizontalSpeed;
                waitToFlipTimer = startWaitToFlipTimer;
            }      
        }
        else
        {
            waitToFlipTimer -= Time.deltaTime;
        }     
    }

    void MoveVertical() // applies movement by giving a velocity
    {
        rb.velocity = new Vector2(VerticalSpeed * Time.deltaTime, rb.velocity.y);
    }
    void MoveHorizontal() // applies movement by giving a velocity
    {
        rb.velocity = new Vector2(rb.velocity.x, HorizontalSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos() // draws a line between the 2 sets of 2 boundaries
    {
        Gizmos.DrawLine(leftBound.position, rightBound.position);
        Gizmos.DrawLine(topBound.position, botBound.position);
    }

    void SetStartingDir() // finds out if enemy moves left or right
    {
        if (VerticalSpeed > 0)
        {
            sr.flipX = true;
        }
        else if (VerticalSpeed < 0)
        {
            sr.flipX = false;
        }
    }
}
