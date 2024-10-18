using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] resources;
    [SerializeField] GameObject quad;

    [SerializeField] float spawnInterval;
    [SerializeField] int spawnCount;
    MeshCollider c;

    private void Start()
    {
        c = quad.GetComponent<MeshCollider>();
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(spawnInterval);
        Spawn();
        StartCoroutine(SpawnWave());
    }

    void Spawn()
    {
        float screenX, screenY;
        for (int i = 0; i < spawnCount; i++)
        {
            screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);

            int k = Random.Range(0, resources.Length);
            Instantiate(resources[k], new Vector2(screenX, screenY), Quaternion.identity);
        }
            
    }
}
