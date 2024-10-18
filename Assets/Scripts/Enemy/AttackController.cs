using UnityEngine;

public class AttackController : MonoBehaviour
{
    EnemyController enemyController;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    public void Attack()
    {
        enemyController.Attack();
    }

}
