using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject camara;
    public GameObject productoSeleccionado;
    [SerializeField] float distMinPlayerProducto;
    [SerializeField] float distMaxPlayerProducto;
    [SerializeField] bool canGrab;

    [SerializeField] GameObject producto;




    private void Awake()
    {
        camara = GetComponentInChildren<Camera>().gameObject;
    }

    private void Start()
    {
        //AgarrarProducto(producto);
    }


    //FixedUpdate reconocia mal Input.GetMouseButtonDown(0), ni idea porque
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (productoSeleccionado == null)
            {
                if (productoSeleccionable() != null) AgarrarProducto(productoSeleccionable());
            }
            else SoltarProducto();
        }
        MostrarRaycast();

        if (productoSeleccionable() != null) producto = productoSeleccionable();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Producto")
        {
            productoSeleccionado = other.gameObject;
            AgarrarProducto(other.gameObject);
        }
    }
    


    void SetAsChildOfCamera(GameObject producto)
    {
        producto.transform.parent = camara.transform;
    }

    void AgarrarProducto(GameObject producto)
    {
        SetAsChildOfCamera(producto);
        producto.transform.localPosition = Vector3.zero;
        Vector3 nuevaPosicion = Vector3.zero.CambiarZ(distMinPlayerProducto);
        producto.transform.localPosition = nuevaPosicion;
        productoSeleccionado = producto;
    }

    GameObject productoSeleccionable()
    {
        RaycastHit hit;
        Physics.Raycast(camara.transform.position, camara.transform.TransformDirection(Vector3.forward), out hit, distMaxPlayerProducto);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Producto")) return hit.collider.transform.gameObject;
        }
        return null;
    }

    void MostrarRaycast()
    {
        RaycastHit hit;
        Physics.Raycast(camara.transform.position, camara.transform.TransformDirection(Vector3.forward), out hit, distMaxPlayerProducto);
        Debug.DrawRay(camara.transform.position, camara.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    }

    void SoltarProducto()
    {
        if (productoSeleccionado.GetComponent<PosicionarProducto>())
        {
            productoSeleccionado.GetComponent<PosicionarProducto>().SetNewPosition();
            productoSeleccionado = null;
        }
    }
}
