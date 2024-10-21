using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{

    public Dictionary<Vector3Int, int> placedBlocks = new();
    public void AddBlock(Vector3Int gridPos, int ID)
    {
        if (placedBlocks.ContainsKey(gridPos))
        {
            Debug.LogError("Grid position already occupied");
        }
        else
        {
            placedBlocks.Add(gridPos, ID);
        }
    }
    public bool CanPlaceBlock(Vector3Int gridPos)
    {
        if (placedBlocks.ContainsKey(gridPos))
        {
            return false;
        }
        return true;
    }

    public float GetThrusterRatio()
    {
        float thrusterCount = 0;
        foreach (Vector3Int key in placedBlocks.Keys)
        {
            if (placedBlocks[key] == 2)
                thrusterCount++;
        }
        return thrusterCount / (float)placedBlocks.Count;
    }
}