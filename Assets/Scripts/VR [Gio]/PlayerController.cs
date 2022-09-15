using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------------------------------------
// Basic movement when we want the player to move using a gamepad
// Author (Discord): Gio#0753
//-----------------------------------------------------------------------

public class PlayerController : MonoBehaviour
{

    Camera cam;
    Rigidbody rb;

    int jumps = 1;
    [SerializeField] bool usaJoystick;
    [SerializeField] float speed = 3;



    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {  
#if UNITY_EDITOR
        //Detecta cuando se esta usando el unity remote (SOLO CUANDO EJECUTAS EN UNITY)
        if (UnityEditor.EditorApplication.isRemoteConnected) usaJoystick = true;
        else usaJoystick = false;
#endif
    }


    void FixedUpdate()
    {
        Caminar();
    }




    void Caminar()
    {
        Vector3 movement;
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        if (!usaJoystick)
        {
            movement = (cam.transform.right * hAxis * (speed/4)) + (cam.transform.forward * vAxis * speed);
        }
        else
        {
            movement = (cam.transform.right * vAxis * (speed/4)) + (cam.transform.forward * hAxis * -speed);
        }
        movement = Vector3.ClampMagnitude(movement, speed - Mathf.Abs(movement.x * 2));
        rb.MovePosition(transform.position + movement * Time.deltaTime);
        Debug.Log(movement.magnitude);
    }

}
