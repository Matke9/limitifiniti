using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera camera;
    [SerializeField]
    Transform gridShader;
    [SerializeField]
    Transform player;
    float zoomTarget;

    [SerializeField]
    float multiplier = 2, minZoom = 1, maxZoom = 10, smoothTime = .1f;
    float velocity = 0;

    enum CamMode
    {
        Player,
        Build,
        Combat
    }

    CamMode camMode = CamMode.Player;

    void Start()
    {
        camera = GetComponent<Camera>();
        zoomTarget = camera.orthographicSize;
    }

    void Update()
    {
        if (camMode == CamMode.Build)
        {
            zoomTarget -= Input.GetAxisRaw("Mouse ScrollWheel") * multiplier;
            zoomTarget = Mathf.Clamp(zoomTarget, minZoom, maxZoom);
            camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, zoomTarget, ref velocity, smoothTime);
            int gridSize = Mathf.RoundToInt(camera.orthographicSize) * 4 + 1;
            gridShader.localScale = new Vector3(gridSize, gridSize, 1);
        }
        else if (camMode == CamMode.Player)
        {
            Vector3 camPos = player.position;
            camPos.z = -10;
            camera.transform.position = camPos;
            camera.orthographicSize = 5;
            int gridSize = Mathf.RoundToInt(camera.orthographicSize) * 4 + 1;
            gridShader.localScale = new Vector3(gridSize, gridSize, 1);
        }
    }
}
