using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody2D target;

    bool isLive = true;
    private Rigidbody2D rb;
    private SpriteRenderer spriter;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 moveDirection = (target.position - rb.position).normalized;
        Vector2 desiredMovement = speed * Time.fixedDeltaTime * moveDirection;
        rb.MovePosition(rb.position + desiredMovement);

        rb.linearVelocity = Vector2.zero;
    }

    void LateUpdate()
    {
        spriter.flipX = target.position.x < rb.position.x;
    }

}
