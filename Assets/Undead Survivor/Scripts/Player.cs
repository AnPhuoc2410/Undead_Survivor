using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 5f; // Speed of the player
    public Vector3 input;
    Rigidbody2D rigidbody;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody2D for better physics handling
        rigidbody.MovePosition(rigidbody.position + new Vector2(input.x, input.y) * speed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue inputValue)
    {
        input = inputValue.Get<Vector2>();
    }
}
