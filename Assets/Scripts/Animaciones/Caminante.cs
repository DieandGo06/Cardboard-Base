using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caminante : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody rb;
    public bool seMueve;
    public float tiempoMax;
    float tiempoTranscurrido;
    GameManager Contador;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }
    void Start()
    {
        speed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (seMueve)
        {
            Marcar();
            tiempoTranscurrido += Time.deltaTime;
        }

        if (tiempoTranscurrido > tiempoMax)
        {
            Detener();
        }
    }

    void Marcar()
    {
        transform.position = transform.position + Vector3.right * speed * Time.deltaTime;
    }

    void Detener()
    {
        seMueve = false;
        GetComponent<TrailRenderer>().emitting = false;
    }

    public void ActivarCaminante()
    {
        seMueve = true;
    }

    // Para activar caminante en el script de la lista = gameObject.SetActive(false);

}
