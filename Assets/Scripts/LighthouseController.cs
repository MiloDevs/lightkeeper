using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighthouseController : MonoBehaviour
{
    public Transform lightSource;
    public float lightRotationSpeed;
    public LayerMask ignoreLayers;
    public float lightPower;

    public Transform debugObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~ignoreLayers))
        {
            debugObject.transform.position = hit.point;
            
            lightSource.rotation = Quaternion.LookRotation(hit.point - lightSource.position, Vector3.up);
        }
    }
}
