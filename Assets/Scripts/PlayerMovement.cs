using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;

    Rigidbody2D rb;
    Animator myAnimator;
    CapsuleCollider2D capCol2D;

    public float walkSpeed = 5f;
    public float jumpSpeed = 5f;
    public float climbSpeed = 5f;

    float startGravityScale;

    bool playerHasHorizontalSpeed = false;  
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        capCol2D = GetComponent<CapsuleCollider2D>();

        startGravityScale = rb.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!capCol2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            return;
        }

        if (value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }

    }

    void ClimbLadder()
    {
        //when the player isn't touching a ladder
        if (!capCol2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.gravityScale = startGravityScale;

            myAnimator.speed = 1;
            myAnimator.SetBool("isClimbing", false);

            return;
        }
        //when the player is touching a ladder

        rb.gravityScale = 0f;

        Vector2 climbVelocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
        rb.velocity = climbVelocity;
        myAnimator.SetBool("isClimbing", true);

        //pauses the climb animation whenever the player isn't moving on the ladder
        if (Mathf.Abs(moveInput.y) > 0)
        {
            myAnimator.speed = 1;
        }
        else
        {
            myAnimator.speed = 0;
        }
    }
    
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;

        //sets playerHasHorizontalSpeed
        if(moveInput.x != 0)
        {
            playerHasHorizontalSpeed = true;
        }
        else
        {
            playerHasHorizontalSpeed = false;
        }

        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    //flips the sprite to the direction the player's moving
    void FlipSprite()
    {
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
}
