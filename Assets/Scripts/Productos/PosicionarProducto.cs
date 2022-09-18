using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionarProducto : MonoBehaviour
{
    public Transform gondolaSpace;
    public CarritoManager carrito;
    public bool OnCollisionCarrito;


    private void Start()
    {
        carrito = GameManager.instance.jugador.GetComponent<PlayerController>().carrito;
        CrearGondolaSpaceInicial();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GondolaSpace"))
        {
            gondolaSpace = other.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Carrito"))
        {
            OnCollisionCarrito = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Carrito"))
        {
            OnCollisionCarrito = false;
        }
    }



    //Se ejecuta en el script PlayerAction
    public void DejarEnGondola()
    {
        transform.tag = "Producto";
        transform.parent = gondolaSpace;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        float posY = transform.parent.localScale.y;
        transform.localPosition = new Vector3(0, posY, 0);
        //float posY = (transform.localScale.y / 2) - (transform.parent.localScale.y / 2);
    }

    public void DejarEnCarrito()
    {
        transform.tag = "Comprado";
        transform.parent = carrito.GetEmptySpace().transform;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        float posY = transform.parent.localScale.y;
        //transform.localPosition = new Vector3(0, posY, 0);
        transform.localPosition = new Vector3(0, 0, 0);

    }

    void CrearGondolaSpaceInicial()
    {
        GameObject espacioInicial = GameObject.CreatePrimitive(PrimitiveType.Cube);
        espacioInicial.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        espacioInicial.transform.position = transform.position;
        espacioInicial.transform.rotation = transform.rotation;
        espacioInicial.transform.name = "GondolaSpace";
        espacioInicial.transform.tag = "GondolaSpace";
        transform.parent = espacioInicial.transform;
        gondolaSpace = espacioInicial.transform;

        espacioInicial.GetComponent<BoxCollider>().isTrigger = true;

    }
}
