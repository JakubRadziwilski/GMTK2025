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
    void Update()
    {
    }
    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * MoveSpeed * Time.deltaTime;
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
