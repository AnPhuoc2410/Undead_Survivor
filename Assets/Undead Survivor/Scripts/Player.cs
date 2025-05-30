using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float pixelsPerUnit = 18f;

    [Header("References")]
    public Scanner scanner;

    public Vector2 input;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float UnitsPerPixel => 1f / pixelsPerUnit;

    private Vector2 targetPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();

        // Initialize target position
        targetPosition = rb.position;
    }

    void Update()
    {
        animator.SetFloat("Speed", input.magnitude);

        if (input.x != 0f)
            spriteRenderer.flipX = input.x < 0;
    }

    void FixedUpdate()
    {
        if (input.magnitude > 0f)
        {
            Vector2 moveDirection = input.normalized;
            Vector2 desiredMovement = speed * Time.fixedDeltaTime * moveDirection;
            targetPosition += desiredMovement;

            Vector2 snappedPosition;
            snappedPosition.x = Mathf.Round(targetPosition.x / UnitsPerPixel) * UnitsPerPixel;
            snappedPosition.y = Mathf.Round(targetPosition.y / UnitsPerPixel) * UnitsPerPixel;

            rb.MovePosition(snappedPosition);

            targetPosition = snappedPosition;
        }
    }

    void OnMove(InputValue inputValue)
    {
        input = inputValue.Get<Vector2>();
    }
}