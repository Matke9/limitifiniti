using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    Dictionary<Vector3Int, int> placedBlocks = new();
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
}