using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicionarProducto : MonoBehaviour
{
    public Transform gondolaPosition;
    public Transform carritoPosition;



    private void Awake()
    {
        if (transform.parent != null)
        {
            gondolaPosition = transform.parent.transform;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GondolaSpace"))
        {
            gondolaPosition = other.transform;
        }
        if (other.CompareTag("Carrito"))
        {
            if (other.transform.GetComponent<CarritoManager>() != null)
            {
                carritoPosition = other.transform.GetComponent<CarritoManager>().GetEmptySpace().transform;
                SetNewPosition();//Mete el producto de forma automatica cuando toca el cllider de carrito
            }
        }
    }



    //Se ejecuta en el script PlayerAction
    public void SetNewPosition()
    {
        if (carritoPosition == null)
        {
            transform.parent = gondolaPosition;
            transform.rotation = Quaternion.Euler(Vector3.zero);
            //float posY = (transform.localScale.y / 2) - (transform.parent.localScale.y / 2);
        }
        else
        {
            transform.tag = "Comprado";
            transform.parent = carritoPosition;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        float posY = transform.parent.localScale.y;
        transform.localPosition = new Vector3(0, posY, 0);
    }
}
