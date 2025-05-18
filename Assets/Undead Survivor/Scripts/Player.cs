using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 5f; // Speed of the player
    public Vector2 input;
    private Rigidbody2D rb;
    private Animator animator;

    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        // Update the animator with the current speed
        animator.SetFloat("Speed", input.magnitude); //check is the player is moving
    }

    void FixedUpdate()
    {
        Vector2 move = input * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move); //ideal for smooth movement

        if (input != Vector2.zero)
        {
            spriteRenderer.flipX = input.x < 0;  //true if moving left - false if moving right
        }
    }

    void OnMove(InputValue inputValue)
    {
        input = inputValue.Get<Vector2>();
    }
}
