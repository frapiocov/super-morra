using UnityEngine;

public class EnemyFollow2D : MonoBehaviour
{
    public Transform Player;
    public float speed = 2f;
    public float jumpForce = 5f;
    public float stoppingDistance = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("Vision Settings")]
    public float visionRange = 5f;               // Raggio visivo
    public bool visionLineOfSight = true;        // Usa raycast per vedere il player

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, Player.position);

        // Se il player è fuori dalla visione, non fa nulla
        if (distanceToPlayer > visionRange || !CanSeePlayer())
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        float direction = Mathf.Sign(Player.position.x - transform.position.x);

        if (Mathf.Abs(Player.position.x - transform.position.x) > stoppingDistance)
        {
            // Movimento orizzontale
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);

            // Salta se c'è ostacolo davanti
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, 0.6f, groundLayer);
            if (hit.collider != null && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private bool CanSeePlayer()
    {
        if (!visionLineOfSight) return true;

        Vector2 directionToPlayer = Player.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer.normalized, visionRange, groundLayer);

        // Se il raycast colpisce qualcosa prima del player → non vede il player
        if (hit.collider != null)
        {
            return false;
        }

        return true;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        // Visualizza campo visivo
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
