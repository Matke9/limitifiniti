using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float fireCooldown = 1f;
    [SerializeField] private byte gunType;
    private CameraController camController;
    private AudioSource laserSource;
    private List<AudioClip> audioClips;

    private float timeSinceLastShot;
    void Start()
    {
        camController = Camera.main.GetComponent<CameraController>();
        laserSource = transform.parent.parent.GetComponents<AudioSource>()[0];
        if (gunType == 0)
        {
            audioClips = transform.parent.parent.GetComponent<PlayerSounds>().railGunSounds;
        }
        else if (gunType == 1) 
        {
            audioClips = transform.parent.parent.GetComponent<PlayerSounds>().rapidCanonSounds;
        }
        else
        {
            audioClips = transform.parent.parent.GetComponent<PlayerSounds>().pulseCanonSounds;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && timeSinceLastShot >= fireCooldown && camController.camMode == CameraController.CamMode.Combat)
        {
            laserSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Count)]);
            laserSource.pitch = Random.Range(0.8f, 1.1f);
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
