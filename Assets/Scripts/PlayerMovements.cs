using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    [Header("Movements")]
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] public int maxJumps = 2;

    [Header("Ground Check")]
    [SerializeField] public Transform groundCheckPos;
    [SerializeField] public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    [SerializeField] public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Wall Check")]
    [SerializeField] public Transform wallCheckPos;
    [SerializeField] public Vector2 wallCheckSize = new Vector2(0.5f, 0.05f);
    [SerializeField] public LayerMask wallLayer;

    [Header("Gravity")]
    [SerializeField] public float baseGravity = 2f;
    [SerializeField] public float maxFallSpeed = 18f;
    [SerializeField] public float fallSpeedMultiplier = 2f;

    [Header("Wall Movement")]
    [SerializeField] public float wallSlideSpeed = 2f;

    [Header("Wall Jump")]
    [SerializeField] public Vector2 wallJumpPower = new Vector2(5f, 10f);

    private bool isWallJumping;
    private float wallJumpDirection;
    private float wallJumpTime = 0.5f;
    private float wallJumpTimer; 

    private bool isWallSliding;
    private Rigidbody2D rb;
    private float horizontalMovement;
    private float jumpsRemaining;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x; 
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpsRemaining--;
            } else if(context.canceled)
            {
                // light jump
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                jumpsRemaining--;
            }
        }

        // wall jumping
        if (context.performed && wallJumpTimer > 0f)
        {
            isWallJumping = true;
            // jump away from wall
            rb.velocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpTimer = 0f;

            // force flip
            if (transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 ls = transform.localScale;
                ls.x *= -1f;
                transform.localScale = ls;
            }

            Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f);
        }
    }

    void Update()
    {
        CheckGround();
        Gravity();
        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
            Flip();
        }
    }

    private void CheckGround()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private bool CheckWall()
    {
        return Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer);
    }

    private void Gravity()
    {
        if(rb.velocity.y < 0)
        {
            // cade velocemente
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void WallSlide()
    {
        // not on ground e on a wall
        if(!isGrounded && CheckWall() && horizontalMovement != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
        } else
        {
            isWallSliding = false;
        }
    }

    private void Flip()
    { 
        if(isFacingRight && horizontalMovement < 0 || !isFacingRight && horizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        } else if (wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
        }
    }

    private void CancelWallJump()
    {
        isWallJumping = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position, groundCheckSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(wallCheckPos.position, wallCheckSize);
    }
}
