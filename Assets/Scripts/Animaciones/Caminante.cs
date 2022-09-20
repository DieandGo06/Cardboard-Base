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

    public Transform startMarker;
    public Transform endMarker;

    // Movement speed in units per second.
   // public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }
    void Start()
    {

        speed = 0.1f;

        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Update is called once per frame
    void Update()
    {
        //if (seMueve)
        //{
        //    Marcar();
        //    tiempoTranscurrido += Time.deltaTime;
        //}

        //if (tiempoTranscurrido > tiempoMax)
        //{
        //    Detener();
        //}

        if (seMueve)
        {
            GetComponent<TrailRenderer>().emitting = true;
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);

            GameManager.instance.jugador.GetComponent<PlayerController>().sePuedeMover = false;
        }
        else {
            GetComponent<TrailRenderer>().emitting = false;
            GameManager.instance.jugador.GetComponent<PlayerController>().sePuedeMover = true;

        }
       // Debug.Log(seMueve);
    }

    //void Marcar()
    //{
    //    transform.position = transform.position + Vector3.right * speed * Time.deltaTime;
    //}

    //void Detener()
    //{
    //    if(transform.position = endMarker.position)
    //    seMueve = false;

    //}

    public void ActivarCaminante()
    {
        seMueve = true;
      
    }

    //// Para activar caminante en el script de la lista = gameObject.SetActive(false);

}
