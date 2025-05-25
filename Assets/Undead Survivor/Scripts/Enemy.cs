using System.Collections;
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
    private Collider2D collid;
    private Animator animator;
    private SpriteRenderer spriter;
    private WaitForFixedUpdate waitForFixed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collid = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        waitForFixed = new();
    }

    void FixedUpdate()
    {
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        collid.enabled = true;
        rb.simulated = true;
        spriter.sortingOrder = 2;
        animator.SetBool("Dead", false);
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive) return;

        Bullet bullet = collision.GetComponent<Bullet>();
        health -= bullet.damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            collid.enabled = false;
            rb.simulated = false;
            spriter.sortingOrder = 1;
            animator.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return waitForFixed;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dir = transform.position - playerPos;
        rb.AddForce(dir.normalized * 5f, ForceMode2D.Impulse);
    }

    void Dead()
    {
        isLive = false;
        gameObject.SetActive(false);
    }
}
