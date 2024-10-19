using System;
using Unity.Mathematics;
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

    CameraController camControler;
    SpriteRenderer spriteRenderer;
    Collider2D collider;

    [SerializeField]
    GameObject ship; //da postane dynamic rigidbody kad istekne tajmer
    [SerializeField]
    GameObject shopUI;
    [SerializeField]
    private LayerMask shipMask;

    [SerializeField]
    PlacementSystem placementSystem;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canClickE = false;
        camControler = Camera.main.GetComponent<CameraController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (camControler.camMode == CameraController.CamMode.Player)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        if (canClickE && Input.GetKeyDown(KeyCode.E))
        {
            camControler.camMode = CameraController.CamMode.Build;
            spriteRenderer.enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            collider.enabled = false;
            shopUI.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && camControler.camMode == CameraController.CamMode.Build)
        {
            camControler.camMode = CameraController.CamMode.Player;
            collider.enabled = true;
            transform.position = MovePlayerOutOfShip(transform.position);
            spriteRenderer.enabled = true;
            shopUI.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private Vector3 MovePlayerOutOfShip(Vector3 position)
    {
        Vector3Int playerGridPos = placementSystem.grid.WorldToCell(position);
        Vector3 newPos = position;
        newPos.z = position.z;
        int xm = playerGridPos.x + placementSystem.minX,
         xp = playerGridPos.x - placementSystem.maxX,
         ym = playerGridPos.y + placementSystem.minY,
         yp = playerGridPos.y - placementSystem.maxX;

        if (math.min(xm, xp) < math.min(ym, yp))
        {
            if (xm < xp)
            {
                newPos.x = placementSystem.minX - 1;
            }
            else
            {
                newPos.x = placementSystem.maxX + 1;
            }
        }
        else
        {
            if (ym < yp)
            {
                newPos.y = placementSystem.minY - 1;
            }
            else
            {
                newPos.y = placementSystem.maxY + 1;
            }
        }
        return newPos;
    }


    void FixedUpdate()
    {
        // Move the player based on the calculated velocity
        if (camControler.camMode == CameraController.CamMode.Player)
        {
            velocity = velocity * friction + moveInput * moveSpeed * (1 - friction);
            rb.velocity = velocity;
        }
        else
        {
            velocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }
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
        if (collision.gameObject.layer == 6 && camControler.camMode == CameraController.CamMode.Player)
        {
            eLetter.SetActive(state);
            canClickE = state;
        }
    }
}