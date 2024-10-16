using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    GameObject cursor;
    [SerializeField]
    Grid grid;
    [SerializeField]
    InputManager inputManager;
    [SerializeField]
    private BlockDataBase database;
    int selectedBlockID = -1;
    [SerializeField]
    GameObject ship;
    [SerializeField]
    GameObject gridShader;

    private GridData blockData;

    void Start()
    {
        StopPlacement();
        blockData = new();
        blockData.AddBlock(grid.WorldToCell(grid.transform.Find("CockpitParent").position), 0);
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectedBlockID = database.blocksData.FindIndex(data => data.ID == ID);
        if (selectedBlockID < 0)
        {
            Debug.LogError($"Block not found {ID}");
            return;
        }
        cursor.SetActive(true);
        gridShader.SetActive(true);
        inputManager.OnClicked += PlaceBlock;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceBlock()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        bool canPlace = blockData.CanPlaceBlock(gridPos);
        if (!canPlace)
            return;
        GameObject block = Instantiate(database.blocksData[selectedBlockID].Prefab, grid.transform);
        blockData.AddBlock(gridPos, selectedBlockID);
        block.transform.position = grid.CellToWorld(gridPos);
        block.transform.rotation = ship.transform.rotation;
    }

    public void StopPlacement()
    {
        selectedBlockID = -1;
        cursor.SetActive(false);
        gridShader.SetActive(false);
        inputManager.OnClicked -= PlaceBlock;
        inputManager.OnExit -= StopPlacement;
    }

    void Update()
    {
        if (selectedBlockID < 0)
            return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        bool canPlace = blockData.CanPlaceBlock(gridPos);
        cursor.transform.GetChild(0).GetComponent<SpriteRenderer>().color = canPlace ? Color.white : Color.red;
        Vector3 cursorPos = grid.CellToWorld(gridPos);
        cursorPos.z = -2;
        cursor.transform.position = cursorPos;
    }
}
