using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float life = 10;
    public float demage = 1;
    private void Awake()
    {
        Destroy(gameObject,life);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Skidanje Healtha i tako to
        Destroy(gameObject);
    }
}
