using System;
using Unity.Mathematics;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float speedMultiplier = 10f;
    public float moveSpeed = 0f; // The maximum speed of the player
    public float friction = 0.9f; // The friction factor (0.9 means 90% of speed retained per frame)

    private Rigidbody2D rb;
    private Vector2 velocity;
    private float rotationSpeed;
    private float moveInput;
    private float steerInput;

    CameraController camControler;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camControler = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        if (camControler.camMode == CameraController.CamMode.Combat)
        {
            moveInput = Input.GetAxisRaw("Vertical");
            steerInput = Input.GetAxisRaw("Horizontal");
        }
    }


    void FixedUpdate()
    {
        // Move the player based on the calculated velocity
        if (camControler.camMode == CameraController.CamMode.Combat)
        {
            velocity = velocity * friction + moveInput * moveSpeed * (1 - friction) * (Vector2)transform.up;
            rb.velocity = velocity;

            rotationSpeed = rotationSpeed * friction + steerInput * moveSpeed * (1 - friction);
            rb.rotation -= rotationSpeed / 5;
        }
    }
}