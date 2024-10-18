using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float life = 10;
    public float damage = 1;
    private void Awake()
    {
        Destroy(gameObject,life);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       //Skidanje Healtha i tako to
       Destroy(gameObject);
    }
}
