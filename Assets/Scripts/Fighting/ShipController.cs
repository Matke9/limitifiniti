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

    private AudioSource engineSound;
    public float minVolume = 0.01f; // Default volume when throttle is zero
    public float maxVolume = 0.06f; // Maximum volume when throttle is full
    public float pitchRange = 0.1f; // Pitch range increase based on throttle
    private float throttle; // Represents the throttle amount

    CameraController camControler;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        camControler = Camera.main.GetComponent<CameraController>();
        engineSound = GetComponents<AudioSource>()[1];
    }

    private void Update()
    {
        if (camControler.camMode == CameraController.CamMode.Combat)
        {
            if (!engineSound.isPlaying)
            {
                engineSound.Play();
            }

            moveInput = Input.GetAxisRaw("Vertical");
            steerInput = Input.GetAxisRaw("Horizontal");

            // Calculate throttle as the absolute value of move input to get a positive range
            throttle = Mathf.Clamp(Mathf.Abs(moveInput), 0f, 1f);

            // Smoothly change the pitch based on the throttle value
            engineSound.pitch = Mathf.Lerp(engineSound.pitch, 1f + throttle * pitchRange, Time.deltaTime * 2f);

            // Smoothly change the volume based on the throttle value
            float targetVolume = Mathf.Lerp(minVolume, maxVolume, throttle);
            engineSound.volume = Mathf.Lerp(engineSound.volume, targetVolume, Time.deltaTime * 2f);
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