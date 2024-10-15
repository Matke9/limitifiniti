using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InoutManager : MonoBehaviour
{
    Camera camera;
    Vector2 lastPosition;
    [SerializeField]
    LayerMask placementLayerMask;

    void Start()
    {
        camera = Camera.main;
    }

    public Vector2 GetMouseTilePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = camera.nearClipPlane;
        Vector2 worldPosition = camera.ScreenToWorldPoint(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 100, placementLayerMask);

        if (hit.collider != null)
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }

    //void Update()
    //{
    //    Debug.Log(GetMouseTilePosition().ToString());
    //}
}
