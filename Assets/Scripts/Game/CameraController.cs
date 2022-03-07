using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 _lastMousePosition;

    void Start()
    {

    }

    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(1))
            {
                _lastMousePosition = Input.mousePosition;
            }

            transform.position += (_lastMousePosition - Input.mousePosition) / 100;
            _lastMousePosition = Input.mousePosition;
        }
    }
}
