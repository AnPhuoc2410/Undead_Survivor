using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f; // Speed of the player
    public Vector3 input;
    Rigidbody2D rigidbody;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        transform.position += input * speed * Time.deltaTime;
        // Rotate the player to face the direction of movement
        if (input != Vector3.zero)
        {
            if (input.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 0);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 0);
            }

        }
    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody2D for better physics handling
        rigidbody.MovePosition(rigidbody.position + new Vector2(input.x, input.y) * speed * Time.fixedDeltaTime);
    }
}
