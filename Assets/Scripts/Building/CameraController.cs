using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField]
    Transform gridShader;
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform ship;
    float zoomTarget;

    [SerializeField]
    float multiplier = 2, minZoom = 1, maxZoom = 10, smoothTime = .1f;
    float velocity = 0;
    [SerializeField]
    PlacementSystem pS;

    public enum CamMode
    {
        Player,
        Build,
        Combat
    }

    public CamMode camMode = CamMode.Player;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        zoomTarget = mainCamera.orthographicSize;
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
        camMode = CamMode.Combat;
        ship.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    void FixedUpdate()
    {
        if (camMode == CamMode.Build)
        {
            Vector3 camPos = ship.position;
            camPos.z = -10;
            mainCamera.transform.position = camPos;
            zoomTarget -= Input.GetAxisRaw("Mouse ScrollWheel") * multiplier;
            zoomTarget = Mathf.Clamp(zoomTarget, minZoom, maxZoom);
            mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, zoomTarget, ref velocity, smoothTime);
            int gridSize = Mathf.RoundToInt(mainCamera.orthographicSize) * 4 + 1;
            gridShader.localScale = new Vector3(gridSize, gridSize, 1);
        }
        else if (camMode == CamMode.Player)
        {
            Vector3 camPos = player.position;
            camPos.z = -10;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, camPos, 10f * Time.deltaTime); ;
            mainCamera.orthographicSize = 5;
            int gridSize = Mathf.RoundToInt(mainCamera.orthographicSize) * 4 + 1;
            gridShader.localScale = new Vector3(gridSize, gridSize, 1);
        }
        else if (camMode == CamMode.Combat)
        {
            Vector3 camPos = ship.transform.position;
            camPos = pS.grid.CellToWorld(new Vector3Int((pS.minX + pS.maxX) / 2, (pS.minY + pS.maxY) / 2, 0));
            camPos.z = -10;
            transform.position = camPos;
            float camSize = Mathf.Clamp(math.max(pS.maxX - pS.minX, pS.maxY - pS.minY) * 2f, minZoom, maxZoom);
            mainCamera.orthographicSize = camSize;
        }
    }
}
