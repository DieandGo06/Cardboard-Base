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
    public Transform hijosContainer;

    [Header("Variables setteables")]
    [SerializeField] float distanciaObjetoAgarrado;
    [SerializeField] float distanciaAgarrarProducto;
    public float speed;


    [Header("Variables p/ debug")]
    [HideInInspector] public bool desactivarEfectos;
    [SerializeField] GameObject productoSeleccionable;
    public GameObject productoSeleccionado;
    public bool usaJoystick;
    public bool sePuedeMover;
    public bool camaraMejoradaEditor;

    //Variables privadas
    Rigidbody rb;
    LayerMask layerToRaycast;
    [HideInInspector] public CarritoManager carrito;
    float rotacionX;


    //Variables para resbalar carrito
    Vector2 lastInputs;
    float tiempoLimite;
    float tiempoTranscurrido;
    float tiempoMaximoResbalando;
    public bool sePuedeResbalar;
    bool seEstaResbalando;

    public GameObject textoLimiteLista;






    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        carrito = GetComponentInChildren<CarritoManager>();
        GameManager.instance.jugador = transform.gameObject;
        layerToRaycast = LayerMask.GetMask("Producto");
        sePuedeMover = true;
    }

    void Start()
    {
#if UNITY_EDITOR
        //Detecta cuando se esta usando el unity remote (SOLO CUANDO EJECUTAS EN UNITY)
        if (UnityEditor.EditorApplication.isRemoteConnected) usaJoystick = true;
        else usaJoystick = false;
#endif

        if (usaJoystick)
        {
            hijosContainer.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
        }

        //Tareas.Nueva(1, () => Debug.Log("hola"));
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
        MoverCamara();
        if (sePuedeMover)
        {
            Caminar();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
#if UNITY_EDITOR
        if (other.transform.tag == "Producto")
        {
            if (productoSeleccionado == null) AgarrarProducto(other.gameObject);
        }
#endif
    }




    void MoverCamara()
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
            float sensibilidad = 600;
            float mouseX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;
            rotacionX -= mouseY;

            if (camaraMejoradaEditor)
            {
                //No te permite agarrar objetos a distancia
                cam.transform.parent = transform;
                rotacionX = Mathf.Clamp(rotacionX, -90f, 45);
                cam.transform.localRotation = Quaternion.Euler(rotacionX, 0, 0f);
                transform.Rotate(Vector3.up * mouseX);
            }
            else
            {
                //No te permite mover la camara horizontalmente
                cam.transform.localRotation = Quaternion.Euler(rotacionX, 0, 0f);
            }
        }
    }

    void Caminar()
    {
        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");
        Vector2 inputs = new Vector2(hAxis, vAxis);
        float cooldown = 0.2f;
        float cooldownResbalando = 3f;

        if (sePuedeResbalar)
        {
            if (inputs != Vector2.zero && seEstaResbalando == false)
            {
                if (tiempoTranscurrido > tiempoLimite)
                {
                    lastInputs = inputs;
                    tiempoLimite = tiempoTranscurrido + cooldown;
                    tiempoMaximoResbalando = tiempoTranscurrido + cooldownResbalando;
                    //Debug.Log("ESTA TOMANDO LOS INPUTS");
                }
            }

            if (inputs == Vector2.zero)
            {
                if (seEstaResbalando == false)
                {
                    Vector3 moveForce = (cam.transform.right * inputs.x * speed) + (cam.transform.forward * inputs.y * speed);
                    rb.AddForce(moveForce, ForceMode.Impulse);
                    seEstaResbalando = true;
                    //Debug.Log("Empujando");

                    Tareas.Nueva(2, () =>
                    {
                        rb.velocity = Vector3.zero;
                        seEstaResbalando = false;
                        //Debug.Log("se detiene");
                    });
                }
                tiempoTranscurrido += Time.deltaTime;
            }
        }
        //Debug.Log("se esta resbalando " + seEstaResbalando);
        Vector3 move = (cam.transform.right * inputs.x * speed) + (cam.transform.forward * inputs.y * speed);
        rb.MovePosition(transform.position + move * Time.deltaTime);
    }


    void CarritoResbaloso()
    {
        float cooldown = 0.5f;
        Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (inputs != Vector2.zero)
        {
            if (tiempoTranscurrido > tiempoLimite)
            {
                lastInputs = inputs;
                tiempoLimite += tiempoTranscurrido + cooldown;
            }
            Debug.Log(lastInputs);
        }

        if (inputs != Vector2.zero)
        {
            if (tiempoTranscurrido % 0.5f == 0)
            {
                lastInputs = inputs;
            }
        }

    }



    #region Agarrar y soltar 
    void SetAsChildOfCamera(GameObject producto)
    {
        producto.transform.parent = cam.transform;
    }

    void AgarrarProducto(GameObject producto)
    {
        if (GameManager.instance.contadorFrutas <= 0)
        {
            if (producto.GetComponent<identificadorDeEfectos>().nombreDeProducto == "frutas")
            {
                textoLimiteLista.SetActive(true);
                Tareas.Nueva(2, () => textoLimiteLista.SetActive(false));
                return;
            }
        }
        if (GameManager.instance.contadorVerduras <= 0)
        {
            if (producto.GetComponent<identificadorDeEfectos>().nombreDeProducto == "verduras")
            {
                textoLimiteLista.SetActive(true);
                Tareas.Nueva(2, () => textoLimiteLista.SetActive(false));
                return;
            }
        }
        if (GameManager.instance.contadorGalletitas <= 0)
        {
            if (producto.GetComponent<identificadorDeEfectos>().nombreDeProducto == "galletitas")
            {
                textoLimiteLista.SetActive(true);
                Tareas.Nueva(2, () => textoLimiteLista.SetActive(false));
                return;
            }
        }
        if (GameManager.instance.contadorCereales <= 0)
        {
            if (producto.GetComponent<identificadorDeEfectos>().nombreDeProducto == "cereales")
            {
                textoLimiteLista.SetActive(true);
                Tareas.Nueva(2, () => textoLimiteLista.SetActive(false));
                return;
            }
        }
        if (GameManager.instance.contadorCarne <= 0)
        {
            if (producto.GetComponent<identificadorDeEfectos>().nombreDeProducto == "carne")
            {
                textoLimiteLista.SetActive(true);
                Tareas.Nueva(2, () => textoLimiteLista.SetActive(false));
                return;
            }
        }
        if (GameManager.instance.contadorGolosinas <= 0)
        {
            if (producto.GetComponent<identificadorDeEfectos>().nombreDeProducto == "golosinas")
            {
                textoLimiteLista.SetActive(true);
                Tareas.Nueva(2, () => textoLimiteLista.SetActive(false));
                return;
            }
        }

        SetAsChildOfCamera(producto);
        producto.transform.localPosition = Vector3.zero;
        Vector3 nuevaPosicion = Vector3.zero.CambiarZ(distanciaObjetoAgarrado);
        producto.transform.localPosition = nuevaPosicion;
        producto.transform.tag = "ProductoAgarrado";
        productoSeleccionado = producto;

        if (producto.GetComponent<AudioSource>() != null)
        {
            producto.GetComponent<AudioSource>().PlayOneShot(producto.GetComponent<ActivaSonido>().ElSonido);
        }
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
            else if (i == 1) origin = cam.transform.position + (Vector3.up * 0.1f);
            else if (i == 2) origin = cam.transform.position + (Vector3.down * 0.1f);
            else if (i == 3) origin = cam.transform.position + (Vector3.right * 0.1f);
            else if (i == 4) origin = cam.transform.position + (Vector3.left * 0.1f);
            Physics.Raycast(origin, cam.transform.TransformDirection(Vector3.forward), out hits[i], distanciaAgarrarProducto, layerToRaycast);
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
            if (producto.OnCollisionCarrito)
            {
                producto.DejarEnCarrito();
                if (producto.GetComponent<AudioSource>() != null || producto.GetComponent<ActivaSonido>() != null)
                {
                    producto.GetComponent<AudioSource>().PlayOneShot(producto.GetComponent<ActivaSonido>().SonidoMeter);
                }
                productoSeleccionado.GetComponent<identificadorDeEfectos>().ReducirContadoryActivarEfectos();
            }
            else
            {
                producto.DejarEnGondola();
                if (producto.GetComponent<AudioSource>() != null || producto.GetComponent<ActivaSonido>() != null)
                {
                    producto.GetComponent<AudioSource>().PlayOneShot(producto.GetComponent<ActivaSonido>().SonidoDejar);
                }
            }
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
