using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public float cameraMoveSpeed;
    public float cameraRotateSpeed;
    public Vector3 zoomAmount;
    public float cameraZoomSpeed;
    
    private Vector3 _originalPos;
    private Quaternion _originalRot;
    private Vector3 _dragStartPos;
    private Vector3 _dragCurrentPos;

    private Vector3 _newZoom;
    // Start is called before the first frame update
    void Start()
    {
        _originalPos = transform.position;
        _originalRot = transform.rotation;
        _newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            _newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }
        if(Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up,Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                _dragStartPos = ray.GetPoint(entry);
            }

        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up,Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                _dragCurrentPos = ray.GetPoint(entry);

                _originalPos = transform.position + (_dragStartPos - _dragCurrentPos);
            }
        }

        transform.position = Vector3.Lerp(transform.position, _originalPos, Time.deltaTime * cameraMoveSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, _originalRot, Time.deltaTime * cameraRotateSpeed);
        cameraTransform.position = Vector3.Lerp(cameraTransform.localPosition, _newZoom, Time.deltaTime * cameraMoveSpeed);
    }
}
