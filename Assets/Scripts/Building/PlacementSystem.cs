using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    GameObject cursor;
    [SerializeField]
    public Grid grid;
    [SerializeField]
    InputManager inputManager;
    [SerializeField]
    public BlockDataBase database;
    int selectedBlockID = -1;
    [SerializeField]
    GameObject ship;
    public ShipController shipController;
    [SerializeField]
    GameObject gridShader;
    [SerializeField]
    PlayerResources playerRes;
    [HideInInspector]
    public int minX = 0, maxX = 0, minY = 0, maxY = 0;

    public GridData blockData;
    private List<int> fourSideBlocks = new List<int>();

    void Start()
    {
        StopPlacement();
        blockData = new();
        blockData.AddBlock(grid.WorldToCell(grid.transform.Find("CockpitParent").position), 0);
        shipController = ship.GetComponent<ShipController>();
        shipController.moveSpeed = shipController.speedMultiplier * blockData.GetThrusterRatio();

        fourSideBlocks.Add(0);
        fourSideBlocks.Add(1);
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
        cursor.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = database.blocksData[selectedBlockID].Prefab.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
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
        canPlace = canPlace && NeedUpDown(gridPos);
        if (!canPlace)
            return;
        Vector3Int price = database.blocksData[selectedBlockID].price;
        if (!playerRes.CanBuy(price))
        {
            Debug.Log("Not enough resources");
            return;
        }
        playerRes.cobalt -= price.x;
        playerRes.silicates -= price.y;
        playerRes.carbon -= price.z;
        GameObject block = Instantiate(database.blocksData[selectedBlockID].Prefab, grid.transform);
        blockData.AddBlock(gridPos, selectedBlockID);
        block.transform.position = grid.CellToWorld(gridPos);
        block.transform.rotation = ship.transform.rotation;
        UpdateMinMax(gridPos);
        shipController.moveSpeed = shipController.speedMultiplier * blockData.GetThrusterRatio();
    }

    public void StopPlacement()
    {
        selectedBlockID = -1;
        cursor.SetActive(false);
        gridShader.SetActive(false);
        inputManager.OnClicked -= PlaceBlock;
        inputManager.OnExit -= StopPlacement;
    }



    bool NeedUpDown(Vector3Int gridPos)
    {
        Vector3Int up = gridPos + new Vector3Int(0, 1, 0);
        Vector3Int down = gridPos + new Vector3Int(0, -1, 0);
        Vector3Int left = gridPos + new Vector3Int(1, 0, 0);
        Vector3Int right = gridPos + new Vector3Int(-1, 0, 0);
        if (database.blocksData[selectedBlockID].needTop && (!blockData.placedBlocks.ContainsKey(up) || !TouchingOk(up)))
        {
            return false;
        }
        if (database.blocksData[selectedBlockID].needBottom && (!blockData.placedBlocks.ContainsKey(down) || !TouchingOk(down)))
        {
            return false;
        }
        if (!database.blocksData[selectedBlockID].needTop && !database.blocksData[selectedBlockID].needBottom)
        {
            if (!TouchingOk(up) && !TouchingOk(down) && !TouchingOk(left) && !TouchingOk(right))
            {
                return false;
            }
        }
        return true;
    }

    bool TouchingOk(Vector3Int gridPos)
    {
        if (blockData.placedBlocks.ContainsKey(gridPos))
        {
            if (!fourSideBlocks.Contains(blockData.placedBlocks[gridPos]))
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    void UpdateMinMax(Vector3Int position)
    {
        if (position.x < minX) minX = position.x;
        else if (position.x > maxX) maxX = position.x;
        if (position.y < minY) minY = position.y;
        else if (position.y > maxY) maxY = position.y;
    }

    void Update()
    {
        if (selectedBlockID < 0)
            return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        bool canPlace = blockData.CanPlaceBlock(gridPos);
        canPlace = canPlace && NeedUpDown(gridPos);
        cursor.transform.GetChild(0).GetComponent<SpriteRenderer>().color = canPlace ? new Color(1, 1, 1, 0.4f) : new Color(1, 0, 0, 0.4f);
        Vector3 cursorPos = grid.CellToWorld(gridPos);
        cursorPos.z = -2;
        cursor.transform.position = cursorPos;
    }
}
