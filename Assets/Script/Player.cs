using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Start()
    {
    
    
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Capture input from the keyboard
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right Arrow
        movement.y = Input.GetAxisRaw("Vertical");   // W/S or Up/Down Arrow

        // Normalize the movement to prevent faster diagonal movement
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D
        rb.velocity = movement * moveSpeed;
    }

}
