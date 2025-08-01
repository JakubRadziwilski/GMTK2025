using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    Camera cam;
    Vector2 mousePos;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookPos = cam.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(lookPos.x - transform.position.x, lookPos.y - transform.position.y);

        transform.up = direction;
    }
    public void Look(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }
}
