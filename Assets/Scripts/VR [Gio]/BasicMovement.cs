using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------------------------------------
// Basic movement when we want the player to move using a gamepad
// Author (Discord): Gio#0753
//-----------------------------------------------------------------------

public class BasicMovement : MonoBehaviour {

    Camera cam;
    Rigidbody rb;

    int jumps = 1;
    [SerializeField] bool usaJoystick;
    [SerializeField] float speed = 3;
    [SerializeField] float jumpForce = 500;



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


    void Update() 
    {
        Caminar();
        if (Input.GetButtonDown("Jump")) Jump();
    }




    void Caminar()
    {
        Vector3 velocity;
        if(!usaJoystick)
        {
            velocity = cam.transform.forward * Input.GetAxis("Vertical") * speed;
        }
        else
        {
            velocity = cam.transform.forward * Input.GetAxis("Horizontal") * -speed;
        }
        Vector3 newPosition = transform.position + velocity * Time.deltaTime;
        rb.MovePosition(newPosition);
    }

    public void Jump()
    {
        if (jumps >= 1)
        {
            rb.AddForce(Vector3.up * jumpForce);
            jumps--;
        }
    }

    void OnCollisionEnter(Collision collision) {
        jumps = 1;
    }
}
