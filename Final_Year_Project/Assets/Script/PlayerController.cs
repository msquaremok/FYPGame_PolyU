using System.Collections;
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
    float colliderOffsetX;
    float colliderOffsetY;
    [SerializeField] float fallMultiplier; // for better fall down feeling
    public int maxJumpCount;
    [SerializeField] int jumpCount;
    [SerializeField] float coyoteTime;
    private float coyoteCounter;
    [SerializeField] Animator animator;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d.freezeRotation = true;
        jumpCount = maxJumpCount;
        halfXLength = GetComponent<BoxCollider2D>().size.x / 2;
        halfYLength = (GetComponent<BoxCollider2D>().size.y + GetComponent<BoxCollider2D>().edgeRadius) / 2;
        colliderOffsetX = GetComponent<BoxCollider2D>().offset.x;
        colliderOffsetY = GetComponent<BoxCollider2D>().offset.y;
        Debug.Log(halfXLength);
        Debug.Log(halfYLength);
    }

    void Update()
    {
        Vector3 charScale = transform.localScale;
        //for flipping char
        halfXLength = charScale.x * GetComponent<BoxCollider2D>().size.x / 2;
        colliderOffsetX = charScale.x * GetComponent<BoxCollider2D>().offset.x;

        //Input Method
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            if (charScale.x < 0)
            {
                charScale.x = -charScale.x; //flip if not facing right
            }
            animator.SetBool("Run", true);
            Move(moveSpeed);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            if (charScale.x > 0)
            {
                charScale.x = -charScale.x; //flip if not facing left
            }
            animator.SetBool("Run", true);
            Move(-moveSpeed);
        }
        else
        {
            animator.SetBool("Run", false);
            Move(0);
        }

        transform.localScale = charScale; //set face direction

        if (Input.GetKeyDown("space"))
        {
            if (coyoteCounter > 0f && jumpCount > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
                jumpCount--;
            }
        }

        //small jump when space is released earlier
        if (Input.GetKeyUp("space") && rb2d.velocity.y > 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
        }

        // Reset jump count prevent over bouncing and Coyote Time Counter on ground  
        if (IsGrounded())
        {
            coyoteCounter = coyoteTime;
            animator.SetBool("Jump", false);
            if (!Input.GetKey("space"))
            {
                jumpCount = maxJumpCount;
            }
        }
        //When it is jumping
        else
        {
            coyoteCounter -= Time.deltaTime;
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        }
    }

    void FixedUpdate()
    {
        //better fall feeling manipulator
        if (rb2d.velocity.y <= 0)
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
        bool isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - halfXLength + colliderOffsetX, transform.position.y - halfYLength + colliderOffsetY),
            new Vector2(transform.position.x + halfXLength + colliderOffsetX, transform.position.y - (halfYLength + 0.01f) + colliderOffsetY), groundLayer);
        return isGrounded;
    }
    //Debugging for Gorund touching check
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(new Vector2((transform.position.x + colliderOffsetX), transform.position.y - halfYLength + colliderOffsetY),
            new Vector2(halfXLength * 2, 0.01f));
    }
}
