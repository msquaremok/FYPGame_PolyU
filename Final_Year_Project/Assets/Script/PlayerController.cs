using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask groundLayer;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    [SerializeField] int moveSpeed;
    [SerializeField] int jumpSpeed;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d.freezeRotation = true;
    }

    private void FixedUpdate()
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
        if(Input.GetKey("space") & IsGrounded())
        {            
            Jump(jumpSpeed);
        }
    }

    void Move(int moveSpeed)
    {
        rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
    }

    void Jump(int jumpSpeed)
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
    }

    // Check if player touches ground
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;
        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if(hit.collider != null)
        {
            Debug.Log("Grounded");
            return true;
        }
        Debug.Log("Not Grounded");
        return false;
    }
}
