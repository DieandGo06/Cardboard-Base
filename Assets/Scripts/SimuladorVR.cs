using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimuladorVR : MonoBehaviour
{
    [SerializeField, Range(.05f, 2)] private float dragRate = .2f;
    [SerializeField] Transform cameraTransform = default;

    private Vector2 dragDegrees;

#if UNITY_EDITOR
    Vector3 lastMousePos;
#endif

    Quaternion initialRotation;
    Quaternion attitude;

    void Awake()
    {
        initialRotation = cameraTransform.rotation;
    }

    void Update()
    {

#if UNITY_EDITOR
        SimulateVR();
#endif

        attitude = initialRotation * Quaternion.Euler(dragDegrees.x, 0, 0);
        cameraTransform.rotation = Quaternion.Euler(0, -dragDegrees.y, 0) * attitude;
    }

#if UNITY_EDITOR
    void SimulateVR()
    {
        Vector3 mousePos = Input.mousePosition;

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Vector3 delta = mousePos - lastMousePos;
            dragDegrees.x -= delta.y * dragRate;
            dragDegrees.y -= delta.x * dragRate;
        }
        lastMousePos = mousePos;
    }
#endif

}
