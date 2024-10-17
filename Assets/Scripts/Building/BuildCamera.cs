using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCamera : MonoBehaviour
{
    Camera camera;
    [SerializeField] Transform gridShader;
    float zoomTarget;

    [SerializeField]
    float multiplier = 2, minZoom = 1, maxZoom = 10, smoothTime = .1f;
    float velocity = 0;

    void Start()
    {
        camera = GetComponent<Camera>();
        zoomTarget = camera.orthographicSize;
    }

    void Update()
    {
        zoomTarget -= Input.GetAxisRaw("Mouse ScrollWheel") * multiplier;
        zoomTarget = Mathf.Clamp(zoomTarget, minZoom, maxZoom);
        camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, zoomTarget, ref velocity, smoothTime);
        int gridSize = Mathf.RoundToInt(camera.orthographicSize) * 4 + 1;
        gridShader.localScale = new Vector3(gridSize, gridSize, 1);
    }
}
