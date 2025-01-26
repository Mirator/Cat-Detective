using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    private Rigidbody2D rb;
    private Vector2 movement;

    public HelpManager helpManager; // Reference to the HelpManager script

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from keyboard
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // Restart the game when R is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        // Toggle help when H is pressed
        if (Input.GetKeyDown(KeyCode.H) && helpManager != null)
        {
            helpManager.ToggleHelp();
        }
    }

    void FixedUpdate()
    {
        // Apply movement to the player
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Restarts the game by reloading the current scene.
    /// </summary>
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
