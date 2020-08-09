using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript_3 : MonoBehaviour {

    /// <summary>
    /// This script controls "EnemyType_3"
    /// </summary>

    public float verticalSpeed; // variables you can change in the inspector
    public float amplitude;
    public float curSpeed;
    public bool canMove;

    private bool facingRight;

    private Rigidbody2D rb; // refrences
    private SpriteRenderer sr;

    public Transform leftBound, rightBound;

    void Start() // on startup
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        SetStartingDir();
    }

    void Update()
    {
        if (canMove == true)// checks if enemy can move
        {
            Move();
        }
        FlipOnEdges();
    }

    void FlipOnEdges()// flips the enemys texture and redirects velocities when a boundary is reached
    {
        if (sr.flipX == true && transform.position.x > rightBound.position.x)
        {
            sr.flipX = false;
            curSpeed = -curSpeed;
        }
        if (sr.flipX == false && transform.position.x < leftBound.position.x)
        {
            sr.flipX = true;
            curSpeed = -curSpeed;
        }
    }

    void Move() // moves the enemy in a sinusodial curve
    {
        rb.velocity = new Vector2(curSpeed * Time.deltaTime, Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed)* amplitude);
    }

    private void OnDrawGizmos() // draws a line between 2 boundaries
    {
        Gizmos.DrawLine(leftBound.position, rightBound.position);
    }

    void SetStartingDir()// finds out if enemy moves left or right
    {
        if (curSpeed > 0)
        {
            sr.flipX = true;
        }
        else if (curSpeed < 0)
        {
            sr.flipX = false;
        }
    }
}
