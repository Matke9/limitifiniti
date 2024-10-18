using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public  ResourceTypes resourceType;
    public int size;// same as amount?
    Rigidbody2D rb;
    Sprite[] sprites;
    [SerializeField] SpriteRenderer sR;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetSize();
        SetSprite();
        SetVelocity();
    }



    void SetSize()
    {
        size = Random.Range(5, 10);
        int sizeI = (int)size / 5;
        transform.localScale = new Vector3(sizeI, sizeI, 0);
    }

    void SetSprite()
    {
        int spirte = Random.Range(0, sprites.Length);
        sR.sprite = sprites[spirte];
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
