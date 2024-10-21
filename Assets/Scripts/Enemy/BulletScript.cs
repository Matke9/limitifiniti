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
        //Skidanje Healtha i tako to
        Debug.Log("Giga");
        if (collision.transform.tag == "ShipPart")
        {
            try
            {
                Debug.Log("nigga");
                collision.transform.GetComponent<Health>().TakeDamage(damage);
            }
            catch { }
        }
        Destroy(gameObject);
    }
}
