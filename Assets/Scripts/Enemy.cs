using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Player settings")]
    public Transform Player;
    public float chaseSpeed = 2f;
    public float jumpForce = 2f;
    public float stoppingDistance = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump;
    public int damage = 1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        if (Player == null) return;

        Vector2 direction = (Player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, Player.position);

        if (distance > stoppingDistance)
        {
            rb.MovePosition(rb.position + direction * chaseSpeed * Time.deltaTime);
        }
        /*
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        // player direction
        float direction = Mathf.Sign(Player.position.x - transform.position.x);

        bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 5f, 1 << Player.gameObject.layer);

        if (isGrounded)
        {
            rb.velocity = new Vector2(direction * chaseSpeed, rb.velocity.y);

            // ground
            RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, groundLayer);

            // gap
            RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector2.down, 2f, groundLayer);

            // platform above
            RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 3f, groundLayer);

            if(!groundInFront.collider && !gapAhead.collider)
            {
                shouldJump = true;
            } else if(isPlayerAbove && platformAbove.collider)
            {
                shouldJump = true;
            }
        }
        */
    }

    /*
    private void FixedUpdate()
    {
        if(isGrounded && shouldJump)
        {
            shouldJump = false;
            Vector2 direction = (Player.position - transform.position).normalized;

            Vector2 jumpDirection = direction * jumpForce;

            rb.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);
        }
    }
    */
}
