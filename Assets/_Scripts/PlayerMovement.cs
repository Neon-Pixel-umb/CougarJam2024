using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Movement speed of the player
    public Rigidbody2D rb;        // Reference to the Rigidbody2D component
    public Animator animator;     // Reference to the Animator component
    public SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer component

    private Vector2 movement;     // Stores movement input
    private string currentFacingDirection = "Idle_Right";  // Track the current facing direction

    void Update()
    {
        // Get input from keyboard (WASD or arrow keys)
        if (enabled)  // Only process input when the script is enabled
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Check if the player is moving
            if (movement != Vector2.zero)
            {
                // Horizontal movement: handle left/right turning
                if (movement.x != 0)
                {
                    currentFacingDirection = "Idle_Right";  // Update current facing direction
                    animator.Play("Idle_Right");  // Play the Idle_Right animation

                    // Flip the sprite if moving left
                    spriteRenderer.flipX = movement.x < 0;
                    if (movement.x < 0)
                    {
                        currentFacingDirection = "Idle_Left";  // Update facing direction if moving left
                    }
                }
                // Vertical movement: handle up/down animations
                else if (movement.y != 0)
                {
                    if (movement.y > 0)
                    {
                        currentFacingDirection = "Idle_Up";
                        animator.Play("Idle_Up");  // Play Idle_Up when moving up
                    }
                    else
                    {
                        currentFacingDirection = "Idle_Down";
                        animator.Play("Idle_Down");  // Play Idle_Down when moving down
                    }

                    // Ensure the sprite isn’t flipped for vertical movement
                    spriteRenderer.flipX = false;
                }
            }
        }
    }

    public string GetCurrentFacingDirection()
    {
        return currentFacingDirection;
    }

    void FixedUpdate()
    {
        // Only move the player when the script is enabled
        if (enabled)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnDisable()
    {
        // When movement is disabled (e.g., during dialog), stop the player immediately
        movement = Vector2.zero;
        rb.velocity = Vector2.zero;  // Ensure the player stops moving
    }
}
