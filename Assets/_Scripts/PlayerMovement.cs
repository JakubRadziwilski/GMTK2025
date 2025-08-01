using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 1f;
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
}
