using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animatorControllers;
    public Rigidbody2D target;

    bool isLive;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriter;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }
    public void Init(SpawnData data)
    {
        speed = data.speed;
        maxHealth = data.health;
        health = maxHealth;
        if (animatorControllers.Length > 0)
        {
            animator.runtimeAnimatorController = animatorControllers[data.spriteType];
        }
    }
}
