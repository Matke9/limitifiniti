using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float fireCooldown = 1f;
    private CameraController camController;

    private float timeSinceLastShot;
    void Start()
    {
        camController = Camera.main.GetComponent<CameraController>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && timeSinceLastShot >= fireCooldown && camController.camMode == CameraController.CamMode.Combat)
        {
            Shoot();
            timeSinceLastShot = 0f;
        }

        timeSinceLastShot += Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;
    }
}
