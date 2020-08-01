using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript_4 : MonoBehaviour {

    public bool PlayerPassesThrough = false; // can be turned on douring game to test actions

    public int gravity; // variables you can change in the inspector
    public bool OnCeiling;
    public float jumpForce;
    public float jumpTimer;
    private float currJumpTimer;

    public Transform ray1Start, ray1End;
    public Transform ray2Start, ray2End;

    private bool hasDetectedPlayer1 = false; // detection checks
    private bool hasDetectedPlayer2 = false;

    private SpriteRenderer sr; // refrences
    private Rigidbody2D rb;

    void Start () // on startup
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb.gravityScale = -gravity;
        OnCeiling = true;
	}
	
	void Update ()
    {

        if(PlayerPassesThrough == true) // can be used to simulate a player getting detected
        {
            DetectedPlayer();
        }

        if (OnCeiling == true) // checks for player
        {
            hasDetectedPlayer1 = Physics2D.Linecast(ray1Start.position, ray1End.position, 1 << LayerMask.NameToLayer("Player"));
            hasDetectedPlayer2 = Physics2D.Linecast(ray2Start.position, ray2End.position, 1 << LayerMask.NameToLayer("Player"));
        }

        if(OnCeiling == false) // falls down if there is a player 
        {
            if(currJumpTimer <= 0) // makes enemy jump when on the ground
            {
                currJumpTimer = jumpTimer;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                
            }
            else if (currJumpTimer > 0)
            {
                currJumpTimer = currJumpTimer - Time.deltaTime;
            }
        }
        
        if(hasDetectedPlayer1 == true || hasDetectedPlayer2 == true) // starts the fall sequence
        {
            hasDetectedPlayer1 = false;
            hasDetectedPlayer2 = false;
            DetectedPlayer();
        }
        	
	}
    void DetectedPlayer()
    {
        OnCeiling = false;
        sr.flipY = false;
        rb.gravityScale = gravity;
    }

    private void OnDrawGizmos()  // draws a line between 2 sets of 2 boundaries
    {
        Gizmos.DrawLine(ray1Start.position, ray1End.position);
        Gizmos.DrawLine(ray2Start.position, ray2End.position);
    }
}
