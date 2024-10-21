using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float life = 10;
    public int damage = 1;
    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Skidanje Healtha i tako to
        Debug.Log(collision.gameObject);

        // Loop through all contact points
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Get the exact object hit by the bullet
            Collider2D hitCollider = contact.collider;
            if (hitCollider != null && hitCollider.CompareTag("ShipPart"))
            {
                // If the collider belongs to a ShipPart, apply damage
                Health shipPartHealth = hitCollider.GetComponent<Health>();
                if (shipPartHealth != null)
                {
                    shipPartHealth.TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }
}
