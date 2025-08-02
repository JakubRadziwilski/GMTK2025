using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float MoveSpeed = 1f;
    Rigidbody2D rb;
    Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb.linearVelocity = MoveSpeed * Time.deltaTime * moveInput;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void SetMoveSpeed(float speed)
    {
        MoveSpeed = speed; // Update the player's movement speed
    }
}
