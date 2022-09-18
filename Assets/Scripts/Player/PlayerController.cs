using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerController : MonoBehaviour
{
    //Variables en editor
    [Header("Referencias")]
    public GameObject cam;
    public Transform googleTracked;

    [Header("Variables setteables")]
    [SerializeField] float distanciaObjetoAgarrado;
    [SerializeField] float distMaxPlayerProducto;
    public float speed;


    [Header("Variables p/ debug")]
    public GameObject productoSeleccionado;
    [SerializeField] bool usaJoystick;
    [SerializeField] bool canGrab;
    public bool desactivarEfectos;

    //Variables privadas
    Rigidbody rb;

    



    private void Awake()
    {
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

    private void Update()
    {

        //Debug.Log(desactivarEfectos);
        //Debug.Log(inputEntrante("anyKey"));

        if (Input.GetMouseButtonDown(0) || inputEntrante("anyKey") == "JoystickButton4")
        {
            if (productoSeleccionado == null)
            {
                if (productoSeleccionable() != null) AgarrarProducto(productoSeleccionable());
            }
            else SoltarProducto();
        }
        MostrarRaycast();


        if (inputEntrante("anyKey") == "JoystickButton4")
        {
            Debug.Log("Funciona");
        }
    }


    void FixedUpdate()
    {
        Caminar();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Producto")
        {
            productoSeleccionado = other.gameObject;
            AgarrarProducto(other.gameObject);
        }

        if (other.GetComponent<AudioSource>() != null)
        {
            other.GetComponent<AudioSource>().PlayOneShot(other.GetComponent<ActivaSonido>().ElSonido);

            Debug.Log("HOLAA");
        }

       

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




    #region Agarrar y soltar 
    void SetAsChildOfCamera(GameObject producto)
    {
        producto.transform.parent = cam.transform;
    }

    void AgarrarProducto(GameObject producto)
    {
        SetAsChildOfCamera(producto);
        producto.transform.localPosition = Vector3.zero;
        Vector3 nuevaPosicion = Vector3.zero.CambiarZ(distanciaObjetoAgarrado);
        producto.transform.localPosition = nuevaPosicion;
        productoSeleccionado = producto;
    }

    GameObject productoSeleccionable()
    {
        RaycastHit hit;
        Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, distMaxPlayerProducto);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Producto")) return hit.collider.transform.gameObject;
        }
        return null;
    }

    void MostrarRaycast()
    {
        RaycastHit hit;
        Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, distMaxPlayerProducto);
        Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    }

    void SoltarProducto()
    {
        if (productoSeleccionado.GetComponent<PosicionarProducto>())
        {
            productoSeleccionado.GetComponent<PosicionarProducto>().SetNewPosition();
            productoSeleccionado = null;
        }
    }
    #endregion
}
