using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public Vector2 input;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public float pixelsPerUnit = 18f; // You can adjust this in the Inspector
    private float UnitsPerPixel => 1f / pixelsPerUnit;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Animate based on movement
        animator.SetFloat("Speed", input.magnitude);

        // Flip sprite based on direction
        if (input.x != 0f)
            spriteRenderer.flipX = input.x < 0; //true if moving left 
    }

    void FixedUpdate()
    {
        Vector2 moveDirection = input.normalized;
        Vector2 desiredMovement = speed * Time.fixedDeltaTime * moveDirection;
        Vector2 newPos = rb.position + desiredMovement;

        // Snap to pixel grid
        newPos.x = Mathf.Round(newPos.x / UnitsPerPixel) * UnitsPerPixel;
        newPos.y = Mathf.Round(newPos.y / UnitsPerPixel) * UnitsPerPixel;

        rb.MovePosition(newPos);
    }

    void OnMove(InputValue inputValue)
    {
        input = inputValue.Get<Vector2>();
    }
}
