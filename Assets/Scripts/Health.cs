using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10, currentHealth = 0;
    PlacementSystem buildSys;
    void Start()
    {
        buildSys = GameObject.FindGameObjectWithTag("BuildingSys").transform.GetComponent<PlacementSystem>();
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (transform.tag == "Enemy")
            {
                Destroy(gameObject);
            }
            if (transform.tag == "ShipPart")
            {
                buildSys.shipController.moveSpeed = buildSys.shipController.speedMultiplier * buildSys.blockData.GetThrusterRatio();
                Destroy(gameObject);
            }
        }
    }
}
