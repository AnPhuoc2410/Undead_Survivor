using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    public int expValue = 1;
    public float moveSpeed = 3f;
    public float magnetRange = 2f;
    
    private Transform player;
    private bool isCollected = false;
    private Rigidbody2D rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void OnEnable()
    {
        player = GameManager.instance.player.transform;
        isCollected = false;
        rb.linearVelocity = Vector2.zero;
    }
    
    void FixedUpdate()
    {
        if (isCollected || !player) return;
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        // If player is within magnet range, move towards player
        if (distanceToPlayer <= magnetRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            GameManager.instance.GetExp(expValue);
            gameObject.SetActive(false);
        }
    }
} 