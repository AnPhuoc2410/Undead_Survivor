using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public Vector2 input;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public Scanner scanner;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    void Update()
    {
        if (!GameManager.instance.isLive) return; // Check if the game is live
        // Animate based on movement
        animator.SetFloat("Speed", input.magnitude);

        // Flip sprite based on direction
        if (input.x != 0f)
            spriteRenderer.flipX = input.x < 0; //true if moving left 
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive) return; // Check if the game is live

        Vector2 moveDirection = input.normalized;
        Vector2 desiredMovement = speed * Time.fixedDeltaTime * moveDirection;
        Vector2 newPos = rb.position + desiredMovement;

        rb.MovePosition(newPos);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive) return; // Check if the game is live
        // Handle collision with targets
        GameManager.instance.health -= Time.deltaTime * 10;

        if (GameManager.instance.health <= 0)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }

    void OnMove(InputValue inputValue)
    {
        input = inputValue.Get<Vector2>();
    }
}
