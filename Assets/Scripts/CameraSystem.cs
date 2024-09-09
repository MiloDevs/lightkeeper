using System;
using UnityEngine;

public class CameraSystem: MonoBehaviour
{
    public float dragSpeed = 1f;
    public bool useEdgeScrolling = false;

    private bool _dragMousePanActive = false;
    private Vector2 _lastMousePos;
    private Vector2 _lastDragPos;
    public void Update()
    {
        var inputDir = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W)) inputDir.z = +1;
        if (Input.GetKey(KeyCode.S)) inputDir.z = -1;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1;
        if (Input.GetKey(KeyCode.D)) inputDir.x = +1;

        if (useEdgeScrolling)
        {
            var edgeScrollingSize = 20;
            if (Input.mousePosition.x < edgeScrollingSize) inputDir.x = -1f;
            if (Input.mousePosition.y < edgeScrollingSize) inputDir.y = -1f;
            if (Input.mousePosition.x > Screen.width - edgeScrollingSize) inputDir.z = 1f;
            if (Input.mousePosition.y > Screen.height - edgeScrollingSize) inputDir.z = 1f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _dragMousePanActive = true;
            _lastMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _dragMousePanActive = false;
        }

        if (_dragMousePanActive)
        {
            var mouseDelta = _lastMousePos - (Vector2)Input.mousePosition;
            
            transform.position += transform.forward * mouseDelta.y + transform.right * mouseDelta.x;
            
            _lastMousePos = Input.mousePosition;
        }

        var moveDir = transform.right * inputDir.x + transform.forward * inputDir.z;
        var moveSpeed = 50f;
        transform.position += Time.deltaTime * moveSpeed * moveDir;

        var rotateDir = 0f;
        if (Input.GetMouseButtonDown(1))
        {
            _lastDragPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            var dragOffset = (Vector2)Input.mousePosition - _lastDragPos;

            transform.eulerAngles += new Vector3(0f, dragOffset.x, 0f);
            
            _lastDragPos = Input.mousePosition;
        }
        if (Input.GetKey(KeyCode.Q))rotateDir = -1;
        if (Input.GetKey(KeyCode.E))rotateDir = +1;

        float rotateSpeed = 300f;
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);

    }
}