using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript_1 : MonoBehaviour {

    /// <summary>
    /// This script controls "EnemyType_1"
    /// </summary>




    public float curSpeed; // variables you can change in the inspector
    public bool canMove;

    private bool facingRight;

    private Rigidbody2D rb; // refrences
    private SpriteRenderer sr;

    public Transform leftBound, rightBound; // the boundaries that the enemy walks between

    void Start() // on startup
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        SetStartingDir();
    }

    void Update()
    {
        if (canMove == true) // checks if the enemy can move
        {
            Move();
        }
        FlipOnEdges();
    }

    void FlipOnEdges() // flips texture when enemy hits a boundary
    {
        if (sr.flipX == true && transform.position.x > rightBound.position.x)
        {
            sr.flipX = false;
            curSpeed = -curSpeed; // flips speed direction
        }
        if (sr.flipX == false && transform.position.x < leftBound.position.x)
        {
            sr.flipX = true;
            curSpeed = -curSpeed;// flips speed direction
        }
    }

    void Move() // moves the enemy my giving a velocity
    {
        rb.velocity = new Vector2(curSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void OnDrawGizmos() // draws a line between the 2 boundaries
    {
        Gizmos.DrawLine(leftBound.position, rightBound.position);
    }

    void SetStartingDir() // finds out if enemy moves left or right
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
