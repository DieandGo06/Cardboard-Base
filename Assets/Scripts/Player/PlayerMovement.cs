using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    #region Importante, leer
    /* Al conectar el joystick, presiona "@ + B" en el control para que funcione.
     * Usa Unity Remote para probar rapidamente desde el telefono. (Sigue las instrucciones del Unity Remote)
     * Ejecutando desde Unity, el codigo ya reconoce cuando tiene el joystick conectado
     * 
     * Importante: Al hacer la build, marca de forma manual que "usaJoystick" es true.
     */
    #endregion
    
    CharacterController controller;
    Vector3 velocity;

    [SerializeField, Range(1, 10)] public float speed;
    [SerializeField] bool usaJoystick;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        //Detecta cuando se esta usando el unity remote (SOLO CUANDO EJECUTAS EN UNITY)
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isRemoteConnected) usaJoystick = true;
        else usaJoystick = false;
#endif
    }

    void Update()
    {
        //Gravedad();
        Caminar();
    }

    void Caminar()
    {
        Vector3 move;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (!usaJoystick)
        {
            //"transform.right" toma la direccion de la flecha en "x" del personaje y "transform.foward" toma la de "z"
            move = transform.right * x + transform.forward * z;
        }
        else
        {
            //Con el jostick de VR Box hay que invertir los ejes
            x = -x;
            move = transform.right * z + transform.forward * x;
        }
        controller.Move(move * speed * Time.deltaTime);
    }

    void Gravedad()
    {
        float fuerza = -9.81f;
        //Formula de velocidad en caida libre (aparentemente): v = (1/2) * g * (t*t)
        velocity.y += fuerza * Mathf.Pow(Time.deltaTime, 2);
        controller.Move(velocity);
    }

    string inputEntrante(string option)
    {
        if (option == "mouse")
        {
            Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            return mouseInput.ToString();
        }
        if (option == "anyKey")
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                    return kcode.ToString();
            }
        }

        if (option == "horizontal") return Input.GetAxis("Horizontal").ToString();
        if (option == "vertical") return Input.GetAxis("Vertical").ToString();
        return "no se ha usado el input buscado";
    }
}