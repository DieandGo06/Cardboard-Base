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
    [HideInInspector] public bool desactivarEfectos;
    [SerializeField] GameObject productoSeleccionable;
    public GameObject productoSeleccionado;
    public bool usaJoystick;

    //Variables privadas
    Rigidbody rb;
    LayerMask layerToRaycast;
    [HideInInspector] public CarritoManager carrito;






    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        carrito = GetComponentInChildren<CarritoManager>();
        GameManager.instance.jugador = transform.gameObject;
        layerToRaycast = LayerMask.GetMask("Producto");
    }

    void Start()
    {

#if UNITY_EDITOR
        //Detecta cuando se esta usando el unity remote (SOLO CUANDO EJECUTAS EN UNITY)
        if (UnityEditor.EditorApplication.isRemoteConnected) usaJoystick = true;
        //if (cam.GetComponent<CameraMovement>() != null) cam.GetComponent<CameraMovement>().enabled = true;
        else usaJoystick = false;
#endif

    }

    private void Update()
    {
        ShootRaycastToGrabProducts();

        //!IMPORTANTE!: El gatillo con el control de VRBox en Android detecta el gatillo como LeftShift y usando Unity5 lo detecta como "JoystickButton4"
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (productoSeleccionado == null)
            {
                if (productoSeleccionable != null) AgarrarProducto(productoSeleccionable);
            }
            else
            {
                SoltarProducto();
            }

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
        producto.transform.tag = "ProductoAgarrado";
        productoSeleccionado = producto;
    }

    void ShootRaycastToGrabProducts()
    {
        RaycastHit[] hits = new RaycastHit[5];
        for (int i = 0; i < hits.Length; i++)
        {
            //0 = central, 1 = up, 2 = down, 3 = right, 4 = left 
            //El orden de los raycast marca la prioridad de los mismo por si golpean golpean a dos productos a la vez
            Vector3 origin = Vector3.zero;
            if (i == 0) origin = cam.transform.position;
            else if (i == 1) origin = cam.transform.position + (Vector3.up * 0.2f);
            else if (i == 2) origin = cam.transform.position + (Vector3.down * 0.2f);
            else if (i == 3) origin = cam.transform.position + (Vector3.right * 0.2f);
            else if (i == 4) origin = cam.transform.position + (Vector3.left * 0.2f);
            Physics.Raycast(origin, cam.transform.TransformDirection(Vector3.forward), out hits[i], distMaxPlayerProducto, layerToRaycast);
            Debug.DrawRay(origin, cam.transform.TransformDirection(Vector3.forward) * hits[i].distance, Color.yellow);

            if (hits[i].collider != null && hits[i].transform.CompareTag("Producto"))
            {
                productoSeleccionable = hits[i].collider.transform.gameObject;
                return;
            }
            else productoSeleccionable = null;
        }
    }

    void SoltarProducto()
    {
        if (productoSeleccionado.GetComponent<PosicionarProducto>())
        {
            PosicionarProducto producto = productoSeleccionado.GetComponent<PosicionarProducto>();
            if (producto.OnCollisionCarrito) producto.DejarEnCarrito();
            else producto.DejarEnGondola();
            productoSeleccionado = null;
        }
    }
    #endregion


    public string inputEntrante(string option)
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
        return "";
    }
}
