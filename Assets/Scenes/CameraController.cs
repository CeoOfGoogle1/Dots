using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public float panSpeed = 20f;
    public float zoomSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    public float maxPan = 10f;
    public float zoomPanMultiplier = 1f;

    private Vector3 lastPanPosition;
    private Vector3 panVelocity;
    private bool isPanning = false;

    private void Update()
    {
        HandlePanning();
        HandleZooming();
    }

    private void HandlePanning()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastPanPosition = Input.mousePosition;
            isPanning = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isPanning = false;
        }

        if (isPanning)
        {
            Vector3 delta = lastPanPosition - Input.mousePosition;
            Vector3 desiredPan = transform.position + delta * Time.deltaTime * panSpeed;
            desiredPan.x = Mathf.Clamp(desiredPan.x, -maxPan, maxPan);
            desiredPan.y = Mathf.Clamp(desiredPan.y, -maxPan, maxPan);
            desiredPan.z = transform.position.z;

            transform.position = Vector3.SmoothDamp(transform.position, desiredPan, ref panVelocity, 0.1f);
        }

        lastPanPosition = Input.mousePosition;
    }

    private void HandleZooming()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        float newZoom = mainCamera.orthographicSize - zoom * zoomSpeed;
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);
        mainCamera.orthographicSize = newZoom;
    }
}
