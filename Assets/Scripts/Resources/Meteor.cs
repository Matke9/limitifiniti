using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public  ResourceTypes resourceType;
    public int size;// same as amount?
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetSize();
        SetVelocity();
    }

    void SetSize()
    {
        size = Random.Range(1, 2);
        transform.localScale = new Vector3(size, size, 0);
    }

    void SetVelocity()
    {
        float vX = -1 * Random.RandomRange(0.05f, 1);
        float vY = Random.RandomRange(-0.3f, 0.3f);
        rb.velocity = new Vector2 (vX, vY);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }
}
