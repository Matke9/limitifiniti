using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // The maximum speed of the player
    public float friction = 0.9f; // The friction factor (0.9 means 90% of speed retained per frame)

    private Rigidbody2D rb;
    private Vector2 velocity;
    private Vector2 moveInput;
    public GameObject eLetter;
    private bool canClickE = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canClickE = false;
    }

    private void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (canClickE && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Udji u brod majstore!");
        }
    }

    void FixedUpdate()
    {
        // Move the player based on the calculated velocity
        velocity = (velocity * friction + moveInput * moveSpeed * (1 - friction));
        rb.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UpdateTriggerState(collision, true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        UpdateTriggerState(collision, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        UpdateTriggerState(collision, false);
    }

    private void UpdateTriggerState(Collider2D collision, bool state)
    {
        if (collision.gameObject.layer == 6)
        {
            eLetter.SetActive(state);
            canClickE = state;
        }
    }
}