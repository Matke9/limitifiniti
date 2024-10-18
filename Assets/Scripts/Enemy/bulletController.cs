using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    EnemyController enemyController;
    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }
    public void shoot()
    {
        enemyController.shoot();
    }

}
