using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from keyboard
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        // Apply movement to the player
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
