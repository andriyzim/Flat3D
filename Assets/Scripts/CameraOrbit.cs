using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _prevMousePosition;

    void Start()
    {
        _transform = transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _prevMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            _transform.Rotate(Vector3.up, -(_prevMousePosition.x - Input.mousePosition.x)*Time.deltaTime*1.5f);
            _prevMousePosition = Input.mousePosition;
        }
    }
}
