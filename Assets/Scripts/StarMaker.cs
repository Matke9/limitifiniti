using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMaker : MonoBehaviour
{
    public List<GameObject> starPrefabs=new List<GameObject>();           // Star prefab to spawn
    public int starsPerCell = 10;           // Number of stars to spawn in each grid cell
    public float spawnRadius = 50f;         // Radius around player for star spawning
    public float gridCellSize = 100f;       // Size of the grid cells
    public float starMinDistance = 5f;      // Minimum distance between stars to avoid clustering

    private Transform player;               // Reference to the player
    private HashSet<Vector2Int> visitedCells; // Keeps track of which grid cells have spawned stars
    private List<Vector2> spawnedPositions; // Keeps track of spawned star positions to avoid clustering

    void Start()
    {
        // Find the player
        player = Camera.main.transform;

        // Initialize sets
        visitedCells = new HashSet<Vector2Int>();
        spawnedPositions = new List<Vector2>();

        // Spawn initial stars at the player's starting location
        SpawnStarsInCurrentCell();
    }

    void Update()
    {
        // Check if the player enters a new grid cell
        Vector2Int currentCell = GetCurrentGridCell();
        if (!visitedCells.Contains(currentCell))
        {
            // Spawn stars in the new cell
            SpawnStarsInCurrentCell();
        }
    }

    // Method to spawn stars in the current grid cell
    void SpawnStarsInCurrentCell()
    {
        Vector2Int currentCell = GetCurrentGridCell();
        visitedCells.Add(currentCell); // Mark this cell as visited

        // Spawn stars within the spawn radius
        for (int i = 0; i < starsPerCell; i++)
        {
            Vector2 randomPos = GetRandomPositionWithinCell();

            // Ensure the stars don't spawn too close to one another
            if (!IsPositionTooClose(randomPos))
            {
                var star = Instantiate(starPrefabs[Random.Range(0,starPrefabs.Count)], randomPos, Quaternion.identity);
                star.transform.parent = transform;
                spawnedPositions.Add(randomPos);
            }
        }
    }

    // Get the player's current grid cell based on their position
    Vector2Int GetCurrentGridCell()
    {
        int cellX = Mathf.FloorToInt(player.position.x / gridCellSize);
        int cellY = Mathf.FloorToInt(player.position.y / gridCellSize);
        return new Vector2Int(cellX, cellY);
    }

    // Generate a random position within the current grid cell
    Vector2 GetRandomPositionWithinCell()
    {
        Vector2 cellCenter = new Vector2(GetCurrentGridCell().x * gridCellSize, GetCurrentGridCell().y * gridCellSize);
        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        return cellCenter + randomOffset;
    }

    // Check if a position is too close to other stars
    bool IsPositionTooClose(Vector2 pos)
    {
        foreach (Vector2 spawnedPos in spawnedPositions)
        {
            if (Vector2.Distance(spawnedPos, pos) < starMinDistance)
                return true;
        }
        return false;
    }
}
