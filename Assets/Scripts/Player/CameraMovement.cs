using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Codigo sacado de: https://www.youtube.com/watch?v=_QajrabyTJc

    public Transform player;
    [Tooltip("Mayor sensibilidad, mayor velocidad")]
    public float sensibilidad = 100f;

    float rotacionX;

    private void Update()
    {
        if (!canMoveCamera())
        {
            //Guarda la posicion del mouse
            Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            //Suaviza la rotacion
            mouseInput = mouseInput * sensibilidad * Time.deltaTime;

            rotacionX -= mouseInput.y;
            //Limita la rotacion en "x" ente (-90, 45) => (rotacion arriba-abajo)
            rotacionX = Mathf.Clamp(rotacionX, -90f, 45);

            //Aplica la rotacion en la camara
            transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
            //Aplica la rotacion en el jugador
            player.Rotate(Vector3.up * mouseInput.x);
        }
    }

    
    bool canMoveCamera()
    {
        return Input.GetMouseButton(1);
    }
    
}
