using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField]
    List<GameObject> enemies = new List<GameObject>();
    public float offset = 2;
    public bool startSpawning = false;
    float timer = 0;
    public float spawningInterval = 1;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(7, 8);
        Physics2D.IgnoreLayerCollision(7, 7);
        /*for (int i = 0; i < 5; i++)
        {
            SpawnEnemy();
        }*/
    }

    public void SpawnEnemy()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float x = Mathf.Cos(angle) * Camera.main.orthographicSize * offset;
        float y = Mathf.Sin(angle) * Camera.main.orthographicSize * offset;
        Vector3 spawnPosition = new Vector3(x, y, 1) + Camera.main.transform.position;
        Instantiate(enemies[Random.Range(0, enemies.Count)], spawnPosition, Quaternion.identity);
    }


    private void OnEnable()
    {
        InvasionTimer.OnTimesUp += StartCombat;
    }

    private void OnDisable()
    {
        InvasionTimer.OnTimesUp -= StartCombat;
    }

    void StartCombat()
    {
        startSpawning = true;
    }
    private void Update()
    {
        if (startSpawning)
        {
            timer += Time.deltaTime;
            if (timer > spawningInterval)
            {
                SpawnEnemy();
                timer = 0;
                spawningInterval -= 0.1f;
            }
            if (spawningInterval <= 0.1f)
            {
                spawningInterval = 0.1f;
            }
        }
    }
}
