using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------------------------------------
// Basic movement when we want the player to move using a gamepad
// Author (Discord): Gio#0753
//-----------------------------------------------------------------------

public class PlayerController : MonoBehaviour
{

    public GameObject cam;
    public Transform googleTracked;


    Rigidbody rb;

    int jumps = 1;
    [SerializeField] bool usaJoystick;
    [SerializeField] float speed = 3;
    public Vector3 movement;



    private void Awake()
    {
        //cam = GetComponentInChildren<Camera>();
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
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        if (usaJoystick)
        {
            Vector3 googleTrackedDegrees = googleTracked.rotation.eulerAngles;
            Vector3 playerDegrees = new Vector3(0, googleTrackedDegrees.y - 180, 0);
            Vector3 camDegrees = new Vector3(googleTrackedDegrees.x, googleTrackedDegrees.y, 0);

            transform.rotation = Quaternion.Euler(playerDegrees);
            cam.transform.rotation = Quaternion.Euler(camDegrees);
            Vector3 move = (cam.transform.right * hAxis * speed) + (cam.transform.forward * vAxis * speed);
            rb.MovePosition(transform.position + move * Time.deltaTime);
        }
        else
        {
            Vector3 move = (cam.transform.right * hAxis * speed) + (cam.transform.forward * vAxis * speed);
            rb.MovePosition(transform.position + move * Time.deltaTime);
        }
        
    }

}
