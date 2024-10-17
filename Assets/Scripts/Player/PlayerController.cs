using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // The maximum speed of the player
    public float friction = 0.9f; // The friction factor (0.9 means 90% of speed retained per frame)

    private Rigidbody2D rb;
    private Vector2 velocity;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        // Move the player based on the calculated velocity
        velocity = (velocity * friction + moveInput * moveSpeed * (1 - friction));
        rb.velocity = velocity;
    }
}